using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform jugador;    
    public float velocidad = 3f; 
    public float distanciaMinima = 1.5f; 

    void Update()
    {
        if (jugador != null)
        {
            float distancia = Vector3.Distance(transform.position, jugador.position);

            if (distancia > distanciaMinima)
            {
                transform.LookAt(jugador);

                transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
            }
            else
            {
                Debug.Log("¡Ataque!");
            }
        }
    }
}
