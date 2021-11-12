using HyperCasualSDK;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class CheckProductionReady
    {
        private const string HasToBeFalseMessage = "{0} option has to be false";

        [Test]
        public void CheckAppearanceOptionsProductionReady()
        {
            var appearanceOptions = Object.FindObjectOfType<AppearanceOptions>();
            Assert.False(appearanceOptions.forceNoUI, HasToBeFalseMessage, "ForceNoUI");
            Assert.False(appearanceOptions.forceReviveAfterDeathAutomatically, HasToBeFalseMessage, "ForceReviveAfterDeathAutomatically");
            Assert.False(appearanceOptions.forceRestartAutomatically, HasToBeFalseMessage, "ForceRestartAutomatically");
            Assert.False(appearanceOptions.enableDebugViews, HasToBeFalseMessage, "EnableDebugViews");
        }
    }
}
