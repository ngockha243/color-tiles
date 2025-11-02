using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Background Music")]
    public AudioClip bgHome;
    public AudioClip bgGame;

    [Header("Sound Effects")]
    public AudioClip buttonClickSFX;
    public AudioClip winSFX;
    public AudioClip loseSFX;
    public AudioClip claimTileSFX;

    private bool musicEnabled = true;
    private bool sfxEnabled = true;

    void Awake()
    {
        // Singleton pattern with DontDestroyOnLoad
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Create audio sources if not assigned
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.loop = true;
                musicSource.playOnAwake = false;
            }
            
            if (sfxSource == null)
            {
                sfxSource = gameObject.AddComponent<AudioSource>();
                sfxSource.loop = false;
                sfxSource.playOnAwake = false;
            }
            
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Background music will be started by scene-specific managers
    }

    void LoadSettings()
    {
        // Load settings from PlayerPrefs (default: ON = 1)
        musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        sfxEnabled = PlayerPrefs.GetInt("SFXEnabled", 1) == 1;
    }

    void SaveSettings()
    {
        PlayerPrefs.SetInt("MusicEnabled", musicEnabled ? 1 : 0);
        PlayerPrefs.SetInt("SFXEnabled", sfxEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (clip != null && musicEnabled)
        {
            // Only change if it's a different clip or not playing
            if (musicSource.clip != clip || !musicSource.isPlaying)
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }
    }

    public void PlayBgHome()
    {
        PlayBackgroundMusic(bgHome);
    }

    public void PlayBgGame()
    {
        PlayBackgroundMusic(bgGame);
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClickSFX);
    }

    public void PlayWin()
    {
        PlaySFX(winSFX);
    }

    public void PlayLose()
    {
        PlaySFX(loseSFX);
    }

    public void PlayClaimTile()
    {
        PlaySFX(claimTileSFX);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxEnabled)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void SetMusicEnabled(bool enabled)
    {
        musicEnabled = enabled;
        
        if (musicEnabled)
        {
            if (!musicSource.isPlaying && musicSource.clip != null)
            {
                musicSource.Play();
            }
        }
        else
        {
            StopBackgroundMusic();
        }
        
        SaveSettings();
    }

    public void SetSFXEnabled(bool enabled)
    {
        sfxEnabled = enabled;
        SaveSettings();
    }

    public bool IsMusicEnabled()
    {
        return musicEnabled;
    }

    public bool IsSFXEnabled()
    {
        return sfxEnabled;
    }

    public void ToggleMusic()
    {
        SetMusicEnabled(!musicEnabled);
    }

    public void ToggleSFX()
    {
        SetSFXEnabled(!sfxEnabled);
    }
}
