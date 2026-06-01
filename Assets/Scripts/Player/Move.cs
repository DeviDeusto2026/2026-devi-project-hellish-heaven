using UnityEngine;

public class Move : MonoBehaviour
{
    public int moveSpeed = 15;
    public float jumpForce = 5f;
    public float rotationSpeed = 15f;

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
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            return;
        }

        Vector3 camForward = _cam.transform.forward;
        Vector3 camRight = _cam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 direction = (camForward * moveV + camRight * moveH).normalized;

        rb.linearVelocity = new Vector3(
            direction.x * moveSpeed,
            rb.linearVelocity.y,
            direction.z * moveSpeed
        );

        Quaternion objectiveRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            objectiveRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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