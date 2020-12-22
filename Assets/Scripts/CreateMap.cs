using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public float speed = 1f;
    public float startPositionX;
    public float endPositonX;
    public float startPositionY;
    public float endPositonY;
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

        if (transform.position.x >= endPositonX)
        {
            ScrollEnd();
        }
    }

    void ScrollEnd()
    {
        transform.Translate(-1 * (endPositonX - startPositionX), 0, 0);
    }
}
