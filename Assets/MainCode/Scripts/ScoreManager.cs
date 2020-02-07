using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public Text scoreKmText,scoreEnemyText;
    private const string HIGH_SCORE = "High Score";
    public int score = 0;
    public int scoreEnemy=0;

    void Awake()
    {
        MakeInstance();
        //SetHighScore(0);
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        scoreKmText.text = "" + score;
        scoreEnemyText.text = "" + scoreEnemy;
    }

    public void SetScore(int x)
    {
        score = x;
    }
    public int GetScore()
    {
        return score;
    }

    public void SetScoreEnemy(int x)
    {
        scoreEnemy = x;
    }
    public int GetScoreEnemy()
    {
        return scoreEnemy;
    }

    public void SetHighScore(int score)
    {
		#if UNITY_ANDROID || UNITY_IOS
		AdsController.instance.UpdateScoreToLeaderboard (score);
		#endif
        PlayerPrefs.SetInt(HIGH_SCORE, score);          
    }
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);       
    }
}
