using HyperCasualSDK;

namespace Runner
{
    public class DangerItem : AbstractTriggerCallback
    {
        private bool _exploded;

        public override void OnTrigger()
        {
            Explode();
        }

        private void Explode()
        {
            if (_exploded)
            {
                return;
            }

            _exploded = true;
            GameStateMachine.Events.PlayerLost.Invoke();
            AudioAssistant.Play(SoundEffectType.ButtonClick);
            gameObject.SetActive(false);
        }
    }
}
