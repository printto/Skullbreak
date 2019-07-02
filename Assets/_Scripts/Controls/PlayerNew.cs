using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerNew : MonoBehaviour
{

    //Player Lifepoint
    private int lifePoint = 2;
    private int coin = 0;

    //Movement
    public float MoveSpeed = 10;
    float CurrentMoveSpeed = 0;
    public float SlowdownMoveSpeed = 5;

    //public float TurnRate = 2f;
    public Vector3 moveVector;
    public double SpeedIncreaseRate = 0.05;
    public float MaxSpeed = 25;
    public float SwipeSpeedLimit = 0.75f;
    float dirX;

    //Jumping
    private float fall = 2.5f;
    private float lowJump = 2f;
    private bool isGrounded;
    private float jumpSpeed = 7f;
    private float touchJumpSpeed = 10f;

    private float animationDuration = 1.0f;

    //Android jumping control
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance = Screen.height * 5 / 100;

    //Slowdown
    public static bool isSlowedDown = false;

    //Teleportation
    private float speedUp = 100f;
    public static bool teleportable = false;
    public static bool isTeleporting = false;

    //Lanes
    public float[] LaneZs;
    public int currentLane = 0;
    float nextZPosition = 0;
    ChangeLaneDirection currentDirection;

    Rigidbody rb;

    void Awake()
    {
        // Call the LevelManager and set the last level.
        LoadPrevScene.setLastLevel(SceneManager.GetActiveScene().name);
        Debug.Log(SceneManager.GetActiveScene().name.ToString());
    }

    // Use this for initialization
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("EndlessMode"))
        {
            GameMaster.SetLife(0);
        }
        else if (SceneManager.GetActiveScene().name.Equals("TutorialLevel"))
        {
            GameMaster.SetLife(99);
        }
        else if (SceneManager.GetActiveScene().name.Equals("GridMode"))
        {
            GameMaster.SetLife(0);
        }
        else
        {
            GameMaster.SetLife(3);
        }

        isSlowedDown = false;
        teleportable = false;
        isTeleporting = false;

        ScoreManager.SetScore(0);
        ScoreManager.SetCoin(0);
        rb = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
        currentDirection = ChangeLaneDirection.STILL;
    }

    int countFrame = 0;

    private void FixedUpdate()
    {
        if (countFrame % 60 == 0 && MoveSpeed < MaxSpeed)
        {
            //Increase speed as the time goes by
            MoveSpeed += (float)SpeedIncreaseRate;
            //Debug.Log("Current movespeed:" + MoveSpeed);
        }
        countFrame++;
    }

    void Update()
    {

        if(currentDirection == ChangeLaneDirection.RIGHT && transform.position.z < nextZPosition)
        {
            transform.Translate(new Vector3(-1, 0f, 1) * MoveSpeed * Time.deltaTime, Space.Self);
        }
        else if (currentDirection == ChangeLaneDirection.LEFT && transform.position.z > nextZPosition)
        {
            transform.Translate(new Vector3(-1, 0f, -1) * MoveSpeed * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Translate(new Vector3(-1, 0f, 0f) * MoveSpeed * Time.deltaTime, Space.Self);
            currentDirection = ChangeLaneDirection.STILL;
        }

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

        //For debugging
        if (Input.GetMouseButtonDown(0)) //button 0 is left click and 1 is right click
        {
            //Dash();
        }

        checkJump();

        MovePlayerFromInputs();

        addTimeScore();
    }

    void checkJump()
    {
        if (Input.touchCount == 1 && !isTeleporting) // user is touching the screen with a single touch
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
                            Jump(touchJumpSpeed);
                            Debug.Log("Up Swipe");
                        }
                        else
                        {
                            //Down swipe
                            //TODO: Teleport
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than dragDistance of the screen height
                    //ShootBullet is unused.
                    //ShootBullet();
                }
            }
        }
    }

    void addTimeScore()
    {
        ScoreManager.AddScore(Time.deltaTime);
    }


    /*
     * 
     * Codes below this comment are
     * for input controlling functions
     * 
     */
    
    void MovePlayerFromInputs()
    {
        //Touch screen
        if (Input.touchCount == 1 && !isTeleporting) // user is touching the screen with a single touch
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
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {
                        if (lp.x > fp.x)
                        {
                            //Right swipe
                            ChangeLane(ChangeLaneDirection.RIGHT);
                            Debug.Log("Right Swipe");
                        }
                        else
                        {
                            //Left swipe
                            ChangeLane(ChangeLaneDirection.LEFT);
                            Debug.Log("Left Swipe");
                        }
                    }
                }

            }
        }
        //Keyboard move
        if (Input.GetAxis("Vertical") < 0 && teleportable && !isTeleporting)
        {
            Debug.Log("teleporting");
            Teleport();
        }
        else
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                if (Input.GetAxisRaw("Horizontal") > 0) ChangeLane(ChangeLaneDirection.RIGHT);
                else if (Input.GetAxisRaw("Horizontal") < 0) ChangeLane(ChangeLaneDirection.LEFT);
            }
        }
    }

    enum ChangeLaneDirection
    {
        LEFT,
        RIGHT,
        STILL
    }
    void ChangeLane(ChangeLaneDirection direction)
    {
        if(direction == ChangeLaneDirection.LEFT && currentLane > 0)
        {
            currentLane -= 1;
            currentDirection = ChangeLaneDirection.LEFT;
            nextZPosition = LaneZs[currentLane];
        }
        else if (direction == ChangeLaneDirection.RIGHT && currentLane < LaneZs.Length -1)
        {
            currentLane += 1;
            currentDirection = ChangeLaneDirection.RIGHT;
            nextZPosition = LaneZs[currentLane];
        }
        /*
        Vector3 newPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Lanes[currentLane].transform.localPosition.z);
        transform.localPosition = newPosition;
        */
    }

    void Jump(float speed)
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, 1, 0) * speed, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // Start the teleportation if player is inside the teleport area
    void Teleport()
    {
        if (teleportable)
        {
            isTeleporting = true;
            MoveSpeed += speedUp;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void CancelTeleport()
    {
        isTeleporting = false;
        MoveSpeed -= speedUp;
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void Slowdown()
    {

        Debug.Log("Slowdown called");
        if (!isSlowedDown)
        {
            CurrentMoveSpeed = MoveSpeed;
            MoveSpeed = SlowdownMoveSpeed;
            Debug.Log("Slowdown started");
            isSlowedDown = true;
            Invoke("CancelSlowdown", 1);
        }
    }

    void CancelSlowdown()
    {
        Debug.Log("Slowdown ended");
        if (isSlowedDown)
        {
            MoveSpeed = CurrentMoveSpeed;
        }
        isSlowedDown = false;
    }


    /*
     * 
     * These are for collision detection
     * 
     */

    private void OnCollisionEnter(Collision collision)
    {
        if (isTeleporting)
        {
            if (!(collision.gameObject.tag.Equals("Ground") || collision.gameObject.tag.Equals("TeleportEnd")))
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
            }
            else if (collision.collider.tag.Equals("TeleportEnd"))
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
                CancelTeleport();
            }
        } else 
        {
            if (collision.gameObject.tag.Equals("Ground"))
            {
                if (!isGrounded)
                {
                    isGrounded = true;
                }
            }
            fallDamage.setSavePoint(transform.position.x, transform.position.y, transform.position.z);
        } 
        

    }

}
