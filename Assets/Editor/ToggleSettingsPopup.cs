using UnityEngine;

public class ToggleSettingsPopup
{
    public static void Execute()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(true);
        foreach (var obj in allObjects)
        {
            if (obj.name == "SettingsPopup" && obj.transform.parent?.name == "Canvas")
            {
                obj.SetActive(false);
                Debug.Log("Settings popup set to inactive");
                return;
            }
        }
    }
}
