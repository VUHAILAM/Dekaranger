using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{

    public GameObject[] enemy;
    public GameObject[] item;
    public float maxHeight;
    public float minHeight;
    public float timeStart=1;
	public float timeSpawn=0;
    // Use this for initialization
    void Start()
    {
		InvokeRepeating("Spawn",timeStart,timeSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void Spawn()
    {
		if (player.isPlay){
            int i = Random.Range(1, 7);
            if (i == 1)
            {
                float randHeight = Random.Range(minHeight, maxHeight);
                int j = Random.Range(0, item.Length);
                GameObject tempCar = Instantiate(item[j]) as GameObject;
                //			CancelInvoke();
                tempCar.transform.position = new Vector3(randHeight, transform.position.y, transform.position.z);
            }
            else
            {
                float randHeight = Random.Range(minHeight, maxHeight);
                int j = Random.Range(0, enemy.Length);
                GameObject tempCar = Instantiate(enemy[j]) as GameObject;
                //			CancelInvoke();
                tempCar.transform.position = new Vector3(randHeight, transform.position.y, transform.position.z);
            }
		}
    }
}
