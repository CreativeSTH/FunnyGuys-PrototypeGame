using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawmObstacle : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    public bool hasSpawned;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && !hasSpawned)
        {
            Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
           
            Instantiate(obstaclePrefab, transform.position, rotation);
            hasSpawned = true; 
        }
    }
}