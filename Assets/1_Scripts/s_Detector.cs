using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class s_Detector : s_DetectionHandler
{
    [SerializeField]
    Vector3 playerTransform;

    private void Start()
    {
        m_ISObj = this.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!m_coroutineBuffer)
        {
            if (other.name == "pr_player_1")
            {
                //Debug.Log("bingbong");
                m_keepGoing = true;
                // make sure it doesn't ontrigger twice and there's a buffer
                m_coroutineBuffer = true;

                // start the coroutine
                DoCoroutine();
            }
        }

        GameManager.instance.timeSlider.gameObject.SetActive(false);
    }

    public void DoCoroutine()
    {
        m_detectionCouroutine = StartCoroutine(Detection());
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.name == "pr_player_1")
        {
            m_keepGoing = false;
            m_coroutineBuffer = false;
            StopCoroutine(m_detectionCouroutine);
            m_ISObj = this.transform.GetChild(1).gameObject;
            m_ISObj.GetComponent<TextMeshPro>().text = "";
            //m_ISObj.SetActive(false);
        }

        GameManager.instance.timeSlider.gameObject.SetActive(true);

    }

    public IEnumerator Detection()
    {
        m_ISObj = this.transform.GetChild(1).gameObject;
        m_ISObj.GetComponent<TextMeshPro>().text = "?";
        m_ISObj.GetComponent<TextMeshPro>().color = Color.yellow;
        m_ISObj.gameObject.SetActive(false);
        m_ISObj.gameObject.SetActive(true);
        //m_mainCam = GameManager.instance.mainCamera;

        //m_ISObj.SetActive(true);
        //m_ISObj.transform.LookAt(m_ISObj.transform.position +
        //    m_mainCam.transform.rotation * Vector3.forward, m_mainCam.transform.rotation * Vector3.up);

        float elapsedTime = 0.0f;
        float totalTime = 2.0f;

        while(elapsedTime < totalTime && m_keepGoing)
        {
            //Debug.Log("bong");
            elapsedTime += Time.deltaTime;
            m_ISObj.GetComponent<TextMeshPro>().color = Color.Lerp(Color.yellow, Color.red, (elapsedTime / totalTime));
            yield return null;
        }

        if(m_keepGoing)
        {
            // FUNCTION HERE TO RESET SLEUTH POSITION

            m_ISObj.GetComponent<TextMeshPro>().text = "!";
            yield return new WaitForSeconds(1); //TEMP: REMOVED THE --
            m_ISObj.GetComponent<TextMeshPro>().text = "";
            m_coroutineBuffer = false;
            Debug.Log("moved");

            GameManager.instance.player.transform.position = GameManager.instance.restartPoint.transform.position;
            m_keepGoing = false;
            m_coroutineBuffer = false;
            //m_ISObj = this.transform.GetChild(1).gameObject;
            //m_ISObj.SetActive(false);

            GameManager.instance.timeSlider.gameObject.SetActive(true);
            StopCoroutine(m_detectionCouroutine);
        }
    }
}
