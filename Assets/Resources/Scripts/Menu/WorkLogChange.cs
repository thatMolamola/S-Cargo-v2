using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkLogChange : MonoBehaviour
{
    private GlobalControl globalController;
    public SpriteRenderer spriteRenderer;
    public Sprite HerbChoice;
    public Sprite LaylaChoice;
    public Sprite FernChoice;
    public Sprite TheoChoice;
    public Sprite PierceChoice;
    public Sprite SquirmyChoice;

    // Start is called before the first frame update
    void Start()
    {
        globalController = GameObject.Find("GameManager").GetComponent<GlobalControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (globalController.snailChoice == 1) {
           spriteRenderer.sprite = HerbChoice;
        } else if (globalController.snailChoice == 2) {
            spriteRenderer.sprite = LaylaChoice;
        } else if (globalController.snailChoice == 3) {
            spriteRenderer.sprite = FernChoice;
        } else if (globalController.snailChoice == 4) {
            spriteRenderer.sprite = TheoChoice;
        } else if (globalController.snailChoice == 5) {
            spriteRenderer.sprite = PierceChoice;
        } else if (globalController.snailChoice == 6) {
            spriteRenderer.sprite = SquirmyChoice;
        }
    }
}
