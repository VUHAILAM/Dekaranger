using UnityEngine;
using System.Collections;

public class BulletXeVang : MonoBehaviour {

    public float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            GetComponent<AudioSource>().Play();
            ScoreManager.instance.SetScoreEnemy(ScoreManager.instance.GetScoreEnemy() + 1);
            Destroy(gameObject);
            //Destroy(other.gameObject);
        }
        else if (other.tag == "spawnenemy")
        {
            Destroy(gameObject);
        }
    }
}
