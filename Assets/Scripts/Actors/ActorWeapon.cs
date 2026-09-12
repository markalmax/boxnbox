using UnityEngine;
using Unity.Netcode;
using Weapons;

namespace Actors
{
    public class ActorWeapon : NetworkBehaviour
    {
        public bool canShoot;
        public GameObject gun;
        public Gun gunScript;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void SetGunRotation(float angleDegrees)
        {
            if (gun == null) return;
            gun.transform.rotation = Quaternion.Euler(0f, 0f, angleDegrees);
        }

        public void Shoot()
        {
            if (!canShoot || gunScript == null || gun == null) return;
            gunScript.Fire();
            rb.AddForce(-gun.transform.up * gunScript.recoil);
        }
    }
}