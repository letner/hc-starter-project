using UnityEngine;
using Cinemachine;
using HyperCasualSDK.Data;

namespace HyperCasualSDK
{
    public class CinemachineController : MonoBehaviour
    {
        [Header("Set in prefab")]
        [SerializeField] private CinemachineVirtualCamera gameplayCamera;
        [SerializeField] private CinemachineVirtualCamera gameOverCamera;
        [SerializeField] private CinemachineVirtualCamera storeCamera;

        private float _originalFOV;
        private float _calculatedFOV;

        private void Awake()
        {
            InitFieldOfView();
            SubscribeToEvents();
        }

        private void InitFieldOfView()
        {
            _originalFOV = gameplayCamera.m_Lens.FieldOfView;
            _calculatedFOV = _originalFOV * (Screen.height / 1920f) * 1080f / Screen.width;
            gameplayCamera.m_Lens.FieldOfView = _calculatedFOV;
        }
        
        private void SubscribeToEvents()
        {
            GameStateMachine.Events.GameLoaded.AddListener(SwitchToLobby);
            GameStateMachine.Events.RestartLevel.AddListener(SwitchToLobby);
            GameStateMachine.Events.Revive.AddListener(SwitchToLobby);
            GameStateMachine.Events.Play.AddListener(SwitchToGameplay);
            GameStateMachine.Events.PlayerLost.AddListener(SwitchToGameOver);
        }

        public void SetGameplayTarget(CameraFollowLookAtPair followLookAtPair)
        {
            gameplayCamera.Follow = followLookAtPair.Follow;
            gameplayCamera.LookAt = followLookAtPair.LookAt;
            gameOverCamera.Follow = followLookAtPair.Follow;
            gameOverCamera.LookAt = followLookAtPair.LookAt;
        }

        private void SwitchToLobby()
        {
            SwitchCamera(gameplayCamera, true);
            SwitchCamera(storeCamera, false);
            SwitchCamera(gameOverCamera, false);
        }

        private void SwitchToGameplay()
        {
            if (!gameplayCamera.enabled)
            {
                SwitchCamera(gameplayCamera, true);
                SwitchCamera(storeCamera, false);
                SwitchCamera(gameOverCamera, false);
            }
        }

        private void SwitchToStore()
        {
            SwitchCamera(gameplayCamera, false);
            SwitchCamera(storeCamera, true);
            SwitchCamera(gameOverCamera, false);
        }

        private void SwitchToGameOver()
        {
            SwitchCamera(gameplayCamera, false);
            SwitchCamera(storeCamera, false);
            SwitchCamera(gameOverCamera, true);
        }

        private void SwitchCamera(CinemachineVirtualCamera virtualCamera, bool enable)
        {
            if (virtualCamera != null)
            {
                virtualCamera.enabled = enable;
            }
        }
    }
}
