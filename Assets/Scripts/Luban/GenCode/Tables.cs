//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using SimpleJSON;

namespace cfg
{
   
public sealed partial class Tables
{
    public banner.TbBanner TbBanner {get; }

    public Tables(System.Func<string, JSONNode> loader)
    {
        var tables = new System.Collections.Generic.Dictionary<string, object>();
        TbBanner = new banner.TbBanner(loader("banner_tbbanner")); 
        tables.Add("banner.TbBanner", TbBanner);
        PostInit();

        TbBanner.Resolve(tables); 
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        TbBanner.TranslateText(translator); 
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}