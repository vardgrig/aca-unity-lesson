using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunnerUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text distanceText;
    [SerializeField] TMP_Text recordText;
    [SerializeField] GameObject startScreenGO;
    [SerializeField] GameObject gameOverScreenGO;
    [SerializeField] Button startBtn;
    [SerializeField] Button restartBtn;
    [SerializeField] CharacterCollision character;
    GameManager manager;

    int distance;
    int bestDistance;

    void Start()
    {
        manager = GameManager.instance;
        manager.OnGameOver += GameOver;

        ShowPanel(startScreenGO);
        HidePanel(gameOverScreenGO);
        startBtn.onClick.AddListener(OnStartButtonClicked);
        restartBtn.onClick.AddListener(OnRestartButtonClicked);
    }

    void OnStartButtonClicked()
    {
        Debug.Log("Start the game");
        StartGame();
        HidePanel(startScreenGO);
    }

    void OnRestartButtonClicked()
    {
        Debug.Log("Restart the game");
        StartGame();
        HidePanel(gameOverScreenGO);
    }

    void HidePanel(GameObject panelToHide)
    {
        panelToHide.SetActive(false);
    }

    void ShowPanel(GameObject panelToShow)
    {
        panelToShow.SetActive(true);
    }

    void StartGame()
    {
        manager.SwitchState(GameState.Running);
    }

    void GameOver()
    {
        ShowPanel(gameOverScreenGO);
        UpdateDistanceText();
    }
    void UpdateDistanceText()
    {
        distance = manager.GetDistance();
        distanceText.text = $"Your distance is: {distance}m";
        UpdateRecordText();
    }
    void UpdateRecordText()
    {
        var isBest = manager.IsNewBestDistance();
        if (isBest)
        {   
            recordText.text = "NEW RECORD!!!";
        }
        else
        {
            bestDistance = manager.GetBestDistance();
            recordText.text = $"Best Record: {bestDistance}m";
        }
    }
}