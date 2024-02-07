using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script is designed to disable and enable colliders, dependent on the character's y velocity, 
// such that the platforms act as one-way platforms
public class Platforms : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private BoxCollider2D platformCollider;


    // Start is called before the first frame update
    void Start()
    {
        platformCollider = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.y > 0) {
            platformCollider.enabled = false;
        } else {
            platformCollider.enabled = true;
        }
    }
}
