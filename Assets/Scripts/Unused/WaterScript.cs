using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is designed to allow the player to undergo different movement when
// inside of the water collider.
public class WaterScript : MonoBehaviour
{

    private GlobalControl globalController;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        globalController =
            GameObject.Find("GameManager").GetComponent<GlobalControl>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            {
                rb.mass = 2.5f;
                rb.drag = 5;
            }
        
    }
    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (Input.GetKey(KeyCode.X)) {
                if (globalController.canMove) {
                    rb.velocity = Vector2.up * 6f;
                }
            }
            rb.mass = 2.5f;
            rb.drag = 5;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            {
                rb.mass = 5;
                rb.drag = 0;
            }
        
    }
}