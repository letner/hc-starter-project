using UnityEngine;

namespace HyperCasualSDK.HelperComponents
{
    public class WaveRotator : MonoBehaviour
    {
        public float frequency = 3f;
        public Vector2 rotationRange = new Vector2(-5, 5);

        private float _phase;

        private void Awake()
        {
            _phase = Random.value * Mathf.PI;
        }

        private void LateUpdate()
        {
            var angle = Mathf.Sin(Time.unscaledTime * frequency + _phase) * Mathf.Abs(rotationRange.y - rotationRange.x) + rotationRange.x;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
