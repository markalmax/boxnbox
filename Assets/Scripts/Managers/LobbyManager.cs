using Unity.Netcode;
using UnityEngine;

namespace Managers
{
    public class LobbyManager : NetworkBehaviour
    {
        public static LobbyManager instance;
        void Awake()
        {
            instance = this;
        }
        void Start()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        }
        void OnClientConnected(ulong clientId)
        {
            Debug.Log($"Network connected: clientId={clientId}");
            UIManager.instance.ActivateButtons();
        }

        void OnClientDisconnected(ulong clientId)
        {
            Debug.Log($"Network disconnected: clientId={clientId}");
            UIManager.instance.ActivateButtons();
        }
    }
}
