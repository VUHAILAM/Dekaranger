using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSence : MonoBehaviour {

    public static LoadSence instance;
    public Image imgLoading;
    public Sprite[] spriteLoadSence;
	public Image fillImg;

    void Awake()
    {
        MakeASingleInstance();
    }

    void MakeASingleInstance()
    {
        if (instance != null)         
        {
            Destroy(gameObject);            
        }
        else                  
        {
            instance = this;             
            DontDestroyOnLoad(gameObject);          
        }
    }

    IEnumerator FadeInAnimate(string levelName)
    {
        imgLoading.sprite = spriteLoadSence[Random.Range(0, spriteLoadSence.Length)];
		imgLoading.transform.parent.gameObject.SetActive(true);
		fillImg.fillAmount = 0;        

		AsyncOperation asyncP = SceneManager.LoadSceneAsync (levelName);
		asyncP.allowSceneActivation = false;

//		while(asyncP.progress < 0.9f){
//			fillImg.fillAmount = asyncP.progress;
//			yield return new WaitForSeconds (0.5f);
//		}

		float percent = asyncP.progress;
		percent = 0;
		while (percent <= 1) {
			percent += 0.005f;
			fillImg.fillAmount = percent;
			yield return new WaitForSeconds (0.01f);
		}
		FadeOut();
		asyncP.allowSceneActivation = true;
		yield break;

        
    }

    IEnumerator FadeOutAnimate()
    {
        yield return StartCoroutine(MyCoroutine.WaitForRealForSeconds(0f));
		imgLoading.transform.parent.gameObject.SetActive(false);
    }

    public void FadeIn(string levelName)
    {
        GameObject mainCameraSound = GameObject.Find("Main Camera");
        mainCameraSound.GetComponent<AudioSource>().Stop();
        StartCoroutine(FadeInAnimate(levelName));        
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutAnimate());            
    }
}
