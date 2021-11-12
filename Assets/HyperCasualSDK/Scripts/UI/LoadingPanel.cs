using UnityEngine;
using UnityEngine.UI;

namespace HyperCasualSDK.UI
{
    [RequireComponent(typeof(CanvasContainer))]
    public class LoadingPanel : AbstractPanel
    {
        public Image progressBar;
        
        private new void Awake()
        {
            base.Awake();
            Show();
            SceneLoader.Events.LoadingProgress.AddListener(SetLoadingProgress);     
            GameStateMachine.Events.GameLoaded.AddListener(Hide);
            SetLoadingProgress(0);
        }

        private void SetLoadingProgress(float progress)
        {
            progressBar.rectTransform.anchorMax = new Vector2(progress, 1);
        }
        
        
    }
}
