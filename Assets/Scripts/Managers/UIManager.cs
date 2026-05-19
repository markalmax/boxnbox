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
        Button m_ExitButton;
        [SerializeField]
        Button m_ResumeButton;
        [SerializeField]
        TMP_InputField m_IP;
        [SerializeField]
        TMP_InputField m_Port;
        [SerializeField]
        TMP_InputField m_PlayerName;
        [SerializeField]
        GameObject m_PauseMenu;
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
            m_ExitButton.onClick.AddListener(Exit);
            m_ResumeButton.onClick.AddListener(TogglePauseMenu);
            m_IP.onEndEdit.AddListener(delegate { ChagneIP(); });
            m_Port.onEndEdit.AddListener(delegate { ChagneIP(); });
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) TogglePauseMenu();
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
        void StartServer()
        {
            try
            {
                NetworkManager.Singleton.StartServer();
                DeactivateButtons();
            }
            catch (Exception e)
            {
                Debug.LogError("Error starting server: " + e.Message);
                ActivateButtons();
            }
        }
        void ChagneIP()
        {
            NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().SetConnectionData(m_IP.text, Convert.ToUInt16(m_Port.text));
        }
        void Exit()
        {
            Application.Quit();
        }
        public void TogglePauseMenu()
        {
            m_PauseMenu.SetActive(!m_PauseMenu.activeSelf);
        }
        public void DeactivateButtons()
        {
            m_StartHostButton.interactable = false;
            m_StartHostButton.gameObject.SetActive(false);
            m_StartClientButton.interactable = false;
            m_StartClientButton.gameObject.SetActive(false);
            m_StartServerButton.interactable = false;
            m_StartServerButton.gameObject.SetActive(false);
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
            m_StartServerButton.interactable = true;
            m_StartServerButton.gameObject.SetActive(true);
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
