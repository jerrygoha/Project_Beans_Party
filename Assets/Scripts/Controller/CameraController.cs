using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject A;
    Transform AT;
    float min = 0;
    void Start()
    {
        AT = A.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, (AT.position.y > min ? AT.position.y : min), transform.position.z);
    }

    public void minSet(float min)
    {
        if(this.min < min)
           this.min = min;
    }
}
