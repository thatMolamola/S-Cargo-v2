using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this script is designed to allow the player to reset the level and access the pause menu and 
//instruction panel using the appropriate key inputs. 
public class LevelControl : MonoBehaviour
{
    private Scene thisScene;

    private GlobalControl globalController;

    public GameObject pausePanel;

    public GameObject instructionsPanel;

    private float timerP;
    private float timerI;
    private bool canOpenClose;

    // Start is called before the first frame update
    void Start()
    {
        globalController =
            GameObject.Find("GameManager").GetComponent<GlobalControl>();
            canOpenClose = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            thisScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(thisScene.name);
            globalController.lettersCollected = 0;
            globalController.allMailCollected = false;
            globalController.hasMoved = false;
            globalController.canMove = true;
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
            if(globalController.pause) {
                globalController.pause = false;
                pausePanel.SetActive(false);
                instructionsPanel.SetActive(false);
            } else {
                globalController.pause = true;
                instructionsPanel.SetActive(false);
                pausePanel.SetActive(true);
            }
    }

    public void instructions() {
        canOpenClose = false; 
            if(globalController.pause) {
                globalController.pause = false;
                instructionsPanel.SetActive(false);
                pausePanel.SetActive(false);
            } else {
                globalController.pause = true;
                instructionsPanel.SetActive(true);
                pausePanel.SetActive(false);
            }
    }
}
