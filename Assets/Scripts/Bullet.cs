using UnityEngine;
using Unity.Netcode;
using Players;

public class Bullet : NetworkBehaviour
{
    public float damage;
    public ulong ownerID;
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent(typeof(Actors)) is Actors)
        {
            Actors actor = collision.gameObject.GetComponent(typeof(Actors)) as Actors;
            if (actor.NetworkObject.OwnerClientId == ownerID)return;
            actor.Damage(damage);
            Destroy(gameObject);            
        }
    }
    public void SetOwner(ulong id)
    {
        ownerID = id;
    }
    public void SetDamage(float f)
    {
        damage = f;
    }
    public void SetColor(Color c)
    {
        GetComponent<SpriteRenderer>().color = c;
    }
}