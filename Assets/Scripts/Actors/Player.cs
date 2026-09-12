using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using UI;

namespace Actors
{
    public class Player : ActorBase
    {
        public NetworkVariable<FixedString128Bytes> playerName = new NetworkVariable<FixedString128Bytes>("Player", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        private void Start()
        {
            if (IsOwner)
            {
                playerName.Value = (FixedString128Bytes)UIManager.instance.PlayerName;
            }
        }

        private void Update()
        {
            if (!IsOwner || !IsSpawned || !Application.isFocused || UIManager.instance.activeState != 0)
                return;

            Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetKey(KeyCode.A)) Move(-1);
            if (Input.GetKey(KeyCode.D)) Move(1);
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) NotMoving();
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
            if (Input.GetKeyDown(KeyCode.Mouse0)) Shoot();

            Vector3 vector2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            vector2 -= transform.position;
            float num = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f - 90f;
            SetGunRotation(num);
        }
    }
}