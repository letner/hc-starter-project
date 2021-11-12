using UnityEngine;

namespace HyperCasualSDK.UI
{
    public class UICelebrationEffect : MonoBehaviour
    {
        private const float TimeForShow = 3.0f;

        private float _shownTime;

        private void Awake()
        {
            GameStateMachine.Events.GameLoaded.AddListener(ForceHide);
            GameStateMachine.Events.PlayerSucceed.AddListener(Show);
            GameStateMachine.Events.CelebrationEnded.AddListener(ForceHide);
        }

        private void Update()
        {
            if (gameObject.activeSelf && Time.time > _shownTime + TimeForShow)
            {
                gameObject.SetActive(false);
            }
        }

        private void Show()
        {
            _shownTime = Time.time;
            gameObject.SetActive(true);
        }

        private void ForceHide()
        {
            gameObject.SetActive(false);
        }
    }
}
