using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_Platform : MonoBehaviour
{
    [SerializeField] int score;

    [SerializeField]private ProgressData progressData;


    [SerializeField] private TextMeshProUGUI scoreText;
    public ProgressData progressData11;
    public float playerPosX, playerPosY, playerPosZ;
    public PlayerController_Plat playerPosition;
    public void SaveProgress()
    {
        string json = JsonUtility.ToJson(progressData);
        SaveData.Save("progressdata2.json", json);
        print("data saved");
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Save Progress"))
        {
            SaveProgress();
        }
    }

    public void LoadProgress()
    {
        progressData = JsonUtility.FromJson<ProgressData>(SaveData.Load("progressdata2.json"));
    }

    public void SaveGameProgress()
    {
        playerPosX = playerPosition.transform.position.x;
        playerPosY = playerPosition.transform.position.y;
        playerPosZ = playerPosition.transform.position.z;
        progressData11.saveData(score, playerPosX, playerPosY, playerPosZ);
    }

    void OnEnable()
    {
        PlayerController_Plat.OnCollisionItem += CurrentScore;
    }
    private void OnDisable()
    {
        PlayerController_Plat.OnCollisionItem -= CurrentScore;
    }
    private void Update()
    {
        scoreText.text = "Score: " + score;

        if(score >= 100)
        {
            SaveGameProgress();
        }
    }
    public void CurrentScore(int numb)
    {
        score = score + numb;


    }
}
