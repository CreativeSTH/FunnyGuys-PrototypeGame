using UnityEngine;

public class PendulumMovement : MonoBehaviour
{
    // Ángulo máximo de oscilación (en grados)
    public float anguloMaximo = 45f;

    // Velocidad de oscilación (grados por segundo)
    public float velocidadOscilacion = 50f;

    // Punto de pivote (el punto alrededor del cual gira el martillo)
    public Transform puntoPivote;

    void Update()
    {
        // Calcula el ángulo de oscilación usando una función senoidal
        float angulo = anguloMaximo * Mathf.Sin(Time.time * velocidadOscilacion * Mathf.Deg2Rad);

        // Aplica la rotación al objeto alrededor del punto de pivote
        transform.RotateAround(puntoPivote.position, Vector3.forward, angulo);
    }
}
