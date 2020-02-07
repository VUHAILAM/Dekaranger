using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuizController : MonoBehaviour {

    public GameObject parentCup;
//	public GameObject parentQues;
    public AudioClip[] audio_clip;
    private int mapsIndex;

    // Use this for initialization
    void Start () {
		CreateQuestion ();
		Invoke ("DisplayQuestion", 0);
		mapsIndex = PlayerPrefs.GetInt("MapsUnlocked");
    }

	public static List<string> listQuestion;// type of "answer"-"question"
	int answer;//1 = true; 0 = false
	int indexQuestion;
	bool questionReady;

	public Text textQuestion;//UI text

	//Create list question.
	public void CreateQuestion(){
		TextAsset txt = (TextAsset)Resources.Load ("question", typeof(TextAsset));
		string question = txt.text;
		listQuestion = new List<string> ();
		for(int i = 0;i < question.Split('\n').Length; i++){
			listQuestion.Add (question.Split ('\n') [i]);
		}
		//Debug.Log (listQuestion.Count);
	}

	//Call 1 random question.
	public void DisplayQuestion(){
		if (listQuestion.Count == 1)
			CreateQuestion ();
		indexQuestion = Random.Range (0, listQuestion.Count);
		textQuestion.text= listQuestion [indexQuestion].Split('-')[1];
		answer = int.Parse (listQuestion [indexQuestion].Split ('-') [0]);
		questionReady = true;
	}

	//If click button True.
	public void ClickButtonTrue(){
		AnswerControl (1);
	}

	//If click button False.
	public void ClickButtonFalse(){
		AnswerControl (0);
	}

	//Check Answer.
	void AnswerControl(int playerAns){
		if (!questionReady)
			return;
		
		questionReady = true;

		if(playerAns == answer){
            //Debug.Log("You're Right");
            GetComponent<AudioSource>().PlayOneShot(audio_clip[0]);
            if (player.levelGame > mapsIndex)
            {
                parentCup.SetActive(true);
//				parentQues.SetActive (false);
				textQuestion.transform.parent.gameObject.SetActive (false);
            }
            else GameController.instance.MissionComplete();
            if (player.levelGame > mapsIndex)
            {
                mapsIndex++;
				PlayerPrefs.SetInt("MapsUnlocked", mapsIndex);
            }
            //GameController.instance.MissionComplete();
			listQuestion.RemoveAt (indexQuestion);//If true next level and remove this question from listQuestion.
			//Level++
			DisplayQuestion ();
		}else{
            //Don't load next level and keep listQuestion.
            GetComponent<AudioSource>().PlayOneShot(audio_clip[1]);
            GameController.instance.GameOver();
			//Debug.Log ("You're Wrong");
		}
	}
}
