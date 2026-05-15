using Unity.Netcode;
using UnityEngine;

public class Gun : NetworkBehaviour
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
        if (!Armed) return;

        if (IsServer)
        {
            FireOnServer();
        }
        else
        {
            FireServerRpc();
        }

        Armed = false;
        Invoke("Arm", fireRate);
    }
    [ServerRpc(RequireOwnership = false)]
    private void FireServerRpc()
    {
        FireOnServer();
    }
    private void FireOnServer()
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnBullet();
        }
    }
    private void Arm()
    {
        Armed = true;
    }
    private void SpawnBullet()
    {
        Debug.Log("Firing");
        Vector3 vec = new Vector3(Random.Range(0f-spread, spread), Random.Range(0f - spread, spread), 0);
        GameObject bullet = Instantiate(bulletPrefab, base.transform.position, base.transform.rotation);
        NetworkObject bulletNetworkObject = bullet.GetComponent<NetworkObject>();
        bulletNetworkObject.Spawn(true);

        NetworkObject ownerNetworkObject = GetComponentInParent<NetworkObject>();
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        if (ownerNetworkObject != null)
        {
            bulletComponent.Initialize(ownerNetworkObject.OwnerClientId, damage);
        }
        bullet.transform.localScale *= 1f + damage / 15f;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = base.transform.up * bulletSpeed + vec;
        bulletComponent.SetColor(color);
    }
    public void SetBulletColor(Color c)
    {
        color = c;
    }
}
