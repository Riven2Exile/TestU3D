using UnityEngine;
using UnityEditor;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundle")]
        static void ExportResource () {
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            ///// 先弄成一个res 对应一个ab包,  以后再做调整
            for (int i = 0; i < selection.Length; ++i)
            {
                Debug.Log( AssetDatabase.GetAssetPath(selection[i]) );
            }

            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, AppConst._ab_pack_name, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.Android);
        }
    
}