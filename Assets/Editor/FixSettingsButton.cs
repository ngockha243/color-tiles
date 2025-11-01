using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.Events;
using UnityEngine.Events;

public class FixSettingsButton
{
    public static void Execute()
    {
        // Find objects
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(true);
        GameObject settingsButton = null;
        GameObject settingsPopup = null;

        foreach (var obj in allObjects)
        {
            if (obj.name == "SettingsButton" && obj.transform.parent?.name == "Canvas")
            {
                settingsButton = obj;
            }
            else if (obj.name == "SettingsPopup" && obj.transform.parent?.name == "Canvas")
            {
                settingsPopup = obj;
            }
        }

        if (settingsButton == null)
        {
            Debug.LogError("SettingsButton not found!");
            return;
        }

        if (settingsPopup == null)
        {
            Debug.LogError("SettingsPopup not found!");
            return;
        }

        Button button = settingsButton.GetComponent<Button>();
        SettingsPopup popup = settingsPopup.GetComponent<SettingsPopup>();

        if (button == null)
        {
            Debug.LogError("Button component not found on SettingsButton!");
            return;
        }

        if (popup == null)
        {
            Debug.LogError("SettingsPopup component not found!");
            return;
        }

        // Clear existing listeners
        button.onClick.RemoveAllListeners();

        // Add persistent listener using UnityEventTools
        UnityAction action = new UnityAction(popup.OpenPopup);
        UnityEventTools.AddPersistentListener(button.onClick, action);

        // Mark as dirty
        EditorUtility.SetDirty(button);
        EditorUtility.SetDirty(settingsButton);

        Debug.Log("Settings button successfully wired to open popup!");
    }
}
