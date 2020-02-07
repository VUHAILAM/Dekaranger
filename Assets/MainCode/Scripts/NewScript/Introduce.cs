using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Introduce : MonoBehaviour {
	public GameObject parentIntro;
	public Image blackImg;
	public GameObject skipBtn;
	// Use this for initialization
	void Start () {
        //Debug.Log(PlayerPrefs.GetInt("introduce"));

        if (PlayerPrefs.GetInt ("introduce") != 1) {
			PlayerPrefs.SetInt ("introduce", 1);
			parentIntro.SetActive (true);
			StartCoroutine (FadeIn ());
			PlayerPrefs.SetInt ("MusicSetting", 1);
			PlayerPrefs.SetInt ("SoundSetting", 1);
		}
       // PlayerPrefs.SetInt("introduce", 0);
//        		PlayerPrefs.DeleteAll ();
    }
	
	public void ClickSkipButton(){
		StartCoroutine (FadeOut ());
	}

	public void ShowIntroduce(){
		parentIntro.SetActive (true);
		StartCoroutine (FadeIn ());
	}

	IEnumerator FadeIn(){
		float alphaImg = blackImg.color.a;
		while(blackImg.color.a > 0){
			alphaImg -= 0.05f;
			blackImg.color = new Color(0,0,0,alphaImg);
			yield return new WaitForEndOfFrame ();
		}
		skipBtn.SetActive (true);
		yield break;
	}

	IEnumerator FadeOut(){
		skipBtn.SetActive (false);
		float alphaImg = blackImg.color.a;
		while(blackImg.color.a < 1){
			alphaImg += 0.05f;
			blackImg.color = new Color(0,0,0,alphaImg);
			yield return new WaitForEndOfFrame ();
		}
		Debug.Log (SceneManager.GetActiveScene ().name);
		parentIntro.SetActive (false);
		yield break;
	}
}
