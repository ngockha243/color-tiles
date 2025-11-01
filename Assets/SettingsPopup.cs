using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [Header("UI References")]
    public Button musicButton;
    public Button sfxButton;
    public Button closeButton;

    [Header("Music Sprites")]
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    [Header("SFX Sprites")]
    public Sprite sfxOnSprite;
    public Sprite sfxOffSprite;

    private Image musicButtonImage;
    private Image sfxButtonImage;

    void Start()
    {
        // Get button images
        if (musicButton != null)
        {
            musicButtonImage = musicButton.GetComponent<Image>();
            musicButton.onClick.AddListener(OnMusicButtonClicked);
        }

        if (sfxButton != null)
        {
            sfxButtonImage = sfxButton.GetComponent<Image>();
            sfxButton.onClick.AddListener(OnSFXButtonClicked);
        }

        // Setup close button
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(ClosePopup);
        }

        // Update button visuals
        UpdateButtonVisuals();
    }

    void OnMusicButtonClicked()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClick();
            SoundManager.Instance.ToggleMusic();
            UpdateButtonVisuals();
        }
    }

    void OnSFXButtonClicked()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClick();
            SoundManager.Instance.ToggleSFX();
            UpdateButtonVisuals();
        }
    }

    void UpdateButtonVisuals()
    {
        if (SoundManager.Instance == null) return;

        // Update music button sprite
        if (musicButtonImage != null)
        {
            musicButtonImage.sprite = SoundManager.Instance.IsMusicEnabled() ? musicOnSprite : musicOffSprite;
        }

        // Update SFX button sprite
        if (sfxButtonImage != null)
        {
            sfxButtonImage.sprite = SoundManager.Instance.IsSFXEnabled() ? sfxOnSprite : sfxOffSprite;
        }
    }

    public void ClosePopup()
    {
        // Play button click sound
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClick();
        }
        
        gameObject.SetActive(false);
    }

    public void OpenPopup()
    {
        // Play button click sound
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayButtonClick();
        }
        
        gameObject.SetActive(true);
        
        // Refresh button visuals
        UpdateButtonVisuals();
    }
}
