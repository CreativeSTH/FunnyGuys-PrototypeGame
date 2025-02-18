using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlayMusic("gameloop");
        AudioManager.Instance.PlayFX("start");
    }

    // Método para manejar la entrada del usuario (Enter o clic)
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) // Enter o clic izquierdo
        {
            GameManager.Instance.PressStart();
        }
    }
    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }
}
