using UnityEngine;
using Unity.Netcode;

namespace Actors
{
    public class ActorHealth : NetworkBehaviour
    {
        public NetworkVariable<float> health = new NetworkVariable<float>(100f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
        public float maxHealth = 100f;
        private bool isDead;
        private NetworkObject no;
        private ActorEffects effects;

        private void Awake()
        {
            no = GetComponent<NetworkObject>();
            effects = GetComponent<ActorEffects>();
        }

        public void SetHealth(float healthValue)
        {
            if (IsServer)
            {
                SetHealthOnServer(healthValue);
            }
            else
            {
                SetHealthServerRpc(healthValue);
            }
        }

        [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
        private void SetHealthServerRpc(float healthValue)
        {
            SetHealthOnServer(healthValue);
        }

        private void SetHealthOnServer(float healthValue)
        {
            health.Value = healthValue;
        }

        public void Damage(float damage)
        {
            if (IsServer)
            {
                DamageOnServer(damage);
            }
            else
            {
                DamageServerRpc(damage);
            }
        }

        private void DamageOnServer(float damage)
        {
            health.Value -= damage;
            effects?.DamageFlash();
            if (health.Value <= 0f)
            {
                Die();
            }

            Debug.Log(no.OwnerClientId + " took " + damage + " damage, health is " + health.Value);
        }

        [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
        private void DamageServerRpc(float damage)
        {
            DamageOnServer(damage);
        }

        public void Die()
        {
            if (no != null)
            {
                no.Despawn(true);
            }
        }

        public void Revive()
        {
            if (IsServer)
            {
                ReviveOnServer();
            }
            else
            {
                ReviveServerRpc();
            }
        }

        [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
        private void ReviveServerRpc()
        {
            ReviveOnServer();
        }

        private void ReviveOnServer()
        {
            health.Value = maxHealth;
            transform.position = Vector2.zero;
            if (no != null)
            {
                no.Spawn();
            }
        }
    }
}