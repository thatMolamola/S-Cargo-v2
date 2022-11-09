using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTimes : MonoBehaviour
{

    private GlobalControl globalController;
    private string path;

    // Start is called before the first frame update
    void Start()
    {
        globalController =
            GameObject.Find("GameManager").GetComponent<GlobalControl>();
    }
}
