using UnityEngine;

namespace HyperCasualSDK.Data
{
    public interface IPlayer
    {
        void ApplyStartState();
        Transform GetFollowTransform();
        Transform GetLookAtTransform();
    }
}
