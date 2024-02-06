using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script is designed to allow the player to change the 
// globalController.cutsceneEnabled value from the Settings screen.
public class CutsceneButton : MonoBehaviour
{

    private GlobalControl globalController;
    private Button buttonCutscene;
    private Text cutsceneText;

    // Start is called before the first frame update
    void Start()
    {
        buttonCutscene = GameObject.Find("CutScene Button").GetComponent<Button>();
        cutsceneText = GameObject.Find("CutsceneText").GetComponent<Text>();
        globalController = GameObject.Find("GameManager").GetComponent<GlobalControl>();
        buttonCutscene.onClick.AddListener( () => {ChangeCutsceneState(); }  );
    }

    void ChangeCutsceneState() {
        if (globalController.cutsceneEnabled) {
            cutsceneText.text = "Enable Cutscenes"; 
        } else {
            cutsceneText.text = "Disable Cutscenes";
        }
        globalController.cutsceneEnabled = !globalController.cutsceneEnabled;
    }
}
