using TMPro;
using UnityEngine;

namespace HyperCasualSDK.UI
{
    [RequireComponent(typeof(PopScaleEffect))]
    public class ReceivedScorePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counterText;

        private PopScaleEffect _popScaleEffect;

        private void Awake()
        {
            _popScaleEffect = GetComponent<PopScaleEffect>();
        }

        public void UpdateAmount(int amount)
        {
            counterText.text = $"{amount:n0}";
            if (amount > 0) {
                if (!_popScaleEffect.gameObject.activeSelf) {
                    _popScaleEffect.gameObject.SetActive(true);
                }
                _popScaleEffect.Play();
            } else {
                _popScaleEffect.gameObject.SetActive(false);
            }
        }
    }
}
