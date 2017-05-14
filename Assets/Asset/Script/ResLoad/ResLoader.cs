using UnityEngine;
using System.Collections;

public abstract class ResLoader
{
    public enum LoadType { 
        editor,
        ab, //ab��
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

    // ������Դ
    public abstract GameObject Load(string strPath);
}