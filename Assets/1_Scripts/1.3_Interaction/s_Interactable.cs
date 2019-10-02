using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class s_Interactable : MonoBehaviour
{
    public s_Sentence[] interactbleDialogue;

    //Is the interactable in the player sphere collider;
    public bool ableToInteract = false;
    public bool interacting = false;
    public bool envTrigger = false;

    //Interaction Starter Object
    private GameObject m_ISObj;
    //Interaction Starter Animator
    private Animator m_ISAnim;
    //Reference to the Main Camera
    private GameObject m_mainCam;

    public bool m_animActive = false;

    //Events
    public UnityEvent QKey;
    public UnityEvent EKey;

    //Linked to the Q Key
    public GameObject inspectObject;
    //Both Linked to the E Key
    public GameObject interactObject;
    public GameObject useItemObject;

    private void Start()
    {
        //m_animActive = GameManager.instance.animActive;
            gameObject.tag = "Interactable";
            m_ISObj = transform.GetChild(0).gameObject;
        m_ISObj.SetActive(false);
        m_mainCam = Camera.main.gameObject;
        m_ISAnim = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        InteractionStarterHandler();

        if (interacting)
        {
            InteractionHandler();
        }

        if(m_ISAnim.gameObject.activeSelf == true)
        {
            //Resets the animation
            if (m_animActive)
            {
                m_ISAnim.SetBool("Interaction Start", true);
            }
            else
            {
                m_ISAnim.SetBool("Interaction Start", false);
                interacting = false;
                //Debug.Log("bingbong");
            }
        }
    }

    private void InteractionHandler()
    {
        if (GameManager.instance.textActive == false && !GameManager.instance.player.m_notebookOpen)
        {
            if (Input.GetKeyDown(KeyCode.Q) && inspectObject.activeSelf == true)
            {
                QKey.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.E) && (interactObject.activeSelf == true || useItemObject.activeSelf == true))
            {
                EKey.Invoke();
            }
        }
    }

    private void InteractionStarterHandler()
    {
        m_ISObj.GetComponent<TextMeshPro>().text = gameObject.name + " [F]";
        m_ISObj.transform.LookAt(m_ISObj.transform.position + 
            m_mainCam.transform.rotation * Vector3.forward, m_mainCam.transform.rotation * Vector3.up);

        if (ableToInteract)
        {
            m_ISObj.SetActive(true);
        }
        else
        {
            m_ISObj.SetActive(false);
        }
    }

    public void InteractionStart()
    {
        //Debug.Log("Fortnite Default Dance Music");
        m_animActive = true;
        interacting = true;
    }

    public void SpecificInteract(GameObject _gameObject)
    {
        string name = _gameObject.name;
        name = this.gameObject.name;
        GameManager.instance.objectAction = name;
        GameManager.instance.player.AlternativeInventoryUIHandler();
    }
}
