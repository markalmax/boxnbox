using UnityEngine;
using Unity.Netcode;
namespace Players
{
    public class Actors : NetworkBehaviour
    {
        private bool isRight;
        private bool canJump;
        private bool isMoving;
        private bool IsGrounded;
        public float speed = 2700f;
        public float maxSpeed = 14f;
        public float jumpForce = 850f;
        private float jumps;
        public float maxJumps;
        private float jumpCount;
        private float health;
        public float maxHealth;
        private Rigidbody2D rb;
        private SpriteRenderer sr;
        public Transform footer;
        public LayerMask ground;
        protected void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
            health = maxHealth;
            jumps = maxJumps;
            isRight = true;
        }
        private void FixedUpdate()
        {
            Friction();
        }
        private void LateUpdate()
        {
            IsGrounded = Physics2D.OverlapCircle(transform.position, 0.7f, ground);
            if(jumps < maxJumps && IsGrounded)
            {
                jumps = maxJumps;
            }
        }
        protected void Move(int dir)
        {
            if ((isRight && dir < 0) || !isRight && dir > 0) {
				isRight = !isRight;
				sr.flipX = !sr.flipX;
			}
            isMoving = true;
            float xVel = rb.linearVelocityX;
            if ((xVel < maxSpeed && dir > 0) || (xVel > -maxSpeed && dir < 0))
			rb.AddForce(speed * Time.deltaTime * Vector2.right * dir);
        }
        protected void Jump()
        {
            if (jumps < 1) return;
            rb.linearVelocityY = jumpForce;
			rb.AddForce(Vector2.up * jumpForce);
			jumps--;
            Debug.Log(IsGrounded);
        }
        private void Friction()
        {
            if(isMoving) return;
            if (rb.linearVelocityX > 0.2f)
				rb.AddForce(speed * Time.deltaTime * -Vector2.right);
			else if (rb.linearVelocityX < -0.2f)
				rb.AddForce(speed * Time.deltaTime * Vector2.right);
        }
        protected void NotMoving()
        {
            isMoving = false;
        }
    }
}
