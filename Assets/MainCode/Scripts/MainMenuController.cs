using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    // Use this for initialization
    public AudioClip[] audio_Clip;
    public GameObject mainCamera;
    public Toggle musicToogle, soundToogle;
    public GameObject parentMission;
	private const string isMusic = "MusicSetting", isSound = "SoundSetting";
    void Awake () {
        int x = GetIsMusic();
        int y = GetIsSound();
        if (x == 1)
        {
            mainCamera.GetComponent<AudioSource>().mute = false;
            musicToogle.isOn = true;
        }
        else
        {
            mainCamera.GetComponent<AudioSource>().mute = true;
            musicToogle.isOn = false;
        }

        if (y == 1)
        {
            gameObject.GetComponent<AudioSource>().mute = false;
			soundToogle.isOn = true;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().mute = true;
			soundToogle.isOn = false;
        }
    }
    public Image[] cupImage;
	// Update is called once per frame
	void Start () {
//		int x = PlayerPrefs.GetInt ("MapsUnlocked");
		for (int i = 1; i <= PlayerPrefs.GetInt ("MapsUnlocked"); i++){
			cupImage [i-1].gameObject.SetActive (true);
			Debug.Log ("Cup " + i);
		}
//		Debug.Log (x);
//        if (x > 0) cupImage[0].gameObject.SetActive(true);
//        if (x > 1) cupImage[1].gameObject.SetActive(true);
//        if (x > 2) cupImage[2].gameObject.SetActive(true);
//        if (x > 3) cupImage[3].gameObject.SetActive(true);
//        if (x > 4) cupImage[4].gameObject.SetActive(true);
//        if (x > 5) cupImage[5].gameObject.SetActive(true);
    }

    public void Adventure()
    {
        GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
        LoadSence.instance.FadeIn("Sence Select");

        /*Debug.Log (player.levelGame);
        GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
        int levelplay = PlayerPrefs.GetInt(SelectMapController.LevelMap);
        if (levelplay == 0)
        {
            LoadSence.instance.FadeIn("Game Play");
            player.levelGame = 1;
        }
        else if (levelplay == 1)
        {
            LoadSence.instance.FadeIn("Game Play 1");
            player.levelGame = 2;
        }
        else if (levelplay == 2)
        {
            LoadSence.instance.FadeIn("Game Play 2");
            player.levelGame = 3;
        }
        else if (levelplay == 3)
        {
            LoadSence.instance.FadeIn("Game Play 3");
            player.levelGame = 4;
        }
        else if (levelplay == 4)
        {
            LoadSence.instance.FadeIn("Game Play 4");
            player.levelGame = 5;
        }
        else if (levelplay == 5)
        {
            LoadSence.instance.FadeIn("Game Play 5");
            player.levelGame = 6;
        }*/
    }

    public void Unlimited()
    {
        GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
        LoadSence.instance.FadeIn("Game Play Unlimited");
        player.levelGame = 0;
    }

    public void SelectSence()
    {
        GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
        LoadSence.instance.FadeIn("Sence Select");
        
    }
	public GameObject parentHelp;
	public void OpenHelp(){
		GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
		parentHelp.SetActive (true);
	}
	

	public GameObject parentCredit;
    public void OpenCredit()
    {
		GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
		parentCredit.SetActive (true);

    }

	public void CloseHelp()
	{
		GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
		parentHelp.SetActive (false);

	}

	public void CloseCredit()
	{
		GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
		parentCredit.SetActive (false);

	}

    public GameObject SettingImage;
    public void OpenSetting()
    {
        GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
        SettingImage.SetActive(true);
    }
    public static int GetIsMusic()
    {
        return PlayerPrefs.GetInt(isMusic);
    }
    public static int GetIsSound()
    {
        return PlayerPrefs.GetInt(isSound);
    }
    public void clickMusicToogle()
    {
        if (musicToogle.isOn == true) mainCamera.GetComponent<AudioSource>().mute = false;
        else mainCamera.GetComponent<AudioSource>().mute = true;
    }
    public void clickSoundToogle()
    {
        if (soundToogle.isOn == true) gameObject.GetComponent<AudioSource>().mute = false;
        else gameObject.GetComponent<AudioSource>().mute = true;
    }
    public void CloseSetting()
    {
        int a,b;
        if (musicToogle.isOn == true) a = 1;
        else a = 0;
        if (soundToogle.isOn == true) b = 1;
        else b = 0;
        PlayerPrefs.SetInt(isMusic, a);
        PlayerPrefs.SetInt(isSound, b);
        GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
        SettingImage.SetActive(false);
    }

    public void OpenMission()
    {
        parentMission.SetActive(true);
    }
    public void CloseMission()
    {
        parentMission.SetActive(false);
    }
}
