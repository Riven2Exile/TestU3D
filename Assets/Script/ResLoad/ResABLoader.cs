using UnityEngine;
using System.Collections;


// ¹Ù·½µÄ AssetBundleLoader

public class ResABLoader : ResLoader
{
    public override GameObject Load(string strPath)
    {
        AssetBundle ab = AssetBundle.LoadFromFile(strPath);
        //ab.LoadAllAssets();
        return null; // AssetBundle.LoadFromFile(strPath);
    }
}