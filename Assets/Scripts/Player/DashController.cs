using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DashController : MonoBehaviour
{
    [Header("Dash config")]

    [SerializeField] private float dashForce = 35f;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float dashFriction = 8f;

    private Rigidbody _rb;
    private StateManager _state;

    private float _cooldownTimer = 0f;
    private float _dashTimer = 0f;
    private bool _isDashing = false;
    private Vector3 _dashDir;

    public bool IsDashing => _isDashing;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _state = GetComponent<StateManager>();
    }

    void Update()
    {
        if (_cooldownTimer > 0f)
            _cooldownTimer -= Time.deltaTime;

        if (_isDashing)
        {
            _dashTimer -= Time.deltaTime;

            if (_dashTimer <= 0f)
            {
                StopDash();
            }
            else
            {
                // La velocidad decae progresivamente hacia el final del dash
                float progress = 1f - (_dashTimer / dashDuration); 
                float currentSpeed = Mathf.Lerp(dashForce, 0f, progress * progress);

                _rb.linearVelocity = new Vector3(
                    _dashDir.x * currentSpeed,
                    _rb.linearVelocity.y,
                    _dashDir.z * currentSpeed
                );
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
            TryDash();
    }

    private void TryDash()
    {
        if (_cooldownTimer > 0f) return;
        if (_isDashing) return;

        StartDash();
    }

    private void StartDash()
    {
        float moveH = 0f;
        float moveV = 0f;
        if (Input.GetKey(KeyCode.A)) moveH = -1f;
        if (Input.GetKey(KeyCode.D)) moveH = 1f;
        if (Input.GetKey(KeyCode.W)) moveV = 1f;
        if (Input.GetKey(KeyCode.S)) moveV = -1f;

        Vector3 inputDir = new Vector3(moveH, 0f, moveV).normalized;
        _dashDir = inputDir.sqrMagnitude > 0.01f ? inputDir : transform.forward;

        _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);

        _rb.AddForce(_dashDir * dashForce, ForceMode.VelocityChange);

        _isDashing = true;
        _dashTimer = dashDuration;
        _cooldownTimer = dashCooldown;

    }

    private void StopDash()
    {
        _isDashing = false;

        _rb.linearVelocity = new Vector3(
            _rb.linearVelocity.x / dashFriction,
            _rb.linearVelocity.y,
            _rb.linearVelocity.z / dashFriction
        );
    }
}