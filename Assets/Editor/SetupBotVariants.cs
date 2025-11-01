using UnityEngine;
using UnityEditor;
using System.IO;

public class SetupBotVariants
{
    [MenuItem("Tools/Setup Bot Variants")]
    public static void Execute()
    {
        // Setup Bot_Duck
        GameObject duckPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Bot_Duck.prefab");
        if (duckPrefab != null)
        {
            GameObject duckInstance = PrefabUtility.LoadPrefabContents("Assets/Prefabs/Bot_Duck.prefab");
            
            // Find and remove old CharacterModel (Pig)
            Transform oldModel = duckInstance.transform.Find("CharacterModel");
            if (oldModel != null)
            {
                GameObject.DestroyImmediate(oldModel.gameObject);
            }
            
            // Add Duck model
            GameObject duckModel = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Quirky Series Ultimate/Quirky Series Vol.1/Farm Vol.1/Prefabs/Duck.prefab");
            GameObject duckChild = PrefabUtility.InstantiatePrefab(duckModel, duckInstance.transform) as GameObject;
            duckChild.name = "CharacterModel";
            duckChild.transform.localPosition = Vector3.zero;
            duckChild.transform.localRotation = Quaternion.identity;
            duckChild.transform.localScale = Vector3.one * 1.2f;
            
            // Add AnimatedCharacter component
            AnimatedCharacter animChar = duckChild.GetComponent<AnimatedCharacter>();
            if (animChar == null)
            {
                animChar = duckChild.AddComponent<AnimatedCharacter>();
            }
            animChar.rotationSpeed = 720f;
            
            PrefabUtility.SaveAsPrefabAsset(duckInstance, "Assets/Prefabs/Bot_Duck.prefab");
            PrefabUtility.UnloadPrefabContents(duckInstance);
            Debug.Log("Bot_Duck setup complete!");
        }
        
        // Setup Bot_Sheep
        GameObject sheepPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Bot_Sheep.prefab");
        if (sheepPrefab != null)
        {
            GameObject sheepInstance = PrefabUtility.LoadPrefabContents("Assets/Prefabs/Bot_Sheep.prefab");
            
            // Find and remove old CharacterModel (Pig)
            Transform oldModel = sheepInstance.transform.Find("CharacterModel");
            if (oldModel != null)
            {
                GameObject.DestroyImmediate(oldModel.gameObject);
            }
            
            // Add Sheep model
            GameObject sheepModel = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Quirky Series Ultimate/Quirky Series Vol.1/Farm Vol.1/Prefabs/Sheep.prefab");
            GameObject sheepChild = PrefabUtility.InstantiatePrefab(sheepModel, sheepInstance.transform) as GameObject;
            sheepChild.name = "CharacterModel";
            sheepChild.transform.localPosition = Vector3.zero;
            sheepChild.transform.localRotation = Quaternion.identity;
            sheepChild.transform.localScale = Vector3.one * 1.2f;
            
            // Add AnimatedCharacter component
            AnimatedCharacter animChar = sheepChild.GetComponent<AnimatedCharacter>();
            if (animChar == null)
            {
                animChar = sheepChild.AddComponent<AnimatedCharacter>();
            }
            animChar.rotationSpeed = 720f;
            
            PrefabUtility.SaveAsPrefabAsset(sheepInstance, "Assets/Prefabs/Bot_Sheep.prefab");
            PrefabUtility.UnloadPrefabContents(sheepInstance);
            Debug.Log("Bot_Sheep setup complete!");
        }
        
        Debug.Log("All bot variants setup complete!");
    }
}
