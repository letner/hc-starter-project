using HyperCasualSDK;
using UnityEngine;

namespace Runner
{
    public class PlayerTrigger : MonoBehaviour
    {
        [SerializeField] private AbstractTriggerCallback triggerCallback;

        private bool _wasTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (!_wasTriggered && other.CompareTag(Tags.Player))
            {
                _wasTriggered = true;
                triggerCallback.OnTrigger();
            }
        }
    }
}
