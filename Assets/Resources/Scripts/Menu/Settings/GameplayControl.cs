using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script is designed to allow the player to change the 
// cutscenes and instructions display from the Settings screen.
public class GameplayControl : MonoBehaviour
{
    [SerializeField] private Button buttonCutscene, buttonInstruct;
    [SerializeField] private Text cutsceneText, instructText;

    // Start is called before the first frame update
    void Start()
    {
        buttonCutscene.onClick.AddListener( () => {ChangeCutsceneState(); }  );
        buttonInstruct.onClick.AddListener( () => {ChangeInstructionsState(); }  );
    }

    private void ChangeCutsceneState() {
        if (GlobalControl.Instance.cutsceneEnabled) {
            cutsceneText.text = "Enable Cutscenes"; 
        } else {
            cutsceneText.text = "Disable Cutscenes";
        }
        GlobalControl.Instance.cutsceneEnabled = !GlobalControl.Instance.cutsceneEnabled;
    }

    private void ChangeInstructionsState() {
        if (GlobalControl.Instance.instructionsEnabled) {
            instructText.text = "Enable Instructions"; 
        } else {
            instructText.text = "Disable Instructions";
        }
        GlobalControl.Instance.instructionsEnabled = !GlobalControl.Instance.instructionsEnabled;
    }
}
