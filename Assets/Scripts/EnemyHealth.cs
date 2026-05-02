using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float vidaMaxima = 100f;
    private float vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    // Esta función será llamada por la espada
    public void RecibirDanyo(float cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log(gameObject.name + " recibió daño. Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("Enemigo eliminado");
        Destroy(gameObject);
    }
}
