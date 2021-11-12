using UnityEngine;

namespace HyperCasualSDK.HelperComponents
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private Transform transformToFollow;
        [SerializeField] private Axis fixedAxis;

        private Vector3 _offset;

        private void Awake()
        {
            _offset = transform.position - transformToFollow.position;
        }

        private void Update()
        {
            var position = transform.position;
            var followPosition = transformToFollow.position;
            switch (fixedAxis)
            {
                case Axis.X:
                    position = new Vector3(transform.position.x, transformToFollow.position.y + _offset.y, followPosition.z + _offset.z);
                    break;
                case Axis.Y:
                    position = new Vector3(transformToFollow.position.x + _offset.x, transform.position.y, followPosition.z + _offset.z);
                    break;
                case Axis.Z:
                    position = new Vector3(transformToFollow.position.x + _offset.x, followPosition.y + _offset.y, transform.position.z);
                    break;
            }

            transform.position = position;
        }
    }
}
