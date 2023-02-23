using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Game
{
    //GOD OBJECT AT THE END OF THE PROJECT!!!!!!!!!!!
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Spawner spawner;
        [SerializeField] private Player player;

        private void Start()
        {
            SpawnNewItem();
        }

        private void OnEnable()
        {
            player.OnEat += SpawnNewItem;
        }

        private void OnDisable()
        {
            player.OnEat -= SpawnNewItem;
        }

        private void SpawnNewItem()
        {
            /*
             * Coin -> [0, 0.42]
             * Bomb -> [0.5, 0.7]
             * Heart -> [0.75, 1]
             */

            float randomValue = Random.value;
            if (randomValue >= 0f && randomValue <= 0.42f)
            {
                spawner.Spawn(SpawnType.Coin);
            }
            else if (randomValue >= 0.5f && randomValue <= 0.7f)
            {
                spawner.Spawn(SpawnType.Bomb);
            }
            else if (randomValue >= 0.75f && randomValue <= 1f)
            {
                spawner.Spawn(SpawnType.Heart);
            }
            else
            {
                //Nothing to spawn
                Debug.Log("Nothing to spawn");
            }
        }
    }
}