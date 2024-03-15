using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private bool charging;

    [SerializeField] private Animator doorAnimator;

    void Update() {
        if (charging) {
            doorAnimator.SetBool("Charging", true);
        } else {
            doorAnimator.SetBool("Charging", false);
        }
    }
}
