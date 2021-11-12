using UnityEngine;

namespace HyperCasualSDK.UI
{
    public sealed class AchievementMessage : MonoBehaviour
    {
        private const float TimeForShow = 1.5f;

        [SerializeField] private Canvas canvas;

        private bool _isActive;
        private float _showTime;

        private void Update()
        {
            if (_isActive)
            {
                var scale = Mathf.Sin((Time.time - _showTime) / TimeForShow * Mathf.PI * .5f + Mathf.PI * .5f);
                transform.localScale = new Vector3(scale, scale, 1);
                if (Time.time > _showTime + TimeForShow)
                {
                    _isActive = false;
                    canvas.enabled = false;
                }
            }
        }

        public void Show()
        {
            canvas.enabled = true;
            _isActive = true;
            _showTime = Time.time;
        }

        public void Hide()
        {
            _isActive = false;
            canvas.enabled = false;
        }
    }
}
