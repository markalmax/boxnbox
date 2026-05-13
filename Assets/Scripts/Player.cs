using UnityEngine;
using Unity.Netcode;
namespace Players
{
    public class Player : Actors
    {
        private new void Start()
        {
            base.Start();
        }
        private void Update()
        {
            if (!IsOwner || !IsSpawned) return;
            if (Input.GetKey(KeyCode.A))Move(-1);
			if (Input.GetKey(KeyCode.D))Move(1);
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))NotMoving();
            if (Input.GetKeyDown(KeyCode.Space))Jump();
        }
    }
}