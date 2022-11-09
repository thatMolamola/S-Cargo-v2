using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is designed to temporarily damage and immobilize the character
//upon contact with a hazard collider. 
public class HazardsTrigger : MonoBehaviour
{
    private GlobalControl globalController;
    private float hurtTimer;
    private float immobileTimer;
    private float immunityTimer;
    private bool hurt; 
    private bool immune;
    private bool inShell;
    public GameObject red;
    public SpriteRenderer player;
    public PlayerController PC;

    // Start is called before the first frame update
    void Start()
    {
        globalController =
            GameObject.Find("GameManager").GetComponent<GlobalControl>();
        immobileTimer = 2f;
        immunityTimer = 6f;
        hurtTimer = .35f;
        inShell = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hurt) {
            immobileTimer -= Time.deltaTime;
            immunityTimer -= Time.deltaTime;
            hurtTimer -= Time.deltaTime;
            if (!inShell) {
                player.color = new Color(.5f,0,0,1);
                if (hurtTimer > 0) {
                    red.SetActive(true);
                } else {
                    red.SetActive(false);
                }
            }
            if (immobileTimer < 0) {
                globalController.canMove = true; 
                player.color = new Color(1,1,1,1);
            }
            if (immunityTimer < 0 ){
                hurt = false; 
                immune = false;
                immobileTimer = 3f;
                immunityTimer = 6f;
                hurtTimer = .35f;
                
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            {
                if (PC.isRolling) {
                    inShell = true;
                } else {
                    inShell = false;
                }
                if (!immune) {
                    hurt = true;
                    globalController.canMove = false;
                    immune = true;
                }
            }
    }
}
