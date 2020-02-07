using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{
    public static enemy instance;
    public float speed=3;
    public GameObject bulletEnemy;
    private bool BanDan = false;
//    public Sprite imgNoEnemy;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isPlay == true)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            if (BanDan == false)
            {
                StartCoroutine(BulletEnemy());
                BanDan = true;
            }
        }
        else BanDan = false;
    }

    public float GetSpeedEnemy()
    {
        return speed;
    }
    public void SetSpeedEnemy(float t)
    {
        speed = t;
    }

    IEnumerator BulletEnemy()
    {
        yield return new WaitForSeconds(1f);
        Vector3 temp = transform.position;
        temp.y -= 0.6f;
        Instantiate(bulletEnemy,temp,Quaternion.identity);
        if (player.isPlay == true)
        StartCoroutine(BulletEnemy());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="bulletxehong")
        {
            speed = 0;
        }
        if(other.tag=="bulletxevang")
        {
            //Quaternion A = transform.rotation;
            //A.z = 180f;
            //transform.rotation = A;
            speed = -speed;
        }
        if (other.tag == "BGCollector"||other.tag=="player"||other.tag== "bulletplayer")
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
        if (GetComponent<Animator>() != null)
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