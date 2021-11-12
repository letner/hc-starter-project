using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HyperCasualSDK
{
    public class InputController : MonoBehaviour, IEventsOwner
    {
        private const int MousePointerId = -1;
        private const int TouchPointerId = 0;

        private GameState _gameState;
        private Vector2 _downPoint;
        private int _previousDelta;
#if UNITY_EDITOR
        private bool _isDown;
#endif

        public void Awake()
        {
            GameStateMachine.Events.SetGameState.AddListener(gameState => _gameState = gameState);
        }

        public void Update()
        {
            switch (_gameState)
            {
                case GameState.Gameplay:
                case GameState.Lobby:
                    ProcessTouch();
                    break;
            }
        }

        private void ProcessTouch()
        {
            var pointerId = TouchPointerId;
#if UNITY_EDITOR
            pointerId = MousePointerId;
#endif
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(pointerId))
            {
                return;
            }

            if (Input.touchCount > 0)
            {
                Debug.Log("InputController: Touch detected");
                var touch = Input.touches[0];
                if (touch.phase == TouchPhase.Began)
                {
                    ProcessTouchBegan(touch.position);
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    ProcessTouchMoved(touch.position);
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    ProcessTouchEnded(touch.position);
                }
            }
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                ProcessTouchBegan(Input.mousePosition);
                _isDown = true;
            }

            if (Input.GetMouseButton(0))
            {
                if (_isDown)
                {
                    ProcessTouchMoved(Input.mousePosition);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                ProcessTouchEnded(Input.mousePosition);
                _isDown = false;
            }
#endif
        }

        private void ProcessTouchBegan(Vector2 point)
        {
            _downPoint = point;
            Events.TouchBegin.Invoke();
        }

        private void ProcessTouchMoved(Vector2 point)
        {
            var delta = (int) (point.x - _downPoint.x);
            if (delta != _previousDelta)
            {
                Events.TouchMoveDelta.Invoke(delta);
            }
            _previousDelta = delta;
        }

        private void ProcessTouchEnded(Vector2 point)
        {
            var delta = point.x - _downPoint.x;
            Events.TouchEndDelta.Invoke(delta);
        }

        private void OnApplicationQuit()
        {
            RemoveAllListeners();
        }

        public void RemoveAllListeners()
        {
            Events.TouchBegin.RemoveAllListeners();
            Events.TouchMoveDelta.RemoveAllListeners();
            Events.TouchEndDelta.RemoveAllListeners();
        }

        public struct Events
        {
            public static readonly UnityEvent TouchBegin = new UnityEvent();
            public static readonly UnityEvent<float> TouchMoveDelta = new UnityEvent<float>();
            public static readonly UnityEvent<float> TouchEndDelta = new UnityEvent<float>();
        }
    }
}
