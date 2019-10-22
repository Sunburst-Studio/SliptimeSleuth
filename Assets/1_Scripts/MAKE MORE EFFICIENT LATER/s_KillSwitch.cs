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

            // tyler i'm so sorry 10/22/19
            if (this.tag == "Safe Panel")
            {
                this.transform.parent.GetComponent<s_Safe>().canType = false;
                //this.GetComponent<s_Safe>().canType = false;
            }

            //else Debug.Log("bingbong");
        }
    }
}
