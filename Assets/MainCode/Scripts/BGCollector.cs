using UnityEngine;
using System.Collections;

public class BGCollector : MonoBehaviour
{
    
    private GameObject[] streets;          //khai báo 1 mảng gameobject
    private float lastBGPosY;             //vị trí cuối của BG

    void Awake()
    {
        streets = GameObject.FindGameObjectsWithTag("street");
        lastBGPosY = streets[0].transform.position.y;

        for (int i = 1; i < streets.Length; i++)
        {
            if (lastBGPosY < streets[i].transform.position.y)
            {
                lastBGPosY = streets[i].transform.position.y;
            }
        }

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "street")             
        {
            Vector3 temp = target.transform.position;           
            float height = ((BoxCollider2D)target).size.y;            
            temp.y = lastBGPosY + height;
            target.transform.position = temp;     
            lastBGPosY = temp.y;
        }
        else if(target.tag!="wall")
        {
            Destroy(target.gameObject);
        }
    }
}
