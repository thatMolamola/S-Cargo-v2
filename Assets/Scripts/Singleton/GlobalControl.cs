using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//this script is designed to hold all the values that need to persist between scenes 
// and those that need to be access by various scripts. 
public class GlobalControl : MonoBehaviour
{
    private static GlobalControl _instance; 
    public static GlobalControl Instance {get; private set;}

    public bool musicState;
    public int musicTrack;
    public int lettersCollected;
    
    public int snailChoice;
    
    public bool allMailCollected;
    public bool finishedDelivery;
    public float lowestTime1;
    public float lowestTime2;
    public float lowestTime3;

    public bool cutsceneEnabled;
    public bool instructionsEnabled;
    public int instructionsStep;
    public bool canMove;
    public bool hasMoved;
    public bool paused;

    public RuntimeAnimatorController[] snailAnims;

    private void Awake(){
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy (gameObject);
        }
    }
}
