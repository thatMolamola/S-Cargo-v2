using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script controls the player movement. 
public enum SnailOrient{UP, DOWN, LEFT, RIGHT}
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

    public bool isShelled;

    public bool isJumping = false;

    public bool isRolling;

    //jumping and rolling float values

    private float jumpHeight = 12.0f;

    //snail orientation booleans

    public SnailOrient orientPlayer; 

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
        orientPlayer = SnailOrient.UP;
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
    }

    void snailFlip() {
        //flip the snail sprite based on movement
        if (moveBy.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (moveBy.x > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    IEnumerator shellToRollDelay(){
        yield return new WaitForSeconds(1.5f);
        GlobalControl.Instance.canMove = true;
    }

    IEnumerator VariableJump() {
        rb.velocity = Vector2.up * jumpHeight;
        yield return new WaitForSeconds(0.25f);
        isJumping = false;
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
                //the player will be in one of 4 states defined by the snailOrient
                if (orientPlayer == SnailOrient.UP) {
                rb.velocity = new Vector2(moveBy.x * moveFactor, rb.velocity.y);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                    if (!isRolling) {
                        rb.gravityScale = 3.0f;
                        moveFactor = 4.2f;
                        snailFlip();

                        //this jumping script allows for variable jump heights
                        if (Input.GetKey(KeyCode.X) && isJumping) {
                            StartCoroutine(VariableJump());
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
                            }

                            //snail retreating
                            if (Input.GetKey(KeyCode.S))
                            {
                                myAnimationController.SetBool("RollUp", true);
                                isShelled = true;
                                snailBoxCollider.enabled = false;
                                snailCircleColliderLeft.enabled = true;
                                GlobalControl.Instance.canMove = false;
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
                            orientPlayer = SnailOrient.DOWN;
                        }

                        //flip upside right
                        if (Input.GetKey(KeyCode.LeftArrow) && isStickingRight)
                        {
                            orientPlayer = SnailOrient.RIGHT;
                        }

                        //flip upside left
                        if (Input.GetKey(KeyCode.RightArrow) && isStickingRight)
                        {
                            orientPlayer = SnailOrient.LEFT;
                        }
                }
                else //if rolling
                {
                    rb.gravityScale = 4.0f;
                    moveFactor = 8f;
                    snailFlip();
                    if (Input.GetKey(KeyCode.X) && isGrounded)
                    {
                        rb.velocity = Vector2.up * jumpHeight;
                        isGrounded = false;
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
                        snailBoxCollider.enabled = true;
                        snailCircleColliderLeft.enabled = false;
                    }
                }
            }
            else if (orientPlayer == SnailOrient.DOWN)
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
                    orientPlayer = SnailOrient.UP;
                }
                if (Input.GetKey(KeyCode.LeftArrow) && isStickingRight)
                {
                    orientPlayer = SnailOrient.RIGHT;
                }
                if (Input.GetKey(KeyCode.RightArrow) && isStickingRight)
                {
                    orientPlayer = SnailOrient.LEFT;
                }
                if (Input.GetKey(KeyCode.DownArrow) && isStickingTop)
                {
                    orientPlayer = SnailOrient.UP;
                }
            }
            else if (orientPlayer == SnailOrient.RIGHT)
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
                    rb.velocity = new Vector2(80, 15);
                    isGrounded = false;
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
                    orientPlayer = SnailOrient.UP;
                }
                if (Input.GetKey(KeyCode.UpArrow) && isStickingRight) {
                    orientPlayer = SnailOrient.DOWN;
                }
                if (Input.GetKey(KeyCode.RightArrow) && isStickingTop) {
                    orientPlayer = SnailOrient.LEFT;
                }
            }
            else if (orientPlayer == SnailOrient.LEFT) {
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
                    rb.velocity = new Vector2(-80, 15);
                    isGrounded = false;
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
                    
                    orientPlayer = SnailOrient.UP;
                }
                if (Input.GetKey(KeyCode.UpArrow) && isStickingRight)
                {
                    orientPlayer = SnailOrient.DOWN;
                }
                if (Input.GetKey(KeyCode.LeftArrow) && isStickingTop)
                {
                    orientPlayer = SnailOrient.RIGHT;
                }
            }
                if (
                    !isGrounded &&
                    !isStickingTop &&
                    !isStickingRight
                )
                { //if airborne, reorient to UP
                    rb.velocity =
                        new Vector2(moveBy.x * moveFactor + rb.velocity.x/2,
                            rb.velocity.y);
                    rb.gravityScale = 3.0f;
                    orientPlayer = SnailOrient.UP;
                }
            }
            else
            {  //if you can't move, stop movement except for gravity
                rb.velocity = new Vector2(0, rb.velocity.y);
                if (isShelled)
                {
                    StartCoroutine(shellToRollDelay());
                }
            }
        }
    }    
}