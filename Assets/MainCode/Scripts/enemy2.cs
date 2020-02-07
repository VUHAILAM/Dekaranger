using UnityEngine;
using System.Collections;

public class enemy2 : MonoBehaviour {
	public static enemy2 instance;
	public float speed=3;
//    public Sprite imgNoEnemy;

    void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	} 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (player.isPlay == true)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
	}
	
	public float GetSpeedEnemy()
	{
		return speed;
	}
	public void SetSpeedEnemy(float t)
	{
		speed = t;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.tag == "bulletxehong")
        {
            speed = 0;
        }
        if (other.tag == "bulletxevang")
        {
            //Quaternion A = transform.rotation;
            //A.z = 180f;
            //transform.rotation = A;
            speed = -speed;
        }
        if (other.tag == "BGCollector" || other.tag == "player" || other.tag == "bulletplayer")
        {
            speed = 0;
            //Destroy(gameObject);
            StartCoroutine(DestroyEnemy());
		}

		if(other.tag=="spawnenemy")
		{
			Destroy(gameObject);
		}
    }

	public GameObject effectDestroyEnemyPref;

    IEnumerator DestroyEnemy()
    {
        if(GetComponent<Animator>()!=null)
            GetComponent<Animator>().enabled = false;
		GameObject eff = Instantiate(effectDestroyEnemyPref, transform.position, Quaternion.identity) as GameObject;
		Destroy (eff, 0.75f);
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
//        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject,1);
		yield break;
    }
}

