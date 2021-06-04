using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateBundle : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Bundles/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
        //BuildPipeline.BuildAssetBundle(null, toinclude.ToArray(), path, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android);
    }
#endif
}
