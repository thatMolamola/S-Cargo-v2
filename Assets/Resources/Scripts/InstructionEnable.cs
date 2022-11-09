using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script is designed to allow the player to change the 
// globalController.instructionsEnabled value from the Settings screen.
public class InstructionEnable : MonoBehaviour
{
    private GlobalControl globalController;
    private Button buttonInstruct;
    private Text iText;

    // Start is called before the first frame update
    void Start()
    {
        buttonInstruct = GameObject.Find("Instructions Button").GetComponent<Button>();
        iText = GameObject.Find("InstructionsText").GetComponent<Text>();
        globalController = GameObject.Find("GameManager").GetComponent<GlobalControl>();
        buttonInstruct.onClick.AddListener( () => {ChangeInstructionsState(); }  );
    }

    void ChangeInstructionsState() {
        if (globalController.instructionsEnabled) {
            iText.text = "Enable Instructions"; 
        } else {
            iText.text = "Disable Instructions";
        }
        globalController.instructionsEnabled = !globalController.instructionsEnabled;
    }
}
