using System;
using Players;
using Unity.Netcode;
using UnityEngine;

namespace Managers
{
    public class LobbyManager : NetworkBehaviour
    {
        public static LobbyManager instance;
        private NetworkList<PlayerData> playerDataList;
        void Awake()
        {
            instance = this;
            playerDataList = new NetworkList<PlayerData>();
        }
        public void StartHost()
        {
            NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
            NetworkManager.Singleton.StartHost();
        }

        private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            
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
