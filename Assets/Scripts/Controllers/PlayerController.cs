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

    private bool newOrientDelayDone = true;

    //jumping and rolling float values

    private float jumpHeight = 12.0f;

    //snail orientation booleans

    public SnailOrient orientPlayer; 

    public bool isStickingRight;

    public bool isStickingTop;

    //snail-to-tile sticking state checks
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private Transform groundCheck1, groundCheck2, groundCheck3;

    [SerializeField] private Transform rightCheck, topCheck;

    private float surroundCheckRadius;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        snailBoxCollider = GetComponent<BoxCollider2D>();
        snailCircleColliderLeft = GetComponent<CircleCollider2D>();
        snailCircleColliderLeft.enabled = false;
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

    IEnumerator newOrient() {
        yield return new WaitForSeconds(0.35f);
        newOrientDelayDone = true;
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
                                StartCoroutine(shellToRollDelay());
                            }
                        }

                        //snail start rolling
                        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && isShelled) {
                            myAnimationController.SetBool("Moving", true);
                            isRolling = true;
                        }

                        //change the snail orientation states
                        //flip upside down
                        if (newOrientDelayDone){
                            if (Input.GetKey(KeyCode.UpArrow) && isStickingTop)
                            {
                                newOrientDelayDone = false;
                                orientPlayer = SnailOrient.DOWN;
                                StartCoroutine(newOrient());
                            }

                            //flip upside right
                            if (Input.GetKey(KeyCode.LeftArrow) && isStickingRight)
                            {
                                newOrientDelayDone = false;
                                orientPlayer = SnailOrient.RIGHT;
                                StartCoroutine(newOrient());
                            }

                            //flip upside left
                            if (Input.GetKey(KeyCode.RightArrow) && isStickingRight)
                            {
                                newOrientDelayDone = false;
                                orientPlayer = SnailOrient.LEFT;
                                StartCoroutine(newOrient());
                            }
                        }

                }
                else //if rolling
                {
                    rb.gravityScale = 4.0f;
                    moveFactor = 8f;
                    snailFlip();
                    isGrounded = Physics2D.OverlapCircle(groundCheck3.position, surroundCheckRadius, whatIsGround);
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
                    new Vector2(moveBy.x * moveFactor, moveBy.y);
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

                if (newOrientDelayDone){
                    //adjust the snail orientation states
                    if (!isGrounded)
                    {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.UP;
                        StartCoroutine(newOrient());
                    }

                    if (Input.GetKey(KeyCode.LeftArrow) && isStickingRight)
                        {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.RIGHT;
                        StartCoroutine(newOrient());
                        }
                    if (Input.GetKey(KeyCode.RightArrow) && isStickingRight)
                        {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.LEFT;
                        StartCoroutine(newOrient());
                        }
            
                    if (Input.GetKey(KeyCode.DownArrow) && isStickingTop)
                        {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.UP;
                        StartCoroutine(newOrient());
                        }
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

                if (newOrientDelayDone){
                    if ((Input.GetKey(KeyCode.DownArrow) ||Input.GetKey(KeyCode.RightArrow)) 
                        &&
                        isStickingRight)
                    {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.UP;
                        StartCoroutine(newOrient());
                    }
                    if (Input.GetKey(KeyCode.UpArrow) && isStickingRight) {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.DOWN;
                        StartCoroutine(newOrient());
                    }
                    if (Input.GetKey(KeyCode.RightArrow) && isStickingTop) {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.LEFT;
                        StartCoroutine(newOrient());
                    }
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
                
                if (newOrientDelayDone){
                    if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow)) 
                        &&
                        isStickingRight)
                    {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.UP;
                        StartCoroutine(newOrient());
                    }
                    if (Input.GetKey(KeyCode.UpArrow) && isStickingRight)
                    {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.DOWN;
                        StartCoroutine(newOrient());
                    }
                    if (Input.GetKey(KeyCode.LeftArrow) && isStickingTop)
                    {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.RIGHT;
                        StartCoroutine(newOrient());
                    }
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
            } else{
                rb.velocity = new Vector2(0,rb.velocity.y);
            }
        }
    }    
}