using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HyperCasualSDK.UI
{
    public abstract class AbstractButton : MonoBehaviour
    {
        public Button button;

        public bool IsShown => gameObject.activeSelf;

        public void AddOnClickAction(UnityAction action)
        {
            button.onClick.AddListener(action);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
