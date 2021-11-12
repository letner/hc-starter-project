using HyperCasualSDK;
using HyperCasualSDK.ExtensionMethods;
using UnityEngine;

namespace Runner
{
    public class LevelGenerator
    {
        public ILevel Generate(int index, Transform parent, RunnerEnvironmentSettings settings)
        {
            const int levelLength = 10;

            var levelObject = new GameObject($"Level #{index}");
            levelObject.AddComponent(typeof(Level));
            levelObject.transform.parent = parent;
            levelObject.transform.ResetLocal();
            FillLevelWithFloors(levelObject.transform, settings, levelLength);
            FillLevelWithCollectables(levelObject.transform, settings, levelLength);
            return levelObject.GetComponent<ILevel>();
        }

        private void FillLevelWithFloors(Transform parent, RunnerEnvironmentSettings settings, int length)
        {
            var position = Vector3.zero;
            CreateLevelItem(settings.StartFloorItemPrefab, position, parent);

            for (var i = 1; i < length; i++)
            {
                position = new Vector3(0, 0, i * settings.FloorLength);
                CreateLevelItem(settings.FloorItemPrefab, position, parent);
            }

            position = new Vector3(0, 0, length * settings.FloorLength);
            CreateLevelItem(settings.FinishFloorItemPrefab, position, parent);
        }

        private void FillLevelWithCollectables(Transform parent, RunnerEnvironmentSettings settings, int length)
        {
            for (var lengthIndex = 1; lengthIndex < length; lengthIndex++)
            {
                var collectablePrefab = settings.CollectablePrefabs[Random.Range(0, settings.CollectablePrefabs.Length)];
                var basePosition = new Vector3(0, 0, lengthIndex * settings.FloorLength);
                var amountOnTheFloor = Random.Range(2, 6);
                for (var collectableIndex = 0; collectableIndex < amountOnTheFloor; collectableIndex++)
                {
                    var x = Random.Range(-settings.FloorWidth * 0.5f, settings.FloorWidth * 0.5f);
                    var z = Random.Range(-settings.FloorLength, 0);
                    var position = basePosition + new Vector3(x, 0, z);
                    CreateLevelItem(collectablePrefab, position, parent);
                }
            }
        }

        private void CreateLevelItem(GameObject prefab, Vector3 position, Transform parent)
        {
            var floor = Object.Instantiate(prefab, parent);
            floor.transform.localPosition = position;
        }
    }
}
