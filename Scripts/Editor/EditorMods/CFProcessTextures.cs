using UnityEngine;
using System.Collections;
using UnityEditor;

public class CFProcessTextures : AssetPostprocessor {

        //if (assetPath.Contains(".max.") && assetPath.Contains("Props") || assetPath.Contains("SetDress") || assetPath.Contains("Environment"))

    void OnPreprocessTexture () {

        // For LinearGradients
        if (assetPath.Contains("ControlGrad")) {

            /*
            AssetDatabase.Refresh();
            AssetDatabase.ImportAsset(assetPathStr);
            TextureImporter importer = AssetImporter.GetAtPath(assetPathStr) as TextureImporter;
            importer.textureType = TextureImporterType.Bump ;
            AssetDatabase.WriteImportSettingsIfDirty(assetPathStr);
            */

                            

            TextureImporter textureImporter  = (TextureImporter) assetImporter;
		    textureImporter.linearTexture = true;
            textureImporter.mipmapEnabled = false;
            
		    textureImporter.textureType = TextureImporterType.Advanced;
            textureImporter.textureFormat = TextureImporterFormat.AutomaticTruecolor;
            textureImporter.isReadable = true;
        }

        TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        importer.maxTextureSize = 4096;
        if (assetPath.Contains("Norm"))
        {
            importer.textureType = TextureImporterType.Bump;
        }
        AssetDatabase.WriteImportSettingsIfDirty(assetPath);

    }
}
    




