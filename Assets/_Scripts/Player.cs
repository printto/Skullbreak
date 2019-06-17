using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Player : MonoBehaviour{

    //Player Lifepoint
    private int lifePoint =  2;

    //Movement
    public float MoveSpeed = 10;
    //public float TurnRate = 2f;
    public Vector3 moveVector;
    public double SpeedIncreaseRate = 0.05;
    public float MaxSpeed = 25;
    public float SwipeSpeedLimit = 0.75f;

    //Jumping
    private float fall = 2.5f;
    private float lowJump = 2f;
    private bool isGrounded;
    private float jumpSpeed = 7f;
    private float touchJumpSpeed = 10f;

    //Bullet asset
    public GameObject Bullet;
    public float BulletForce = 100000;
    public Camera playerCam;

    private float animationDuration = 1.0f;

    //Android jumping control
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance = Screen.height * 5 / 100;

    Rigidbody rb;

    private Ray GenerateMouseRay(Vector3 touchPos)
    {
        Vector3 mousePosFar = new Vector3(touchPos.x, touchPos.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(touchPos.x, touchPos.y, Camera.main.nearClipPlane);

        Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);


        Ray mr = new Ray(mousePosN, mousePosF - mousePosN);
        return mr;

    }

    // Use this for initialization
    void Start () {
        GameMaster.SetLife(3);
        ScoreManager.SetScore(0);
        rb = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
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
        if (Input.GetButton("Jump"))
        {
            Jump(jumpSpeed);
        }

        /*
        //Shooty bits
        if (Input.GetButtonDown("Fire1")) //button 0 is left click and 1 is right click
        {
            ShootBullet();
        }
        */

        checkJump();

        MovePlayerFromInputs();

        addTimeScore();

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Hit something");
        if (collision.gameObject.tag.Equals("Ground"))
        {
            if (!isGrounded)
            {
                isGrounded = true;
            }
            fallDamage.setSavePoint(transform.position.x, transform.position.y, transform.position.z);
        }

    }

    void checkJump()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than dragDistance of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                    //It's a drag
                    //the vertical movement is greater than the horizontal movement
                    if (Mathf.Abs(lp.x - fp.x) < Mathf.Abs(lp.y - fp.y))
                    {
                        if (lp.y > fp.y)
                        {
                            //Up swipe
                            Debug.Log("Up Swipe");
                            Jump(touchJumpSpeed);
                        }
                        else
                        {
                            //Down swipe
                            //TODO: May have something to do with swipe down?
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    ShootBullet();
                }
            }
        }
    }

    /*
     * //This code doesnt seems to work, but it is easy to understand na :(
     * 
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging event end detected");
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        DetectJump(dragVectorDirection);
    }

    void DetectJump(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        if (positiveX < positiveY)
        {
            Debug.Log("Drag vector: " + dragVector.y);
            if (dragVector.y > 1) {
                Jump();
            }
        }
    }
    */

    void addTimeScore()
    {
        ScoreManager.AddScore(Time.deltaTime);
    }


    /*
     * Controlling functions 
     */

    void ShootBullet()
    {
        GameObject temp = Instantiate(Bullet, new Vector3(transform.position.x - 3, transform.position.y, transform.position.z), playerCam.transform.rotation);
        temp.GetComponent<Rigidbody>().velocity = playerCam.transform.forward * BulletForce * 100;
    }

    void MovePlayerFromInputs()
    {
        //Touch screen
        if (Input.touchCount > 0 &&
       Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            // Get movement of the finger since last frame
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            float toGo = touchDeltaPosition.x / 10;
            if (toGo > SwipeSpeedLimit)
            {
                toGo = SwipeSpeedLimit;
            }
            else if (toGo < -SwipeSpeedLimit)
            {
                toGo = -SwipeSpeedLimit;
            }

            // Move object across XY plane
            transform.Translate(new Vector3(0f, 0f, toGo) * MoveSpeed * Time.deltaTime, Space.Self);
        }

        //Keyboard move
        transform.Translate(new Vector3(-1, 0f, Input.GetAxis("Horizontal")) * MoveSpeed * Time.deltaTime, Space.Self);
    }

    void Jump(float speed)
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, 1, 0) * speed, ForceMode.Impulse);
            isGrounded = false;
        }
    }

}
