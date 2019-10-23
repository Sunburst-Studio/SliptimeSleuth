using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    //public GameObject codeEnterUI;
    public TextMeshProUGUI codeEnterUIText;

    public GameObject enterCodePopUp;
    public GameObject insertKeyPopUp;

    int keyboardInput;

    public bool canType;

    private void Update()
    {
        #region<Keyboard Input>
        if(canType)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                AddNumber(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                AddNumber(2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                AddNumber(3);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                AddNumber(4);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            {
                AddNumber(5);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            {
                AddNumber(6);
            }

            if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
            {
                AddNumber(7);
            }

            if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
            {
                AddNumber(8);
            }

            if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
            {
                AddNumber(9);
            }
        }
        
        #endregion

        //if(gameObject.activeSelf == false)
        //{
        //    canType = false;
        //}
    }

    private void Start()
    {
        m_firstCode = 7;
        m_secondCode = 2;
        m_thirdCode = 9;
        m_fourthCode = 4;
        //codeEnterUIText = codeEnterUI.GetComponent<Text>();
        canType = false;
        ResetCode();
    }

    public void AddNumber(int entry)
    {
        if(m_firstEntry == -1)
        {
            m_firstEntry = entry;
            codeEnterUIText.text = codeEnterUIText.text + entry + " ";
        }

        else if(m_secondEntry == -1)
        {
            m_secondEntry = entry;
            codeEnterUIText.text = codeEnterUIText.text + entry + " ";
        }

        else if(m_thirdEntry == -1)
        {
            m_thirdEntry = entry;
            codeEnterUIText.text = codeEnterUIText.text + entry + " ";
        }

        else if(m_fourthEntry == -1)
        {
            m_fourthEntry = entry;
            codeEnterUIText.text = codeEnterUIText.text + entry;

            if (m_firstEntry == m_firstCode && m_secondEntry == m_secondCode && m_thirdEntry == m_thirdCode && m_fourthEntry == m_fourthCode)
            {
                m_unlocked = true;
                Debug.Log("CORRECT CODE");
                //correctUI.SetActive(false);
                //correctUI.SetActive(true);
                //codeEnterUIText.text = "UNLOCKED";
                codeEnterUIText.color = Color.green;

                enterCodePopUp.SetActive(false);
                insertKeyPopUp.SetActive(true);
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

        codeEnterUIText.text = "";
    }
}
