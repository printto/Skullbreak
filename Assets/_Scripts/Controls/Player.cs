using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
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

    //Bullet asset
    public GameObject Bullet;
    public float BulletForce = 100000;
    public Camera playerCam;

    private float animationDuration = 1.0f;

    //Android jumping control
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance = Screen.height * 5 / 100;

    //Buffs
    public static bool isSlowedDown = false;
    public static bool isDashing = false;
    public static bool isTeleporting = false;
    public static float speedUp = 20;

    // Detect if player has a contact with the wall
    public static bool hasContactWithLWall;
    public static bool hasContactWithRWall;

    // Cooldowns
    //public float MaxTeleportDuration = 1.5f;
    public float MaxTeleportCooldown = 1.5f;
    //float teleportDuration = 0;
    float teleportCooldown = 0;
    bool isTeleportCooldown = false;

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
        else
        {
            GameMaster.SetLife(3);
        }
        ScoreManager.SetScore(0);
        ScoreManager.SetCoin(0);
        rb = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
        isSlowedDown = false;
        isDashing = false;
        //Input.gyro.enabled = true;
        isTeleporting = false;
        hasContactWithLWall = false;
        hasContactWithRWall = false;
    }

    private Ray GenerateMouseRay(Vector3 touchPos)
    {
        Vector3 mousePosFar = new Vector3(touchPos.x, touchPos.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(touchPos.x, touchPos.y, Camera.main.nearClipPlane);

        Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);

        Ray mr = new Ray(mousePosN, mousePosF - mousePosN);
        return mr;
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
        //  transform.position = new Vector2(Mathf.Clamp (transform.position.x, 7.5f,5f), transform.position.y);
        
        /*
        if (Time.time < animationDuration)
        {
            transform.Translate(new Vector3(-1, 0f, 0f) * MoveSpeed * Time.deltaTime, Space.Self);
            return;
        }

        moveVector = Vector3.zero;

        //X Forward and Backward
        moveVector.x = MoveSpeed;
        */

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
        //Tilt
        //float tilt = -Input.gyro.attitude.x*10;
        //transform.Translate(new Vector3(0f, 0f, tilt) * MoveSpeed * Time.deltaTime, Space.Self);

        checkJump();

        MovePlayerFromInputs();

        addTimeScore();
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
                //This can do the teleport things I think. Check for swipe down and detect ending in TouchPhase.Ended
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance * 4)
                {
                    if (lp.y < fp.y && !isTeleporting && teleportCooldown < MaxTeleportCooldown && !isTeleportCooldown)
                    {
                        TeleportSwipeTest();
                        teleportCooldown += Time.deltaTime;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                if (isTeleporting)
                {
                    CancelTeleportSwipeTest();
                    //teleportCooldown = MaxTeleportCooldown;
                }

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
                            //Dashing is unused
                            //Dash();
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than dragDistance of the screen height

                    //ShootBullet();
                }
            }
            if (teleportCooldown > 0 && !isTeleporting)
            {
                isTeleportCooldown = true;
                teleportCooldown -= Time.deltaTime;
            }
            else
            {
                isTeleportCooldown = false;
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

        if (Input.GetAxis("Vertical") < 0)
        {
            Teleport();
        }

        //Keyboard move
        //Make player cannot move toward walls once they have already contacted them
        //Still have a problem with right wall
        if (hasContactWithRWall)
        {
            if (Input.GetAxis("Horizontal") > 0f)
            {
                Debug.Log("Axis : " + Input.GetAxis("Horizontal"));
                transform.Translate(new Vector3(-1, 0f, 0f) * MoveSpeed * Time.deltaTime, Space.Self);
            }
            else
            {
                transform.Translate(new Vector3(-1, 0f, Input.GetAxis("Horizontal")) * MoveSpeed * Time.deltaTime, Space.Self);
            }
        }
        else if (hasContactWithLWall)
        {
            if (Input.GetAxis("Horizontal") < 0f)
            {
                transform.Translate(new Vector3(-1, 0f, 0f) * MoveSpeed * Time.deltaTime, Space.Self);
            }
            else
            {
                transform.Translate(new Vector3(-1, 0f, Input.GetAxis("Horizontal")) * MoveSpeed * Time.deltaTime, Space.Self);
            }
        }
        else
        {
            transform.Translate(new Vector3(-1, 0f, Input.GetAxis("Horizontal")) * MoveSpeed * Time.deltaTime, Space.Self);
        }
    }

    void Jump(float speed)
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, 1, 0) * speed, ForceMode.Impulse);
            if (isDashing)
            {
                CancelDash();
            }
            isGrounded = false;
        }
    }

    void Teleport()
    {
        if (isGrounded && !isTeleporting)
        {
            isTeleporting = true;
            MoveSpeed += speedUp;
            GetComponent<MeshRenderer>().enabled = false;
            Invoke("CancelTeleport", 1);
        }
    }

    void CancelTeleport()
    {
        if (isTeleporting)
        {
            isTeleporting = false;
            MoveSpeed -= speedUp;
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void TeleportSwipeTest()
    {
        if (isGrounded && !isTeleporting)
        {
            isTeleporting = true;
            MoveSpeed += speedUp;
            GetComponent<MeshRenderer>().enabled = false;
            //Invoke("CancelTeleport", 1);
        }
    }

    void CancelTeleportSwipeTest()
    {
        if (isTeleporting)
        {
            isTeleporting = false;
            MoveSpeed -= speedUp;
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void Dash()
    {
        Debug.Log("Dashing called");
        if (isGrounded && !isDashing)
        {
            Debug.Log("Dashing started");
            isDashing = true;
            //TODO: Animations??
            transform.localScale += new Vector3(0, -0.5f, 0);
            Invoke("CancelDash", 1);
        }
    }

    void CancelDash()
    {
        Debug.Log("Dashing ended");
        if (isDashing)
        {
            transform.localScale += new Vector3(0, +0.5f, 0);
        }
        isDashing = false;
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
            if (!collision.gameObject.tag.Equals("Ground") && !collision.gameObject.tag.Equals("LWall") && !collision.gameObject.tag.Equals("RWall"))
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), collision.collider);
                Physics.IgnoreCollision(GetComponentInChildren<Collider>(), collision.collider);
            }
        }
        else
        {
            if (collision.gameObject.tag.Equals("Ground"))
            {
                if (!isGrounded)
                {
                    isGrounded = true;
                }
            }

            if (collision.gameObject.tag.Equals("Monster") && !isTeleporting)
            {
                Slowdown();
            }

            fallDamage.setSavePoint(transform.position.x, transform.position.y, transform.position.z);
        }

    }

    //Detect left or right wall that player touch.
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("LWall") && !hasContactWithLWall)
        {
            hasContactWithLWall = true;
        }

        if (collision.gameObject.tag.Equals("RWall") && !hasContactWithRWall)
        {
            hasContactWithRWall = true;
        }
    }

    //For player leave wall detection
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("LWall"))
        {
            hasContactWithLWall = false;
        }
        if (collision.gameObject.tag.Equals("RWall"))
        {
            hasContactWithRWall = false;
        }
    }

}
