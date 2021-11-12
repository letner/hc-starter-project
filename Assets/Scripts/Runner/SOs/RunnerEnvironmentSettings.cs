using UnityEngine;

namespace Runner
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Environment/Runner Settings", fileName = "Plain Runner Environment.asset")]
    [System.Serializable]
    public class RunnerEnvironmentSettings : ScriptableObject
    {
        public int FloorLength => floorLength;
        public int FloorWidth => floorWidth;
        public GameObject FloorItemPrefab => floorItemPrefab;
        public GameObject StartFloorItemPrefab => startFloorItemPrefab;
        public GameObject FinishFloorItemPrefab => finishFloorItemPrefab;
        public GameObject[] CollectablePrefabs => collectablePrefabs;

        [SerializeField] private int floorLength;
        [SerializeField] private int floorWidth;
        [SerializeField] private GameObject floorItemPrefab;
        [SerializeField] private GameObject startFloorItemPrefab;
        [SerializeField] private GameObject finishFloorItemPrefab;
        [SerializeField] private GameObject[] collectablePrefabs;
    }
}
