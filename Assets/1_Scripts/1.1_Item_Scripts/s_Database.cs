using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Database : MonoBehaviour
{
    // the database
    [Header("Items")]
    public s_Item[] allItems;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}