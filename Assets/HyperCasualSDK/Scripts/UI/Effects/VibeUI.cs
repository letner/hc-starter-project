using UnityEngine;

namespace HyperCasualSDK.UI
{
    public class VibeUI : MonoBehaviour
    {
        public float frequency = 3f;
        public float scale = 1.0f;
        public float amplitude = 0.7f;

        private float _phase;

        private void Awake()
        {
            _phase = Random.value * Mathf.PI;
        }

        private void LateUpdate()
        {
            var scaleRatio = Mathf.Abs(Mathf.Sin(Time.unscaledTime * frequency + _phase)) * (scale * (1f - amplitude)) + scale * amplitude;
            transform.localScale = new Vector3(scaleRatio, scaleRatio, scaleRatio);
        }
    }
}
