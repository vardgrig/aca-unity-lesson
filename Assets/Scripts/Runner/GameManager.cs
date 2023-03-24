using UnityEngine;

public enum GameState
{
    Preparing,
    Running,
    GameOver
}
public delegate void OnPrepare();
public delegate void OnGameStarted();
public delegate void OnGameOver();

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    #region Events
    public OnPrepare OnPrepare;
    public OnGameStarted OnGameStarted;
    public OnGameOver OnGameOver;
    #endregion

    #region Private Variables
    private GameState gameState;
    private int playerDistance;
    private int playerRecordDistance;
    [SerializeField] private CharacterMovement character;
    #endregion

    #region Private Methods
    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }
    void Start()
    {
        playerRecordDistance = PlayerPrefs.GetInt("Best");
        SwitchState(GameState.Preparing);
    }

    #endregion

    #region Public Methods
    public void SwitchState(GameState state)
    {
        switch (state)
        {
            case GameState.Preparing:
                gameState = state;
                OnPrepare?.Invoke();
                break;
            case GameState.Running:
                gameState = state;
                OnGameStarted?.Invoke();
                AudioManager.instance.Play("Background");
                break;
            case GameState.GameOver:
                gameState = state;
                playerDistance = character.GetDistance();
                OnGameOver?.Invoke();
                AudioManager.instance.Stop("Background");
                AudioManager.instance.Play("GameOver");
                break;
        }
    }

    public int GetDistance()
    {
        return playerDistance;
    }

    public bool IsNewBestDistance()
    {
        if (playerDistance > playerRecordDistance)
        {
            playerRecordDistance = playerDistance;
            PlayerPrefs.SetInt("Best", playerRecordDistance);
            return true;
        }
        return false;
    }

    public int GetBestDistance()
    {
        return playerRecordDistance;
    }
    #endregion
}