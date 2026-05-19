using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        [SerializeField]
        Button m_StartHostButton;
        [SerializeField]
        Button m_StartClientButton;
        [SerializeField]
        Button m_StartServerButton;
        [SerializeField]
        Button m_DisconnectButton;
        [SerializeField]
        TMP_InputField m_IP;
        [SerializeField]
        TMP_InputField m_Port;
        [SerializeField]
        TMP_InputField m_PlayerName;
        void Awake()
        {
            instance = this;
        }
        void Start()
        {
            m_StartHostButton.onClick.AddListener(StartHost);
            m_StartClientButton.onClick.AddListener(StartClient);
            m_StartServerButton.onClick.AddListener(StartServer);
            m_DisconnectButton.onClick.AddListener(Disconnect);
            m_IP.onEndEdit.AddListener(delegate { ChagneIP(); });
            m_Port.onEndEdit.AddListener(delegate { ChagneIP(); });
        }
        void StartClient()
        {
            try
            {
                NetworkManager.Singleton.StartClient();
            }
            catch (Exception e)
            {
                Debug.LogError("Error starting client: " + e.Message);
            }
        }

        void StartHost()
        {
            try
            {
                NetworkManager.Singleton.StartHost();
            }
            catch (Exception e)
            {
                Debug.LogError("Error starting host: " + e.Message);
            }
        }
        void Disconnect()
        {
            try
            {
                NetworkManager.Singleton.Shutdown();
            }
            catch (Exception e)
            {
                Debug.LogError("Error shutting down network: " + e.Message);
            }
        }
        void StartServer()
        {
            try
            {
                NetworkManager.Singleton.StartServer();
            }
            catch (Exception e)
            {
                Debug.LogError("Error starting server: " + e.Message);
            }
        }
        void ChagneIP()
        {
            NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().SetConnectionData(m_IP.text, Convert.ToUInt16(m_Port.text));
        }
        public void DeactivateButtons()
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
        public void ActivateButtons()
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
}
