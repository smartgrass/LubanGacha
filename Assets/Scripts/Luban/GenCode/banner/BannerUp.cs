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

public sealed partial class BannerUp :  Bright.Config.BeanBase 
{
    public BannerUp(JSONNode _json) 
    {
        { if(!_json["id"].IsString) { throw new SerializationException(); }  Id = _json["id"]; }
        { if(!_json["name"].IsString) { throw new SerializationException(); }  Name = _json["name"]; }
        { var __json0 = _json["quaUpItemMap"]; if(!__json0.IsArray) { throw new SerializationException(); } QuaUpItemMap = new System.Collections.Generic.Dictionary<banner.Quality, banner.UpItem>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { banner.Quality _k0;  { if(!__e0[0].IsNumber) { throw new SerializationException(); }  _k0 = (banner.Quality)__e0[0].AsInt; } banner.UpItem _v0;  { if(!__e0[1].IsObject) { throw new SerializationException(); }  _v0 = banner.UpItem.DeserializeUpItem(__e0[1]);  }  QuaUpItemMap.Add(_k0, _v0); }   }
        { var __json0 = _json["quaUpListMap"]; if(!__json0.IsArray) { throw new SerializationException(); } QuaUpListMap = new System.Collections.Generic.Dictionary<banner.Quality, banner.UpList>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { banner.Quality _k0;  { if(!__e0[0].IsNumber) { throw new SerializationException(); }  _k0 = (banner.Quality)__e0[0].AsInt; } banner.UpList _v0;  { if(!__e0[1].IsObject) { throw new SerializationException(); }  _v0 = banner.UpList.DeserializeUpList(__e0[1]);  }  QuaUpListMap.Add(_k0, _v0); }   }
        PostInit();
    }

    public BannerUp(string id, string name, System.Collections.Generic.Dictionary<banner.Quality, banner.UpItem> quaUpItemMap, System.Collections.Generic.Dictionary<banner.Quality, banner.UpList> quaUpListMap ) 
    {
        this.Id = id;
        this.Name = name;
        this.QuaUpItemMap = quaUpItemMap;
        this.QuaUpListMap = quaUpListMap;
        PostInit();
    }

    public static BannerUp DeserializeBannerUp(JSONNode _json)
    {
        return new banner.BannerUp(_json);
    }

    public string Id { get; private set; }
    public string Name { get; private set; }
    public System.Collections.Generic.Dictionary<banner.Quality, banner.UpItem> QuaUpItemMap { get; private set; }
    public System.Collections.Generic.Dictionary<banner.Quality, banner.UpList> QuaUpListMap { get; private set; }

    public const int __ID__ = -2044618871;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var _e in QuaUpItemMap.Values) { _e?.Resolve(_tables); }
        foreach(var _e in QuaUpListMap.Values) { _e?.Resolve(_tables); }
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var _e in QuaUpItemMap.Values) { _e?.TranslateText(translator); }
        foreach(var _e in QuaUpListMap.Values) { _e?.TranslateText(translator); }
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "QuaUpItemMap:" + Bright.Common.StringUtil.CollectionToString(QuaUpItemMap) + ","
        + "QuaUpListMap:" + Bright.Common.StringUtil.CollectionToString(QuaUpListMap) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
