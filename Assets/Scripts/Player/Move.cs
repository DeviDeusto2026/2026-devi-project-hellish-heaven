using UnityEngine;

public class Move : MonoBehaviour
{
    public int movSpeed = 15;
    public float fuerzaSalto = 5f;
    private bool canJump;

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
        float moveH = 0;
        float moveV = 0;

        // Detectamos inputs
        if (Input.GetKey(KeyCode.A)) moveH = -1;
        if (Input.GetKey(KeyCode.D)) moveH = 1;
        if (Input.GetKey(KeyCode.W)) moveV = 1;
        if (Input.GetKey(KeyCode.S)) moveV = -1;

        // Creamos el vector de movimiento
        Vector3 direccion = new Vector3(moveH, 0, moveV).normalized;

        // APLICAMOS EL MOVIMIENTO F�SICO
        // Opci�n A: Usando Velocity (movimiento directo pero respeta muros)
        rb.linearVelocity = new Vector3(direccion.x * movSpeed, rb.linearVelocity.y, direccion.z * movSpeed);
    }


    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canJump = true;
        }
    }
}
