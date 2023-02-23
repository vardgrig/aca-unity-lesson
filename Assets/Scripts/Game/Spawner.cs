using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Game
{
    public enum SpawnType
    {
        Coin,
        Heart,
        Bomb
    }

    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Coin coinTemplate;
        [SerializeField] private Heart heartTemplate;
        [SerializeField] private Bomb bombTemplate;

        [SerializeField] private Transform container;

        [SerializeField] private Vector2 boundHorizontal;
        [SerializeField] private Vector2 boundVertical;

        public void Spawn(SpawnType spawnType)
        {
            switch (spawnType)
            {
                case SpawnType.Coin:
                    Spawn(coinTemplate);
                    break;
                case SpawnType.Heart:
                    Spawn(heartTemplate);
                    break;
                case SpawnType.Bomb:
                    Spawn(bombTemplate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(spawnType), spawnType, null);
            }
        }

        private void Spawn(AbstractItem itemGameObject)
        {
            AbstractItem item = Instantiate(itemGameObject, container);
            item.transform.position = GetRandomPosition();
            item.Activate();
        }
        
        private Vector2 GetRandomPosition()
        {
            float x = Random.Range(boundHorizontal.x, boundHorizontal.y);
            float y = Random.Range(boundVertical.x, boundVertical.y);
            return new Vector2(x, y);
        }
    }
}