using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MAY DELETE!!!!!! //
[System.Serializable]
public class s_ItemConstructor
{
    public string m_itemName;
    public string m_itemDescription;
    public Sprite m_image;

    // constructor to create the actual note's information
    public s_ItemConstructor(s_Item item)
    {
        // the constructor consumes a note
        // need to store the consumed parameter values in the initialized variables above so we can actually use them
        this.m_itemName = item.m_title;
        this.m_itemDescription = item.m_description;
        this.m_image = item.m_image;
    }
}
