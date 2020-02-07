using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour {

    //private int mapsIndex;

    // Use this for initialization
    void Start () {
        //mapsIndex = PlayerPrefs.GetInt(SelectMapController.LevelMap);
    }

	//Load lai scene vua choi
	public void ReloadLevel(){
        //SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
        //Application.LoadLevel(Application.loadedLevel);
        if (player.levelGame == 0) LoadSence.instance.FadeIn("Game Play Unlimited");
        else if (player.levelGame==1) LoadSence.instance.FadeIn("Game Play");
        else if (player.levelGame == 2) LoadSence.instance.FadeIn("Game Play 1");
        else if (player.levelGame == 3) LoadSence.instance.FadeIn("Game Play 2");
        else if (player.levelGame == 4) LoadSence.instance.FadeIn("Game Play 3");
        else if (player.levelGame == 5) LoadSence.instance.FadeIn("Game Play 4");
        else if (player.levelGame == 6) LoadSence.instance.FadeIn("Game Play 5");

//        Time.timeScale = 1f;
	}

    public void LoadNextLevel()
    {
        player.levelGame++;
        //Debug.Log(player.levelGame);
        if (player.levelGame == 2)
        {
            //mapsIndex = 1;
            //Application.LoadLevel("Game Play 1");
            LoadSence.instance.FadeIn("Game Play 1");
        }
        else if (player.levelGame == 3)
        {
            // mapsIndex = 2;
            //Application.LoadLevel("Game Play 2");
            LoadSence.instance.FadeIn("Game Play 2");
        }
        else if (player.levelGame == 4)
        {
            // mapsIndex = 3;
            //Application.LoadLevel("Game Play 3");
            LoadSence.instance.FadeIn("Game Play 3");
        }
        else if (player.levelGame == 5)
        {
            //mapsIndex = 4;
            //Application.LoadLevel("Game Play 4");
            LoadSence.instance.FadeIn("Game Play 4");
        }
        else if (player.levelGame == 6)
        {
            // mapsIndex = 5;
            //Application.LoadLevel("Game Play 5");
            LoadSence.instance.FadeIn("Game Play 5");
        }
        else GameController.instance.OpenWinGame();
        /*if (player.levelGame > mapsIndex + 1)
        {
            mapsIndex++;
            PlayerPrefs.SetInt(SelectMapController.LevelMap, mapsIndex);
        }*/
    }

    public void HomeButton()
    {
        //Application.LoadLevel("Main Menu");
        LoadSence.instance.FadeIn("Main Menu");
    }
}
