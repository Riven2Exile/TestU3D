using UnityEngine;
using System.Collections;


// 官方的 AssetBundleLoader

public class ResABLoader : ResLoader
{
    private AssetBundle _ab = null;
    public override GameObject Load(string strname)
    {
        if(_ab == null)
        {
            _ab = AssetBundle.LoadFromFile(AppConst._ab_pack_name);

            // 打印一下ab包所有东西?
            string[] s = _ab.GetAllAssetNames();
            Debug.Log("打印ab");
//             for(int i = 0 ; i < s.Length; ++i)
//             {
//                 Debug.Log(s[i]);
//             }
        }

        if (_ab != null)
        {
            Debug.Log("load ab ok");
            return _ab.LoadAsset(strname) as GameObject;
        }
        else
        {
            Debug.Log("load ab null");
            return null;
        }
        
        //ab.LoadAllAssets();
    }
}