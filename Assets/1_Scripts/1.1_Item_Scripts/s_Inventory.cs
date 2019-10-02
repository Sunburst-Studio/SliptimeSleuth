using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class s_Inventory : MonoBehaviour
{
    public List<s_Item> m_currentItems;
    public s_ItemSlots[] itemSlots;

    // !!! MAKE SURE TO DRAG THIS IN FOR EVERY SCENE !!! //
    public GameObject itemsParent;

    private int m_inventoryMax;

    private void Start()
    {
        m_inventoryMax = 6;
    }

    // use this for completeing interactions
    public bool HasItem(string item)
    {
        bool found = false;
        foreach (s_Item i in m_currentItems)
        {
            // once you find the item in the database matching the name of the item you want to add to your inventory
            if (i.m_title.Equals(item))
            {
                found = true;
                return true;
            }
        }

        if (!found) return false;
        else return true;
    }

    public void UseItem(string item)
    {
        int h = 0;

        // for every item in the database
        foreach (s_Item i in GameManager.instance.database)
        { 
            // once you find the item in the database matching the name of the item you want to add to your inventory
            if (i.m_title.Equals(item))
            {
                // remove the item from the inventory
                m_currentItems.Remove(GameManager.instance.database[h]);
                
                // set it back to the default no item sprite
                foreach (Transform itemSlot in itemsParent.transform)
                {
                    if(itemSlot.GetComponent<s_ItemSlots>().m_item != null)
                    {
                        if (itemSlot.GetComponent<s_ItemSlots>().m_item.m_title == item)
                        {
                            itemSlot.GetComponent<s_ItemSlots>().m_item = null;
                            itemSlot.GetComponent<s_ItemSlots>().ResetSprite();
                            break;
                        }
                    }
                }

                // make the item's status in the inventory as false
                GameManager.instance.database[h].m_inInventory = false;

                // reorder the items properly
                int j = 0;
                foreach (Transform itemSlot in itemsParent.transform)
                {
                    // once a gap is found
                    if (itemSlot.GetComponent<s_ItemSlots>().m_item == null)
                    {
                         //Debug.Log(j);

                        // buffer to make sure it's not the end of the list and to make sure that reshuffling is not needed
                        // if statement to make sure there's no out of bounds exception if you're checking the end of the inventory
                        if (j + 1 < m_inventoryMax)
                        {
                            //Debug.Log(j);
                            Transform nextChild = itemsParent.transform.GetChild(j + 1);
                            if (nextChild.GetComponent<s_ItemSlots>().m_item == null)
                            {
                                //if(j + 2 < m_inventoryMax)
                                //{

                                //}
                                break;
                            }
                            else
                            {
                                Transform currentChild = itemsParent.transform.GetChild(j);
                                currentChild.GetComponent<s_ItemSlots>().m_item = nextChild.GetComponent<s_ItemSlots>().m_item;
                                currentChild.GetComponent<s_ItemSlots>().AddSprite();
                                nextChild.GetComponent<s_ItemSlots>().ResetSprite();
                                nextChild.GetComponent<s_ItemSlots>().m_item = null;
                                j++;
                            }
                        }

                        // if there's another item and it actually needs reshuffling
                        else
                        {
                            if (j + 1 < m_inventoryMax)
                            {
                                //Debug.Log("switcheroo");
                                Transform nextChild = itemsParent.transform.GetChild(j + 1);
                                Transform currentChild = itemsParent.transform.GetChild(j);
                                nextChild.GetComponent<s_ItemSlots>().m_item = currentChild.GetComponent<s_ItemSlots>().m_item;
                                j++;
                            }
                            else break;
                        }
                    }

                    else
                    {
                        j++;
                    }
                }

                break;
            }

            else
            {
                // h is used to iterate through the entire array
                h++;
            }
        }
    }

    public Sprite ReturnImageSprite(string item)
    {
        Sprite sprite = null;
        int j = 0;
        foreach (s_Item i in GameManager.instance.database)
        {
            // once you find the item in the database matching the name of the item you want to add to your inventory
            if (i.m_title.Equals(item))
            {
                sprite = GameManager.instance.database[j].m_image;
            }

            else
            {
                j++;
            }
        }

        return sprite;
    }

    // !!! make sure the GameObject in the hierarchy is named the item in the Database's title !!! //
    public void AddItem(string item)
    {
        int h = 0;

        // for every item in the database
        foreach (s_Item i in GameManager.instance.database)
        {
            //Debug.Log(GameManager.instance.database[h].m_title);
            //Debug.Log(i.m_title); 

            // once you find the item in the database matching the name of the item you want to add to your inventory
            if (i.m_title.Equals(item))
            {
                    // add the item to your inventory
                    m_currentItems.Add(GameManager.instance.database[h]);

                    // make the item's status in the inventory as true
                    GameManager.instance.database[h].m_inInventory = true;

                foreach (Transform itemSlot in itemsParent.transform)
                {
                    if (itemSlot.GetComponent<s_ItemSlots>().m_item == null)
                    {
                        itemSlot.GetComponent<s_ItemSlots>().m_item = GameManager.instance.database[h];
                        itemSlot.GetComponent<s_ItemSlots>().AddSprite();
                        break;
                    }
                }
                //Debug.Log(m_currentItems.Count);
                break;
                //}
            }
            else
            {
                // h is used to iterate through the entire array
                h++;
                //Debug.Log(GameManager.instance.database[h].m_title);
            }
        }
    }
}
