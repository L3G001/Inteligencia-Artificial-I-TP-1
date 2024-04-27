using UnityEditor;
using UnityEngine;

public class TextureToMaterialConverter : AssetPostprocessor
{
    [MenuItem("Assets/Mods/Convertir a Material/URP_LIT",false,0)]
    private static void ConvertToMaterialURP_lit(MenuCommand menuCommand)
    {
        Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        string assetPath = default;
        foreach (var guid in Selection.assetGUIDs)
        {
            assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            if (texture != null)
            {
                if (texture.name.ToLower().Contains("albedo"))
                {
                    material.mainTexture = texture;
                    Debug.LogWarning("mainCargado");
                }
                else if (texture.name.ToLower().Contains("normal"))
                {

                    material.SetTexture("_BumpMap", texture);
                    Debug.LogWarning("normalCargado");
                }
                else if (texture.name.ToLower().Contains("emissive"))
                {
                    material.EnableKeyword("_EMISSION");
                    material.SetTexture("_EmissionMap", texture);
                    Color emissionColor = Color.white; // Puedes cambiar esto a cualquier color que desees
                    material.SetColor("_EmissionColor", emissionColor);
                    Debug.LogWarning("emisionCargado");
                }
                else if (texture.name.ToLower().Contains("ao"))
                {
                    material.SetTexture("_OcclusionMap", texture);
                    Debug.LogWarning("oclucionCargado");
                }
                else if (texture.name.ToLower().Contains("height"))
                {
                    material.SetTexture("_ParallaxMap", texture);
                    Debug.LogWarning("alturaCargado");
                }
                else if (texture.name.ToLower().Contains("metallic"))
                {
                    material.SetTexture("_MetallicGlossMap", texture);
                    Debug.LogWarning("metalicCargado");
                }

            }
        }
        string materialPath = assetPath.Replace(".png", ".mat").Replace(".jpg", ".mat").Replace(".jpeg", ".mat");
        AssetDatabase.CreateAsset(material, materialPath);
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Mods/Convertir a Material/Standar",false,0)]
    private static void ConvertToMaterialStandar(MenuCommand menuCommand)
    {
        Material material = new Material(Shader.Find("Standard"));
        string assetPath = default;
        foreach (var guid in Selection.assetGUIDs)
        {
            assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            if (texture != null)
            {
                if (texture.name.ToLower().Contains("albedo"))
                {
                    material.mainTexture = texture;
                    Debug.LogWarning("mainCargado");
                }
                else if (texture.name.ToLower().Contains("normal"))
                {

                    material.SetTexture("_BumpMap", texture);
                    Debug.LogWarning("normalCargado");
                }
                else if (texture.name.ToLower().Contains("emissive"))
                {
                    material.EnableKeyword("_EMISSION");
                    material.SetTexture("_EmissionMap", texture);
                    Color emissionColor = Color.white; // Puedes cambiar esto a cualquier color que desees
                    material.SetColor("_EmissionColor", emissionColor);
                    Debug.LogWarning("emisionCargado");
                }
                else if (texture.name.ToLower().Contains("ao"))
                {
                    material.SetTexture("_OcclusionMap", texture);
                    Debug.LogWarning("oclucionCargado");
                }
                else if (texture.name.ToLower().Contains("height"))
                {
                    material.SetTexture("_ParallaxMap", texture);
                    Debug.LogWarning("alturaCargado");
                }
                else if (texture.name.ToLower().Contains("metallic"))
                {
                    material.SetTexture("_MetallicGlossMap", texture);
                    Debug.LogWarning("metalicCargado");
                }

            }
        }
        string materialPath = assetPath.Replace(".png", ".mat").Replace(".jpg", ".mat").Replace(".jpeg", ".mat");
        AssetDatabase.CreateAsset(material, materialPath);
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Mods/Convertir a Material/Standar",true)]
    static bool ValidateConvert()
    {
        return ValidateSelection();
    }
    [MenuItem("Assets/Mods/Convertir a Material/URP_LIT", true)]
    static bool ValidateConvertUrp()
    {
        return ValidateSelection();
    }
    private static bool ValidateSelection()
    {
        // Verifica si hay texturas seleccionadas en Selection.assetGUIDs
        if (Selection.assetGUIDs != null && Selection.assetGUIDs.Length > 0)
        {
            foreach (var guid in Selection.assetGUIDs)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
                if (texture != null)
                {
                    // Hay al menos una textura seleccionada
                    return true;
                }
            }
        }

        // No hay texturas seleccionadas
        return false;
    }
}
