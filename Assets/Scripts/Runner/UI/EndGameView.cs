using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnRestartGame();
public class EndGameView : AbstractView
{
    public OnRestartGame OnRestartGame;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnButton;

    public override void Init()
    {

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        returnButton.onClick.AddListener(OnReturnButtonClicked);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        returnButton.onClick.RemoveListener(OnReturnButtonClicked);
    }


    private void OnRestartButtonClicked()
    {
        Debug.Log("Restart Button Pressed");
        OnRestartGame?.Invoke();
    }

    private void OnReturnButtonClicked()
    {
        Debug.Log("Return Button Pressed");
    }

    public void SetCurrentScore(int score)
    {
        scoreText.text = $"{score:D8}";
        SetBestScore(score);
    }

    private void SetBestScore(int score)
    {
        var bestScore = Store.BestScore;
        if (score > bestScore)
        {
            Store.BestScore = score;
            bestScore = score;
        }
        bestScoreText.text = $"{bestScore:D8}";
    }
}