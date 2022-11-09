using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this script is designed to load scenes for whatever buttons may be trying to do so
public class SceneLoader : MonoBehaviour
{

    private Scene thisScene;
    private GlobalControl globalController;
    private CutsceneControl cutsceneScript;

    void Start()
    {
        globalController = GameObject.Find("GameManager").GetComponent<GlobalControl>();
    }
    
    public void LoadMenu()
    {
        SceneManager.LoadScene("Scene_Menu");
    }

    public void LoadPlayLevel1_1()
    {
        globalController.allMailCollected = false;
        globalController.lettersCollected = 0;
        globalController.hasMoved = false;
        globalController.canMove = true; 
        SceneManager.LoadScene("Scene_Level1_1"); 
        cutsceneScript = GameObject.Find("EventSystem").GetComponent<CutsceneControl>();
        cutsceneScript.mailboxTrigger = false;
    }

    public void LoadPlayLevel1_2()
    {
        globalController.allMailCollected = false;
        globalController.lettersCollected = 0;
        globalController.hasMoved = false;
        globalController.canMove = true;  
        SceneManager.LoadScene("Scene_Level1_2"); 
        cutsceneScript = GameObject.Find("EventSystem").GetComponent<CutsceneControl>();
        cutsceneScript.mailboxTrigger = false;
    }

    public void LoadPlayLevel2()
    {
        globalController.allMailCollected = false;
        globalController.lettersCollected = 0;
        globalController.hasMoved = false;
        globalController.canMove = true;
        SceneManager.LoadScene("Scene_Level2_1");
        cutsceneScript = GameObject.Find("EventSystem").GetComponent<CutsceneControl>();
        cutsceneScript.mailboxTrigger = false;
    }

     public void LoadPlayLevel3()
    {
        globalController.allMailCollected = false;
        globalController.lettersCollected = 0;
        globalController.hasMoved = false;
        globalController.canMove = true;
        SceneManager.LoadScene("Scene_Level3_1");
        cutsceneScript = GameObject.Find("EventSystem").GetComponent<CutsceneControl>();
        cutsceneScript.mailboxTrigger = false;
    }

    public void LoadRollChar()
    {
        SceneManager.LoadScene("Scene_Char_Select");
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Scene_Settings");
    }

    public void LoadAbout()
    {
        SceneManager.LoadScene("Scene_About");
    }

    public void LoadLevelSelect()
    {
        globalController.allMailCollected = false;
        globalController.lettersCollected = 0;
        globalController.hasMoved = false;
        globalController.canMove = true;
        SceneManager.LoadScene("Scene_LevelSelect");
    }

    public void ReloadThisScene(){
        globalController.allMailCollected = false;
        globalController.lettersCollected = 0;
        globalController.hasMoved = false;
        globalController.canMove = true;
        thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    //When the Exit button is clicked, quit the application.
    public void doExitGame()
    {
#if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;   
#else  
          Application.Quit();
#endif
    }
}
