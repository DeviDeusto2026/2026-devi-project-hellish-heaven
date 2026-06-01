using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;    
    public float speed = 3f; 


    private float initialHeight;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) this.player = player.transform;
        initialHeight = transform.position.y;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 posicionObjetivoLook = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(posicionObjetivoLook);
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);
        }
    }
}
