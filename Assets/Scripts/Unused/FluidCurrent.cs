using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidCurrent : MonoBehaviour
{

public Vector2 direction; 

void OnTriggerStay2D(Collider2D other)
{
    other.GetComponent<Rigidbody2D>().AddForce (direction*20000*Time.deltaTime);
}

}
