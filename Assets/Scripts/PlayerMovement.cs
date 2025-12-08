using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 5f;
    public float jumpForce = 60f;
    public float jumpVelocity = 5f;
    public float jumpCount = 2f;
    public float jumpCurrent = 0f;
    public float jumpCD = 10f;
    private Rigidbody2D rb;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!IsOwner || !IsSpawned) return;
        
        float input = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * input * Time.deltaTime * speed);
        if(IsGrounded())
        {
            jumpCurrent = jumpCount;
        }
        if (Input.GetKeyDown(KeyCode.Space)&& jumpCurrent > 0)
        {
            //rb.AddForce(new Vector2 (0,jumpForce));
            rb.linearVelocityY = jumpVelocity;
            jumpCurrent--;
        }
    }
    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));
    }
}
