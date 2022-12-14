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

public sealed partial class Banner :  Bright.Config.BeanBase 
{
    public Banner(JSONNode _json) 
    {
        { if(!_json["id"].IsString) { throw new SerializationException(); }  Id = _json["id"]; }
        { if(!_json["name"].IsString) { throw new SerializationException(); }  Name = _json["name"]; }
        { var _j = _json["upConfig"]; if (_j.Tag != JSONNodeType.None && _j.Tag != JSONNodeType.NullValue) { { if(!_j.IsString) { throw new SerializationException(); }  UpConfig = _j; } } else { UpConfig = null; } }
        { var __json0 = _json["upList"]; if(!__json0.IsArray) { throw new SerializationException(); } UpList = new System.Collections.Generic.List<string>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { string __v0;  { if(!__e0.IsString) { throw new SerializationException(); }  __v0 = __e0; }  UpList.Add(__v0); }   }
        { var __json0 = _json["qualityPowerList"]; if(!__json0.IsArray) { throw new SerializationException(); } QualityPowerList = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  QualityPowerList.Add(__v0); }   }
        { var __json0 = _json["bannerItemList"]; if(!__json0.IsArray) { throw new SerializationException(); } BannerItemList = new System.Collections.Generic.List<banner.MainBannerItem>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { banner.MainBannerItem __v0;  { if(!__e0.IsObject) { throw new SerializationException(); }  __v0 = banner.MainBannerItem.DeserializeMainBannerItem(__e0);  }  BannerItemList.Add(__v0); }   }
        PostInit();
    }

    public Banner(string id, string name, string upConfig, System.Collections.Generic.List<string> upList, System.Collections.Generic.List<int> qualityPowerList, System.Collections.Generic.List<banner.MainBannerItem> bannerItemList ) 
    {
        this.Id = id;
        this.Name = name;
        this.UpConfig = upConfig;
        this.UpList = upList;
        this.QualityPowerList = qualityPowerList;
        this.BannerItemList = bannerItemList;
        PostInit();
    }

    public static Banner DeserializeBanner(JSONNode _json)
    {
        return new banner.Banner(_json);
    }

    public string Id { get; private set; }
    public string Name { get; private set; }
    public string UpConfig { get; private set; }
    public System.Collections.Generic.List<string> UpList { get; private set; }
    public System.Collections.Generic.List<int> QualityPowerList { get; private set; }
    public System.Collections.Generic.List<banner.MainBannerItem> BannerItemList { get; private set; }

    public const int __ID__ = -484808626;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var _e in BannerItemList) { _e?.Resolve(_tables); }
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var _e in BannerItemList) { _e?.TranslateText(translator); }
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "UpConfig:" + UpConfig + ","
        + "UpList:" + Bright.Common.StringUtil.CollectionToString(UpList) + ","
        + "QualityPowerList:" + Bright.Common.StringUtil.CollectionToString(QualityPowerList) + ","
        + "BannerItemList:" + Bright.Common.StringUtil.CollectionToString(BannerItemList) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
