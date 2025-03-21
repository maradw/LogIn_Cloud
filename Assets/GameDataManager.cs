using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameDataManager : MonoBehaviour
{
    private string url = "http://localhost/insert_users.php";
    [SerializeField] private NewUser user;
    [SerializeField] private ServerResponse response;
    public void NewUsername(string username_n)
    {
        user.user_name = username_n;
    }
    public void NewPassword(string password_n)
    {
        user.user_password = password_n;
    }
    public void NewState (string state_n)
    {
        user.user_state = state_n;
    }
    public void InsertUserN()
    {
        StartCoroutine("NewUserA");
    }

    private IEnumerator NewUserA()
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
            print("Error al insertar usuario.");
        }
        else
        {
            string responseText = request.downloadHandler.text;
            ServerResponse response = JsonUtility.FromJson<ServerResponse>(responseText);

            if (response.message == "New user inserted successfully")
            {
                print("new user registered :)");
            }
            else
            {
                print("Error en la inserción del usuario: " + response.message);
            }
        }
    }
    [System.Serializable]
    public class NewUser
    {
        public string user_name;
        public string user_password;
        public string user_state;
    }
    public class ServerResponse
    {
        public string message;
    }

}
