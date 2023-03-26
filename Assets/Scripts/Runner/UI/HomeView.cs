using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HomeView : AbstractView
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button aboutButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI versionText;

    [SerializeField] private GameplayAggregator gameplayAggregator;
    [SerializeField] private GameView gameViewUI;
    [SerializeField] private SettingsView settingsViewUI;

    public override void Init()
    {
        SetVersion();
    }

    private void SetVersion()
    {
        string version = $"Dev. ver. {Application.version}";
        versionText.text = version;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        aboutButton.onClick.AddListener(OnAboutButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        aboutButton.onClick.RemoveListener(OnAboutButtonClicked);
        quitButton.onClick.RemoveListener(OnQuitButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        gameplayAggregator.Init();
        gameViewUI.gameObject.SetActive(true);
        HidePanel();
    }

    private void OnSettingsButtonClicked()
    {
        settingsViewUI.gameObject.SetActive(true);
        HidePanel();
    }

    private void OnAboutButtonClicked()
    {
        HidePanel();
    }

    private void HidePanel()
    {
        gameObject.SetActive(false);
    }
    private void OnQuitButtonClicked()
    {
        QuitGame();
    }
    
    private void QuitGame() 
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}