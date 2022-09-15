﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace XiaoCao
{
    public static class XiaocaoEditorTool
    {
        public static void SelectSelf(this UnityEngine.Object self)
        {
            Selection.activeObject = self;
        }

        public static Object[] ToObjectArray(this IEnumerable<UnityEngine.Object> list)
        {
            return list.ToArray();
        }


        public static List<Object> FindAssetListByName(string nameStr, string TypeName, string path = "Assets")
        {
            List<Object> objList = new List<Object>();
            string[] guids = AssetDatabase.FindAssets($"{nameStr} t:{TypeName}", new string[] { path });
            List<string> paths = new List<string>();
            new List<string>(guids).ForEach(m => paths.Add(AssetDatabase.GUIDToAssetPath(m)));
            for (int i = 0; i < paths.Count; i++)
            {
                objList.Add(AssetDatabase.LoadAssetAtPath(paths[i], typeof(Object)));
            }
            return objList;
        }

        public static Object FindAssetByName(string nameStr, string TypeName, string path = "Assets")
        {
            Object obj = null;
            string[] guids = AssetDatabase.FindAssets($"{nameStr} t:{TypeName}", new string[] { path });
            List<string> paths = new List<string>();
            new List<string>(guids).ForEach(m => paths.Add(AssetDatabase.GUIDToAssetPath(m)));
            if (paths.Count > 0)
                obj = AssetDatabase.LoadAssetAtPath(paths[0], typeof(Object));
            return obj;
        }

        public static List<Object> FindAssetsByDir(string TypeName, string path = "Assets")
        {
            List<Object> objList = new List<Object>();
            string[] guids = AssetDatabase.FindAssets($"t:{TypeName}", new string[] { path });
            List<string> paths = new List<string>();
            new List<string>(guids).ForEach(m => paths.Add(AssetDatabase.GUIDToAssetPath(m)));
            for (int i = 0; i < paths.Count; i++)
            {
                objList.Add(AssetDatabase.LoadAssetAtPath(paths[i], typeof(Object)));
            }
            return objList;
        }

    }
}
