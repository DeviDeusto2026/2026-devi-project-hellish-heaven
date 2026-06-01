using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 10f, -10f);
    public float softness = 5f;

    [Header("Colisión con paredes")]
    public float radioColision = 0.3f;
    public LayerMask capasColision;

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 wantedPosition = player.position + offset;

        Vector3 direccion = wantedPosition - player.position;
        if (Physics.SphereCast(player.position, radioColision, direccion.normalized,
            out RaycastHit hit, direccion.magnitude, capasColision))
        {
            wantedPosition = hit.point + hit.normal * radioColision;
        }

        Vector3 finalPosition = Vector3.Lerp(transform.position, wantedPosition, softness * Time.deltaTime);
        transform.position = finalPosition;
    }
}