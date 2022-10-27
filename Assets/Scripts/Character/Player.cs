using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //speeds
    public float moveSpeed = 5f;
    public float dashSpeed = 30f;
    public float hideSpeed = 2f;

    //movment variables
    Rigidbody2D rb;
    private Vector2 moveDirection; //Uses move x and y together
    private bool moving = false;

    //Dash variables
    [SerializeField]
    [Header("How Long the Dash Lasts")]
    private float dashTime = 0.1f;
    private bool dashing = false;
    [SerializeField]
    [Header("How Long the Recoil Time After the Dash Lasts")]
    private float recoilTime = 1.0f;
    private bool recoiling = false;
    private float keyTapCheck;
    [SerializeField]
    [Header("How Quickly the Dash Keys Need to be Pressed")]
    private float keyTapTime = 0.3f; //time to double tap
    private KeyCode lastKeyArrow;
    private KeyCode lastKeyWASD;

    //Hide variables
    private bool hiding;
    [SerializeField]
    [Header("The Canavas Acting as a Billboard")]
    private GameObject billboard;


    //direction variables
    private float input; 

    //animation variables
    Animator animator;
    private string currentAnimaton;

    //Animation states
    const string PLAYER_IDLE = "player_idle";
    const string PLAYER_RUN = "player_run";
    const string PLAYER_HIDE = "player_hide";
    const string PLAYER_HIDE_MOVE = "player_hide_move";

    void Start()
    {
        animator = GetComponent<Animator>(); //Sets the animator to the player animator
        rb = GetComponent<Rigidbody2D>(); //Sets rb to the players rigid body
    }
    void Update()
    {
        //Processing Inputs
        ProcessInputs();
        //Animation
        //animator.SetFloat("Magnitude", rb.velocity.magnitude); //set animation
        Flip();
    }
    void FixedUpdate()
    {
        //Physics Calcualtions
        Move();
    }
    void ProcessInputs()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; //normalized caps vector at 1
        input = Input.GetAxisRaw("Horizontal"); //for direction and flipping

        ProcessMoving();

        //Dash action
        if (!hiding && !recoiling)
        {
            ProcessDash();
        }

        //Hide Toggle
        if (!dashing && !recoiling)
        {
            ProcessHide();
            if (hiding) 
            {
                if (moving)
                {
                    ChangeAnimationState(PLAYER_HIDE_MOVE);
                }
                else
                {
                    ChangeAnimationState(PLAYER_HIDE);
                }
            }
        }

        if (!hiding && !recoiling)
        {
            if (moving)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
    }
    void Move()
    {
        //Dashing Movement
        if (dashing)
        {
            rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed); //Movement formula
        }
        //Recoiling Movement
        else if (recoiling)
        {
            rb.velocity = new Vector2(0, 0);
        }
        //Hiding Movement
        else if (hiding)
        {
            rb.velocity = new Vector2(moveDirection.x * hideSpeed, moveDirection.y * hideSpeed);
        }
        //Basic Movement
        else
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed); //Movement formula
        }


    }

    void Flip()
    {
        if (input > 0) //moving right
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (input < 0) //moving left
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    void ProcessMoving()
    {
        //if moving
        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            moving = true;
        }
        //if not moving
        else
        {
            moving = false;
        }
    }
    void ProcessDash()
    {
        //DASH
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (keyTapCheck > Time.time && (lastKeyArrow == KeyCode.RightArrow || lastKeyWASD == KeyCode.D))
            {
                StartCoroutine(Dash());
            }
            else
            {
                keyTapCheck = Time.time + keyTapTime;
            }
            lastKeyArrow = KeyCode.RightArrow;
            lastKeyWASD = KeyCode.D;
        } //Right

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
        {
            if (keyTapCheck > Time.time && (lastKeyArrow == KeyCode.LeftArrow || lastKeyWASD == KeyCode.A))
            {
                StartCoroutine(Dash());
            }
            else
            {
                keyTapCheck = Time.time + keyTapTime;
            }
            lastKeyArrow = KeyCode.LeftArrow;
            lastKeyWASD = KeyCode.A;
        } //Left

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            if (keyTapCheck > Time.time && (lastKeyArrow == KeyCode.UpArrow || lastKeyWASD == KeyCode.W))
            {
                StartCoroutine(Dash());
            }
            else
            {
                keyTapCheck = Time.time + keyTapTime;
            }
            lastKeyArrow = KeyCode.UpArrow;
            lastKeyWASD = KeyCode.W;
        } //Up

        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
        {
            if (keyTapCheck > Time.time && (lastKeyArrow == KeyCode.DownArrow || lastKeyWASD == KeyCode.S))
            {
                StartCoroutine(Dash());
            }
            else
            {
                keyTapCheck = Time.time + keyTapTime;
            }
            lastKeyArrow = KeyCode.DownArrow;
            lastKeyWASD = KeyCode.S;
        } //Down
    }
    IEnumerator Dash()
    {
        dashing = true;
        yield return new WaitForSeconds(dashTime);
        dashing = false;

        recoiling = true;
        yield return new WaitForSeconds(recoilTime);
        recoiling = false;
    }

    void ProcessHide()
    {
        //If button is pressed
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            //Check if it is on or off
            if (billboard.activeInHierarchy == true)
            {
                billboard.SetActive(false);
                hiding = true;
                Debug.Log("hiding=" + hiding);
            }
            else
            {
                billboard.SetActive(true);
                hiding = false;
                Debug.Log("hiding=" + hiding);
            }
        } //Hide
    }

    //=====================================================
    // mini animation manager
    //=====================================================
    void ChangeAnimationState(string newAnimation)
    {
        //Debug.Log("currentAnimation=" + currentAnimaton);
        //Debug.Log("newAnimation=" + newAnimation);
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}
