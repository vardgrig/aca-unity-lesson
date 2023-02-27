using System;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private Vector2 boundHorizontal;
        [SerializeField] private Vector2 boundVertical;

        public void Spawn<T>() where T : AbstractItem
        {
            CreateItem<T>();
        }

        private void CreateItem<T>() where T : AbstractItem
        {
            string templateName = $"{typeof(T).Name}Template";
            T item = Factory.Create<T>(templateName);
            item.transform.position = GetRandomPosition();
            item.transform.parent = container;
        }

        private Vector2 GetRandomPosition()
        {
            float x = Random.Range(boundHorizontal.x, boundHorizontal.y);
            float y = Random.Range(boundVertical.x, boundVertical.y);
            return new Vector2(x, y);
        }
    }
}