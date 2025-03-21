using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManagerCat : MonoBehaviour
{
    [HeaderAttribute(" Score variables")]
    int score;
    public TextMeshProUGUI scoreText;

   
    void OnEnable()
    {
        PlayerControlCat.OnCollisionGhost += CurrentScore;
    }
    private void OnDisable()
    {
        PlayerControlCat.OnCollisionGhost -= CurrentScore;
    }
    private void Update()
    {
        scoreText.text = "Score: " + score;
    }
    public void CurrentScore(int numb)
    {
        score = score + numb;
  

    }
    void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
