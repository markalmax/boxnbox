using UnityEngine;
using Unity.Netcode;
namespace Players
{
    public class Actors : NetworkBehaviour
    {
        private bool isRight;
        private bool canJump;
        private bool CanShoot = true;
        private bool isMoving;
        private bool IsGrounded;
        public float speed = 2700f;
        public float maxSpeed = 14f;
        public float jumpForce = 850f;
        private float jumps;
        public float maxJumps;
        public NetworkVariable<float> health = new NetworkVariable<float>(100f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
        public float maxHealth = 100f;
        public GameObject gun;
        public Gun gunScript;
        private Rigidbody2D rb;
        private SpriteRenderer sr;
        private NetworkObject no;
        private Color color;
        public LayerMask ground;
        private Vector2 scale;
        protected void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
            no = GetComponent<NetworkObject>();
            health.Value = maxHealth;
            jumps = maxJumps;
            isRight = true;
            color = sr.color;
            scale = base.transform.localScale;
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
            if(base.transform.localScale.y < scale.y)
            {
                base.transform.localScale = new Vector2(base.transform.localScale.x, base.transform.localScale.y + 0.03f);
            }
            else base.transform.localScale = new Vector2(base.transform.localScale.x, scale.y);
            if(base.transform.localScale.x < scale.x)
            {
                base.transform.localScale = new Vector2(base.transform.localScale.x + 0.03f, base.transform.localScale.y);
            }
            else base.transform.localScale = new Vector2(scale.x, base.transform.localScale.y);
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
            float num = base.transform.rotation.eulerAngles.z % 180f;
			if (num < 45f || num > 135f)
			{
				base.transform.localScale = new Vector2(base.transform.localScale.x, base.transform.localScale.y / 4f);
			}
			else
			{
				base.transform.localScale = new Vector2(base.transform.localScale.x / 4f, base.transform.localScale.y);
			}
            rb.linearVelocityY=0;
			rb.AddForce(Vector2.up * jumpForce);
			jumps--;
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
        public void Damage(float damage)
        {
            health.Value -= damage;
            DamageFlash();
            if (health.Value <= 0f) Die();
            Debug.Log(no.OwnerClientId + " took " + damage + " damage, health is " + health.Value);
        }
        public void DamageFlash()
        {
            sr.color = Color.red;
            Invoke("ResetColor", 0.1f);
        }
        private void ResetColor()
        {
            sr.color = color;
        }
        public void Die()
        {
            
        }
        protected void Shoot()
        {
            if (!CanShoot) return;
            gunScript.Fire();
            rb.AddForce(-gun.transform.up * gunScript.recoil);
        }
    }
}
