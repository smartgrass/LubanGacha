using cfg.banner;
using NaughtyAttributes;
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

    [Button]
    private void Gacha()
    {
        string banId = "normal_ches";

        GachaCondition condition = new GachaCondition();

        condition.itemType = ItemType.Weapon;
        condition.quality =  Quality.Purple;

        var getItem = GachaHelper.GetItem(banId, condition);

        getItem.ToString().LogStr();
    }


}
