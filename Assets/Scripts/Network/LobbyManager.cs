using Unity.Netcode;
using UnityEngine;

namespace Network
{
    public class LobbyManager : NetworkBehaviour
    {
        public static LobbyManager instance;
        public Transform[] spawnPoints;
        private const int MAX_PLAYERS = 4;
        private bool gameStarted = false;
        [SerializeField]private NetworkList<PlayerData> playerDataList;
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
            Debug.Log($"Connection approval requested from client {request.ClientNetworkId}. Current players: {NetworkManager.Singleton.ConnectedClientsIds.Count}");    
            if (NetworkManager.Singleton != null && IsServer)
            {
                Debug.Log($"Client {request.ClientNetworkId} is trying to connect. Current players: {NetworkManager.Singleton.ConnectedClientsIds.Count}");
                if (NetworkManager.Singleton.ConnectedClientsIds.Count >= MAX_PLAYERS){
                    response.Approved = false;
                    response.CreatePlayerObject = false;
                    response.Reason = "Server is full";
                    Debug.Log($"Client {request.ClientNetworkId} connection rejected: {response.Reason}. Current players: {NetworkManager.Singleton.ConnectedClientsIds.Count}");
                    return;        
                }
                if (playerDataList.Contains(new PlayerData { clientId = request.ClientNetworkId }))
                {
                    response.Approved = false;
                    response.CreatePlayerObject = false;
                    response.Reason = "Client already connected";
                    Debug.Log($"Client {request.ClientNetworkId} connection rejected: {response.Reason}. Current players: {NetworkManager.Singleton.ConnectedClientsIds.Count}");
                    return;
                }
                if (gameStarted)
                {
                    response.Approved = false;
                    response.CreatePlayerObject = false;
                    response.Reason = "Game already started";
                    Debug.Log($"Client {request.ClientNetworkId} connection rejected: {response.Reason}. Current players: {NetworkManager.Singleton.ConnectedClientsIds.Count}");
                    return;
                }
            }
            response.Approved = true;
            response.CreatePlayerObject = true;
            response.Position = spawnPoints[NetworkManager.Singleton.ConnectedClientsIds.Count % spawnPoints.Length].position;
            Debug.Log($"Client {request.ClientNetworkId} connection approved. Current players: {NetworkManager.Singleton.ConnectedClientsIds.Count}");
        }

        void Start()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        }
        void OnClientConnected(ulong clientId)
        {
            Debug.Log($"Client {clientId} connected.");
            if (NetworkManager.Singleton != null && IsServer)
            {
                playerDataList.Add(new PlayerData { clientId = clientId });
            }
            
        }

        void OnClientDisconnected(ulong clientId)
        {
            Debug.Log($"Client {clientId} disconnected.");
            if (NetworkManager.Singleton != null && IsServer)
            {
                playerDataList.Remove(new PlayerData { clientId = clientId });
            }
        }
    }
}
