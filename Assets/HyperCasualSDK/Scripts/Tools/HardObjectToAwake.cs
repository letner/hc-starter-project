using System.Threading;
using UnityEngine;

namespace HyperCasualSDK.Tools
{
    public class HardObjectToAwake : MonoBehaviour
    {
        private void OnEnable()
        {
            Debug.Log("And 3 more seconds in in OnEnable");
            Thread.Sleep(3000);
        }

        private void Awake()
        {
            Debug.Log("And 3 more seconds inNow we'll wait 3 seconds in Awake");
            Thread.Sleep(3000);
        }

        private void Start()
        {
            GameStateMachine.Events.GameLoaded.Invoke();
        }
    }
}
