using UnityEngine;
using System.Collections;

public abstract class ResLoader
{
    public enum LoadType { 
        editor,
        ab, //ab包
    }

    static public ResLoader CreateLoader(LoadType t)
    {
        if (t == LoadType.ab)
        {
            return new ResABLoader();
        }
        else
        {
            return new ResEditorLoader();
        }
    }

    // 同步加载资源
    public abstract GameObject Load(string strPath);
}