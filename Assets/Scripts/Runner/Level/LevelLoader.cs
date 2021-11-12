using HyperCasualSDK;
using UnityEngine;

namespace Runner
{
    public class LevelLoader : AbstractLevelLoader
    {
        [SerializeField] private RunnerEnvironmentSettings environmentSettings;

        private GameObject _levelObject;
        private readonly LevelGenerator _levelGenerator = new LevelGenerator();

        protected override void Load(int index)
        {
            base.Load(index);
            _levelObject = _levelGenerator.Generate(index, transform, environmentSettings).GetGameObject();
            Loaded(index);
        }

        protected override void Destroy()
        {
            DestroyImmediate(_levelObject);
        }
    }
}
