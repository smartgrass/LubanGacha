using cfg.banner;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.UI;
using XiaoCao;
using ButtonAttribute = XiaoCao.ButtonAttribute;

public class XiaoCaoGacha : XiaoCaoWindow
{
    [MenuItem("Tools/XiaoCao/XiaoCaoGacha")]
    static void Open()
    {
        OpenWindow<XiaoCaoGacha>();
    }

    public int GoldTime = 10;
    public int PrupleTime = 10;

    public string[] stringList;

    [Button]
    private void Gacha()
    {
        string banId = "normal_ches";

        Dictionary<Quality, int> QualityTimeDic = new Dictionary<Quality, int>();
        QualityTimeDic.Add(Quality.Glod, GoldTime);
        QualityTimeDic.Add(Quality.Purple, PrupleTime);

        Dictionary<Quality, List<string>> upListDic = new Dictionary<Quality, List<string>>();

        upListDic.Add(Quality.Glod,new List<string>( stringList));

        if(stringList.Length == 0)
        {
            upListDic = null;
        }

        var gachaResult = GachaHelper.GetGacha(banId, QualityTimeDic, upListDic);


        gachaResult.item.ToString().LogStr("get");
    }
}
