using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class tile : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        if (pos.y < -5)
        {
            Destroy(gameObject);
        }
    }
}
