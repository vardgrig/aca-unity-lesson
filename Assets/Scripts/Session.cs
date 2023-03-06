using UnityEngine;

namespace DefaultNamespace
{
    public static class Session
    {
        private const string USERNAME_KEY = "username";
        private const string PASSWORD_KEY = "password";

        public static UserData UserData { get; private set; }

        public static void SetUserData(UserData userData)
        {
            UserData = userData;
        }

        public static void SaveProfile()
        {
            if (UserData == null)
            {
                return;
            }

            PlayerPrefs.SetString(USERNAME_KEY, UserData.Username);
            PlayerPrefs.SetString(PASSWORD_KEY, UserData.Password);
            PlayerPrefs.Save();
        }

        public static void LoadProfile()
        {
            string username = PlayerPrefs.GetString(USERNAME_KEY);
            string password = PlayerPrefs.GetString(PASSWORD_KEY);
            SetUserData(new UserData(username,password));
        }

        public static void ClearProfile()
        {
            PlayerPrefs.DeleteKey(USERNAME_KEY);
            PlayerPrefs.DeleteKey(PASSWORD_KEY);
            PlayerPrefs.Save();
        }

        public static bool HasProfile()
        {
            return PlayerPrefs.HasKey(USERNAME_KEY);
        }
    }
}

/*
 * username - admin
 * password - 1234
 */