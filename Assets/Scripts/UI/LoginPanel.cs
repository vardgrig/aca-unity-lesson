using System;
using DefaultNamespace;
using DefaultNamespace.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void LoginCompleteDelegate(bool success);
public class LoginPanel : AbstractPanel
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Button loginButton;

    public event LoginCompleteDelegate OnLoginComplete;
    
    private void OnEnable()
    {
        usernameInputField.onValueChanged.AddListener(OnUserNameValueChanged);
        passwordInputField.onValueChanged.AddListener(OnPasswordValueChanged);
        loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    private void OnDisable()
    {
        usernameInputField.onValueChanged.RemoveListener(OnUserNameValueChanged);
        passwordInputField.onValueChanged.RemoveListener(OnPasswordValueChanged);
        loginButton.onClick.RemoveListener(OnLoginButtonClicked);
    }

    private void OnLoginButtonClicked()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        Debug.Log($"Username: {username}, Password: {password}");

        if (DataStore.TryFind(username, password, out UserData userData))
        {
            Session.SetUserData(userData);
            OnLoginComplete?.Invoke(true);
        }
        else
        {
            OnLoginComplete?.Invoke(false);
        }
    }

    private void OnUserNameValueChanged(string value)
    {
    }

    private void OnPasswordValueChanged(string value)
    {
    }
}