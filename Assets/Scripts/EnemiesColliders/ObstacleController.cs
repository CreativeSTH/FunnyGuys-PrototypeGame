using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float speed = 2f; 
    public float distance = 5f; 
    private Vector3 startPosition;
    private bool movingRight = true;
    public float pushForce = 10f; // Fuerza de empuje

    void Start()
    {
        // Guardar la posición inicial de la plataforma
        startPosition = transform.position;
    }

    void Update()
    {
        // Calcular el movimiento
        float movement = speed * Time.deltaTime;

        if (movingRight)
        {
            transform.Translate(Vector3.right * movement);
            if (transform.position.x >= startPosition.x + distance)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * movement);
            if (transform.position.x <= startPosition.x - distance)
            {
                movingRight = true;
            }
        }
    }

     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Aplica una fuerza al objeto empujable
                Vector3 forceDirection = collision.contacts[0].point - transform.position;
                rb.AddForce(forceDirection.normalized * pushForce, ForceMode.Impulse);
            }
        }
    }
}