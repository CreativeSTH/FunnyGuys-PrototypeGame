using UnityEngine;

public class PlayerFallChecker : MonoBehaviour
{
    private float fallThreshold = -10f;
    // Update is called once per frame
    void Update()
    {
       // Verifica la posición en Y del personaje
        if (transform.position.y < fallThreshold)
        {
            // Llama al método para reiniciar la escena
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ReloadCurrentScene();
            }
            else
            {
                Debug.LogError("GameManager.Instance es null.");
            }
        }  
    }
}
