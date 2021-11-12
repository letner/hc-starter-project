using UnityEngine;

namespace HyperCasualSDK.UI
{
    public class HandUI : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;

        private void LateUpdate()
        {
            var minX = Mathf.Sin(Time.time * 3.0f) * 0.25f + 0.35f;
            var maxX = minX + 0.3f;
            rectTransform.anchorMin = new Vector2(minX, rectTransform.anchorMin.y);
            rectTransform.anchorMax = new Vector2(maxX, rectTransform.anchorMax.y);
        }
    }
}
