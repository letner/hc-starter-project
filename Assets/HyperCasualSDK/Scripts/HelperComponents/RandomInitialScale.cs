using UnityEngine;

namespace HyperCasualSDK.HelperComponents
{
    public class RandomInitialScale : MonoBehaviour
    {
        public Vector2 scaleRange;

        private void Awake()
        {
            var scale = transform.localScale.x * Random.Range(scaleRange.x, scaleRange.y);
            transform.localScale = new Vector3(scale, scale, scale);
        }

    }
}
