using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public GameObject gameOverPanel;
	//private int mapsIndex;
    [SerializeField]
    GameObject nextLevel;
    public Text endScoreCP,endScoreGOV,endScoreEnemyCP,endScoreEnemyGOV;
    public Text HighScoreUnlimitedText;
    public Image[] star;
    public Button pauseButton;
    public GameObject pausePanel,questionPanel;

    void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }

        //mapsIndex = PlayerPrefs.GetInt("LevelUnlock");
        //mapsIndex = PlayerPrefs.GetInt(SelectMapController.LevelMap);
    }

    public void PauseGame()
    {
        pauseButton.gameObject.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseButton.gameObject.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        player.isPlay = false;
        questionPanel.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        endScoreGOV.text = "" + ScoreManager.instance.GetScore();
        endScoreEnemyGOV.text = "" + ScoreManager.instance.GetScoreEnemy();
        if (player.levelGame == 0)
        {
            if (ScoreManager.instance.GetScore() > ScoreManager.instance.GetHighScore())
            {
                ScoreManager.instance.SetHighScore(ScoreManager.instance.GetScore());
            }
            HighScoreUnlimitedText.text = "" + ScoreManager.instance.GetHighScore();
            if (ScoreManager.instance.GetScore() >= ScoreManager.instance.GetHighScore() / 3)
            {
                star[0].gameObject.SetActive(true);
            }
            if (ScoreManager.instance.GetScore() >= 2 * ScoreManager.instance.GetHighScore() / 3)
            {
                star[1].gameObject.SetActive(true);
            }
            if (ScoreManager.instance.GetScore() == ScoreManager.instance.GetHighScore())
            {
                star[2].gameObject.SetActive(true);
            }
        }
        else
        {
            if (ScoreManager.instance.GetScore() >= player.scoreNextLevel / 3)
            {
                star[0].gameObject.SetActive(true);
            }
            if (ScoreManager.instance.GetScore() >= 2 * player.scoreNextLevel / 3)
            {
                star[1].gameObject.SetActive(true);
            }
            if (ScoreManager.instance.GetScore() == player.scoreNextLevel)
            {
                star[2].gameObject.SetActive(true);
            }
        }
        gameOverPanel.SetActive(true);
        //        Time.timeScale = 0f;
		Debug.Log ("Over Game");
    }

	public GameObject endGame;
	public void MissionComplete()
	{
        questionPanel.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        //mapsIndex++;
        //PlayerPrefs.SetInt ("LevelUnlock", mapsIndex);
        //PlayerPrefs.SetInt(SelectMapController.LevelMap,mapsIndex);
        endScoreCP.text = "" + ScoreManager.instance.GetScore();
        endScoreEnemyCP.text = "" + ScoreManager.instance.GetScoreEnemy();
        //star[3].gameObject.SetActive(true);
        //star[4].gameObject.SetActive(true);
        //star[5].gameObject.SetActive(true);
		/*if(player.levelGame == 6)
		{
			endGame.SetActive (true);
            PlayerPrefs.SetInt(SelectMapController.LevelMap,6);
            player.levelGame = 1;
			return;
		}*/
        nextLevel.SetActive(true);
		Debug.Log ("next Game");

		//        Time.timeScale = 0f;
	}

    public void OpenWinGame()
    {

        endGame.SetActive(true);
		PlayerPrefs.SetInt("MapsUnlocked", 6);
        player.levelGame = 1;
    }

    public void QuestionGame()
    {
        player.isPlay = false;
        pauseButton.gameObject.SetActive(false);
        questionPanel.SetActive(true);
    }

    public GameObject parentCup;
    public void OpenComplete()
    {
        parentCup.SetActive(false);
        MissionComplete();
    }

	public void ShowLeaderboard(){
		AdsController.instance.ShowLeaderBoardUI ();
	}

}
