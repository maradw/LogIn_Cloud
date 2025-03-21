using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GameScoreManager : MonoBehaviour
{
    private string scoreUrl = "http://localhost/insert_zombie.php"; // URL del PHP para insertar puntajes

    // Clase que representa el puntaje del juego
    [System.Serializable]
    public class GameScore
    {
        public int user_id; // ID del usuario
        public int zombie_score; // Puntaje o tiempo del juego (ejemplo: mejor tiempo en el juego de cartas)
    }

    [SerializeField] private GameScore gameScore;

    public void SetUserID(int id)
    {
        gameScore.user_id = 1;
    }

    public void SetBestCardTime(int time)
    {
        gameScore.zombie_score = 58;
    }

    public void InsertScore()
    {
        StartCoroutine(InsertScoreCoroutine());
    }

    private IEnumerator InsertScoreCoroutine()
    {
        string jsonString = JsonUtility.ToJson(gameScore);
        UnityWebRequest request = new UnityWebRequest(scoreUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al insertar el mejor tiempo: " + request.error);
        }
        else
        {

            string responseText = request.downloadHandler.text;
            ServerResponseCard response = JsonUtility.FromJson<ServerResponseCard>(responseText);

            if (response.message == "Zombie score inserted successfully")
            {
                Debug.Log("puntaje zombie agregado");
            }
            else
            {
                Debug.LogError("Error en la inserción del puntaje: " + response.message);
            }
        }
    }
}

[System.Serializable]
public class ServerResponseCard
{
    public string message;
}
