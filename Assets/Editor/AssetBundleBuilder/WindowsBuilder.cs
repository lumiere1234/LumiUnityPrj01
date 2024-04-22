using CoreManager;
using UnityEditor;

public class WindowsBuilder : AssetBundleBuilder<WindowsBuilder>
{
    public override void DoBuildAssetBundle()
    {
        base.DoBuildAssetBundle();
    }
    public override void DoBuildAction()
    {
        string bundleMainDir = $"{PathDefine.BundleBaseDir}/{PathDefine.BundleSubWindow}";
        AssetBundleBuild[] buildMap = assetBundleBuilds.ToArray();
        BuildAssetBundleOptions options = BuildAssetBundleOptions.ChunkBasedCompression;
        BuildPipeline.BuildAssetBundles(bundleMainDir, buildMap, options, BuildTarget.StandaloneWindows64);
        AssetDatabase.Refresh();
    }
}
