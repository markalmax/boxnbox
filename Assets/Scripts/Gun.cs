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
    [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
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
        NetworkObject ownerNetworkObject = GetComponentInParent<NetworkObject>();
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        Color color = gameObject.GetComponentInParent<SetColor>().color;
        if (ownerNetworkObject != null && bulletNetworkObject != null && bulletComponent != null)
        {
            bulletNetworkObject.Spawn(true);
            bulletComponent.Initialize(ownerNetworkObject.OwnerClientId, damage,color);
        }
        bullet.GetComponent<Rigidbody2D>().linearVelocity = base.transform.up * bulletSpeed + vec;
    }
}
