using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float fireRate;
    public float recoil;
    public float damage;
    public int amount;
    public float spread;
    public bool Armed;
    [SerializeField]
    private Color color;
    public void Fire()
    {
        if(Armed)
        {
            for (int i = 0; i < amount; i++)
            {   
                SpawnBullet();
            }
            Armed=false;
            Invoke("Arm", fireRate);
        }
    }
    private void Arm()
    {
        Armed=true;
    }
    private void SpawnBullet()
    {
        Debug.Log("Firing");
        Vector3 vec = new Vector3(Random.Range(0f-spread, spread), Random.Range(0f - spread, spread), 0);
        GameObject bullet = Instantiate(bulletPrefab, base.transform.position, base.transform.rotation);
        //bullet.transform.localScale *= 1f + damage / 15f;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = base.transform.up * bulletSpeed + vec;
        bullet.GetComponent<Bullet>().SetDamage(damage);
        bullet.GetComponent<Bullet>().SetColor(color);
    }
    public void SetBulletColor(Color c)
    {
        color = c;
    }
}
