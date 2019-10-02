using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_ItemSlots : MonoBehaviour
{
    public s_Item m_item;
    public Image m_image;
    public Sprite m_defaultSprite;
    GameObject uiCamera;
    Camera _uiCamera;
    bool m_canClick;

    private void Start()
    {
        m_image = GetComponent<Image>();
        ResetSprite();
    }

    public void AddSprite()
    { 
        if(m_item != null )m_image.sprite = m_item.m_image;
        //m_currentSprite = m_item.m_image;
        //Debug.Log("bingbong");
    }

    public void ResetSprite()
    {
        //m_image.sprite = m_defaultSprite.sprite;
        m_image.sprite = m_defaultSprite;
    }

    public void mouseOverItem()
    {
        if(m_item != null)
        {
            GameManager.instance.toolTip.SetActive(true);

            GameManager.instance.toolTip.GetComponent<s_Tooltip>().m_toolTitle.text = m_item.m_title;
            GameManager.instance.toolTip.GetComponent<s_Tooltip>().m_toolDescription.text = m_item.m_description;
            //Debug.Log("bong");
        }
    }

    public void mouseExitItem()
    {
        GameManager.instance.toolTip.SetActive(false);
    }

    public void toggleCanClick()
    {
        m_canClick = !m_canClick;
    } 

    // used get to the master switchcase function
    public void DoorWaySwitchMethod()
    {
        //Debug.Log(m_item);
        GameManager.instance.masterMH.SwitchCaseUseItem(this.m_item.m_title);
    }
}
