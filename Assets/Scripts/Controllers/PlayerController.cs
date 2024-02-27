using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script controls the player movement across 4 orientations and one substate of rolling
public enum SnailOrient{UP, DOWN, LEFT, RIGHT}
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D snailBoxCollider;
    private CircleCollider2D snailCircleColliderLeft;
    private Vector2 moveBy;
    private float moveFactor1 = 4f;
    private float moveFactor2 = 10f;

    [SerializeField] private Animator myAnimationController;

    //snail movement booleans
    [SerializeField] private bool isGrounded = true;

    public bool isShelled, isRolling;

    public bool isJumping = false;
    private bool reorientDD = true;

    //jumping and rolling float values

    private float jumpHeight = 9.5f;
    private float rolljumpHeight = 7f;

    //snail orientation booleans

    public SnailOrient orientPlayer; 

    private bool isStickingRight, isStickingTop;

    //snail-to-tile sticking state checks
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private Transform groundCheck1, groundCheck2, groundCheck3;

    [SerializeField] private Transform rightCheck, topCheck;

    private float surroundCheckRadius;

    private bool oneJFlag;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        snailBoxCollider = GetComponent<BoxCollider2D>();
        snailCircleColliderLeft = GetComponent<CircleCollider2D>();
        snailCircleColliderLeft.enabled = false;
        rb.gravityScale = 2.0f;
        surroundCheckRadius = .125f;
        orientPlayer = SnailOrient.UP;
    }

//this handles the player inputs, and sets the appropriate flags to trigger the physics in the FixedUpdate
    public void Update() {

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
        
        if (!GlobalControl.Instance.pause && GlobalControl.Instance.canMove){
            moveBy.x = Input.GetAxisRaw("Horizontal");
            moveBy.y = Input.GetAxisRaw("Vertical");
            //the player will be in one of 4 states defined by the snailOrient
            if (orientPlayer == SnailOrient.UP) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
                snailFlip(moveBy.x, true);
                if (!isRolling) {  //Standard Snail Movement
                    if (isGrounded)
                    {
                        //If grounded and player clicks to jump, set Jump Flags
                        if (Input.GetKeyDown(KeyCode.X) && !oneJFlag)
                        {
                            oneJFlag = true;
                        }

                        //If grounded and player clicks to retreat, change Snail State
                        if (Input.GetKey(KeyCode.S))
                        {
                            myAnimationController.SetBool("RollUp", true);
                            isShelled = true;
                            snailBoxCollider.enabled = false;
                            snailCircleColliderLeft.enabled = true;
                            GlobalControl.Instance.canMove = false;
                            StartCoroutine(shellToRollDelay());
                        }

                        //snail start rolling
                        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && isShelled) {
                            myAnimationController.SetBool("Moving", true);
                            isRolling = true;
                        }
                    }
                    
                    //Update Jump Inputs
                    //these jumping scripts allows for variable jump heights
                    if (Input.GetKey(KeyCode.X) && isJumping) {
                        StartCoroutine(VariableJump());
                    }
                
                    if (Input.GetKeyUp(KeyCode.X))
                        {
                            isJumping = false; 
                            oneJFlag = false;
                        }


                    //Change the snail orientation state conditions: 
                    //If the reorient delay is over
                    if (reorientDD){
                        //flip upside down
                        if (isStickingTop && Input.GetKey(KeyCode.UpArrow)) {
                            reorientDD = false;
                            orientPlayer = SnailOrient.DOWN;
                            StartCoroutine(reorient(.35f));
                        }

                        //flip upside right
                        if (Input.GetKey(KeyCode.LeftArrow) && isStickingRight)
                        {
                            reorientDD = false;
                            orientPlayer = SnailOrient.RIGHT;
                            StartCoroutine(reorient(.35f));
                        }

                        //flip upside left
                        if (Input.GetKey(KeyCode.RightArrow) && isStickingRight)
                        {
                            reorientDD = false;
                            orientPlayer = SnailOrient.LEFT;
                            StartCoroutine(reorient(.35f));
                        }
                    }
                } else {        
                    //if you stop moving in the rolling state and are holding down no keys, return to base state
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

                    if (isGrounded)
                    {
                        //If grounded and player clicks to jump, set Jump Flags
                        if (Input.GetKeyDown(KeyCode.X) && !oneJFlag)
                        {
                            oneJFlag = true;
                        }
                    }
                }
            } else if (orientPlayer == SnailOrient.DOWN) {
                transform.rotation = Quaternion.Euler(0, 0, 180);
                // flip the snail sprite based on movement
                snailFlip(moveBy.x, false);         

                //adjust the snail orientation states
                if (reorientDD){
                    if (Input.GetKey(KeyCode.LeftArrow) && isStickingRight)
                        {
                        reorientDD = false;
                        orientPlayer = SnailOrient.RIGHT;
                        StartCoroutine(reorient(.35f));
                        }
                    if (Input.GetKey(KeyCode.RightArrow) && isStickingRight)
                        {
                        reorientDD = false;
                        orientPlayer = SnailOrient.LEFT;
                        StartCoroutine(reorient(.35f));
                        }
            
                    if (Input.GetKey(KeyCode.DownArrow) && isStickingTop)
                        {
                        reorientDD = false;
                        orientPlayer = SnailOrient.UP;
                        StartCoroutine(reorient(.35f));
                        }
                }
                
            } else if (orientPlayer == SnailOrient.RIGHT) {
                transform.rotation = Quaternion.Euler(0, 0, 270);
                snailFlip(moveBy.y, false);    

                //snail walljumping
                if (Input.GetKey(KeyCode.X) && isGrounded && !oneJFlag)
                {
                    oneJFlag = true;
                }

                if (reorientDD){
                    if ((Input.GetKey(KeyCode.DownArrow) ||Input.GetKey(KeyCode.RightArrow)) 
                        &&
                        isStickingRight)
                    {
                        reorientDD = false;
                        orientPlayer = SnailOrient.UP;
                        StartCoroutine(reorient(.35f));
                    }
                    if (Input.GetKey(KeyCode.UpArrow) && isStickingRight) {
                        reorientDD = false;
                        orientPlayer = SnailOrient.DOWN;
                        StartCoroutine(reorient(.35f));
                    }
                    if (Input.GetKey(KeyCode.RightArrow) && isStickingTop) {
                        reorientDD = false;
                        orientPlayer = SnailOrient.LEFT;
                        StartCoroutine(reorient(.35f));
                    }
                }
            } else if (orientPlayer == SnailOrient.LEFT) {
                transform.rotation = Quaternion.Euler(0, 0, 90);
                snailFlip(moveBy.y, true);  

                //snail walljumping
                if (Input.GetKey(KeyCode.X) && isGrounded && !oneJFlag)
                {
                    oneJFlag = true;
                }
            
                if (reorientDD){
                    if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow)) 
                        &&
                        isStickingRight)
                    {
                        reorientDD = false;
                        orientPlayer = SnailOrient.UP;
                        StartCoroutine(reorient(.35f));
                    }
                    if (Input.GetKey(KeyCode.UpArrow) && isStickingRight)
                    {
                        reorientDD = false;
                        orientPlayer = SnailOrient.DOWN;
                        StartCoroutine(reorient(.35f));
                    }
                    if (Input.GetKey(KeyCode.LeftArrow) && isStickingTop)
                    {
                        reorientDD = false;
                        orientPlayer = SnailOrient.RIGHT;
                        StartCoroutine(reorient(.35f));
                    }
                }
            }
        }
    }    


//Fixed Update should handle all physics changes
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
                Physics2D.OverlapCircle(topCheck.position, surroundCheckRadius * 2.5f, whatIsGround);
            if (GlobalControl.Instance.canMove){
                if (isGrounded) {
                    //the player will be in one of 4 states defined by the snailOrient
                    if (orientPlayer == SnailOrient.UP) {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        rb.gravityScale = 2.0f;
                        if (!isRolling) {
                            //snail jumping
                            if (oneJFlag)
                            {
                                oneJFlag = false;
                                rb.velocity = new Vector2(moveBy.x * moveFactor1, jumpHeight);
                                isJumping = true;
                            } else {
                                rb.velocity = new Vector2(moveBy.x * moveFactor1, rb.velocity.y);
                            }
                        } else {    //if rolling
                            isGrounded = Physics2D.OverlapCircle(groundCheck3.position, surroundCheckRadius, whatIsGround);
                            if (oneJFlag)
                            {
                                oneJFlag = false;
                                rb.velocity = new Vector2(moveBy.x * moveFactor2, rolljumpHeight); 
                                isGrounded = false;
                            } else {
                                rb.velocity = new Vector2(moveBy.x * moveFactor2, rb.velocity.y);
                            }
                        }

                    } else if (orientPlayer == SnailOrient.DOWN) {
                        rb.velocity = new Vector2(moveBy.x * moveFactor1, moveBy.y);
                        rb.gravityScale = 0.0f;

                    } else if (orientPlayer == SnailOrient.RIGHT) {     //RIGHT
                        rb.gravityScale = 2.0f;

                        //snail walljumping
                        if (oneJFlag)  //Walljump motion
                        {
                            oneJFlag = false;
                            rb.velocity = new Vector2(24, 12);
                        } else {           //Standard motion
                            rb.velocity = new Vector2(moveBy.x * moveFactor1, moveBy.y * moveFactor1);
                        }

                    } else if (orientPlayer == SnailOrient.LEFT) {      //LEFT

                        rb.gravityScale = 2.0f;
                        //snail walljumping
                        if (oneJFlag) {  //Walljump motion
                            oneJFlag = false;
                            rb.velocity = new Vector2(-24, 12);
                        } else {             //Standard motion
                            rb.velocity = new Vector2(moveBy.x * moveFactor1, moveBy.y * moveFactor1);
                        }
                    }
                } else { 
                    if (isRolling) {
                        rb.velocity = new Vector2(moveBy.x * moveFactor2, rb.velocity.y);
                    } else {
                        rb.velocity = new Vector2(moveBy.x * moveFactor1, rb.velocity.y);
                    }

                    if (reorientDD){
                            //if airborne, reorient to UP
                            rb.gravityScale = 2.0f;
                            orientPlayer = SnailOrient.UP;
                    }
                }
            } else{
                rb.velocity = new Vector2(0,rb.velocity.y);
            }
        }
    }    

    void snailFlip(float movement, bool pos) {
        //flip the snail sprite based on movement
        if (pos) {
            if (movement < 0) {
                transform.localScale = new Vector3(-1, 1, 1);
            } else if (movement > 0) {
                transform.localScale = new Vector3(1, 1, 1);
            }
        } else {
            if (movement < 0) {
                transform.localScale = new Vector3(1, 1, 1);
            } else if (movement > 0) {
                transform.localScale = new Vector3(-1, 1, 1);
            }
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

    IEnumerator reorient(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        reorientDD = true;
    }
}