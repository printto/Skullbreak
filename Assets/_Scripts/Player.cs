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

    //Bullet asset
    public GameObject Bullet;
    public float BulletForce = 100000;
    public Camera playerCam;


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

        /*
        foreach (Touch t in Input.touches)
        {
            if(t.phase == TouchPhase.Began)
            {
                initTouch = t;
            }
            else if (t.phase == TouchPhase.Moved && !swiping)
            {
                float xMoved = initTouch.position.x - t.position.x;
                float yMoved = initTouch.position.y - t.position.y;
                float distance = Mathf.Sqrt((xMoved * xMoved) + (yMoved * yMoved));
                bool swipedLeft = Mathf.Abs(xMoved) > Mathf.Abs(yMoved);

                if(distance > 50f)
                {
                    if (swipedLeft && xMoved > 0 )
                    {
                        cube.transform.Translate(-1, 0, 0);
                    }
                    else if (swipedLeft && xMoved < 0)
                    {
                        cube.transform.Translate(1, 0, 0);
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
        */
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                // get the touch position from the screen touch to world point
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                // lerp and set the position of the current object to that of the touch, but smoothly over time.
                transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime) * 10;
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

        /*
        //Shooty bits
        if (Input.GetMouseButtonDown(0)) //button 0 is left click and 1 is right click
        {
            GameObject temp = Instantiate(Projectile, transform.position, playerCam.transform.rotation);
            temp.GetComponent<Rigidbody>().velocity = playerCam.transform.forward * ProjectileForce;
        }
        */

        //Shooty bits
        if (Input.GetMouseButtonDown(0)) //button 0 is left click and 1 is right click
        {
            GameObject temp = Instantiate(Bullet, new Vector3(transform.position.x - 3, transform.position.y, transform.position.z), playerCam.transform.rotation);
            temp.GetComponent<Rigidbody>().velocity = playerCam.transform.forward * BulletForce * 100;
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
