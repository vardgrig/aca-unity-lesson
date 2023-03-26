using UnityEngine;
using UnityEngine.UI;

public class SettingsView : AbstractView
{
    [SerializeField] private Toggle difficultyToggle;
    public override void Init()
    {
        GetDifficulty();
    }


    private void GetDifficulty()
    {
        var difficultyValue = PlayerPrefs.GetInt("Difficulty");
        if(difficultyValue == 0)
        {
            ChangeToggle(false);
        }
        else
        {
            ChangeToggle(true);
        }
    }
    
    private void ChangeToggle(bool isOn)
    {
        difficultyToggle.isOn = isOn;
    }
    private void SetDifficulty(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("Difficulty", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Difficulty", 0);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        difficultyToggle.onValueChanged.AddListener(SetDifficulty);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        difficultyToggle.onValueChanged.RemoveListener(SetDifficulty);

    }
}
