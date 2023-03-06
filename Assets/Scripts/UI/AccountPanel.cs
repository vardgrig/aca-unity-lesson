using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public delegate void LogOutDelegate();

    public class AccountPanel : AbstractPanel
    {
        [SerializeField] private TMP_InputField userNameInputField;
        [SerializeField] private Button logOutButton;
        public event LogOutDelegate OnLogOut;

        public void Setup(UserData userData)
        {
            userNameInputField.text = userData.Username;
        }

        private void OnEnable()
        {
            logOutButton.onClick.AddListener(OnLogOutButtonClicked);
        }

        private void OnDisable()
        {
            logOutButton.onClick.RemoveListener(OnLogOutButtonClicked);
        }

        private void OnLogOutButtonClicked()
        {
            OnLogOut?.Invoke();
        }
    }
}