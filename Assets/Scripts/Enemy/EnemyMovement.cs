using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform jugador;    
    public float velocidad = 3f; 


    private float alturaInicial;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) jugador = player.transform;
        alturaInicial = transform.position.y;
    }

    void Update()
    {
        if (jugador != null)
        {
            Vector3 posicionObjetivoLook = new Vector3(jugador.position.x, transform.position.y, jugador.position.z);
            transform.LookAt(posicionObjetivoLook);
            transform.position = Vector3.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, alturaInicial, transform.position.z);
        }
    }
}
