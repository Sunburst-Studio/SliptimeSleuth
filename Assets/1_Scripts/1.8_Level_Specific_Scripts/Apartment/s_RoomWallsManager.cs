using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_RoomWallsManager : MonoBehaviour
{
    public GameObject sleuth;
    public GameObject wallsInside;
    public GameObject wallsOutside;

    private void OnTriggerStay(Collider theCollider)
    {
        if (theCollider.name == sleuth.name)
        {
            wallsInside.SetActive(true);
            wallsOutside.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider theCollider)
    {
        if (theCollider.name == sleuth.name)
        {
            wallsInside.SetActive(false);
            wallsOutside.SetActive(true);
        }
    }

}
