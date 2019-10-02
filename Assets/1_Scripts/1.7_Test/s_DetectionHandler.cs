using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_DetectionHandler : MonoBehaviour
{
    public bool m_coroutineBuffer;
    public bool m_keepGoing;
    public Coroutine m_detectionCouroutine;
    public GameObject m_punctuationHolder;
    public GameObject m_mainCam;
    public GameObject m_ISObj;
    public Color lerpedColor = Color.yellow;
    public string m_punctuation;

    private void Start()
    {
        m_mainCam = GameManager.instance.mainCamera;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            if (!this.transform.GetChild(0).gameObject.activeSelf)
            {
                this.transform.GetChild(0).gameObject.SetActive(true);
                m_punctuation = "?";
                DoDetection();
            }

            else
            {
                this.transform.GetChild(0).gameObject.SetActive(false);
                DoDetection();
            }
        }
    }

    public void DoDetection()
    {

        if (!this.transform.GetChild(0).gameObject.activeSelf)
        {
            m_keepGoing = false;
            if (m_detectionCouroutine != null) StopCoroutine(m_detectionCouroutine);
            m_ISObj = this.transform.GetChild(0).transform.GetChild(0).gameObject;
            m_ISObj.SetActive(false);
        }

        else
        {
            m_keepGoing = true;
            // make sure it doesn't ontrigger twice and there's a buffer
            m_coroutineBuffer = true;

            // start the coroutine
            this.transform.GetChild(0).GetComponent<s_Detector>().DoCoroutine();
        }
    }
}
