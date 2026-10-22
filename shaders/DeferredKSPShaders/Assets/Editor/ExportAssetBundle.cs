using UnityEditor;
using System.Linq;
using System.IO;

namespace Deferred
{
	public class CreateAssetBundles
	{
		[MenuItem ("Assets/Build AssetBundles")]
		static void BuildAllAssetBundles ()
		{
			var outDir = "C:\\Users\\Memphis\\Documents\\Visual Studio 2022\\Deferred\\shaders\\DeferredKSPShaders\\AssetBundles";

			if (!Directory.Exists (outDir))
				Directory.CreateDirectory (outDir);

			var opts = BuildAssetBundleOptions.ForceRebuildAssetBundle;
            BuildTarget[] platforms = { BuildTarget.StandaloneWindows };
			BuildPipeline.BuildAssetBundles(outDir, opts, BuildTarget.StandaloneWindows);

            // Cleanup
            File.Delete(outDir + "/shaders");
            foreach (string file in Directory.GetFiles(outDir, "*.*").Where(item => (item.EndsWith(".meta") || item.EndsWith(".manifest"))))
			{
				File.Delete(file);
			}

            // Rename replacementshaders to replacementshaders.shab so it gets loaded by shabby
            string shaderFile = Path.Combine(outDir, "replacementshaders");
            string shaderFileRenamed = shaderFile + ".shab";

            if (File.Exists(shaderFile))
            {
                if (File.Exists(shaderFileRenamed))
                {
                    File.Delete(shaderFileRenamed);
                }

                File.Move(shaderFile, shaderFileRenamed);
            }
        }
    }

}