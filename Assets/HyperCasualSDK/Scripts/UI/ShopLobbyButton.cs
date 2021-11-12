using UnityEngine;
using UnityEngine.UI;

namespace HyperCasualSDK.UI
{
    public class ShopLobbyButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            AudioAssistant.Play(SoundEffectType.ButtonClick);
        }
    }
}
