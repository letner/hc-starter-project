using HyperCasualSDK;

namespace Runner
{
    public class ConsumableItem : AbstractTriggerCallback
    {
        private bool _consumed;

        public override void OnTrigger()
        {
            Consume();
        }

        private void Consume()
        {
            if (_consumed)
            {
                return;
            }
            _consumed = true;
            GameStateMachine.Events.IncreaseLevelScore.Invoke();
            AudioAssistant.Play(SoundEffectType.Consume);
            gameObject.SetActive(false);
        }
    }
}
