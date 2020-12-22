using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    //여기 true 잠시 넣음
    public bool isDead = false;
    public Text Score;
    int score;
    bool isJumping;
    private Vector2 touchBeganPos;
    private Vector2 touchEndedPos;
    private Vector2 currentSwipe;
    public CameraController cameraCtr;
    private AudioSource musicPlayer;
    public AudioClip[] audioclip;
    float vol =100; 


    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        var singleton = AudioManager.instance;
        vol = singleton.volumevalue();

        musicPlayer.volume = vol;
        musicPlayer.loop = false;
    }
    void Update()
    {
        if (cameraCtr.GetComponent<Transform>().position.y - GetComponent<Transform>().position.y > 9&&!isDead )
        {
          
            musicPlayer.clip = audioclip[0];
            musicPlayer.Play();
            Debug.Log("Dead");
            isDead = true;
        }

        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            // 클릭
            if (Input.GetMouseButtonDown(0))
            {
                touchBeganPos = Input.mousePosition;
            }
            //여기 true 잠시넣음
            if (isJumping == false && Input.GetMouseButtonUp(0))
            {
                touchEndedPos = Input.mousePosition;
                musicPlayer.clip = audioclip[1];
                musicPlayer.Play();
                currentSwipe = new Vector2(touchEndedPos.x - touchBeganPos.x, touchEndedPos.y - touchBeganPos.y);
                float x = Mathf.Abs(currentSwipe.x) / 100 > 5 ? 5 : Mathf.Abs(currentSwipe.x) / 100, y = Mathf.Abs(currentSwipe.y) / 100 > 15 ? 15 : Mathf.Abs(currentSwipe.y) / 100;
                currentSwipe.Normalize();
                currentSwipe.y = (currentSwipe.y <= .2f ? .5f : currentSwipe.y);
                y = (y <= 1 ? 3 : y);
                if (currentSwipe.y > 0)
                    isJumping = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(currentSwipe.x * x, currentSwipe.y * y);
            }

            // 터치
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    touchBeganPos = touch.position;
                }

                if (isJumping == false && touch.phase == TouchPhase.Ended)
                {
                    touchEndedPos = touch.position;
                    currentSwipe = new Vector2(touchEndedPos.x - touchBeganPos.x, touchEndedPos.y - touchBeganPos.y);
                    float x = Mathf.Abs(currentSwipe.x) / 100 > 5 ? 5 : Mathf.Abs(currentSwipe.x) / 100, y = Mathf.Abs(currentSwipe.y) / 100 > 15 ? 15 : Mathf.Abs(currentSwipe.y) / 100;
                    currentSwipe.Normalize();
                    currentSwipe.y = (currentSwipe.y <= .2f ? .5f : currentSwipe.y);
                    y = (y <= 1 ? 3 : y);
                    if (currentSwipe.y > 0)
                        isJumping = true;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(currentSwipe.x * x, currentSwipe.y * y);
                }
            }
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-currentSwipe.x * 5, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (isGround(col.contacts[0].point.y))
        {
            isJumping = false;
        }
    }

    private bool isGround(float colY)
    {
        float y = this.transform.position.y;
        Debug.Log(y - colY);
        if (y > colY)
        {
            score = (score > (int)y + 7 ? score : (int)y + 7);
            Score.text = "Score: " + score;
            cameraCtr.minSet(y);
            return true;
        }

        return false;
    }
 
    public int getScore()
    {
        return score;
    }

}