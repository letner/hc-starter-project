using HyperCasualSDK.Data;
using UnityEngine;

namespace HyperCasualSDK.Tools
{
    public class ColorSchemeSwitcher : MonoBehaviour
    {
        private static readonly int BottomColor = Shader.PropertyToID("_BottomColor");
        private static readonly int TopColor = Shader.PropertyToID("_TopColor");
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private static readonly int Color = Shader.PropertyToID("_Color");

        [SerializeField] private Material universalMaterial;
        [SerializeField] private Material[] effectMaterials;
        [SerializeField] private Material skybox;
        [SerializeField] private Material foundationMaterial;

        [SerializeField] private ColorScheme[] colorSchemes;

        private ColorScheme _defaultColorScheme;

        private void Awake()
        {
            CacheDefaultColorScheme();
            SubscribeToEvents();
        }

        private void CacheDefaultColorScheme()
        {
            _defaultColorScheme = new ColorScheme
            {
                universalMaterialTexture = universalMaterial.GetTexture(MainTex),
                effectMaterialColors = ExtractColorsFromMaterials(effectMaterials),
                skyboxBottom = skybox.GetColor(BottomColor),
                skyboxTop = skybox.GetColor(TopColor),
                foundationColor = foundationMaterial.GetColor(Color)
            };
        }

        private void SubscribeToEvents()
        {
            LevelCounter.Events.LoadLevel.AddListener(levelIndex => ApplyScheme(levelIndex % colorSchemes.Length));
        }

        private void ApplyScheme(int index)
        {
            var colorScheme = colorSchemes[index];
            ApplyScheme(colorScheme);
        }

        private void ApplyScheme(ColorScheme scheme)
        {
            universalMaterial.SetTexture(MainTex, scheme.universalMaterialTexture);
            for (var i = 0; i < effectMaterials.Length; i++) {
                effectMaterials[i].color = scheme.effectMaterialColors[i];
            }
            skybox.SetColor(BottomColor, scheme.skyboxBottom);
            skybox.SetColor(TopColor, scheme.skyboxTop);
            foundationMaterial.SetColor(Color, scheme.foundationColor);
        }

        private Color[] ExtractColorsFromMaterials(Material[] materials)
        {
            var result = new Color[materials.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = materials[i].color;
            }
            return result;
        }

        private void OnApplicationQuit()
        {
            ApplyScheme(_defaultColorScheme);
        }
    }
}
