using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class DBConn : MonoBehaviour
{
    private string url = "http://localhost/user_login.php";
    [SerializeField] private User user;
    [SerializeField] private ServerResponse response;
    public void Username(string username)
    {
        user.username = username;
    }
    public void Password(string password)
    {
        user.password = password;
    }
    public void Login()
    {
        StartCoroutine("LoginE");
    }
    IEnumerator LoginE()
    {
        string jsonString = JsonUtility.ToJson(user);
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            response = JsonUtility.FromJson<ServerResponse>(responseText);
            if (response.message == "Login succesful")
            {
                SceneHelper.LoadScene(1);
            }
            else
            {
                print("Login Failed");
            }
        }
    }
}
[System.Serializable]
public class User
{
    public string username;
    public string password;
}
[System.Serializable]
public class ServerResponse
{
    public string message;
}

