using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this script is designed to allow the player to reset the level and access the pause menu and 
//instruction panel using the appropriate key inputs. 
public class LevelControl : MonoBehaviour
{
    private Scene thisScene;

    public GameObject pausePanel, instructionsPanel;

    private float timerP, timerI;
    private bool canOpenClose = true;
    [SerializeField] private Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.name);
            GlobalControl.Instance.lettersCollected = 0;
            GlobalControl.Instance.allMailCollected = false;
            GlobalControl.Instance.hasMoved = false;
            GlobalControl.Instance.canMove = true;
        }
        if (Input.GetKey(KeyCode.P) && canOpenClose)
        {
            unpause();
        }
        if (Input.GetKey(KeyCode.I) && canOpenClose)
        {
            instructions();
        }   
        if (!canOpenClose)
            {
                timerI += Time.deltaTime;
                timerP += Time.deltaTime;
                if (timerI > .5 || timerP > .5)
                {
                    canOpenClose = true;
                    timerI = 0;
                    timerP = 0;
                }
            }     
    }
    public void unpause() {
        canOpenClose = false; 
            if(GlobalControl.Instance.pause) {
                GlobalControl.Instance.pause = false;
                pausePanel.SetActive(false);
                instructionsPanel.SetActive(false);
                rb.bodyType = RigidbodyType2D.Dynamic;
            } else {
                GlobalControl.Instance.pause = true;
                instructionsPanel.SetActive(false);
                pausePanel.SetActive(true);
                rb.bodyType = RigidbodyType2D.Static;
            }
    }

    public void instructions() {
        canOpenClose = false; 
            if(GlobalControl.Instance.pause) {
                GlobalControl.Instance.pause = false;
                instructionsPanel.SetActive(false);
                pausePanel.SetActive(false);
            } else {
                GlobalControl.Instance.pause = true;
                instructionsPanel.SetActive(true);
                pausePanel.SetActive(false);
            }
    }
}
