using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class s_Timeslider : MonoBehaviour {

    public s_TimePeriod[] timePeriods;
    private Slider m_timeSlider;

    private void Awake()
    {
        m_timeSlider = GetComponent<Slider>();
        TimePeriodHandler();
    }


    private void Update()
    {
        TimePeriodHandler();
    }

    public void TimePeriodHandler()
    {
        foreach (s_TimePeriod t in timePeriods)
        {
            if (m_timeSlider.value <= t.maxTime && m_timeSlider.value >= t.minTime)
            {
                //GameManager.instance.animActive = false;
                t.timePeriodContents.SetActive(true);
            }
            else if (m_timeSlider.value >= t.maxTime || m_timeSlider.value <= t.minTime)
            {
                //t.timePeriodContents.GetComponent<s_Interactable>().m_animActive = false;
                t.timePeriodContents.SetActive(false);
            }
            if (t.timePeriodContents.activeSelf == false)
            {
                foreach(Transform _object in t.timePeriodContents.transform)
                if (_object.GetComponent<s_Interactable>() != null)
                {
                        //Debug.Log(t.timePeriodContents.);
                        //t.timePeriodContents.GetComponentInChildren<s_Interactable>().m_animActive = false;
                        //t.timePeriodContents.GetComponentInChildren<s_Interactable>().ableToInteract = false;

                        _object.GetComponent<s_Interactable>().m_animActive = false;
                        _object.GetComponent<s_Interactable>().ableToInteract = false;
                }
            }
        }
    }
}
