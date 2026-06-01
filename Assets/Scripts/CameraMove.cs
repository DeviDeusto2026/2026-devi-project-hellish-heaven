using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 10f, -10f);

    public float softness = 5f;

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 wantedPosition = player.position + offset;

        Vector3 finalPosition = Vector3.Lerp(transform.position, wantedPosition, softness * Time.deltaTime);

        transform.position = finalPosition;
    }
}