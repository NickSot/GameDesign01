using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {      // Moves the object forward at two units per second.
            transform.position += new Vector3(0, -2,0) * Time.deltaTime;
 
    }
}
