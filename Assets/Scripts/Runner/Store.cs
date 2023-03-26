using UnityEngine;

public static class Store
{
    private const string BEST_SCORE_KEY = "bestScore";

    private static int _bestScore;

    public static int BestScore
    {
        get => _bestScore == 0 ? GetBestScore() : _bestScore;
        set
        {
            _bestScore = value;
            SetBestScore(value);
        }
    }

    private static int GetBestScore()
    {
        return PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
    }

    private static void SetBestScore(int score)
    {
        PlayerPrefs.SetInt(BEST_SCORE_KEY, score);
    }
}