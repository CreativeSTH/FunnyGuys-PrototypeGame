using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuSceneManagement : MonoBehaviour
{
    public Slider sliderMusic;   // Asigna el Slider de música desde el Inspector
    public Slider sliderFx;      // Asigna el Slider de FX desde el Inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlayMusic("mainmenu");

        // Configura los valores iniciales de los sliders
        if (sliderMusic != null)
        {
            sliderMusic.onValueChanged.AddListener(SetMusicVolume);
            sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", 1f); // Carga el valor guardado
        }

        if (sliderFx != null)
        {
            sliderFx.onValueChanged.AddListener(SetFxVolume);
            sliderFx.value = PlayerPrefs.GetFloat("FxVolume", 1f); // Carga el valor guardado
        }
    }

    public void NewGame()
    {
        GameManager.Instance.LoadNextScene();
    }

    public void FxClick()
    {
        AudioManager.Instance.PlayFX("start");
    }

    public void SetMusicVolume(float volume)
    {
        if (AudioManager.Instance.audioMixer != null)
        {
            // Convierte el valor lineal del slider a logarítmico para el AudioMixer
            AudioManager.Instance.audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("MusicVolume", volume); // Guarda el valor
        }
    }

    public void SetFxVolume(float volume)
    {
        if (AudioManager.Instance.audioMixer != null)
        {
            // Convierte el valor lineal del slider a logarítmico para el AudioMixer
            AudioManager.Instance.audioMixer.SetFloat("Fx", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("FxVolume", volume); // Guarda el valor
        }
    }
}