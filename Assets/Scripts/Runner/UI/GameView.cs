using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameView : AbstractView
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private Button returnButton;

    protected override void OnEnable()
    {
        returnButton.onClick.AddListener(ReturnHome);
    }

    protected override void OnDisable()
    {
        returnButton.onClick.RemoveListener(ReturnHome);
    }

    public override void Init()
    {
        Debug.Log("Init Gameview");
    }

    private void Update()
    {
        UpdateDistance();
    }

    private void ReturnHome()
    {
        Debug.Log("Return Home");

    }
    private void UpdateDistance()
    {
        var distance = (int)player.transform.position.z;
        distanceText.text = $"{distance:D8}";
    }
}