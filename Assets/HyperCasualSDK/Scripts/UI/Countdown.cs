using UnityEngine;

namespace HyperCasualSDK.UI
{

    public class Countdown : MonoBehaviour
    {
        [SerializeField] private RectTransform progressRect;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void UpdateProgress(float progress)
        {
            progressRect.anchorMax = new Vector2(progress, progressRect.anchorMax.y);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
