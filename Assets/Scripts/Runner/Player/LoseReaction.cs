using HyperCasualSDK;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runner
{
    [RequireComponent(typeof(Rigidbody))]
    public class LoseReaction : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameStateMachine.Events.PlayerLost.AddListener(Die);
            GameStateMachine.Events.Revive.AddListener(Revive);
            GameStateMachine.Events.RestartLevel.AddListener(Revive);
        }

        private void Die()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            _rigidbody.AddForce(new Vector3(Random.Range(-3, 3), 5, -5), ForceMode.Impulse);
        }

        private void Revive()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
