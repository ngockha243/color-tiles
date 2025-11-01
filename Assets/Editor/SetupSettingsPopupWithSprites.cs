using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SetupSettingsPopupWithSprites
{
    public static void Execute()
    {
        // Find objects - SettingsPopup is at root level under Canvas
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(true);
        GameObject settingsPopup = null;
        GameObject musicButton = null;
        GameObject sfxButton = null;
        GameObject closeButton = null;

        foreach (var obj in allObjects)
        {
            if (obj.name == "SettingsPopup" && obj.transform.parent?.name == "Canvas")
            {
                settingsPopup = obj;
            }
            else if (obj.name == "MusicButton")
            {
                musicButton = obj;
            }
            else if (obj.name == "SFXButton")
            {
                sfxButton = obj;
            }
            else if (obj.name == "CloseButton" && obj.transform.parent?.name == "bg")
            {
                closeButton = obj;
            }
        }
        
        if (settingsPopup == null)
        {
            Debug.LogError("SettingsPopup not found!");
            return;
        }

        SettingsPopup popup = settingsPopup.GetComponent<SettingsPopup>();
        if (popup == null)
        {
            Debug.LogError("SettingsPopup component not found!");
            return;
        }

        // Load sprites
        Sprite musicOn = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GUI PRO Kit - Casual Game/ResourcesData/Sprite/Demo/Demo_Icon/Icon_PictoIcon_Music_on.png");
        Sprite musicOff = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GUI PRO Kit - Casual Game/ResourcesData/Sprite/Demo/Demo_Icon/Icon_PictoIcon_Music_off.png");
        Sprite sfxOn = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GUI PRO Kit - Casual Game/ResourcesData/Sprite/Demo/Demo_Icon/Icon_PictoIcon_Sound_on.png");
        Sprite sfxOff = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GUI PRO Kit - Casual Game/ResourcesData/Sprite/Demo/Demo_Icon/Icon_PictoIcon_Sound_off.png");

        // Assign references
        var popupType = typeof(SettingsPopup);
        popupType.GetField("musicButton").SetValue(popup, musicButton?.GetComponent<Button>());
        popupType.GetField("sfxButton").SetValue(popup, sfxButton?.GetComponent<Button>());
        popupType.GetField("closeButton").SetValue(popup, closeButton?.GetComponent<Button>());
        
        popupType.GetField("musicOnSprite").SetValue(popup, musicOn);
        popupType.GetField("musicOffSprite").SetValue(popup, musicOff);
        popupType.GetField("sfxOnSprite").SetValue(popup, sfxOn);
        popupType.GetField("sfxOffSprite").SetValue(popup, sfxOff);

        // Set initial sprites on buttons
        if (musicButton != null)
        {
            var img = musicButton.GetComponent<Image>();
            if (img != null && musicOn != null)
            {
                img.sprite = musicOn;
                // Make button transparent background
                var colors = img.GetComponent<Button>().colors;
                colors.normalColor = Color.white;
                img.GetComponent<Button>().colors = colors;
            }
        }

        if (sfxButton != null)
        {
            var img = sfxButton.GetComponent<Image>();
            if (img != null && sfxOn != null)
            {
                img.sprite = sfxOn;
                // Make button transparent background
                var colors = img.GetComponent<Button>().colors;
                colors.normalColor = Color.white;
                img.GetComponent<Button>().colors = colors;
            }
        }

        // Mark as dirty
        EditorUtility.SetDirty(popup);
        if (musicButton != null) EditorUtility.SetDirty(musicButton);
        if (sfxButton != null) EditorUtility.SetDirty(sfxButton);

        Debug.Log("Settings popup configured with sprites!");
        Debug.Log($"Music Button: {musicButton != null}, SFX Button: {sfxButton != null}, Close Button: {closeButton != null}");
        Debug.Log($"Music On: {musicOn != null}, Music Off: {musicOff != null}");
        Debug.Log($"SFX On: {sfxOn != null}, SFX Off: {sfxOff != null}");
    }
}
