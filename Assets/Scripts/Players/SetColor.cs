using System;
using Unity.Netcode;
using UnityEngine;

namespace Players
{
    public class SetColor : NetworkBehaviour
    {
        public Color color;
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            SetColorBasedOnOwner();
        }
        protected override void OnOwnershipChanged(ulong previous, ulong current)
        {
            SetColorBasedOnOwner();
        }
        void SetColorBasedOnOwner()
        {
            UnityEngine.Random.InitState((int)OwnerClientId);
            color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);
            GetComponent<SpriteRenderer>().color = color;
        }
    }
}