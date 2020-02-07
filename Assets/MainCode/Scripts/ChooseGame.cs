using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ChooseGame : MonoBehaviour {
	
	[SerializeField] Rigidbody2D parentMenu;
//	[SerializeField] GameObject uiMenu;
	Transform[] listChild;
	int numChild;

	int mapUnlocked;


	public Sprite unlocked,locked;

	void Start () {
		SetMusic ();
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;

		numChild = parentMenu.transform.childCount;
		listChild = new Transform[numChild];

		mapUnlocked = PlayerPrefs.GetInt ("MapsUnlocked");

		for (int i = 0; i < numChild; i++){
			listChild [i] = parentMenu.transform.GetChild (i);
			if(int.Parse(listChild[i].name) <= mapUnlocked)
				parentMenu.transform.GetChild (i).GetChild (1).GetComponent<SpriteRenderer> ().sprite = unlocked;
			else
				parentMenu.transform.GetChild (i).GetChild (1).GetComponent<SpriteRenderer> ().sprite = locked;

		}
	}

	void SetMusic(){
		int x = MainMenuController.GetIsMusic();
		int y = MainMenuController.GetIsSound();
		if (x == 1)
		{
			Camera.main.gameObject.GetComponent<AudioSource>().mute = false;
		}
		else
		{
			Camera.main.gameObject.GetComponent<AudioSource>().mute = true;
		}

		if (y == 1)
		{
			gameObject.GetComponent<AudioSource>().mute = false;
		}
		else
		{
			gameObject.GetComponent<AudioSource>().mute = true;
		}
	}

	bool isMove;
	[HideInInspector]public float posOld, posCrt,offSetX,posBegin;
	int touchCount;
	void Update () {		
		CheckChild ();
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);

		if (hit.collider != null) {
			GameObject bc;
			bc = hit.collider.gameObject;

			if (Input.GetMouseButtonDown (0)) {
				posBegin = mousePosition.x;
				isMove= false;
			} else if (Input.GetMouseButton (0)) {
				if (Mathf.Abs(mousePosition.x - posBegin) > 0.25f && !isMove) {
					isMove = true;
				}
			} else if (Input.GetMouseButtonUp (0)) {
				if(!isMove && bc.tag == "ButtonMaps"){
					if(int.Parse(bc.name) <= mapUnlocked){
						string sceneName;
						if (bc.name == "0") {
							sceneName = "Game Play";
						} else
							sceneName = "Game Play " + bc.name;
						GetComponent<AudioSource> ().Play ();
						player.levelGame = int.Parse (bc.name) + 1;
						LoadSence.instance.FadeIn(sceneName);

					}
				}
			}
		}

		#if UNITY_ANDROID
		if(touchCount != Input.touchCount){
			touchCount = Input.touchCount;
			offSetX = mousePosition.x - parentMenu.transform.position.x;
		}
		#endif

		if (Input.GetMouseButtonDown (0)){
			iTween.Stop(parentMenu.gameObject);
			StopAllCoroutines ();
			parentMenu.velocity = Vector2.zero;

			posOld = parentMenu.transform.position.x;

			offSetX = mousePosition.x - parentMenu.transform.position.x;
		}
		if(Input.GetMouseButton(0)){
			
			parentMenu.transform.position = new Vector3 (mousePosition.x - offSetX, parentMenu.transform.position.y, parentMenu.transform.position.z);
			posCrt = mousePosition.x;
			posOld = posCrt;
		}
		if(Input.GetMouseButtonUp(0)){
			
			float offsetXXX = parentMenu.transform.position.x % distance;
			float posNew;
			if (Mathf.Abs (offsetXXX) > (distance / 2)) {
				if (offsetXXX > 0)
					posNew = distance * (int)(parentMenu.transform.position.x / distance) + distance;
				else
					posNew = distance * (int)(parentMenu.transform.position.x / distance) - distance;				
			}else {
				posNew = distance * (int)(parentMenu.transform.position.x / distance);
			}

			posCrt = mousePosition.x;
			float thoigian = Time.deltaTime;
			float forceX = (posCrt - posOld) / (2*thoigian);
			if (forceX > 60)
				forceX = 60;
			float quangduong = thoigian* 4 * forceX;

			if (Mathf.Abs (quangduong) >= 1)
				posss = (int)(quangduong / 1f) * distance;
			else if (Mathf.Abs (quangduong) >= 0.2f)
				posss = (quangduong / Mathf.Abs (quangduong)) * distance;
			else
				posss = 0;
			posss += posNew;
			iTween.MoveTo (parentMenu.gameObject, iTween.Hash ("x", posss, "time",thoigian*100, "easytype", iTween.EaseType.easeOutSine));
		}
	}

	float posss;


	IEnumerator GameLoading(){
//		uiMenu.SetActive (false);
		parentMenu.gameObject.SetActive (false);
		AsyncOperation asyncP = SceneManager.LoadSceneAsync ("PlayGame");
		asyncP.allowSceneActivation = false;
		while(asyncP.progress < 0.9f){
//			Debug.Log ("log...");
//			Debug.Log (asyncP.progress + "%");
			yield return new WaitForFixedUpdate ();
		}

		float percent = asyncP.progress;
		while (percent <= 1) {
			percent += 0.01f;
//			Debug.Log (percent + "%");
			yield return new WaitForSeconds (Time.deltaTime);
		}
		asyncP.allowSceneActivation = true;
		yield break;
	}

	public float distance;
	void CheckChild(){
		for (int i = 0; i < numChild; i++){
			listChild [i].SetParent (parentMenu.transform);
			Vector3 pos = listChild [i].position;
			float scale = 1 - Mathf.Abs (pos.x) / ((numChild - 1) * distance / 2f);
			if (pos.x < -distance*3f){
				pos.x += distance * numChild;
			}else if (pos.x > distance*3f){
				pos.x -= distance * numChild;
			}
			listChild [i].position = pos;
			listChild [i].localScale = new Vector3 (scale, scale, 1);
		}
	}
}
