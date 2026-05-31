using UnityEngine;

public class Move : MonoBehaviour
{
    public int movSpeed = 15;
    public float fuerzaSalto = 5f;
    public float velocidadRotacion = 15f;

    private bool canJump;
    private DashController _dash;
    private Rigidbody rb;
    private Camera _cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _dash = GetComponent<DashController>();
        _cam = Camera.main;
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

        if (Input.GetKey(KeyCode.A)) moveH = -1;
        if (Input.GetKey(KeyCode.D)) moveH = 1;
        if (Input.GetKey(KeyCode.W)) moveV = 1;
        if (Input.GetKey(KeyCode.S)) moveV = -1;

        Vector3 input = new Vector3(moveH, 0f, moveV).normalized;

        if (input.sqrMagnitude < 0.01f)
        {
            // Sin input: frenar en X y Z manteniendo Y (gravedad)
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            return;
        }

        // Calcular dirección relativa a la cámara
        Vector3 camForward = _cam.transform.forward;
        Vector3 camRight = _cam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 direccion = (camForward * moveV + camRight * moveH).normalized;

        // Mover
        rb.linearVelocity = new Vector3(
            direccion.x * movSpeed,
            rb.linearVelocity.y,
            direccion.z * movSpeed
        );

        // Rotar el personaje hacia donde se mueve, suavemente
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            rotacionObjetivo,
            velocidadRotacion * Time.deltaTime
        );
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
            canJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            canJump = true;
    }
}