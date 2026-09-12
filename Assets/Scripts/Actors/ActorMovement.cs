using UnityEngine;
using Unity.Netcode;
using Audio;

namespace Actors
{
    public class ActorMovement : NetworkBehaviour
    {
        private bool isMoving;
        private bool isGrounded;
        public float speed;
        public float maxSpeed; 
        public float jumpForce;
        private float jumps;
        public float maxJumps;
        private Rigidbody2D rb;
        public LayerMask ground;
        private Vector2 scale;

        protected void Start()
        {
            scale = transform.localScale;
            rb = GetComponent<Rigidbody2D>();
            jumps = maxJumps;
        }

        private void FixedUpdate()
        {
            Friction();
        }

        private void LateUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(transform.position, 0.7f, ground);
            if (jumps < maxJumps && isGrounded)
            {
                jumps = maxJumps;
            }
            NormalizeScale();
        }

        public void Move(int dir)
        {
            isMoving = true;
            float xVel = rb.linearVelocityX;
            if ((xVel < maxSpeed && dir > 0) || (xVel > -maxSpeed && dir < 0))
                rb.AddForce(speed * Time.deltaTime * Vector2.right * dir);
        }

        public void NotMoving()
        {
            isMoving = false;
        }

        public void Jump()
        {
            if (jumps < 1) return;
            float num = base.transform.rotation.eulerAngles.z % 180f;
            if (num < 45f || num > 135f)
            {
                base.transform.localScale = new Vector2(base.transform.localScale.x, base.transform.localScale.y / 4f);
            }
            else
            {
                base.transform.localScale = new Vector2(base.transform.localScale.x / 4f, base.transform.localScale.y);
            }
            rb.linearVelocityY = 0;
            rb.AddForce(Vector2.up * jumpForce);
            AudioManager.Play("Jump");
            jumps--;
        }

        private void Friction()
        {
            if (isMoving) return;
            if (rb.linearVelocityX > 0.2f)
                rb.AddForce(speed * Time.deltaTime * -Vector2.right);
            else if (rb.linearVelocityX < -0.2f)
                rb.AddForce(speed * Time.deltaTime * Vector2.right);
        }

        private void NormalizeScale()
        {
            if (transform.localScale.y < scale.y)
            {
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + 0.03f);
            }
            else transform.localScale = new Vector2(transform.localScale.x, scale.y);

            if (transform.localScale.x < scale.x)
            {
                transform.localScale = new Vector2(transform.localScale.x + 0.03f, transform.localScale.y);
            }
            else transform.localScale = new Vector2(scale.x, transform.localScale.y);
        }
    }
}