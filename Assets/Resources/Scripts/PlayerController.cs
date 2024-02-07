using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script controls the player movement. It's complicated, buckle up. 
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private BoxCollider2D snailBoxCollider;
    private CircleCollider2D snailCircleColliderLeft;

    private Vector2 moveBy;
    private float moveFactor = 5f;

    [SerializeField]
    private Animator myAnimationController;

    //snail movement booleans
    public bool isGrounded = true;

    private bool isShelled;

    public bool canJump = true;

    public bool isJumping = false;

    public bool isRolling;

    //jumping and rolling float values

    private float timeToRoll;

    private float jumpHeight = 12.0f;

    private float jumpTimeCounter;

    //snail orientation booleans
    public bool upsideUp, upsideDown, upsideRight, upsideLeft;

    public bool isStickingRight;

    public bool isStickingTop;

    //snail-to-tile sticking state checks
    public LayerMask whatIsGround;

    public Transform groundCheck1, groundCheck2;

    public Transform leftCheck, rightCheck, topCheck;

    private float surroundCheckRadius;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        snailBoxCollider = GetComponent<BoxCollider2D>();
        snailCircleColliderLeft = GetComponent<CircleCollider2D>();
        rb.gravityScale = 3.0f;
        surroundCheckRadius = .125f;
        upsideUp = true;
    }

    public void Update()
    {   
        //if the character can move and moves, then they have moved. 
        if (!GlobalControl.Instance.hasMoved)
        {
            if (GlobalControl.Instance.canMove)
            {
                if (
                    Input.GetKey(KeyCode.UpArrow) ||
                    Input.GetKey(KeyCode.DownArrow) ||
                    Input.GetKey(KeyCode.RightArrow) ||
                    Input.GetKey(KeyCode.LeftArrow)
                )
                {
                    GlobalControl.Instance.hasMoved = true;
                }
            }
        }

        if (isShelled) {
            snailBoxCollider.enabled = false;
            snailCircleColliderLeft.enabled = true;
        } else {
            snailBoxCollider.enabled = true;
            snailCircleColliderLeft.enabled = false;
        }
    }

    IEnumerator JumpAllowDelay() {
        yield return new WaitForSeconds(.7f);
        canJump = true;
    }

    void FixedUpdate() {
        if (!GlobalControl.Instance.pause){
            //update the player's state based on its positional sensors
            isGrounded =
                Physics2D.OverlapCircle(groundCheck1.position, surroundCheckRadius, whatIsGround)
                ||
                Physics2D.OverlapCircle(groundCheck2.position, surroundCheckRadius, whatIsGround);
            isStickingRight =
                Physics2D.OverlapCircle(rightCheck.position, surroundCheckRadius, whatIsGround);
            isStickingTop =
                Physics2D.OverlapCircle(topCheck.position, surroundCheckRadius, whatIsGround);
            if (GlobalControl.Instance.canMove){
                moveBy.x = Input.GetAxisRaw("Horizontal");
                moveBy.y = Input.GetAxisRaw("Vertical");
                //the player will be in one of 4 states: upsideUp, upsideLeft, upsideRight, or upsideDown
                if (upsideUp) {
                rb.velocity = new Vector2(moveBy.x * moveFactor, rb.velocity.y);
                    if (!isRolling) {
                        rb.gravityScale = 3.0f;
                        moveFactor = 5f / 1.2f;

                        //flip the snail sprite based on movement
                        if (moveBy.x < 0) {
                            transform.localScale = new Vector3(-1, 1, 1);
                        } else if (moveBy.x > 0) {
                            transform.localScale = new Vector3(1, 1, 1);
                        }

                        //this jumping script allows for variable jump heights
                        if (Input.GetKey(KeyCode.X) && isJumping) {
                            if (jumpTimeCounter > 0) {
                                rb.velocity = Vector2.up * jumpHeight;
                                jumpTimeCounter -= Time.deltaTime;
                            } else {
                                isJumping = false;
                            } 
                        }
                    
                        if (Input.GetKeyUp(KeyCode.X))
                            {
                                isJumping = false; 
                            }

                        if (isGrounded)
                        {
                            //snail jumping
                            if (Input.GetKeyDown(KeyCode.X))
                            {
                                    rb.velocity = Vector2.up * 2 * jumpHeight / 3;
                                    isJumping = true;
                                    jumpTimeCounter = .15f;
                            }

                            //snail retreating
                            if (Input.GetKey(KeyCode.S))
                            {
                                myAnimationController.SetBool("RollUp", true);
                                isShelled = true;
                                GlobalControl.Instance.canMove = false;
                                timeToRoll = 1.5f;
                            }
                        }

                        //snail rolling
                        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && isShelled) {
                            myAnimationController.SetBool("Moving", true);
                            isRolling = true;
                        }

                        //change the snail orientation states
                        //flip upside down
                        if (Input.GetKey(KeyCode.UpArrow) && isStickingTop)
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 180);
                            upsideDown = true;
                            upsideUp = false;
                            upsideRight = false;
                            upsideLeft = false;
                        }

                        //flip upside right
                        if (Input.GetKey(KeyCode.LeftArrow) && isStickingRight)
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 270);
                            upsideRight = true;
                            upsideUp = false;
                        }

                        //flip upside left
                        if (Input.GetKey(KeyCode.RightArrow) && isStickingRight)
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 90);
                            upsideLeft = true;
                            upsideUp = false;
                        }
                }
                else //if rolling
                {
                    rb.gravityScale = 4.0f;
                    moveFactor = 8f;

                    //flip the snail sprite based on movement
                    if (moveBy.x < 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    if (moveBy.x > 0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    if (Input.GetKey(KeyCode.X) && isGrounded)
                    {
                        if (canJump)
                        {
                            rb.velocity = Vector2.up * jumpHeight;
                            isGrounded = false;
                            canJump = false;
                            StartCoroutine(JumpAllowDelay());
                        }
                    }
                    if (
                        moveBy.x == 0 &&
                        (
                        !Input.GetKey(KeyCode.LeftArrow) &&
                        !Input.GetKey(KeyCode.RightArrow)
                        )
                    )
                    {
                        myAnimationController.SetBool("Moving", false);
                        myAnimationController.SetBool("RollUp", false);
                        isRolling = false;
                        isShelled = false;
                    }
                }
            }
            else if (upsideDown)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
                rb.velocity =
                    new Vector2(moveBy.x * moveFactor, moveBy.y * moveFactor);
                rb.gravityScale = 0.0f;
                // flip the snail sprite based on movement
                if (moveBy.x < 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                if (moveBy.x > 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                //adjust the snail orientation states
                if (!isGrounded)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    upsideUp = true;
                    upsideDown = false;
                }
                if (Input.GetKey(KeyCode.LeftArrow) && isStickingRight)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 270);
                    upsideRight = true;
                    upsideDown = false;
                }
                if (Input.GetKey(KeyCode.RightArrow) && isStickingRight)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    upsideLeft = true;
                    upsideDown = false;
                }
                if (Input.GetKey(KeyCode.DownArrow) && isStickingTop)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        upsideDown = false;
                        upsideUp = true;
                    }
            }
            else if (upsideRight)
            {
                transform.rotation = Quaternion.Euler(0, 0, 270);

                rb.velocity =
                    new Vector2(moveBy.x * moveFactor, moveBy.y * moveFactor);
                rb.gravityScale = 3.0f;
                if (moveBy.y < 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                if (moveBy.y > 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

                //snail walljumping
                if (Input.GetKey(KeyCode.X) && isGrounded)
                {
                    if (canJump)
                    {
                        rb.velocity = new Vector2(80, 15);
                        isGrounded = false;
                        canJump = false;
                        StartCoroutine(JumpAllowDelay());
                    }
                }

                //adjust the snail orientation states
                if (
                    (
                    Input.GetKey(KeyCode.DownArrow) ||
                    Input.GetKey(KeyCode.RightArrow)
                    ) &&
                    isStickingRight
                )
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    upsideUp = true;
                    upsideRight = false;
                }
                if (Input.GetKey(KeyCode.UpArrow) && isStickingRight)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    upsideDown = true;
                    upsideRight = false;
                }
                if (Input.GetKey(KeyCode.RightArrow) && isStickingTop)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    upsideLeft = true;
                    upsideRight = false;
                }
            }
            else if (upsideLeft)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
                rb.velocity =
                    new Vector2(moveBy.x * moveFactor, moveBy.y * moveFactor);
                rb.gravityScale = 3.0f;
                if (moveBy.y < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if (moveBy.y > 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

                //snail walljumping
                if (Input.GetKey(KeyCode.X) && isGrounded)
                {
                    if (canJump)
                    {
                        rb.velocity = new Vector2(-80, 15);
                        isGrounded = false;
                        canJump = false;
                        StartCoroutine(JumpAllowDelay());
                    }
                }

                //adjust the snail orientation states
                if (
                    (
                    Input.GetKey(KeyCode.DownArrow) ||
                    Input.GetKey(KeyCode.LeftArrow)
                    ) &&
                    isStickingRight
                )
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    upsideUp = true;
                    upsideLeft = false;
                }
                if (Input.GetKey(KeyCode.UpArrow) && isStickingRight)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    upsideDown = true;
                    upsideLeft = false;
                }
                if (Input.GetKey(KeyCode.LeftArrow) && isStickingTop)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    upsideRight = true;
                    upsideLeft = false;
                }
            }
            if (
                !isGrounded &&
                !isStickingTop &&
                !isStickingRight
            )
            { //if airborne, reorient to upsideUp
                rb.velocity =
                    new Vector2(moveBy.x * moveFactor + rb.velocity.x/2,
                        rb.velocity.y);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rb.gravityScale = 3.0f;
                upsideUp = true;
                upsideLeft = false;
                upsideRight = false;
                upsideDown = false;
            }
            }
            else
            {  //if you can't move, stop movement except for gravity
                rb.velocity = new Vector2(0, rb.velocity.y);
                if (isShelled)
                {
                    timeToRoll -= Time.deltaTime;
                    if (timeToRoll < 0)
                    {
                        GlobalControl.Instance.canMove = true;
                    }
                }
            }
        }
    }    
}

