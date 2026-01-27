using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Unity.Multiplayer.Center.NetcodeForGameObjectsExample
{
    public class TemporaryUI : MonoBehaviour
    {
        [SerializeField]
        Button m_StartHostButton;
        [SerializeField]
        Button m_StartClientButton;
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
            m_IP.onEndEdit.AddListener(delegate { ChagneIP(); });
            m_Port.onEndEdit.AddListener(delegate { ChagneIP(); });
        }

        void StartClient()
        {
            NetworkManager.Singleton.StartClient();
            DeactivateButtons();
        }

        void StartHost()
        {
            NetworkManager.Singleton.StartHost();
            DeactivateButtons();
        }
        void ChagneIP()
        {
            NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().SetConnectionData(m_IP.text, Convert.ToUInt16(m_Port.text));
        }

        void DeactivateButtons()
        {
            m_StartHostButton.interactable = false;
            m_StartClientButton.interactable = false;
        }
    }
}