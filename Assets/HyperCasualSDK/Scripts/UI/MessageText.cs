using TMPro;
using UnityEngine;

namespace HyperCasualSDK.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MessageText : MonoBehaviour
    {
        private const float TimeForShow = 1.5f;

        [SerializeField] private TextMeshProUGUI textField;
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
                    gameObject.SetActive(false);
                }
            }
        }

        public void ShowMessage(string textString)
        {
            if (textString == null)
            {
                return;
            }

            gameObject.SetActive(true);
            textField.text = textString;
            _isActive = true;
            _showTime = Time.time;
        }

        public void Hide()
        {
            _isActive = false;
            gameObject.SetActive(false);
        }
    }
}
