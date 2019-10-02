using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class s_Tooltip : MonoBehaviour
{
    public Text m_toolTitle;
    public TMP_Text m_toolDescription;

    private void Start()
    {
        //this.gameObject.SetActive(false);
        m_toolTitle.text = "";
        m_toolDescription.text = "";
    }
    

}
