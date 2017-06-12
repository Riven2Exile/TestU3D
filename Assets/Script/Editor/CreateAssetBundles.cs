using UnityEngine;
using UnityEditor;

public class CreateAssetBundles
{
    static string path = "Assets/myAssetBundle.unity3d";
    [MenuItem("Assets/Build AssetBundle")]
        static void ExportResource () {
            Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets);
        }
    
}