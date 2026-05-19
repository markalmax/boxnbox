using Unity.Netcode;
using UnityEngine;

namespace Managers
{
    public class GameManager : NetworkBehaviour
    {
        public static GameManager instance;

        void Awake()
        {
            instance = this;
        }
        void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
        void Update()
        {

        }
    }
}