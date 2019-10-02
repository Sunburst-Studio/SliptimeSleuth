using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_KillSwitch : MonoBehaviour
{
    //If we keep this script at release I'll kill all of you ~ Tyler <3
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
