using System;
using Unity.Netcode;
using UnityEngine;

public class SetColorBasedOnOwnerId : NetworkBehaviour
{
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
        GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
    }
}

