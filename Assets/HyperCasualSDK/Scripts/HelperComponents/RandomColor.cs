using UnityEngine;

namespace HyperCasualSDK.HelperComponents
{
    public class RandomColor : MonoBehaviour
    {
        public Renderer objectRenderer;
        public Color[] colors;

        private void Awake()
        {
            var index = Random.Range(0, colors.Length);
            UpdateColor(colors[index]);
        }

        private void UpdateColor(Color color)
        {
            objectRenderer.material.color = color;
        }
    }
}
