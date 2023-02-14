using cfg;
using cfg.banner;
using NaughtyAttributes;
using SimpleJSON;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class GachaHelper
{
    public static Tables GetTable()
    {
        Tables tables = new Tables(TableLoader);
        return tables;
    }

    private static JSONNode TableLoader(string fileName)
    {
        string dir = "Config/LuaConfig";
        string ResourcesPath = string.Format("{0}/{1}", dir, fileName);
        //Debug.Log($"yns TableLoader ");
        //string path = string.Format("{0}/{1}.json", dir, fileName);
        TextAsset text = Resources.Load<TextAsset>(ResourcesPath);
        return JSON.Parse(text.text);
    }

    public static GachaResult GetGacha(string banId, Dictionary<Quality, int> QualityTimeDic, Dictionary<Quality, List<string>> upListDic)
    {
        var table = GachaHelper.GetTable();
        Debug.Log($"yns  tbbanner");
        var banList = table.TbBanner;

        var curBan = banList.GetOrDefault(banId);

        var bannerUp = table.TbBannerUp.GetOrDefault(curBan.UpConfig);

        var rateList = curBan.QualityPowerList.ToRateList();


        //获取保底次数
        foreach (var item in bannerUp.QuaUpItemMap)
        {
            int curTime = QualityTimeDic[item.Key];

            //达到保底次数 触发保底机制
            if (curTime >= item.Value.StartTime)
            {
                float targetRate = GetUpTargetRate(item.Value, curTime, rateList[(int)item.Key]);
                InsertRate(rateList, (int)item.Key, targetRate);
            }
        }

        List<MainBannerItem> itemList = curBan.BannerItemList;

        GachaCondition condition = new GachaCondition();


        condition.RandomQualityCondition(rateList);

        GachaResult result = new GachaResult();

        //强制保底
        if (upListDic != null && upListDic.ContainsKey(condition.quality))
        {
            condition.TargetList(upListDic[condition.quality], true);
        }
        else
        {
            //目标 up 
            if (bannerUp.QuaUpListMap.ContainsKey(condition.quality))
            {
                //保底物品
                var upList = bannerUp.QuaUpListMap[condition.quality];
                //出货
                bool isGachaOn = IsOnRate(upList.UpTargetRate,true);
                Debug.Log($"yns 出货? {isGachaOn}");
                condition.TargetList(upList.List, isGachaOn);
                result.isGachaOn = isGachaOn;
            }
        }

        Debug.Log($"yns get Quality {condition.quality} {rateList.IELogListStr("rate", false)} ");

        FitByCondition(condition, ref itemList);

        result.item = itemList.GetPowerRandom();

        return result;
    }

    private static void FitByCondition(GachaCondition condition, ref List<MainBannerItem> itemList)
    {
        List<MainBannerItem> tmpList = itemList;

        if ((condition.gachaCondType & GachaCondType.ItemType) != 0)
        {
            Debug.Log($"yns 限定 {GachaCondType.ItemType} {condition.itemType}");
            tmpList = itemList;
            itemList = itemList.FindAll(item => item.ItemType == condition.itemType);
            itemList = CheckCount(itemList, tmpList, GachaCondType.ItemType);
        }
        if ((condition.gachaCondType & GachaCondType.Quality) != 0)
        {
            Debug.Log($"yns 限定 {GachaCondType.Quality} {condition.quality}");
            tmpList = itemList;
            itemList = itemList.FindAll(item => item.Quality == condition.quality);
            itemList = CheckCount(itemList, tmpList, GachaCondType.Quality);
        }
        if ((condition.gachaCondType & GachaCondType.TargetList) != 0)
        {
            Debug.Log($"yns 限定 ={condition.isTarget} {GachaCondType.TargetList}");
            tmpList = itemList;

            itemList = itemList.FindAll(item => condition.isTarget == condition.targetList.Contains(item.ItemId));

            itemList = CheckCount(itemList, tmpList, GachaCondType.TargetList);
        }
    }

    //
    private static float GetUpTargetRate(UpItem upItem, int curTime, float rate)
    {
        int len = (upItem.EndTime - upItem.StartTime) + 1;

        int addstep = curTime - upItem.StartTime + 1;
        if (addstep <= 0)
        {
            return rate;
        }
        float step = (upItem.TargetRate - rate) / len;

        if (curTime >= upItem.EndTime)
        {
            //return upItem.TargetRate;
            return 1;
        }
        return rate + addstep * step;
    }

    private static void InsertRate(List<float> rateList, int index, float targetRate)
    {
        if (rateList[index] == targetRate)
        {
            return;
        }

        //更高优先级 所占的概率
        float topper = 0;
        int length = rateList.Count;
        for (int i = length - 1; i > index; i--)
        {
            topper += rateList[i];
        }
        //限制最大概率
        float maxRate = 1 - topper;

        targetRate = Mathf.Min(maxRate, targetRate);
        float detal = targetRate - rateList[index];
        rateList[index] = targetRate;

        for (int i = 0; i < index; i++)
        {
            if (rateList[i] >= detal)
            {
                //足够则 直接减少
                //Debug.Log($"yns get 减少 {detal}");
                rateList[i] -= detal;
                break;
            }
            else
            {
                //Debug.Log($"yns get 清空 {detal}");
                detal -= rateList[i];
                rateList[i] = 0;
            }
        }
    }

    //是否在概率内 Rate <1
    public static bool IsOnRate(float Rate, bool isLog = false)
    {
        float random = UnityEngine.Random.Range(0, 1f);
        bool res = random < Rate;
        if (isLog)
        {
            Debug.Log($"IsOnRate {random} < {Rate} = {res}");
        }
        return res;
    }
    //权重专百分比数
    public static List<float> ToRateList(this List<int> powerList)
    {
        List<float> rateList = new List<float>();
        //归一化
        float count = 0;
        foreach (var item in powerList)
        {
            count += item;
        }

        foreach (var item in powerList)
        {
            rateList.Add(item / count);
        }
        return rateList;
    }

    //防止查找结果为空
    private static List<MainBannerItem> CheckCount(List<MainBannerItem> itemList, List<MainBannerItem> tmpList, GachaCondType condType)
    {
        if (itemList.Count == 0)
        {
            itemList = tmpList;
            Debug.LogError($"yns no {condType}");
        }
        return itemList;
    }

    private static GachaCondition RandomQualityCondition(this GachaCondition condition, List<float> QualityPowerList)
    {
        int Index = QualityPowerList.GetRateRandomIndex();
        Quality quality = (Quality)Index;
        condition.SetQuality(quality);
        return condition;
    }

    public static T GetPowerRandom<T>(this List<T> powerModel) where T : BasePowerItem
    {
        float total = 0;
        foreach (var item in powerModel)
        {
            total += item.Power;
        }
        if (powerModel.Count == 1 || total == 0)
        {
            return powerModel[0];
        }

        float random = UnityEngine.Random.Range(0, total);
        float rangeMax = 0;

        int length = powerModel.Count;
        for (int i = 0; i < length; i++)
        {
            rangeMax += powerModel[i].Power;
            //当随机数小于 rangeMax 说明在范围内
            if (rangeMax > random && powerModel[i].Power > 0)
            {
                return powerModel[i];
            }
        }
        Debug.LogError("yns ??? power = 0");
        return null;
    }

    public static int GetRateRandomIndex(this List<float> powerModel)
    {
        float total = 0;
        foreach (var item in powerModel)
        {
            total += item;
        }
        if (powerModel.Count == 1 || total == 0)
        {
            return 0;
        }

        float random = UnityEngine.Random.Range(0, total);
        float rangeMax = 0;

        int length = powerModel.Count;
        for (int i = 0; i < length; i++)
        {
            rangeMax += powerModel[i];
            //当随机数小于 rangeMax 说明在范围内
            if (rangeMax > random && powerModel[i] > 0)
            {
                return i;
            }
        }
        Debug.LogError("yns ??? power = 0");
        return 0;
    }

    public static DropdownList<string> GetSelectValues()
    {
        var tb = GetTable();
        DropdownList<string> res = new DropdownList<string>();
        res.Add("-", "");
        foreach (var item in tb.TbBanner.DataList)
        {
            res.Add(item.Id, item.Id);
        }
        return res;
    }
}


public class GachaCondition
{
    public GachaCondType gachaCondType;
    public Quality quality;
    public ItemType itemType;
    public List<string> targetList;
    public bool isTarget = true;

    private void SetType(GachaCondType gachaCondType)
    {
        this.gachaCondType |= gachaCondType;
    }

    public void SetQuality(Quality quality)
    {
        SetType(GachaCondType.Quality);
        this.quality = quality;
    }
    public void TargetList(List<string> targetList, bool isTarget)
    {
        if (targetList != null && targetList.Count > 0)
        {
            SetType(GachaCondType.TargetList);
            this.targetList = targetList;
            this.isTarget = isTarget;
        }
    }

}

public class GachaResult
{
    public MainBannerItem item;
    public bool isGachaOn;//出货
}

public enum GachaCondType : int
{
    Null = 0,
    Quality = 1 << 0,
    ItemType = 1 << 1,
    TargetList = 1 << 2,
    IgronList = 1 << 3
}