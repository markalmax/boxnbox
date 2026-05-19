using UnityEngine;
using Unity.Netcode;
using Players;

namespace Guns
{
public class Bullet : NetworkBehaviour
{
    public NetworkVariable<float> damage = new NetworkVariable<float>(0f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<ulong> ownerID = new NetworkVariable<ulong>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsServer) return;

        if (collision.gameObject.GetComponent(typeof(Actors)) is Actors actor)
        {
            if (actor.NetworkObject.OwnerClientId == ownerID.Value) return;
            actor.Damage(damage.Value);
            actor.DamageFlash();
        }
        GetComponent<NetworkObject>().Despawn(true);
    }
    public void Initialize(ulong id, float damageValue, Color c)
    {
        if (!IsServer) return;
        ownerID.Value = id;
        damage.Value = damageValue;
        SetColorClientRpc(c);
        transform.localScale *= 1f + damageValue / 15f;
    }
    [ClientRpc]
    private void SetColorClientRpc(Color c)
    {
        GetComponent<SpriteRenderer>().color = c;
    }
}
}