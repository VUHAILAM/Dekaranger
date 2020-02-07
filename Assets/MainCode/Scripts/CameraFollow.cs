using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private Transform tfplayer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (tfplayer != null&&player.isPlay==true)
        {
            Vector3 temp = transform.position;
            temp.y = tfplayer.position.y + 3f;
            transform.position = temp;
        }
    }
}
