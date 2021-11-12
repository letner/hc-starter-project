using UnityEngine;

namespace HyperCasualSDK.HelperComponents
{
    public class CopyTargetTransform : MonoBehaviour
    {
        public Transform target;

        private void LateUpdate()
        {
            transform.SetPositionAndRotation(target.position, target.rotation);
        }
    }
}
