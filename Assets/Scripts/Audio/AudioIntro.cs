using UnityEngine;

public class AudioIntro : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlayMusic("gameintro");
        AudioManager.Instance.PlayFX("start");
    }
}
