using UnityEngine;

namespace HyperCasualSDK.Data
{
    public struct CameraFollowLookAtPair
    {
        public Transform Follow { get; private set; }
        public Transform LookAt { get; private set; }

        public CameraFollowLookAtPair(Transform follow, Transform lookAt)
        {
            Follow = follow;
            LookAt = lookAt;
        }
    }
}