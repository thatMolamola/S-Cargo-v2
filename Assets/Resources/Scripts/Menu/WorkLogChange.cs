using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkLogChange : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] workLogSelectSprites;

    // Update is called once per frame
    public void onEmployeeClick()
    {
        spriteRenderer.sprite = workLogSelectSprites[GlobalControl.Instance.snailChoice];
    }
}
