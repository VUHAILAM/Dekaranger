using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour {
	public static int levelGame=1;//level map
    public static int scoreNextLevel;
    public float speed;
    public float moveSpeed;
    //public Sprite[] ImgPlayer;
    private SpriteRenderer spriteRen;
    private int ktImg = 0,ready=4;
    public GameObject bulletMayBay,bulletXeHong,bulletXeVang;
    public Button fireButton;
    private float fireTime,posScore;
    public static bool isPlay = false;
    public Text readyText;
    private bool levelComplite = false;
    public AudioClip[] audio_Clip;
    public GameObject endLevel,mainCamera;
    public Sprite[] ImgBienHinh;
    public GameObject shadowPlayer;
    public Sprite[] ImgShadowPlayer;
    public GameObject effectNoXe;
    public Slider sliderItem1,sliderItem2,sliderItem3;
    public Image itemImage;
    public Sprite[] spriteItem;
    public Image iconImage1, iconImage2, iconImage3;
    public Sprite[] iconItemSprite;
    private int countItem;
    private bool isItemToNho, isItemNhanhCham, isItemBienHinh;

	bool isFire;
	int countTap;
    //public Sprite[] imgNoXeEnemy;

	// Use this for initialization
	void Start () {
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

        isItemToNho = false;
        isItemNhanhCham = false;
        isItemBienHinh = false;
        countItem = 0;
        spriteRen = gameObject.GetComponent<SpriteRenderer>();
		StartCoroutine (CountDown ());//start countdown.
        posScore = transform.position.y;
        //PlayerPrefs.SetInt(SelectMapController.LevelMap, 6);
        if (levelGame == 1) scoreNextLevel = 200;
        else if (levelGame == 2) scoreNextLevel = 300;
        else if (levelGame == 3) scoreNextLevel = 400;
        else if (levelGame == 4) scoreNextLevel = 500;
        else if (levelGame == 5) scoreNextLevel = 650;
        else if (levelGame == 6) scoreNextLevel = 750;
        sliderItem1.maxValue = 5;
        sliderItem1.minValue = 0;
        sliderItem2.maxValue = 5;
        sliderItem2.minValue = 0;
        sliderItem3.maxValue = 5;
        sliderItem3.minValue = 0;
    }

	/// <summary>
	/// COuntDown when start the game.
	/// </summary>
	/// <returns>play game.</returns>
	IEnumerator CountDown(){
		isPlay = false;
		if(Time.timeScale != 1)
			Time.timeScale = 1;
		readyText.text = "READY";
		for(int i = 3; i >= 0 ; i--){
            yield return new WaitForSeconds(1);
            readyText.text = "" + i;
            readyText.fontSize = 250;
		}

		readyText.gameObject.SetActive (false);
		isPlay = true;
		yield break;
	}
    
    private bool moveLeft=false,moveRight=false;

    public void SetMoveLeft(bool set)
    {
        moveLeft = set;
    }

    public void SetMoveRight(bool set)
    {
        moveRight = set;
    }

    void Update()
    {
        if (isPlay)
        {
            float v = moveSpeed * Time.deltaTime;
            float h =  speed * Time.deltaTime;
            if (moveLeft)
            {
                transform.Translate(-h, v, 0);
            }
            else if(moveRight)
            {
                transform.Translate(h, v, 0);
            }
            else
            {
                transform.Translate(0, v, 0);
            }
            int score = (int)(transform.position.y - posScore);
            ScoreManager.instance.SetScore(score);
            MovePlayer();

			//new
			if(isFire && Input.GetMouseButtonDown(0)){
				countTap++;
				if (countTap == 2){
					SpawnBullet ();
					countTap = 0;
				}			
			}
        }

        if (levelComplite == false&& ScoreManager.instance.GetScore() >= scoreNextLevel && levelGame!=0)
        { 
                //Debug.Log("Mission Complete!");
                //Goi ra bang next level
                isPlay = false;
            //GameController.instance.MissionComplete();
            levelComplite = true;
            //levelGame++;
            mainCamera.GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(audio_Clip[5]);
        }

        if(ScoreManager.instance.GetScore() >= scoreNextLevel && levelGame!=0)
        {
            Vector3 temp = endLevel.transform.position;
            temp.x = transform.position.x;
            endLevel.transform.position = temp;
            endLevel.SetActive(true);
            transform.Translate(0, 10*moveSpeed*Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag== "nvtolen" && isItemToNho==false&&isPlay==true)
        {
            isItemToNho = true;
            countItem++;
            ktImg = 5;
            GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
            if (countItem == 1)
            {
                StartCoroutine(SliderThoiGianItem(1,1));
                iconImage1.sprite = iconItemSprite[0];
            }
            else if (countItem == 2)
            {
                StartCoroutine(SliderThoiGianItem(1, 2));
                iconImage2.sprite = iconItemSprite[0];
            }
            else if (countItem == 3)
            {
                StartCoroutine(SliderThoiGianItem(1, 3));
                iconImage3.sprite = iconItemSprite[0];
            }

            StartCoroutine(HienImageItem(spriteItem[0]));
            transform.localScale = new Vector3(0.3375f, 0.4125f,1f);
            if (ktImg == 1) StartCoroutine(BienHinh(2, 1));
            else if (ktImg == 2) StartCoroutine(BienHinh(3, 2));
            else if (ktImg == 3) StartCoroutine(BienHinh(4, 3));
            else if (ktImg == 4) StartCoroutine(BienHinh(5, 4));
            //Invoke("ResetImg", 10f);
        }
        if (other.tag == "nvnhoxuong" && isItemToNho==false && isPlay == true)
        {
            isItemToNho = true;
            countItem++;
            ktImg = 6;
            GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
            if (countItem == 1)
            {
                StartCoroutine(SliderThoiGianItem(1, 1));
                iconImage1.sprite = iconItemSprite[1];
            }
            else if (countItem == 2)
            {
                StartCoroutine(SliderThoiGianItem(1, 2));
                iconImage2.sprite = iconItemSprite[1];
            }
            else if (countItem == 3)
            {
                StartCoroutine(SliderThoiGianItem(1, 3));
                iconImage3.sprite = iconItemSprite[1];
            }
            StartCoroutine(HienImageItem(spriteItem[1]));
            transform.localScale = new Vector3(0.15f,0.184f,1f);
            if (ktImg == 1) StartCoroutine(BienHinh(2, 1));
            else if (ktImg == 2) StartCoroutine(BienHinh(3, 2));
            else if (ktImg == 3) StartCoroutine(BienHinh(4, 3));
            else if (ktImg == 4) StartCoroutine(BienHinh(5, 4));
            //Invoke("ResetImg", 10f);
        }
        if (other.tag == "tangtocdoquai"&& isItemNhanhCham==false && isPlay == true)
        {
            isItemNhanhCham = true;
            countItem++;
            ktImg = 7;
            GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
            if (countItem == 1)
            {
                StartCoroutine(SliderThoiGianItem(2, 1));
                iconImage1.sprite = iconItemSprite[2];
            }
            else if (countItem == 2)
            {
                StartCoroutine(SliderThoiGianItem(2, 2));
                iconImage2.sprite = iconItemSprite[2];
            }
            else if (countItem == 3)
            {
                StartCoroutine(SliderThoiGianItem(2, 3));
                iconImage3.sprite = iconItemSprite[2];
            }
            StartCoroutine(HienImageItem(spriteItem[2]));
            moveSpeed = 8f;
            /*if (enemy.instance != null && enemy2.instance != null)
            {
                enemy.instance.SetSpeedEnemy(9f);
                enemy2.instance.SetSpeedEnemy(9f);
            }*/
            //Invoke("ResetImg", 10f);
        }
        if (other.tag == "giamtocdoquai" && isItemNhanhCham == false && isPlay == true)
        {
            isItemNhanhCham = true;
            countItem++;
            ktImg = 8;
            GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
            if (countItem == 1)
            {
                StartCoroutine(SliderThoiGianItem(2, 1));
                iconImage1.sprite = iconItemSprite[3];
            }
            else if (countItem == 2)
            {
                StartCoroutine(SliderThoiGianItem(2, 2));
                iconImage2.sprite = iconItemSprite[3];
            }
            else if (countItem == 3)
            {
                StartCoroutine(SliderThoiGianItem(2, 3));
                iconImage3.sprite = iconItemSprite[3];
            }
            StartCoroutine(HienImageItem(spriteItem[3]));
            moveSpeed = 3.5f;
            /*if (enemy.instance != null && enemy2.instance != null){
				enemy.instance.SetSpeedEnemy(1f);
				enemy2.instance.SetSpeedEnemy(1f);
			}*/

            //Invoke("ResetImg", 10f);
        }
        if(other.tag== "itemmaybay" && isItemBienHinh==false && isPlay == true)
        {
            ktImg = 1;
            isItemBienHinh = true;
            countItem++;
            GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
            if (countItem == 1)
            {
                StartCoroutine(SliderThoiGianItem(3, 1));
                iconImage1.sprite = iconItemSprite[4];
            }
            else if (countItem == 2)
            {
                StartCoroutine(SliderThoiGianItem(3, 2));
                iconImage2.sprite = iconItemSprite[4];
            }
            else if (countItem == 3)
            {
                StartCoroutine(SliderThoiGianItem(3, 3));
                iconImage3.sprite = iconItemSprite[4];
            }
            StartCoroutine(HienImageItem(spriteItem[4]));
            StartCoroutine(BienHinh(2,1));
            Destroy(other.gameObject);
            //spriteRen.sprite = ImgPlayer[1];
//            fireButton.gameObject.SetActive(true);
			countTap =0;
			isFire = true;
			Debug.Log ("Unlock Fire");
            //StartCoroutine(SpawnBullet());
            //Invoke("ResetImg", 10f);
        }
        if (other.tag == "itemxetai" && isItemBienHinh == false && isPlay == true)
        {
            ktImg = 2;
            isItemBienHinh = true;
            countItem++;
            GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
            if (countItem == 1)
            {
                StartCoroutine(SliderThoiGianItem(3, 1));
                iconImage1.sprite = iconItemSprite[5];
            }
            else if (countItem == 2)
            {
                StartCoroutine(SliderThoiGianItem(3, 2));
                iconImage2.sprite = iconItemSprite[5];
            }
            else if (countItem == 3)
            {
                StartCoroutine(SliderThoiGianItem(3, 3));
                iconImage3.sprite = iconItemSprite[5];
            }
            StartCoroutine(HienImageItem(spriteItem[5]));
            StartCoroutine(BienHinh(3,2));
//          fireButton.gameObject.SetActive(false);
			Debug.Log ("Lock Fire");
			isFire = false;
            Destroy(other.gameObject);
            //spriteRen.sprite = ImgPlayer[2];
            //Invoke("ResetImg", 10f);
        }
        if (other.tag == "itemxehong" && isItemBienHinh == false && isPlay == true)
        {
            ktImg = 3;
            isItemBienHinh = true;
            countItem++;
            GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
            if (countItem == 1)
            {
                StartCoroutine(SliderThoiGianItem(3, 1));
                iconImage1.sprite = iconItemSprite[6];
            }
            else if (countItem == 2)
            {
                StartCoroutine(SliderThoiGianItem(3, 2));
                iconImage2.sprite = iconItemSprite[6];
            }
            else if (countItem == 3)
            {
                StartCoroutine(SliderThoiGianItem(3, 3));
                iconImage3.sprite = iconItemSprite[6];
            }
            StartCoroutine(HienImageItem(spriteItem[6]));
            StartCoroutine(BienHinh(4,3));
            Destroy(other.gameObject);
//            fireButton.gameObject.SetActive(true);
			countTap = 0;
			isFire = true;
			Debug.Log ("Unlock Fire");
            //Invoke("ResetImg", 10f);
        }
        if (other.tag == "itemxevang" && isItemBienHinh == false && isPlay == true)
        {
            ktImg = 4;
            isItemBienHinh = true;
            countItem++;
            GetComponent<AudioSource>().PlayOneShot(audio_Clip[0]);
            if (countItem == 1)
            {
                StartCoroutine(SliderThoiGianItem(3, 1));
                iconImage1.sprite = iconItemSprite[7];
            }
            else if (countItem == 2)
            {
                StartCoroutine(SliderThoiGianItem(3, 2));
                iconImage2.sprite = iconItemSprite[7];
            }
            else if (countItem == 3)
            {
                StartCoroutine(SliderThoiGianItem(3, 3));
                iconImage3.sprite = iconItemSprite[7];
            }
            StartCoroutine(HienImageItem(spriteItem[7]));
            StartCoroutine(BienHinh(5,4));
            Destroy(other.gameObject);
//            fireButton.gameObject.SetActive(true);
			countTap = 0;
			isFire = true;
			Debug.Log ("Unlock Fire");
            //Invoke("ResetImg", 10f);
        }
        if (other.tag == "score" && isPlay == true)
        {
            ScoreManager.instance.scoreEnemy++;
        }
        if(other.tag=="endlevel")
        {
            GameController.instance.QuestionGame();
            GetComponent<BoxCollider2D>().enabled = false;
        }
        if (other.tag == "bulletenemy" && (ktImg != 2) && isPlay == true)
        {
//            Instantiate(effectNoXe, transform.position, Quaternion.identity);
//            GetComponent<AudioSource>().PlayOneShot(audio_Clip[2]);
//            isPlay = false;
//            mainCamera.GetComponent<AudioSource>().Stop();
//            GetComponent<SpriteRenderer>().enabled = false;
//            GameController.instance.GameOver();
			StartCoroutine (GameOver ());
        }
        if (other.tag == "enemy" &&  isPlay == true)
        {
            if (ktImg!=2)
            {
//                Instantiate(effectNoXe, transform.position, Quaternion.identity);
//                GetComponent<AudioSource>().PlayOneShot(audio_Clip[2]);
//                isPlay = false;
//                mainCamera.GetComponent<AudioSource>().Stop();
//                GetComponent<SpriteRenderer>().enabled = false;
//                GameController.instance.GameOver();
				StartCoroutine (GameOver ());
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(audio_Clip[1]);
                ScoreManager.instance.SetScoreEnemy(ScoreManager.instance.GetScoreEnemy() + 1);
                //Instantiate(effectNoEnemy, other.transform.position, Quaternion.identity);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="wall"&&ktImg!=2)
        {
//            Instantiate(effectNoXe, transform.position, Quaternion.identity);
//            GetComponent<AudioSource>().PlayOneShot(audio_Clip[2]);
//            isPlay = false;
//            mainCamera.GetComponent<AudioSource>().Stop();
//            GetComponent<SpriteRenderer>().enabled = false;
//            GameController.instance.GameOver();
			StartCoroutine (GameOver ());
        }
    }


	IEnumerator GameOver()
	{
		Instantiate(effectNoXe, transform.position, Quaternion.identity);
		GetComponent<AudioSource>().PlayOneShot(audio_Clip[2]);
		isPlay = false;
		mainCamera.GetComponent<AudioSource>().Stop();
		GetComponent<SpriteRenderer>().enabled = false;
        shadowPlayer.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds (1.5f);
		GameController.instance.GameOver();
		yield break;
	}

    void ResetImg(int x)
    {
        //typeItem = 0;
        countItem--;
        if (x==1)
        {
            isItemToNho = false;
            transform.localScale = new Vector3(0.225f, 0.275f, 1f);
            if(ktImg==1) StartCoroutine(BienHinh(2,1));
            else if (ktImg ==2) StartCoroutine(BienHinh(3,2));
            else if (ktImg == 3) StartCoroutine(BienHinh(4,3));
            else if (ktImg == 4) StartCoroutine(BienHinh(5,4));
        }
        else if(x==2)
        {
            isItemNhanhCham = false;
            moveSpeed = 5f;
            if (ktImg == 1) StartCoroutine(BienHinh(2,1));
            else if (ktImg == 2) StartCoroutine(BienHinh(3,2));
            else if (ktImg == 3) StartCoroutine(BienHinh(4,3));
            else if (ktImg == 4) StartCoroutine(BienHinh(5,4));
        }
        else if(x==3)
        {
            isItemBienHinh = false;
            ktImg = 0;
            Debug.Log("Lock Fire");
            isFire = false;
            ktImg = 0;
            StartCoroutine(BienHinh(1,0));
        }
        if(isPlay==true)
        GetComponent<AudioSource>().PlayOneShot(audio_Clip[4]);
        //spriteRen.sprite = ImgPlayer[0];
//        fireButton.gameObject.SetActive(false);
    }

    public void SpawnBullet()
    {
        GetComponent<AudioSource>().PlayOneShot(audio_Clip[3]);
        Vector3 temp = transform.position;
        temp.y += 0.7f;
        if (Time.time>fireTime+0.5f)
        {
            fireTime = Time.time;
            if (ktImg == 1)
            {
                Instantiate(bulletMayBay, temp, Quaternion.identity);
            }
            else if (ktImg == 3)
            {
                Instantiate(bulletXeHong, temp, Quaternion.identity);
            }
            else if (ktImg == 4)
            {
                Instantiate(bulletXeVang, temp, Quaternion.identity);
            }
        }
    }

    IEnumerator BienHinh(int x,int y)
    {
        shadowPlayer.GetComponent<SpriteRenderer>().sprite = ImgShadowPlayer[y];
        GetComponent<SpriteRenderer>().sprite = ImgBienHinh[x];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = ImgBienHinh[0];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = ImgBienHinh[x];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = ImgBienHinh[0];
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = ImgBienHinh[x];
    }

    IEnumerator SliderThoiGianItem(int x,int sttItem)
    {
        if (sttItem == 1)
        {
            sliderItem1.value = 5;
            sliderItem1.gameObject.SetActive(true);
            float i;
            for (i = 5; i > 0;)
            {
                i -= 0.01f;
                yield return new WaitForSeconds(0.01f);
                sliderItem1.value = i;
            }
            sliderItem1.gameObject.SetActive(false);
        }
        else if (sttItem == 2)
        {
            sliderItem2.value = 5;
            sliderItem2.gameObject.SetActive(true);
            float i;
            for (i = 5; i > 0;)
            {
                i -= 0.01f;
                yield return new WaitForSeconds(0.01f);
                sliderItem2.value = i;
            }
            sliderItem2.gameObject.SetActive(false);
        }
        else if (sttItem == 3)
        {
            sliderItem3.value = 5;
            sliderItem3.gameObject.SetActive(true);
            float i;
            for (i = 5; i > 0;)
            {
                i -= 0.01f;
                yield return new WaitForSeconds(0.01f);
                sliderItem3.value = i;
            }
            sliderItem3.gameObject.SetActive(false);
        }
        if (x==1) ResetImg(1);
        else if (x == 2) ResetImg(2);
        else if (x == 3) ResetImg(3);
        yield break;
    }

    IEnumerator HienImageItem(Sprite sp)
    {
        itemImage.sprite = sp;
        itemImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        itemImage.gameObject.SetActive(false);
    }

    int LastFingerId;
    Vector3 moveToward1, moveToward2;
    void MovePlayer()
    {
//        if (Input.touchCount > 0)
//        {
//            Touch touch = Input.GetTouch(0);
//
//            if (touch.phase == TouchPhase.Began)
//            {
//                Vector3 moveToward = Camera.main.ScreenToWorldPoint(touch.position);
//                // run(returnRotation(transform.position, moveToward));
//                run(transform.position, moveToward);
//                return;
//            }
//            if (touch.phase == TouchPhase.Ended)
//            {
//                LastFingerId = -1;
//                return;
//            };
//
//            if (touch.phase == TouchPhase.Moved)
//            {
//                Vector3 moveToward = Camera.main.ScreenToWorldPoint(touch.position);
//                //run(returnRotation(transform.position, moveToward));
//                run(transform.position, moveToward);
//                return;
//            };
//
//        }

        Vector3 mouse = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            moveToward1 = Camera.main.ScreenToWorldPoint(mouse);
            //run(transform.position, moveToward1);
            run1(moveToward1);
            LastFingerId = 255;
            return;
        }
        if (Input.GetMouseButtonUp(0) && LastFingerId == 255)
        {
            LastFingerId = -1;
            return;
        };

        if (Input.GetMouseButton(0) && LastFingerId == 255)
        {
            moveToward2 = Camera.main.ScreenToWorldPoint(mouse);
            run(transform.position, moveToward2);
            //run1(moveToward2);
            return;
        };
    }

    void run1(Vector3 end)
    {
        end.z = 0;
        if (end.x > transform.position.x)
            transform.Translate((speed/2) * Time.deltaTime, 0, 0);
        if (end.x < transform.position.x)
            transform.Translate((-speed/2) * Time.deltaTime, 0, 0);
    }
    void run(Vector3 begin,Vector3 end)
    {
        Vector3 move = end - begin;
        //float s = Mathf.Sqrt(move.x * move.x + move.y + move.y);
        transform.Translate((move.x*speed*Time.deltaTime),0, 0);
//		transform.Translate()

    }
    /*Quaternion returnRotation(Vector3 origion, Vector3 diretion)
    {
        Vector3 vec = diretion - origion;
        float zAngle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.Euler(0, 0, zAngle);
        return rotation;
    }*/
}
