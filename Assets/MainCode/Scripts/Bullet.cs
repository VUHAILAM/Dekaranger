using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed;
//    public GameObject effectNoXe;

	// Use this for initialization
	void Start () {

        int y =MainMenuController.GetIsSound();
        if (y == 1)
        {
            gameObject.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().mute = true;
        }

    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, speed*Time.deltaTime, 0);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="enemy")
        {
//            Instantiate(effectNoXe, transform.position, Quaternion.identity);
            GetComponent<AudioSource>().Play();
            ScoreManager.instance.SetScoreEnemy(ScoreManager.instance.GetScoreEnemy() + 1);
            Destroy(gameObject);
        }
        if (other.tag == "bulletenemy")
        {
            //            Instantiate(effectNoXe, transform.position, Quaternion.identity);
            GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.tag=="spawnenemy")
        {
            Destroy(gameObject);
        }
    }
}
