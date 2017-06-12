using UnityEngine;
using System.Collections;
using UnityEditor;


public class ResEditorLoader : ResLoader
{
    public override GameObject Load(string strPath)
    {
        return AssetDatabase.LoadAssetAtPath(strPath, typeof(UnityEngine.Object)) as GameObject;
    }
}