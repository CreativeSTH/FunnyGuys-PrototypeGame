using UnityEngine;
using TMPro;

public class FlickerText : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public float velocidadParpadeo = 3f;
    public Color colorInicial = Color.white; // Color inicial del texto
    public Color colorFinal = Color.red; // Color final del texto

    void Update()
    {
        // Interpola entre los dos colores usando Mathf.PingPong
        float t = Mathf.PingPong(Time.time * velocidadParpadeo, 1f);
        texto.color = Color.Lerp(colorInicial, colorFinal, t);
    }
}
