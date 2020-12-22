using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class tiletest : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform sp;
    public Vector3 wrdSize;
    int i = 1;
    
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        maketile();
    }

    void maketile()

    {

      
        int cnt = GameObject.FindGameObjectsWithTag("tile").Length;
        if (cnt < 10)
        {
            Vector3 pos = sp.position;
            Vector3 pos2 = sp.position;
            pos.x = Random.Range(-4f, -2f);
       
            GameObject t = Instantiate(Resources.Load("tile")) as GameObject;
            t.transform.position = pos;
           
            sp.position += new Vector3(0, 2.5f, 0);
           pos = sp.position;
            
            pos.x = Random.Range(0f, 1f);
            GameObject e = Instantiate(Resources.Load("tile")) as GameObject;
            e.transform.position = pos;
            sp.position += new Vector3(0, 1.5f, 0);
            pos2 = sp.position;
            pos2.x = Random.Range(-4f, 4f);
            GameObject r = Instantiate(Resources.Load("tile")) as GameObject;
            r.transform.position = pos2;

            sp.position += new Vector3(0, 2.5f, 0);
        }
    }
}
