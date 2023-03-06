using System.Collections.Generic;

namespace DefaultNamespace
{
    public class UserData
    {
        public string Username { get; }
        public string Password { get; }

        public UserData(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public static class DataStore
    {
        private static List<UserData> _userDatas = new List<UserData>
        {
            new UserData("admin", "1234"),
            new UserData("moderator", "qqqqqq")
        };

        public static bool TryFind(string username, string password, out UserData userData)
        {
            userData = _userDatas.Find(data => username == data.Username && password == data.Password);
            return userData != null;
        }
    }
}