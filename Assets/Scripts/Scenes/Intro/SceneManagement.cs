using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlayMusic("Menumusic");
    }

    // Método para manejar la entrada del usuario (Enter o clic)
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Enter o clic izquierdo
        {
            AudioManager.Instance.PlayFX("start");
            GameManager.Instance.PressStart();
        }
    }
    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }
}
