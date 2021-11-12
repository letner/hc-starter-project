using UnityEngine;

namespace HyperCasualSDK.UI
{
    public sealed class AdsImageSwitcher : AbstractButton
    {
        [SerializeField] private GameObject adImage;
        [SerializeField] private GameObject noAdImage;

        private void Awake()
        {
            AppearanceOptions.ForceEvents.NoAds.AddListener(ForceAllAdsOff);
        }

        private void ForceAllAdsOff()
        {
            adImage.SetActive(false);
            noAdImage.SetActive(true);
        }
    }
}
