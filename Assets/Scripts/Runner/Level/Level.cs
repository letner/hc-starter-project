using HyperCasualSDK;
using UnityEngine;

namespace Runner
{
    public class Level : MonoBehaviour, ILevel
    {
        private Vector3 _startPosition = Vector3.zero;

        public Vector3 GetStartPosition()
        {
            return _startPosition;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
