using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource fxSource;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Inicializa las fuentes de audio
        fxSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
    }

    // Método para reproducir efectos de sonido
    public void PlayFX(string clipName)
    {
        string path = $"Sounds/{clipName}"; // Ruta dentro de la carpeta Resources
        AudioClip clip = Resources.Load<AudioClip>(path); // Carga el archivo .wav
        if (clip != null)
        {
            fxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"No se encontró el archivo de audio: {path}");
        }
    }

    // Método para reproducir música
    public void PlayMusic(string clipName, bool loop = true)
    {
        string path = $"Music/{clipName}"; // Ruta dentro de la carpeta Resources
        AudioClip clip = Resources.Load<AudioClip>(path); // Carga el archivo .wav
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"No se encontró el archivo de audio: {path}");
        }
    }

    // Método para detener la música
    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}