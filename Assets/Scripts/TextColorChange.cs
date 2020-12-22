using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorChange : MonoBehaviour
{
    [SerializeField]
    private Text text;

    float time;
    void Update()
    {
        if(time < .5f)
        {
            text.color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            text.color = new Color(1, 1, 1, time);
            if (time > 1f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
    }
}
