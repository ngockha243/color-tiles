using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class WireSettingsButton
{
    public static void Execute()
    {
        GameObject settingsButton = GameObject.Find("Canvas/SettingsButton");
        GameObject settingsPopup = GameObject.Find("Canvas/SettingsPopup");
        
        if (settingsButton != null && settingsPopup != null)
        {
            var button = settingsButton.GetComponent<Button>();
            var popup = settingsPopup.GetComponent<SettingsPopup>();
            
            if (button != null && popup != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => {
                    // Play button click sound
                    if (SoundManager.Instance != null)
                    {
                        SoundManager.Instance.PlayButtonClick();
                    }
                    popup.OpenPopup();
                });
                
                Debug.Log("Settings button wired with sound!");
            }
        }
    }
}
