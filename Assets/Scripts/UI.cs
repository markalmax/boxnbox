using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TemporaryUI : MonoBehaviour
{
    [SerializeField]
    Button m_StartHostButton;
    [SerializeField]
    Button m_StartClientButton;
    [SerializeField]
    Button m_DisconnectButton;
    [SerializeField]
    TMP_InputField m_IP;
    [SerializeField]
    TMP_InputField m_Port;
    [SerializeField]
    TMP_InputField m_PlayerName;
    void Start()
    {
        m_StartHostButton.onClick.AddListener(StartHost);
        m_StartClientButton.onClick.AddListener(StartClient);
        m_DisconnectButton.onClick.AddListener(Disconnect);
        m_IP.onEndEdit.AddListener(delegate { ChagneIP(); });
        m_Port.onEndEdit.AddListener(delegate { ChagneIP(); });

        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        }
    }

    void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
        }
    }

    void StartClient()
    {
        try
        {
            NetworkManager.Singleton.StartClient();
            DeactivateButtons();
        }
        catch (Exception e)
        {
            Debug.LogError("Error starting client: " + e.Message);
            ActivateButtons();
        }
    }

    void StartHost()
    {
        try
        {
            NetworkManager.Singleton.StartHost();
            DeactivateButtons();
        }
        catch (Exception e)
        {
            Debug.LogError("Error starting host: " + e.Message);
            ActivateButtons();
        }
    }
    void Disconnect()
    {
        try
        {
            NetworkManager.Singleton.Shutdown();
            ActivateButtons();
        }
        catch (Exception e)
        {
            Debug.LogError("Error shutting down network: " + e.Message);
            DeactivateButtons();
        }
    }

    void OnClientConnected(ulong clientId)
    {
        Debug.Log($"Network connected: clientId={clientId}");
    }

    void OnClientDisconnected(ulong clientId)
    {
        Debug.Log($"Network disconnected: clientId={clientId}");
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsHost)
        {
            ActivateButtons();
        }
    }

    void ChagneIP()
    {
        NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().SetConnectionData(m_IP.text, Convert.ToUInt16(m_Port.text));
    }
    void DeactivateButtons()
    {
        m_StartHostButton.interactable = false;
        m_StartHostButton.gameObject.SetActive(false);
        m_StartClientButton.interactable = false;
        m_StartClientButton.gameObject.SetActive(false);
        m_IP.interactable = false;
        m_IP.gameObject.SetActive(false);
        m_Port.interactable = false;
        m_Port.gameObject.SetActive(false);
        m_PlayerName.interactable = false;
        m_PlayerName.gameObject.SetActive(false);

        m_DisconnectButton.interactable = true;
    }
    void ActivateButtons()
    {
        m_StartHostButton.interactable = true;
        m_StartHostButton.gameObject.SetActive(true);
        m_StartClientButton.interactable = true;
        m_StartClientButton.gameObject.SetActive(true);
        m_IP.interactable = true;
        m_IP.gameObject.SetActive(true);
        m_Port.interactable = true;
        m_Port.gameObject.SetActive(true);
        m_PlayerName.interactable = true;
        m_PlayerName.gameObject.SetActive(true);

        m_DisconnectButton.interactable = false;
    }
}
