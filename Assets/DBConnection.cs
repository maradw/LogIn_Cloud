using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class DBConn : MonoBehaviour
{
    private string url = "http://localhost/UserLogin.php";
    [SerializeField] private User user;
    [SerializeField] private ServerResponse response;

    //
    private GameScoreManager gameScoreManager;

   

    //
    public static int user_id { get; private set; }
    public void Username(string username)
    {
        user.user_name = username;
    }
    public void Password(string password)
    {
        user.user_password = password;
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
            print("terrible");
        }
        else
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Server response: " + responseText);
            response = JsonUtility.FromJson<ServerResponse>(responseText);
            if (response.message == "Login successful")
            {
                user_id = response.user_id;
                print("Login successful, " + user.user_name + " user: " + user_id);
                SceneHelper.LoadScene(1);
            }
            else
            {
                print("Login failed");
            }
        }
    }
    private void Start()
    {
        gameScoreManager = GetComponent<GameScoreManager>();
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SendTime(int userID, int bestTime)
    {
        gameScoreManager.SetUserID(userID);
        gameScoreManager.SetBestCardTime(bestTime);
        gameScoreManager.InsertScore();
    }
}
[System.Serializable]
public class User
{
    public string user_name;
    public string user_password;
}
public class ServerResponse
{
    public string message;
    public int user_id;
}

