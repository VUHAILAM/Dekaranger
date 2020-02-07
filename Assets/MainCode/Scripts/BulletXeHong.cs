using UnityEngine;
using System.Collections;

public class BulletXeHong : MonoBehaviour {

    public float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.isPlay == true)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            GetComponent<AudioSource>().Play();
            speed = 0;
        }
        if (other.tag == "BGCollector")
        {
            Destroy(gameObject);
        }
    }
}
