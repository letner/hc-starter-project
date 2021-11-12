using UnityEngine;

namespace HyperCasualSDK.Data
{
    [System.Serializable]
    public class TransformProperties
    {
        private Transform _transform;
        private Vector3 _localPosition;
        private Quaternion _localRotation;

        public TransformProperties(Transform transform)
        {
            _transform = transform;
            _localPosition = transform.localPosition;
            _localRotation = transform.localRotation;
        }

        public void Reset()
        {
            _transform.localPosition = _localPosition;
            _transform.localRotation = _localRotation;
        }
    }
}