using UnityEngine;
using Unity.Netcode;

namespace Actors
{
    public class ActorBase : NetworkBehaviour
    {
        private ActorMovement movement;
        private ActorHealth health;
        private ActorWeapon weapon;
        private ActorEffects effects;
        private NetworkObject actorNetworkObject;
        private Rigidbody2D body;
        private SpriteRenderer spriteRenderer;
        private void Awake()
        {
            movement = GetComponent<ActorMovement>();
            health = GetComponent<ActorHealth>();
            weapon = GetComponent<ActorWeapon>();
            effects = GetComponent<ActorEffects>();
            actorNetworkObject = GetComponent<NetworkObject>();
            body = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Move(int dir)
        {
            movement?.Move(dir);
        }

        public void NotMoving()
        {
            movement?.NotMoving();
        }

        public void Jump()
        {
            movement?.Jump();
        }

        public void Shoot()
        {
            weapon?.Shoot();
        }

        public void SetGunRotation(float degrees)
        {
            weapon?.SetGunRotation(degrees);
        }

        public void SetHealth(float healthValue)
        {
            health?.SetHealth(healthValue);
        }

        public void Damage(float damage)
        {
            health?.Damage(damage);
        }

        public void Revive()
        {
            health?.Revive();
        }

        public void DamageFlash()
        {
            effects?.DamageFlash();
        }

        public void FlipSprite(int dir)
        {
            effects?.FlipSprite(dir);
        }

        public NetworkObject NetworkObjectRef => actorNetworkObject;
        public Rigidbody2D Body => body;
        public SpriteRenderer SpriteRendererRef => spriteRenderer;
    }
}
