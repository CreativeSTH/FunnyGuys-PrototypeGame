using UnityEngine;

public class RotarObjeto : MonoBehaviour
{
    // Velocidad de rotación (grados por segundo)
    public float velocidadRotacion = 200f;

    // Eje de rotación (puedes cambiarlo a Vector3.up, Vector3.forward, etc.)
    public Vector3 ejeRotacion = Vector3.up;

    void Update()
    {
        // Rotar el objeto en el eje especificado
        transform.Rotate(ejeRotacion * velocidadRotacion * Time.deltaTime);
    }
}