using UnityEngine;

namespace HyperCasualSDK.Data
{
    [System.Serializable]
    public struct ColorScheme
    {
        public Texture universalMaterialTexture;
        public Color[] effectMaterialColors;
        public Color skyboxBottom;
        public Color skyboxTop;
        public Color foundationColor;
    }
}
