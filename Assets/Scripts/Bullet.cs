using UnityEngine;
using Players;

public class Bullet : MonoBehaviour
{
    public float damage;
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent(typeof(Actors)) is Actors)
        {
            Actors actor = collision.gameObject.GetComponent(typeof(Actors)) as Actors;
            actor.Damage(damage);
            Destroy(gameObject);            
        }
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