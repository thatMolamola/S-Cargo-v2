using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//this script is designed to load scenes for whatever buttons may be trying to do so
public class SceneLoader : MonoBehaviour
{
    private CutsceneControl cutsceneScript;
    
    public void LoadMenu()
    {
        SceneManager.LoadScene("Scene_Menu");
    }

    public void LoadPlayLevel1_1()
    {
        GlobalControl.Instance.allMailCollected = false;
        GlobalControl.Instance.lettersCollected = 0;
        GlobalControl.Instance.hasMoved = false;
        GlobalControl.Instance.canMove = true; 
        SceneManager.LoadScene("Scene_Level1_1"); 
    }

    public void LoadPlayLevel1_2()
    {
        GlobalControl.Instance.allMailCollected = false;
        GlobalControl.Instance.lettersCollected = 0;
        GlobalControl.Instance.hasMoved = false;
        GlobalControl.Instance.canMove = true;  
        SceneManager.LoadScene("Scene_Level1_2"); 
    }

    public void LoadPlayLevel2()
    {
        GlobalControl.Instance.allMailCollected = false;
        GlobalControl.Instance.lettersCollected = 0;
        GlobalControl.Instance.hasMoved = false;
        GlobalControl.Instance.canMove = true;
        SceneManager.LoadScene("Scene_Level2_1");
    }

     public void LoadPlayLevel3()
    {
        GlobalControl.Instance.allMailCollected = false;
        GlobalControl.Instance.lettersCollected = 0;
        GlobalControl.Instance.hasMoved = false;
        GlobalControl.Instance.canMove = true;
        SceneManager.LoadScene("Scene_Level3_1");
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
        GlobalControl.Instance.allMailCollected = false;
        GlobalControl.Instance.lettersCollected = 0;
        GlobalControl.Instance.hasMoved = false;
        GlobalControl.Instance.canMove = true;
        SceneManager.LoadScene("Scene_LevelSelect");
    }

    public void ReloadThisScene(){
        GlobalControl.Instance.allMailCollected = false;
        GlobalControl.Instance.lettersCollected = 0;
        GlobalControl.Instance.hasMoved = false;
        GlobalControl.Instance.canMove = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
