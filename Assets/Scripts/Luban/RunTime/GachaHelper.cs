using cfg;
using cfg.banner;
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
        string dir = "Config/Luban";
        string ResourcesPath = string.Format("{0}/{1}", dir, fileName);
        //string path = string.Format("{0}/{1}.json", dir, fileName);
        TextAsset text = Resources.Load<TextAsset>(ResourcesPath);
        return JSON.Parse(text.text);
    }

    public static MainBannerItem GetItem(string banId, GachaCondition condition = null)
    {
        var table = GachaHelper.GetTable();

        var banList = table.TbBanner;

        var curBan = banList.GetOrDefault(banId);





        MainBannerItem getItem = null;

        List<MainBannerItem> allItemList = curBan.BannerItemList;
        List<MainBannerItem> itemList = new List<MainBannerItem>();
        List<MainBannerItem> tmpList = new List<MainBannerItem>();

        itemList = allItemList;
        Debug.Log($"yns {curBan.ToString()} {allItemList.Count}");

        if (condition != null)
        {
            if ((condition.gachaCondType & GachaCondType.ItemType) != 0)
            {
                Debug.Log($"yns 限定 {GachaCondType.ItemType}");
                tmpList = itemList;
                itemList = itemList.FindAll(item => item.ItemType == condition.itemType);
                itemList = CheckCount(itemList, tmpList, GachaCondType.ItemType);
            }
            if ((condition.gachaCondType & GachaCondType.Quality) != 0)
            {
                Debug.Log($"yns 限定 {GachaCondType.Quality}");
                tmpList = itemList;
                itemList = itemList.FindAll(item => item.Quality == condition.quality);
                itemList = CheckCount(itemList, tmpList, GachaCondType.Quality);
            }
            if ((condition.gachaCondType & GachaCondType.TargetList) != 0)
            {
                Debug.Log($"yns 限定 {GachaCondType.TargetList}");
                tmpList = itemList;
                itemList = itemList.FindAll(item => condition.targetList.Contains(item.ItemId));
                itemList = CheckCount(itemList, tmpList, GachaCondType.TargetList);
            }

        }
        Debug.Log($"yns Count {itemList.Count}");
        getItem = itemList.GetPowerRandom();
        return getItem;
    }



    private static List<MainBannerItem> CheckCount(List<MainBannerItem> itemList, List<MainBannerItem> tmpList, GachaCondType condType)
    {
        if (itemList.Count == 0)
        {
            itemList = tmpList;
            Debug.LogError($"yns no {condType}");
        }
        return itemList;
    }

    public static T GetPowerRandom<T>(this List<T> powerModel) where T : BasePowerItem
    {
        int total = 0;
        foreach (var item in powerModel)
        {
            total += item.Power;
        }
        if (powerModel.Count == 1 || total == 0)
        {
            return powerModel[0];
        }

        int random = UnityEngine.Random.Range(0, total);
        int rangeMax = 0;

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

}


public class GachaCondition
{
    public GachaCondType gachaCondType;
    public Quality quality;
    public ItemType itemType;
    public List<string> targetList;

    public void SetType(GachaCondType gachaCondType) 
    {
        this.gachaCondType = gachaCondType;
    }

}

public enum GachaCondType : int
{
    Null = 0,
    Quality = 1<<0,
    ItemType = 1 << 1,
    TargetList = 1 << 2
}