using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DashController : MonoBehaviour
{
    [Header("Dash config")]
    [SerializeField] private float dashForce = 35f;   // era dashSpeed, ahora más alto
    [SerializeField] private float dashDuration = 0.25f; // un poco más largo
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float dashFriction = 8f;    // qué tan rápido frena al acabar

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
                float progress = 1f - (_dashTimer / dashDuration); // 0 al inicio, 1 al final
                float currentSpeed = Mathf.Lerp(dashForce, 0f, progress * progress);

                _rb.linearVelocity = new Vector3(
                    _dashDir.x * currentSpeed,
                    _rb.linearVelocity.y,
                    _dashDir.z * currentSpeed
                );
            }
        }

        // Input: mismo sistema legacy que usa tu Move.cs
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

        // Cancelar velocidad previa para que el dash se sienta limpio
        _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);

        // Aplicar impulso inicial fuerte
        _rb.AddForce(_dashDir * dashForce, ForceMode.VelocityChange);

        _isDashing = true;
        _dashTimer = dashDuration;
        _cooldownTimer = dashCooldown;

    }

    private void StopDash()
    {
        _isDashing = false;

        // Freno suave, no brusco
        _rb.linearVelocity = new Vector3(
            _rb.linearVelocity.x / dashFriction,
            _rb.linearVelocity.y,
            _rb.linearVelocity.z / dashFriction
        );
    }
}