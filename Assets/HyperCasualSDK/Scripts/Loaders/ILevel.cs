using UnityEngine;

namespace HyperCasualSDK
{
    public interface ILevel
    {
        Vector3 GetStartPosition();
        GameObject GetGameObject();
    }
}
