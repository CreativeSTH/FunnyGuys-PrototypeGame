using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager instanciado.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private bool isPaused;

    // Recarga la escena actual
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Carga la siguiente escena
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Verifica si hay una siguiente escena
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No hay más escenas. ¡Has completado el juego!");
            ReloadCurrentScene(); // Recargar la primera escena
        }
    }

    public void NextLvMenu(){
         SceneManager.LoadScene("NextLv");
    }

    // Método para manejar la derrota
    public void GameOverLose()
    {
        ReloadCurrentScene();
    }

    // Método para pausar el juego
    public void PauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
}