using UnityEngine;

namespace HyperCasualSDK.UI
{
    public sealed class CanvasContainer : MonoBehaviour
    {
        public bool IsShown => _canvas.enabled;

        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void Show()
        {
            _canvas.enabled = true;
        }

        public void Hide()
        {
            _canvas.enabled = false;
        }
    }
}
