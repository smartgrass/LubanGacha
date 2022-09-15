using cfg;
using cfg.banner;
using NaughtyAttributes;
using SimpleJSON;
using UnityEditor;
using UnityEngine;
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

    [Button]
    private void Gacha()
    {
        var table = GachaHelper.GetTable();

        var banList = table.TbBanner;

        string banId = "normal_ches";

        var curBan = banList.GetOrDefault(banId);




    }



}


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
        TextAsset text = Resources.Load<TextAsset>(string.Format("{0}/{1}.json", dir, fileName));
        return JSON.Parse(text.text);
    }



}