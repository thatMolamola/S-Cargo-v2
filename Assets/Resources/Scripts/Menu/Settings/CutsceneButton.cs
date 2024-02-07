using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script is designed to allow the player to change the 
// globalController.cutsceneEnabled value from the Settings screen.
public class CutsceneButton : MonoBehaviour
{
    [SerializeField ]private Button buttonCutscene;
    [SerializeField] private Text cutsceneText;

    // Start is called before the first frame update
    void Start()
    {
        buttonCutscene.onClick.AddListener( () => {ChangeCutsceneState(); }  );
    }

    void ChangeCutsceneState() {
        if (GlobalControl.Instance.cutsceneEnabled) {
            cutsceneText.text = "Enable Cutscenes"; 
        } else {
            cutsceneText.text = "Disable Cutscenes";
        }
        GlobalControl.Instance.cutsceneEnabled = !GlobalControl.Instance.cutsceneEnabled;
    }
}
