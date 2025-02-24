using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector playableDirector; // Asigna el Playable Director desde el Inspector

    private void Start()
    {
        if (playableDirector == null)
        {
            playableDirector = GetComponent<PlayableDirector>();
        }

        // Suscribe el método al evento "stopped" del Playable Director
        playableDirector.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        // Verifica si el Timeline que terminó es el que estamos controlando
        if (director == playableDirector)
        {
            // Carga la siguiente escena
            GameManager.Instance.LoadNextScene();
        }
    }

    private void OnDestroy()
    {
        // Desuscribe el método para evitar problemas de memoria
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnTimelineFinished;
        }
    }
}