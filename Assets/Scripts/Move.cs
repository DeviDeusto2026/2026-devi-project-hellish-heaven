using UnityEngine;

public class Move : MonoBehaviour
{
    public int movSpeed = 15;
    public float fuerzaSalto = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        move();
        jump();
    }

    private void move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position += Vector3.left * Time.deltaTime * movSpeed;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.position += Vector3.forward * Time.deltaTime * movSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.position += Vector3.back * Time.deltaTime * movSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += Vector3.right * Time.deltaTime * movSpeed;
        }
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }
}
