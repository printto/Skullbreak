using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Movement
    public float MoveSpeed = 10;
    //public float TurnRate = 2f;
    public Vector3 moveVector;
    public double SpeedIncreaseRate = 0.05;
    public float MaxSpeed = 25;

    //Jumping
    private float fall = 2.5f;
    private float lowJump = 2f;
    private bool isGrounded;
    private float jumpSpeed = 7f;

    private float animationDuration = 1.0f;

    //Android Control
    Touch initTouch;
    bool swiping = false;
    public GameObject cube;

    Rigidbody rb;

    // Use this for initialization
    void Start () {
        ScoreManager.SetScore(0);
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
	}

    int countFrame = 0;
    private void FixedUpdate()
    {
        if(countFrame % 60 == 0 && MoveSpeed < MaxSpeed)
        {
            //Increase speed as the time goes by
            MoveSpeed += (float) SpeedIncreaseRate;
            Debug.Log("Current movespeed:" + MoveSpeed);
        }
        countFrame++;
    }

    // Update is called once per frame
    void Update () {

        foreach(Touch t in Input.touches)
        {
            if(t.phase == TouchPhase.Began)
            {
                initTouch = t;
            }
            else if (t.phase == TouchPhase.Moved)
            {
                float xMoved = initTouch.position.x - t.position.y;
                float yMoved = initTouch.position.x - t.position.y;
                float distance = Mathf.Sqrt((xMoved * xMoved) + (yMoved * yMoved));
                bool swipedLeft = Mathf.Abs(xMoved) > Mathf.Abs(yMoved);

                if(distance > 50f)
                {
                    if (swipedLeft && xMoved > 0)
                    {
                        cube.transform.Translate(-5, 0, 0);
                    }
                    else if (swipedLeft && xMoved < 0)
                    {
                        cube.transform.Translate(5, 0, 0);
                    }
                    swiping = true;
                }
            }
            else if (t.phase == TouchPhase.Ended)
            {
                initTouch = new Touch();
                swiping = false;
            }
        }



        if (Time.time < animationDuration)
        {
            transform.Translate(new Vector3(-1, 0f, 0f) * MoveSpeed * Time.deltaTime, Space.Self);
            return;
        }

        moveVector = Vector3.zero;

        //X Forward and Backward
        moveVector.x = MoveSpeed;

        // jump improve
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics2D.gravity.y * (fall - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics2D.gravity.y * (lowJump - 1) * Time.deltaTime;
        }

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }

        transform.Translate(new Vector3(-1, 0f, Input.GetAxis("Horizontal")) * MoveSpeed * Time.deltaTime, Space.Self);

        addTimeScore();

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hit something");
  
        if (collision.gameObject.tag.Equals("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }
        
    } 

    void addTimeScore()
    {
        ScoreManager.AddScore(Time.deltaTime);
    }

}
