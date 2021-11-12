using HyperCasualSDK;
using HyperCasualSDK.HelperComponents;
using UnityEngine;

namespace Runner
{
    public class Locomotion : MonoBehaviour
    {
        [SerializeField] [Range(1.0f, 30f)] private float maxVelocity = 6.0f;
        [SerializeField] [Range(0f, 25f)] private float acceleration = 1.0f;
        [SerializeField] public AxisBounds xBounds;
        [SerializeField] [Range(0.1f, 5f)] private float touchMoveSensitivity = 3.0f;

        private State _state = State.Idle;
        private float _velocity;
        private float _touchControlRatio;
        private float _xOnTouchDown;
        private float _x;
        private float _z;

        private void Awake()
        {
            _touchControlRatio = (Mathf.Abs(xBounds.min) + Mathf.Abs(xBounds.max)) / Screen.width * touchMoveSensitivity;
            SubscribeToEvents();
        }

        private void Update()
        {
            switch (_state)
            {
                case State.Accelerating:
                    _velocity += acceleration * Time.deltaTime;
                    if (_velocity > maxVelocity)
                    {
                        _velocity = maxVelocity;
                        _state = State.Running;
                    }

                    UpdateForwardMovement();
                    break;
                case State.Running:
                    UpdateForwardMovement();
                    break;
            }
        }

        private void SubscribeToEvents()
        {
            GameStateMachine.Events.Play.AddListener(Run);
            GameStateMachine.Events.PlayerSucceed.AddListener(Stop);
            GameStateMachine.Events.PlayerLost.AddListener(Stop);
            GameStateMachine.Events.RestartLevel.AddListener(ToStartPosition);
            GameStateMachine.Events.LevelFinished.AddListener(ToStartPosition);
            InputController.Events.TouchBegin.AddListener(() => _xOnTouchDown = transform.position.x);
            InputController.Events.TouchMoveDelta.AddListener(Slide);
        }

        private void Run()
        {
            _velocity = 0;
            _state = State.Accelerating;
        }

        private void Stop()
        {
            _velocity = 0;
            _state = State.Idle;
        }

        private void ToStartPosition()
        {
            _velocity = 0;
            _x = 0;
            _z = 0;
            UpdateForwardMovement();
        }

        private void UpdateForwardMovement()
        {
            _z += _velocity * Time.deltaTime;
            transform.position = new Vector3(_x, 0, _z);
        }

        private void Slide(float delta)
        {
            _x = Mathf.Lerp(_x, _xOnTouchDown + delta * _touchControlRatio, 0.5f);
            if (_x < xBounds.min)
            {
                _x = xBounds.min;
            }
            else if (_x > xBounds.max)
            {
                _x = xBounds.max;
            }
        }

        private enum State
        {
            Running,
            Idle,
            Accelerating
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                var position = transform.position;
                var minPosition = new Vector3(position.x - 0.5f + xBounds.min, position.y + 1.5f, position.z + 5f);
                var maxPosition = new Vector3(position.x + 0.5f + xBounds.max, position.y + 1.5f, position.z + 5f);
                var gizmoSize = new Vector3(1, 3, 20);
                var color = Gizmos.color;
                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(minPosition, gizmoSize);
                Gizmos.DrawWireCube(maxPosition, gizmoSize);
                Gizmos.color = color;
            }
        }
#endif
    }
}
