using HyperCasualSDK;

namespace Runner
{
    public class FinishFloor : AbstractTriggerCallback
    {
        public override void OnTrigger()
        {
            GameStateMachine.Events.PlayerSucceed.Invoke();
        }
    }
}
