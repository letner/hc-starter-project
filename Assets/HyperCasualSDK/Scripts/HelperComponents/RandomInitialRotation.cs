using UnityEngine;

namespace HyperCasualSDK.HelperComponents
{
    public class RandomInitialRotation : MonoBehaviour
    {
        public Vector2 angleRange;
        public Axis axis;

        private void Awake()
        {
            var angle = Random.Range(angleRange.x, angleRange.y);
            switch (axis)
            {
                case Axis.X:
                    transform.localRotation *= Quaternion.Euler(angle, 0, 0);
                    break;
                case Axis.Y:
                    transform.localRotation *= Quaternion.Euler(0, angle, 0);
                    break;
                case Axis.Z:
                    transform.localRotation *= Quaternion.Euler(0, 0, angle);
                    break;
            }
        }
    }
}