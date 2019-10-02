using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_RoomWallsManager : MonoBehaviour
{
    public GameObject sleuth;
    public GameObject wallsInside;
    public GameObject wallsOutside;
    public bool colliderToGoInside;

    private void OnTriggerEnter(Collider theCollider)
    {
        if (theCollider.name == sleuth.name && colliderToGoInside)
        {
            wallsInside.SetActive(true);
            wallsOutside.SetActive(false);
        }
        else if (theCollider.name == sleuth.name && !colliderToGoInside)
        {
            wallsInside.SetActive(false);
            wallsOutside.SetActive(true);
        }
    }
}
