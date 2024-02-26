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
    private float moveFactor1 = 4f;
    private float moveFactor2 = 10f;

    [SerializeField] private Animator myAnimationController;

    //snail movement booleans
    public bool isGrounded = true;

    public bool isShelled;

    public bool isJumping = false;

    public bool isRolling;

    private bool newOrientDelayDone = true;

    //jumping and rolling float values

    private float jumpHeight = 9.5f;
    private float rolljumpHeight = 7f;

    //snail orientation booleans

    public SnailOrient orientPlayer; 

    public bool isStickingRight;

    public bool isStickingTop;

    //snail-to-tile sticking state checks
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private Transform groundCheck1, groundCheck2, groundCheck3;

    [SerializeField] private Transform rightCheck, topCheck;

    private float surroundCheckRadius;

    private bool fJumpTrigger;


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

    IEnumerator newOrient(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        newOrientDelayDone = true;
    }

    IEnumerator stickDelay() {
        yield return new WaitForSeconds(.100f);
        isStickingTop = false;
    }

//this handles the player inputs
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
        
        if (!GlobalControl.Instance.pause){
            if (GlobalControl.Instance.canMove){
                moveBy.x = Input.GetAxisRaw("Horizontal");
                moveBy.y = Input.GetAxisRaw("Vertical");
                //the player will be in one of 4 states defined by the snailOrient
                if (orientPlayer == SnailOrient.UP) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                    if (!isRolling) {
                        snailFlip();

                        //Update Jump Inputs
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
                                fJumpTrigger = true;
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
                            if (isStickingTop)
                            {
                                Debug.Log("canGrab " +  Time.deltaTime);
                                if (Input.GetKey(KeyCode.UpArrow)) {
                                    Debug.Log("didGrab " +  Time.deltaTime);
                                    newOrientDelayDone = false;
                                    orientPlayer = SnailOrient.DOWN;
                                    StartCoroutine(newOrient(.35f));
                                }
                            }

                            //flip upside right
                            if (Input.GetKey(KeyCode.LeftArrow) && isStickingRight)
                            {
                                newOrientDelayDone = false;
                                orientPlayer = SnailOrient.RIGHT;
                                StartCoroutine(newOrient(.35f));
                            }

                            //flip upside left
                            if (Input.GetKey(KeyCode.RightArrow) && isStickingRight)
                            {
                                newOrientDelayDone = false;
                                orientPlayer = SnailOrient.LEFT;
                                StartCoroutine(newOrient(.35f));
                            }
                        }

                }
                else //if rolling
                {
                    snailFlip();
                    
                    if (Input.GetKey(KeyCode.X) && isGrounded)
                    {
                        fJumpTrigger = true;
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
                    if (Input.GetKey(KeyCode.LeftArrow) && isStickingRight)
                        {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.RIGHT;
                        StartCoroutine(newOrient(.35f));
                        }
                    if (Input.GetKey(KeyCode.RightArrow) && isStickingRight)
                        {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.LEFT;
                        StartCoroutine(newOrient(.35f));
                        }
            
                    if (Input.GetKey(KeyCode.DownArrow) && isStickingTop)
                        {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.UP;
                        StartCoroutine(newOrient(.35f));
                        }
                }
                
            }
            else if (orientPlayer == SnailOrient.RIGHT)
            {
                transform.rotation = Quaternion.Euler(0, 0, 270);
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
                    fJumpTrigger = true;
                }

                if (newOrientDelayDone){
                    if ((Input.GetKey(KeyCode.DownArrow) ||Input.GetKey(KeyCode.RightArrow)) 
                        &&
                        isStickingRight)
                    {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.UP;
                        StartCoroutine(newOrient(.35f));
                    }
                    if (Input.GetKey(KeyCode.UpArrow) && isStickingRight) {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.DOWN;
                        StartCoroutine(newOrient(.35f));
                    }
                    if (Input.GetKey(KeyCode.RightArrow) && isStickingTop) {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.LEFT;
                        StartCoroutine(newOrient(.35f));
                    }
                }
            }
            else if (orientPlayer == SnailOrient.LEFT) {
                transform.rotation = Quaternion.Euler(0, 0, 90);
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
                    fJumpTrigger = true;
                }
                
                if (newOrientDelayDone){
                    if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow)) 
                        &&
                        isStickingRight)
                    {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.UP;
                        StartCoroutine(newOrient(.35f));
                    }
                    if (Input.GetKey(KeyCode.UpArrow) && isStickingRight)
                    {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.DOWN;
                        StartCoroutine(newOrient(.35f));
                    }
                    if (Input.GetKey(KeyCode.LeftArrow) && isStickingTop)
                    {
                        newOrientDelayDone = false;
                        orientPlayer = SnailOrient.RIGHT;
                        StartCoroutine(newOrient(.35f));
                    }
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
                //the player will be in one of 4 states defined by the snailOrient
                if (orientPlayer == SnailOrient.UP) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rb.gravityScale = 2.0f;
                    if (!isRolling) {
                        rb.velocity = new Vector2(moveBy.x * moveFactor1, rb.velocity.y);
                        if (isGrounded)
                        {
                            //snail jumping
                            if (fJumpTrigger)
                            {
                                rb.velocity = Vector2.up * jumpHeight;
                                isJumping = true;
                                fJumpTrigger = false;
                            }
                        }
                    }
                    else //if rolling
                    {
                        rb.velocity = new Vector2(moveBy.x * moveFactor2, rb.velocity.y);
                        isGrounded = Physics2D.OverlapCircle(groundCheck3.position, surroundCheckRadius, whatIsGround);
                        if (fJumpTrigger)
                        {
                            rb.velocity = Vector2.up * rolljumpHeight;
                            isGrounded = false;
                            fJumpTrigger = false;
                        }
                    }
                }
                else if (orientPlayer == SnailOrient.DOWN)
                {
                    rb.velocity =
                        new Vector2(moveBy.x * moveFactor1, moveBy.y);
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
                }
                else if (orientPlayer == SnailOrient.RIGHT)
                {
                    rb.velocity =
                        new Vector2(moveBy.x * moveFactor1, moveBy.y * moveFactor1);
                    rb.gravityScale = 2.0f;
                    if (moveBy.y < 0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    if (moveBy.y > 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }

                    //snail walljumping
                    if (fJumpTrigger)
                    {
                        rb.velocity = new Vector2(80, 15);
                        isGrounded = false;
                        fJumpTrigger = false;
                    }

                } else if (orientPlayer == SnailOrient.LEFT) {
                    rb.velocity =
                        new Vector2(moveBy.x * moveFactor1, moveBy.y * moveFactor1);
                    rb.gravityScale = 2.0f;
                    if (moveBy.y < 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    if (moveBy.y > 0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }

                    //snail walljumping
                    if (fJumpTrigger)
                    {
                        rb.velocity = new Vector2(-80, 15);
                        isGrounded = false;
                        fJumpTrigger = false;
                    }
                }
                
                if (!isGrounded)
                { 
                    if (newOrientDelayDone){
                        //if airborne, reorient to UP
                        newOrientDelayDone = false;
                        rb.velocity =
                            new Vector2(moveBy.x * moveFactor1 + rb.velocity.x/2,
                                rb.velocity.y);
                        rb.gravityScale = 2.0f;
                        orientPlayer = SnailOrient.UP;
                        StartCoroutine(newOrient(.1f));
                    }
                }
            } else{
                rb.velocity = new Vector2(0,rb.velocity.y);
            }
        }
    }    
}