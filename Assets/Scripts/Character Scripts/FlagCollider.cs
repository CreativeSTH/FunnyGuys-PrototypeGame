using UnityEngine;

public class FlagCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.NextLvMenu();
            }
            else
            {
                Debug.LogError("GameManager.Instance es null.");
            }
        }
    }
}