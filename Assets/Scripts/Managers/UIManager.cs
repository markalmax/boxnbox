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
        [SerializeField]Button StartHostButton, StartClientButton, StartServerButton, DisconnectButton, ExitButton, BackButton, SettingsButton, ResumeButton;
        [SerializeField]TMP_InputField IP, Port, PlayerName;
        [SerializeField]GameObject Menu, PauseMenu, SettingsMenu;
        public int activeState = 0;
        void Awake()
        {
            instance = this;
        }
        void Start()
        {
            StartHostButton.onClick.AddListener(StartHost);
            StartClientButton.onClick.AddListener(StartClient);
            StartServerButton.onClick.AddListener(StartServer);
            DisconnectButton.onClick.AddListener(Disconnect);
            ExitButton.onClick.AddListener(Exit);
            IP.onEndEdit.AddListener(delegate { ChangeIP(); });
            Port.onEndEdit.AddListener(delegate { ChangeIP(); });
            ResumeButton.onClick.AddListener(delegate { OnStateChange(0); }); 
            BackButton.onClick.AddListener(delegate { OnStateChange(1); });
            SettingsButton.onClick.AddListener(delegate { OnStateChange(2); });
        }
        void OnStateChange(int state)
        {
            //0 means no menu, 1 means pause, 2 means settings
                switch (state)
                {
                    case 0:
                        Menu.SetActive(false);
                        PauseMenu.SetActive(false);
                        SettingsMenu.SetActive(false);
                        activeState = 0;
                        break;
                    case 1:
                        Menu.SetActive(true);
                        PauseMenu.SetActive(true);
                        SettingsMenu.SetActive(false);
                        activeState = 1;
                        break;
                    case 2:
                        Menu.SetActive(true);
                        PauseMenu.SetActive(false);
                        SettingsMenu.SetActive(true);
                        activeState = 2;
                        break;
                }
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (activeState)
                {
                    case 0:
                        OnStateChange(1);
                        break;
                    case 1:
                        OnStateChange(0);
                        break;
                    case 2:
                        OnStateChange(1);
                        break;
                }
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
        void ChangeIP()
        {
            NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().SetConnectionData(IP.text, Convert.ToUInt16(Port.text));
        }
        void Exit()
        {
            Application.Quit();
        }
        public void DeactivateButtons()
        {
            StartHostButton.interactable = false;
            StartHostButton.gameObject.SetActive(false);
            StartClientButton.interactable = false;
            StartClientButton.gameObject.SetActive(false);
            StartServerButton.interactable = false;
            StartServerButton.gameObject.SetActive(false);
            IP.interactable = false;
            IP.gameObject.SetActive(false);
            Port.interactable = false;
            Port.gameObject.SetActive(false);
            PlayerName.interactable = false;
            PlayerName.gameObject.SetActive(false);

            DisconnectButton.interactable = true;
        }
        public void ActivateButtons()
        {
            StartHostButton.interactable = true;
            StartHostButton.gameObject.SetActive(true);
            StartClientButton.interactable = true;
            StartClientButton.gameObject.SetActive(true);
            StartServerButton.interactable = true;
            StartServerButton.gameObject.SetActive(true);
            IP.interactable = true;
            IP.gameObject.SetActive(true);
            Port.interactable = true;
            Port.gameObject.SetActive(true);
            PlayerName.interactable = true;
            PlayerName.gameObject.SetActive(true);

            DisconnectButton.interactable = false;
        }
    }
}
