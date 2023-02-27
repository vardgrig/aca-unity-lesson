using UnityEngine;

namespace DefaultNamespace.Game
{
    public static class Factory
    {
        public static T Create<T>(string name) where T : MonoBehaviour
        {
            T prefab = Resources.Load<T>($"Prefabs/{name}");
            T instance = Object.Instantiate(prefab);
            return instance;
        }
    }
}