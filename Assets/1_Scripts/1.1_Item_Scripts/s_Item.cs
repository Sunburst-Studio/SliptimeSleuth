using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// make it possible to make the object through the asset menu
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]

[System.Serializable]
public class s_Item : ScriptableObject
{
    // when hovering over the note, the short worded title of what it's about
    public string m_title;

    // when selecting the note, the full description/note/clue
    public string m_description;

    // the image of the item
    public Sprite m_image;

    public bool m_inInventory;

    // method to add the SO to the inventory
    public void AddItem(s_Item item)
    {
        GameManager.instance.inventory.m_currentItems.Add(item);
    }
}


