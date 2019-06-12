using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //Movement
    public float MoveSpeed = 10;
    //public float TurnRate = 2f;
    public Vector3 moveVector;
    public float SpeedIncreaseRate = 0.0001f;
    public float MaxSpeed = 25f;

    //Jumping
    private float fall = 2.5f;
    private float lowJump = 2f;
    private bool isGrounded;
    private float jumpSpeed = 10f;

    private float animationDuration = 1.0f;

    Rigidbody rb;

    // Use this for initialization
    void Start () {
        ScoreManager.SetScore(0);
        rb = GetComponent<Rigidbody>();
	}

    int countFrame = 0;
    private void FixedUpdate()
    {
        if(countFrame % 60 == 0 && MoveSpeed < MaxSpeed)
        {
            //Increase speed as the time goes by
            MoveSpeed += SpeedIncreaseRate;
            Debug.Log("Current movespeed:" + MoveSpeed);
        }
        countFrame++;
    }

    // Update is called once per frame
    void Update () {

        if(Time.time < animationDuration)
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
