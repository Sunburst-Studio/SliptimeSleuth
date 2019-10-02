using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Safe : MonoBehaviour
{
    // the needed code for the safe
    int m_firstCode;
    int m_secondCode;
    int m_thirdCode;
    int m_fourthCode;

    // every number a player enters
    public int m_firstEntry;
    public int m_secondEntry;
    public int m_thirdEntry;
    public int m_fourthEntry;

    // the code the player enters
    int m_enteredCode;
    bool m_unlocked;

    public GameObject wrongUI;
    public GameObject correctUI;
    public GameObject noKeyUI;
    public GameObject winUI;

    private void Start()
    {
        m_firstCode = 7;
        m_secondCode = 2;
        m_thirdCode = 9;
        m_fourthCode = 4;

        ResetCode();
    }

    public void AddNumber(int entry)
    {
        if(m_firstEntry == -1)
        {
            m_firstEntry = entry;
        }

        else if(m_secondEntry == -1)
        {
            m_secondEntry = entry;
        }

        else if(m_thirdEntry == -1)
        {
            m_thirdEntry = entry;
        }

        else if(m_fourthEntry == -1)
        {
            m_fourthEntry = entry;

            if(m_firstEntry == m_firstCode && m_secondEntry == m_secondCode && m_thirdEntry == m_thirdCode && m_fourthEntry == m_fourthCode)
            {
                m_unlocked = true;
                Debug.Log("CORRECT CODE");
                correctUI.SetActive(false);
                correctUI.SetActive(true);
            }
            
            else
            {
                ResetCode();
                Debug.Log("WRONG CODE");
                wrongUI.SetActive(false);
                wrongUI.SetActive(true);
            }
        }
    }

    public void UseKey()
    {
        if(m_unlocked)
        {
            if (GameManager.instance.inventory.GetComponent<s_Inventory>().HasItem("Safe Key"))
            {
                // CLOSE THE SAFE PANEL
                GameObject m_safePanel;
                GameObject m_realSafePanel;

                m_safePanel = FindObjectOfType<s_Safe>().gameObject;
                m_realSafePanel = m_safePanel.transform.GetChild(0).gameObject;
                m_realSafePanel.SetActive(false);
                Debug.Log("UNLOCKED!!!");
                winUI.SetActive(true);
                // CARRY ON FROM HERE. PLAY SAFE ANIMATION AND SET THE DOCUMENTS TO BE PICKED UP ACTIVE)
            }

            else
            {
                noKeyUI.SetActive(false);
                noKeyUI.SetActive(true);
                // mention that you need the key via dialogue   
                Debug.Log("YOU NEED THE KEY!");
            }
        }
    }

    public void ResetCode()
    {
        m_firstEntry = -1;
        m_secondEntry = -1;
        m_thirdEntry = -1;
        m_fourthEntry = -1;
    }
}
