using UnityEngine;

namespace HyperCasualSDK.UI
{
    [RequireComponent(typeof(CanvasContainer))]
    public class AbstractPanel : MonoBehaviour
    {
        public CanvasContainer canvasContainer;

        public void Awake()
        {
            if (canvasContainer == null)
            {
                Debug.LogWarning($"Canvas container for {gameObject.name} was null. Please, set it in the Inspector to slightly reduce loading time.");
                canvasContainer = GetComponent<CanvasContainer>();
            }
        }
        
        public void Show()
        {
            canvasContainer.Show();
        }

        public void Hide()
        {
            canvasContainer.Hide();
        }

    }
}
