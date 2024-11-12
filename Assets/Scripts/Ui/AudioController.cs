using UnityEngine;

public class AudioController : MonoBehaviour, IVolumeControl, IMuteControl
{
    private static AudioController _instance;
    public static AudioController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioController>();
                if (_instance == null)
                {
                    GameObject audioControllerObject = new GameObject("AudioController");
                    _instance = audioControllerObject.AddComponent<AudioController>();
                    DontDestroyOnLoad(audioControllerObject);
                }
            }
            return _instance;
        }
    }

    [SerializeField] private AudioSource backgroundMusicSource;

    private bool _isMuted = false;
    private float _currentVolume = 1.0f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Cargar el volumen guardado
        _currentVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        ApplyVolume();
    }

    private void Start()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.Play();
        }
    }

    public void SetVolume(float volumePercentage)
    {
        _currentVolume = Mathf.Clamp(volumePercentage / 100f, 0f, 1f);
        ApplyVolume();
    }

    public void ToggleMute()
    {
        _isMuted = !_isMuted;
        ApplyVolume();
    }

    public float GetVolumePercentage()
    {
        return _currentVolume * 100f;
    }

    public bool IsMuted()
    {
        return _isMuted;
    }

    private void ApplyVolume()
    {
        float volumeScale = _isMuted ? 0f : _currentVolume;

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
            {
                source.volume = volumeScale;
            }
        }

        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = volumeScale;
        }
    }
}
