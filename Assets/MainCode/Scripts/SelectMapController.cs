using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectMapController1 : MonoBehaviour
{

    public GameObject Map1, Map2, Map3;
    public GameObject img1, img2, img3;
    private Vector3 posMap1, posMap2, posMap3;
    //public Text levelNameText;
    public Sprite[] mapSprite;
    public Sprite[] imgSprite;
    public Sprite lockMapSprite,okMapSprite;
    public static string LevelMap = "";
    public Button selectButton;
    public AudioClip[] audio_Clip;
    public GameObject mainCamera;
    //private int 

    // Use this for initialization
    void Start()
    {
        int x = MainMenuController.GetIsMusic();
        int y = MainMenuController.GetIsSound();
        if (x == 1)
        {
            mainCamera.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            mainCamera.GetComponent<AudioSource>().mute = true;
        }

        if (y == 1)
        {
            gameObject.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().mute = true;
        }

        posMap1 = Map1.transform.position;
        posMap2 = Map2.transform.position;
        posMap3 = Map3.transform.position;
        //PlayerPrefs.SetInt(LevelMap, 0);
//        player.levelGame = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if((player.levelGame==2&&PlayerPrefs.GetInt(LevelMap)<1)|| (player.levelGame == 3 && PlayerPrefs.GetInt(LevelMap) < 2)|| (player.levelGame == 4 && PlayerPrefs.GetInt(LevelMap) < 3)|| (player.levelGame == 5 && PlayerPrefs.GetInt(LevelMap) < 4)|| (player.levelGame == 6 && PlayerPrefs.GetInt(LevelMap) < 5))
        {
            selectButton.GetComponent<Image>().sprite = lockMapSprite;
        }
        else
        {
            selectButton.GetComponent<Image>().sprite = okMapSprite;
        }

    }

    public void SelectRight()
    {
        GetComponent<AudioSource>().PlayOneShot(audio_Clip[1]);
        if (Map2.transform.position == posMap2)
        {
            iTween.MoveTo(Map2, posMap1, 0.5f);
            Map2.GetComponent<SpriteRenderer>().enabled = false;
            img2.GetComponent<SpriteRenderer>().enabled = false;
            Map3.GetComponent<SpriteRenderer>().enabled = true;
            img3.GetComponent<SpriteRenderer>().enabled = true;
            if (player.levelGame != 6)
                img3.GetComponent<SpriteRenderer>().sprite = imgSprite[player.levelGame];
            else img3.GetComponent<SpriteRenderer>().sprite = imgSprite[0];
            iTween.MoveTo(Map3, posMap2, 0.5f);
            Map1.transform.position = posMap3;
            player.levelGame++;
            if (player.levelGame > 6)
            {
                player.levelGame = 1;
            }
            if (player.levelGame != 6)
                Map1.GetComponent<SpriteRenderer>().sprite = mapSprite[player.levelGame];
            else
                Map1.GetComponent<SpriteRenderer>().sprite = mapSprite[0];
            //levelNameText.text = "" + player.levelGame;
        }
        else if (Map1.transform.position == posMap2)
        {
            iTween.MoveTo(Map1, posMap1, 0.5f);
            Map1.GetComponent<SpriteRenderer>().enabled = false;
            img1.GetComponent<SpriteRenderer>().enabled = false;
            Map2.GetComponent<SpriteRenderer>().enabled = true;
            img2.GetComponent<SpriteRenderer>().enabled = true;
            if (player.levelGame != 6)
                img2.GetComponent<SpriteRenderer>().sprite = imgSprite[player.levelGame];
            else img2.GetComponent<SpriteRenderer>().sprite = imgSprite[0];
            iTween.MoveTo(Map2, posMap2, 0.5f);
            Map3.transform.position = posMap3;
            player.levelGame++;
            if (player.levelGame > 6)
            {
                player.levelGame = 1;
            }
            if (player.levelGame != 6)
                Map3.GetComponent<SpriteRenderer>().sprite = mapSprite[player.levelGame];
            else
                Map3.GetComponent<SpriteRenderer>().sprite = mapSprite[0];
            //levelNameText.text = "" + player.levelGame;

        }
        else if (Map3.transform.position == posMap2)
        {
            iTween.MoveTo(Map3, posMap1, 0.5f);
            Map3.GetComponent<SpriteRenderer>().enabled = false;
            img3.GetComponent<SpriteRenderer>().enabled = false;
            Map1.GetComponent<SpriteRenderer>().enabled = true;
            img1.GetComponent<SpriteRenderer>().enabled = true;
            if (player.levelGame != 6)
                img1.GetComponent<SpriteRenderer>().sprite = imgSprite[player.levelGame];
            else img1.GetComponent<SpriteRenderer>().sprite = imgSprite[0];
            iTween.MoveTo(Map1, posMap2, 0.5f);
            Map2.transform.position = posMap3;
            player.levelGame++;
            if (player.levelGame > 6)
            {
                player.levelGame = 1;
            }
            if (player.levelGame != 6)
                Map2.GetComponent<SpriteRenderer>().sprite = mapSprite[player.levelGame];
            else
                Map2.GetComponent<SpriteRenderer>().sprite = mapSprite[0];
            //levelNameText.text = "" + player.levelGame;

        }
    }

    public void SelectLeft()
    {
        GetComponent<AudioSource>().PlayOneShot(audio_Clip[1]);
        if (Map2.transform.position == posMap2)
        {
            iTween.MoveTo(Map2, posMap3, 0.5f);
            Map2.GetComponent<SpriteRenderer>().enabled = false;
            img2.GetComponent<SpriteRenderer>().enabled = false;
            Map1.GetComponent<SpriteRenderer>().enabled = true;
            img1.GetComponent<SpriteRenderer>().enabled = true;
            if (player.levelGame != 1)
                img1.GetComponent<SpriteRenderer>().sprite = imgSprite[player.levelGame-2];
            else img1.GetComponent<SpriteRenderer>().sprite = imgSprite[5];
            iTween.MoveTo(Map1, posMap2, 0.5f);
            Map3.transform.position = posMap1;
            player.levelGame--;
            if (player.levelGame < 1)
                player.levelGame = 6;
            if (player.levelGame != 1)
                Map3.GetComponent<SpriteRenderer>().sprite = mapSprite[player.levelGame - 2];
            else
                Map3.GetComponent<SpriteRenderer>().sprite = mapSprite[5];
            //levelNameText.text = "" + player.levelGame;
        }
        else if (Map1.transform.position == posMap2)
        {
            iTween.MoveTo(Map1, posMap3, 0.5f);
            Map1.GetComponent<SpriteRenderer>().enabled = false;
            img1.GetComponent<SpriteRenderer>().enabled = false;
            Map3.GetComponent<SpriteRenderer>().enabled = true;
            img3.GetComponent<SpriteRenderer>().enabled = true;
            if (player.levelGame != 1)
                img3.GetComponent<SpriteRenderer>().sprite = imgSprite[player.levelGame - 2];
            else img3.GetComponent<SpriteRenderer>().sprite = imgSprite[5];
            iTween.MoveTo(Map3, posMap2, 0.5f);
            Map2.transform.position = posMap1;
            player.levelGame--;
            if (player.levelGame < 1)
                player.levelGame = 6;
            if (player.levelGame != 1)
                Map2.GetComponent<SpriteRenderer>().sprite = mapSprite[player.levelGame - 2];
            else
                Map2.GetComponent<SpriteRenderer>().sprite = mapSprite[5];
            //levelNameText.text = "" + player.levelGame;

        }
        else if (Map3.transform.position == posMap2)
        {
            iTween.MoveTo(Map3, posMap3, 0.5f);
            Map3.GetComponent<SpriteRenderer>().enabled = false;
            img3.GetComponent<SpriteRenderer>().enabled = false;
            Map2.GetComponent<SpriteRenderer>().enabled = true;
            img2.GetComponent<SpriteRenderer>().enabled = true;
            if (player.levelGame != 1)
                img2.GetComponent<SpriteRenderer>().sprite = imgSprite[player.levelGame - 2];
            else img2.GetComponent<SpriteRenderer>().sprite = imgSprite[5];
            iTween.MoveTo(Map2, posMap2, 0.5f);
            Map1.transform.position = posMap1;
            player.levelGame--;
            if (player.levelGame < 1)
                player.levelGame = 6;
            if (player.levelGame != 1)
               Map1.GetComponent<SpriteRenderer>().sprite = mapSprite[player.levelGame -2];
            else
               Map1.GetComponent<SpriteRenderer>().sprite = mapSprite[5];

            //levelNameText.text = "" + player.levelGame;

        }
    }

    public void Select()
    {
        GetComponent<AudioSource>().PlayOneShot(audio_Clip[1]);
        if (player.levelGame == 1)
        {
//            Application.LoadLevel("Game Play");
            LoadSence.instance.FadeIn("Game Play");
        }
        else if (player.levelGame == 2&& PlayerPrefs.GetInt(LevelMap)>0)
        {
            //Application.LoadLevel("Game Play 1");
            LoadSence.instance.FadeIn("Game Play 1");
        }
        else if (player.levelGame == 3 && PlayerPrefs.GetInt(LevelMap) >1)
        {
            //Application.LoadLevel("Game Play 2");
            LoadSence.instance.FadeIn("Game Play 2");
        }
        else if (player.levelGame == 4 && PlayerPrefs.GetInt(LevelMap) >2)
        {
            //Application.LoadLevel("Game Play 3");
            LoadSence.instance.FadeIn("Game Play 3");
        }
        else if (player.levelGame == 5 && PlayerPrefs.GetInt(LevelMap) >3)
        {
            //Application.LoadLevel("Game Play 4");
            LoadSence.instance.FadeIn("Game Play 4");
        }
        else if (player.levelGame == 6 && PlayerPrefs.GetInt(LevelMap) >4)
        {
            //Application.LoadLevel("Game Play 5");
            LoadSence.instance.FadeIn("Game Play 5");
        }
    }
}
