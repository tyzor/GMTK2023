using UnityEngine;
using UnityEditor;

public class MaterialRemapper : AssetPostprocessor
{

/*
    void OnPreprocessModel()
    {
        ModelImporter modelImporter = (ModelImporter)assetImporter;

        // Disable material import
        //modelImporter.materialImportMode = ModelImporterMaterialImportMode.None;
        modelImporter.materialImportMode = ModelImporterMaterialImportMode.ImportViaMaterialDescription;
    }
    */

    void OnPostprocessModel(GameObject model)
    {
        // Check if the imported asset is an FBX file
        if (assetPath.EndsWith(".fbx"))
        {
            // Get all the materials used by the model
            Renderer[] renderers = model.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                Material[] materials = renderer.sharedMaterials;
                for (int i = 0; i < materials.Length; i++)
                {
                    // Remap the material based on your requirements
                    if (materials[i].name == "texture")
                    {
                        // Load and assign the new material
                        string newMaterialPath = "Assets/Materials/MainMat.mat";
                        Material newMaterial = AssetDatabase.LoadAssetAtPath<Material>(newMaterialPath);
                        materials[i] = newMaterial;
                        Debug.Log(newMaterial.name);        
                    }
                }
                renderer.sharedMaterials = materials;
                
            }
        }
    }
}
