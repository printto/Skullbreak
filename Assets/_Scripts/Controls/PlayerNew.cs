using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerNew : MonoBehaviour
{

    //Player Lifepoint
    public int lifePoint = 2;
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
    public static GameObject teleportEnd;
    public static bool teleportable = false;
    public static bool isTeleporting = false;

    //Crash
    public static bool isDamaged = false;


    //Lanes
    public float[] LaneZs;
    public int currentLane = 0;
    float nextZPosition = 0;
    LaneDirection currentDirection;

    //animations
    public Animator ControllingAnimator;
    public float IdlePositionError = 0.2f;
    public GameObject PlayerModelToAnimate;
    Animator PlayerModelAnimator;

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
        GameMaster.SetLife(lifePoint);

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

        isSlowedDown = false;
        teleportable = false;
        isTeleporting = false;
        isDamaged = false;

        ScoreManager.SetScore(0);
        ScoreManager.SetCoin(0);
        rb = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
        currentDirection = LaneDirection.STILL;
        nextZPosition = LaneZs[currentLane];

        PlayerModelAnimator = PlayerModelToAnimate.GetComponent<Animator>();
    }

    int countFrame = 0;
    float lastYAirPosition = 0;
    private void FixedUpdate()
    {
        if (countFrame % 60 == 0 && MoveSpeed < MaxSpeed)
        {
            //Increase speed as the time goes by
            MoveSpeed += (float)SpeedIncreaseRate;
            //Debug.Log("Current movespeed:" + MoveSpeed);
        }
        countFrame++;
        if (transform.position.y <= -5)
        {
            MoveSpeed = 0;
            DeadScene();
        }
        
        if (transform.position.y < lastYAirPosition)
        {
            //TODO: Trigger the jumping down animation here.
            setAnimation(AnimationStates.JUMP_FALL);
        }
        else if ((transform.position.z + IdlePositionError >= LaneZs[currentLane] && currentAnimationState == AnimationStates.TURN_RIGHT) ||
            (transform.position.z - IdlePositionError <= LaneZs[currentLane] && currentAnimationState == AnimationStates.TURN_LEFT) ||
            (currentAnimationState == AnimationStates.JUMP_FALL))
        {
            //TODO: Cancel the turning animation by triggering the normal animation.
            setAnimation(AnimationStates.IDLE);
        }
        lastYAirPosition = transform.position.y;
        //Debug.Log("Current Z: " + transform.position.z + "\nLaneZ: " + LaneZs[currentLane]);
    }

    void LerpByLanePosition()
    {
        LerpPlayer();
    }


    private void DeadScene()
    {
        //SceneManager.LoadScene(2);
        //Initiate.Fade("DeadScene", Color.black, 6f);
        Debug.Log("Trigger dead scene");
        SceneTransition.setAnimator(ControllingAnimator);
        Debug.Log(ControllingAnimator.ToString());
        SceneTransition.setScene("DeadScene");
        SceneTransition.getScene();
        StartCoroutine(SceneTransition.LoadScene());
    }

    private void EndingScene()
    {
        SceneTransition.setAnimator(animator);
        SceneTransition.setScene("EndingScene");
        StartCoroutine(SceneTransition.LoadScene());
    }

    void LerpPlayer()
    {
        transform.Translate(new Vector3(-1, 0, 0) * MoveSpeed * Time.deltaTime, Space.Self);
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, LaneZs[currentLane]), (MoveSpeed * 0.5f) * Time.deltaTime);
    }

    void Update()
    {
        LerpByLanePosition();
        // jump improve
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics2D.gravity.y * (fall - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics2D.gravity.y * (lowJump - 1) * Time.deltaTime;
        }
        //For debugging
        if (Input.GetMouseButtonDown(0)) //button 0 is left click and 1 is right click
        {
            //Dash();
        }
        MovePlayerFromInputs();

        //addTimeScore();
    }

    void addTimeScore()
    {
        ScoreManager.AddScore(-Time.deltaTime);
    }

    /*
     * 
     * These are for animations
     *
     */
    AnimationStates currentAnimationState = AnimationStates.IDLE;
    enum AnimationStates
    {
        IDLE,
        JUMP_UP,
        JUMP_FALL,
        TURN_LEFT,
        TURN_RIGHT,
        DIE,
        TELEPORTING,
        HIT
    }
    void setAnimation(AnimationStates stateToSet)
    {
        if (currentAnimationState != stateToSet)
        {
            string animationName = stateToSet.ToString();
            //Debug.Log(animationName);
            PlayerModelAnimator.SetTrigger(animationName);
            currentAnimationState = stateToSet;
        }
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
                            ChangeLane(LaneDirection.RIGHT);
                            //TODO: Change change lane to the right animation here.
                            setAnimation(AnimationStates.TURN_RIGHT);
                            Debug.Log("Right Swipe");
                        }
                        else
                        {
                            //Left swipe
                            ChangeLane(LaneDirection.LEFT);
                            //TODO: Change change lane to the left animation here.
                            setAnimation(AnimationStates.TURN_LEFT);
                            Debug.Log("Left Swipe");
                        }
                    }
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
                            /* This moved to tapping
                            if (teleportable && !isTeleporting)
                            {
                                Teleport();
                            }
                            */
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than dragDistance of the screen height
                    if (teleportable && !isTeleporting)
                    {
                        Teleport();
                    }
                }
            }

        }
        //Keyboard moves
        if (Input.GetAxisRaw("Vertical") < 0 && teleportable && !isTeleporting)
        {
            Teleport();
        }
        //Jumping
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            Jump(jumpSpeed);
        }
        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                ChangeLane(LaneDirection.RIGHT);
                setAnimation(AnimationStates.TURN_RIGHT);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                ChangeLane(LaneDirection.LEFT);
                setAnimation(AnimationStates.TURN_LEFT);
            }
        }
    }

    enum LaneDirection
    {
        LEFT,
        RIGHT,
        STILL
    }
    void ChangeLane(LaneDirection direction)
    {
        if (direction == LaneDirection.LEFT && currentLane > 0)
        {
            currentLane -= 1;
            currentDirection = LaneDirection.LEFT;
            nextZPosition = LaneZs[currentLane];
        }
        else if (direction == LaneDirection.RIGHT && currentLane < LaneZs.Length - 1)
        {
            currentLane += 1;
            currentDirection = LaneDirection.RIGHT;
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
            //TODO: Start jumping up animation here.
            setAnimation(AnimationStates.JUMP_UP);
        }
    }

    // Start the teleportation if player is inside the teleport area
    void Teleport()
    {
        if (teleportable)
        {
            if (teleportEnd == null)
            {
                Debug.Log("Null End Gate!!");
            }
            isTeleporting = true;
            Vector3 endPos = teleportEnd.transform.position;
            transform.position = new Vector3(endPos.x, endPos.y, transform.position.z);
        }
    }

    void CancelTeleport()
    {
        isTeleporting = false;
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
        if (collision.gameObject.tag.Equals("Ground"))
        {
            if (!isGrounded)
            {
                isGrounded = true;
            }
            //fallDamage.setSavePoint(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("TeleportEnd"))
        {
            if (teleportEnd != null)
            {
                Destroy(teleportEnd);
                teleportEnd = null;
            }
            CancelTeleport();
        } else if (other.gameObject.tag.Equals("EndingGate"))
        {
            EndingScene();
        }
    }

}