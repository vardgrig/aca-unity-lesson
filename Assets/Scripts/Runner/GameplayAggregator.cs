using UnityEngine;

public class GameplayAggregator : MonoBehaviour
{
    [SerializeField] private RoadSpawner roadSpawner;
    [SerializeField] private MobileInput input;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private GameView gameView;
    [SerializeField] private EndGameView endGameView;

    private void OnEnable()
    {
        playerCollision.OnCollisionObstacle += OnPlayerCollisionObstacle;
        endGameView.OnRestartGame += RestartGame;
    }


    private void OnDisable()
    {
        playerCollision.OnCollisionObstacle -= OnPlayerCollisionObstacle;
    }

    [ContextMenu("Play")]
    public void Init()
    {
        roadSpawner.Init();
        SetComponentActiveState(true);
        playerMovement.SetDifficultySpeed();
    }

    private void Update()
    {
        roadSpawner.UpdateFrame();
        input.UpdateFrame();
    }

    private void SetComponentActiveState(bool state)
    {
        input.SetInteractableState(state);
        playerMovement.SetBlockState(state);
        roadSpawner.SetCanSpawnState(state);
    }

    private void OnPlayerCollisionObstacle(IObstacle obstacle)
    {
        SetComponentActiveState(false);
        endGameView.gameObject.SetActive(true);
        gameView.gameObject.SetActive(false);
        endGameView.SetCurrentScore((int)playerMovement.transform.position.z);
    }
    private void RestartGame()
    {
        SetComponentActiveState(true);
        roadSpawner.ResetPositions();
        playerMovement.ResetPosition();
        endGameView.gameObject.SetActive(false);
        gameView.gameObject.SetActive(true);
    }
}