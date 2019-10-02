using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_InteractableManager : MonoBehaviour
{
    public s_Interactable[] Interactables;

    void Awake()
    {
        //maybe moved to findEverything function in gameManager.
        Interactables = FindObjectsOfType<s_Interactable>();
    }

    void Update()
    {
        
    }
}
