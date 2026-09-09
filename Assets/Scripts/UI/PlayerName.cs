using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset = new Vector3(0, 1f, 0);
    public float smoothness = 1f;
    Vector3 velocity = Vector3.zero;   
    void LateUpdate()
    {
        Vector3 position = Player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position,position,ref velocity, smoothness);
    }
}
