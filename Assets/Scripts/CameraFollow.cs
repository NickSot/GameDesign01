using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 newpos;
    private Vector3 offset;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        offset = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newpos.x = player.transform.position.x - offset.x;
        newpos.y = player.transform.position.y - offset.y;
        newpos.z = player.transform.position.z - offset.z;

        transform.position = newpos;
    }
}
