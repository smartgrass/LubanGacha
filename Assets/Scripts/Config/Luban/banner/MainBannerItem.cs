//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace cfg.banner
{

public sealed partial class MainBannerItem :  banner.BasePowerItem 
{
    public MainBannerItem(JSONNode _json)  : base(_json) 
    {
        { if(!_json["itemId"].IsString) { throw new SerializationException(); }  ItemId = _json["itemId"]; }
        { if(!_json["itemType"].IsNumber) { throw new SerializationException(); }  ItemType = (banner.ItemType)_json["itemType"].AsInt; }
        { if(!_json["quality"].IsNumber) { throw new SerializationException(); }  Quality = (banner.Quality)_json["quality"].AsInt; }
        PostInit();
    }

    public MainBannerItem(float power, string itemId, banner.ItemType itemType, banner.Quality quality )  : base(power) 
    {
        this.ItemId = itemId;
        this.ItemType = itemType;
        this.Quality = quality;
        PostInit();
    }

    public static MainBannerItem DeserializeMainBannerItem(JSONNode _json)
    {
        return new banner.MainBannerItem(_json);
    }

    public string ItemId { get; private set; }
    public banner.ItemType ItemType { get; private set; }
    public banner.Quality Quality { get; private set; }

    public const int __ID__ = -2097713638;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "Power:" + Power + ","
        + "ItemId:" + ItemId + ","
        + "ItemType:" + ItemType + ","
        + "Quality:" + Quality + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}