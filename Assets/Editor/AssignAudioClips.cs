using UnityEngine;
using UnityEditor;

public class AssignAudioClips
{
    public static void Execute()
    {
        // Find SoundManager in the scene
        SoundManager soundManager = GameObject.FindObjectOfType<SoundManager>();
        
        if (soundManager == null)
        {
            Debug.LogError("SoundManager not found in scene!");
            return;
        }

        // Load audio clips
        AudioClip bgHome = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sfx/bg_home.mp3");
        AudioClip bgGame = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sfx/bg_game.mp3");
        AudioClip buttonClick = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sfx/sfx_button_click.mp3");
        AudioClip win = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sfx/sfx_win.mp3");
        AudioClip lose = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sfx/sfx_lose.mp3");
        AudioClip claimTile = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Sfx/sfx_claim_tile.mp3");

        // Assign to SoundManager using reflection
        var soundManagerType = typeof(SoundManager);
        
        soundManagerType.GetField("bgHome").SetValue(soundManager, bgHome);
        soundManagerType.GetField("bgGame").SetValue(soundManager, bgGame);
        soundManagerType.GetField("buttonClickSFX").SetValue(soundManager, buttonClick);
        soundManagerType.GetField("winSFX").SetValue(soundManager, win);
        soundManagerType.GetField("loseSFX").SetValue(soundManager, lose);
        soundManagerType.GetField("claimTileSFX").SetValue(soundManager, claimTile);

        // Mark as dirty to save changes
        UnityEditor.EditorUtility.SetDirty(soundManager);
        
        Debug.Log("Audio clips assigned successfully!");
        Debug.Log($"bg_home: {bgHome != null}");
        Debug.Log($"bg_game: {bgGame != null}");
        Debug.Log($"buttonClick: {buttonClick != null}");
        Debug.Log($"win: {win != null}");
        Debug.Log($"lose: {lose != null}");
        Debug.Log($"claimTile: {claimTile != null}");
    }
}
