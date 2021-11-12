using UnityEngine;

namespace HyperCasualSDK.HelperComponents
{
    public class Rotator : MonoBehaviour
    {
        public float speed = 120f;
        public bool slightlyRandom;
        public Axis axis;

        private Quaternion _xRotation;
        private Quaternion _yRotation;
        private Quaternion _zRotation;

        public bool isActive;

        private void Start()
        {
            if (slightlyRandom)
            {
                speed *= (1.0f + Random.Range(-.1f, .1f));
            }

            _xRotation = Quaternion.Euler(Time.deltaTime * speed, 0, 0);
            _yRotation = Quaternion.Euler(0, Time.deltaTime * speed, 0);
            _zRotation = Quaternion.Euler(0, 0, Time.deltaTime * speed);

        }

        private void Update()
        {
            if (isActive)
            {
                switch (axis)
                {
                    case Axis.X:
                        transform.rotation *= _xRotation;
                        break;
                    case Axis.Y:
                        transform.rotation *= _yRotation;
                        break;
                    case Axis.Z:
                        transform.rotation *= _zRotation;
                        break;
                }
            }
        }

        public void Activate()
        {
            isActive = true;
        }

        public void Deactivate()
        {
            isActive = false;
        }
    }
}
