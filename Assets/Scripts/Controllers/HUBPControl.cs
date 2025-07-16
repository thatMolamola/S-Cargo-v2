using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBPControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveBy;

    private float moveVelocity = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveBy.x = Input.GetAxisRaw("Horizontal");
        moveBy.y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(moveBy.x * moveVelocity, moveBy.y * moveVelocity);
        rb.gravityScale = 0.0f;
        snailFlip(moveBy.x);
    }

    void snailFlip(float movement) {
        //flip the snail sprite based on movement
        if (movement < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (movement > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
