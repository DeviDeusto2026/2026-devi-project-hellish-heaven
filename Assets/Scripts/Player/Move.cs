using UnityEngine;

public class Move : MonoBehaviour
{
    public int movSpeed = 15;
    public float fuerzaSalto = 5f;
    public float velocidadRotacion = 25f;
    private Quaternion rotacionObjetivo;

    private bool canJump;

    private DashController _dash;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _dash = GetComponent<DashController>();
    }

    void Update()
    {
        move();
        jump();
    }

    private void move()
    {
        if (_dash != null && _dash.IsDashing) return;
        float moveH = 0;
        float moveV = 0;

        // Detectamos inputs
        if (Input.GetKey(KeyCode.A)) moveH = -1;
        if (Input.GetKey(KeyCode.D)) moveH = 1;
        if (Input.GetKey(KeyCode.W)) moveV = 1;
        if (Input.GetKey(KeyCode.S)) moveV = -1;

        if (Input.GetKeyDown(KeyCode.A))
        {
            rotacionObjetivo = Quaternion.LookRotation(Vector3.left); // Mira a la izquierda (-1 en X)
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rotacionObjetivo = Quaternion.LookRotation(Vector3.right); // Mira a la derecha (1 en X)
        }

        // Aplicamos la rotación suavemente hacia ese objetivo que ya se fijó con el único click
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);

        // Creamos el vector de movimiento y aplicamos velocidad física
        Vector3 direccion = new Vector3(moveH, 0, moveV).normalized;
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
