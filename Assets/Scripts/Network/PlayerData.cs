using System;
using Unity.Netcode;

namespace Network
{
public struct PlayerData : INetworkSerializable, IEquatable<PlayerData>
{
    public ulong clientId;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref clientId);
    }

    public bool Equals(PlayerData other)
    {
        return clientId == other.clientId;
    }
}
}