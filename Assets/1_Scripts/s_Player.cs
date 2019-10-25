using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class s_Player : MonoBehaviour
{
    #region<Main Variables>
    // player's rigidbody
    Rigidbody m_rb;

    // player's speed
    public float speed;
    // player's turn sped;
    public float turnSpeed;

    // the horizontal and vertical axis' for WASD movement
    float hAxis;
    float vAxis;

    // variable for notebook
    bool m_isOpen;

    // if the notebook is open
    public bool m_notebookOpen;

    // if the inventory is open
    bool m_inventoryUIOpen;

    bool m_inInteractableRange = false;

    public GameObject m_currentInteractble;

    //  interaction manager
    private s_InteractableManager m_iManager;

    // the animation component of the inventory UI
    Animation inventoryUIAnimation;

    #endregion

    #region<Main Functions>
    private void Start()
    {
        m_iManager = FindObjectOfType<s_InteractableManager>();
        m_rb = GetComponent<Rigidbody>();
        inventoryUIAnimation = GameManager.instance.inventoryUI.GetComponent<Animation>();
    }

    private void FixedUpdate()
    {
        // handle standard player movement
        WalkHandler();
    }

    private void Update()
    {
        // to give alternative exit for UI
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_notebookOpen)
            {
                NotebookHandler();
            }

            else if (m_inventoryUIOpen)
            {
                InventoryUIHandler();
            }
        }

        // if you want to open the notebook
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            NotebookHandler();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryUIHandler();
        }

        if (m_inInteractableRange)
        {
            var interactable = m_currentInteractble.GetComponent<s_Interactable>();
            if (Input.GetKeyDown(KeyCode.F) && !m_notebookOpen)
            {
                interactable.InteractionStart();
                GameManager.instance.currentInteractable = m_currentInteractble.gameObject.GetComponent<s_Interactable>();
            }
        }

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    FindObjectOfType<s_MasterMH>().AddObjective("Yeehaw Objective");
        //}

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    FindObjectOfType<s_MasterMH>().CrossOffObjective("Yeehaw Objective");
        //}
    }

    public void AlternativeInventoryUIHandler()
    {
        //Debug.Log("bingbong");
        if (!m_notebookOpen)
        {
            // if you are opening the inventoryUI
            if (!m_inventoryUIOpen)
            {
                GameManager.instance.disableMovement = true;
                m_inventoryUIOpen = true;

                // "open" the inventory
                inventoryUIAnimation.Play("a_Inventory_Open");

                //int h = 0;
                foreach (Transform itemSlot in GameManager.instance.inventory.itemsParent.transform)
                {
                    if (itemSlot.GetComponent<s_ItemSlots>().m_item != null)
                    {
                        //itemSlot.GetComponent<s_ItemSlots>().m_item = GameManager.instance.database[h];
                        itemSlot.GetComponent<Button>().interactable = true;
                    }
                }
            }

            // if you are closing the inventoryUI
            else
            {
                GameManager.instance.disableMovement = false;
                m_inventoryUIOpen = false;

                // "close" the inventory
                inventoryUIAnimation.Play("a_Inventory_Close");

                foreach (Transform itemSlot in GameManager.instance.inventory.itemsParent.transform)
                {
                    if (itemSlot.GetComponent<s_ItemSlots>().m_item != null)
                    {
                        //itemSlot.GetComponent<s_ItemSlots>().m_item = GameManager.instance.database[h];
                        itemSlot.GetComponent<Button>().interactable = false;
                    }
                }

                //GameManager.instance.inventoryUI.SetActive(false);
            }
        }
    }

    public void InventoryUIHandler()
    {
        //Debug.Log(inven);
        // if you are not inside of the notebook
        if (!m_notebookOpen)
        {
            // if you are opening the inventoryUI
            if (!m_inventoryUIOpen)
            {
                GameManager.instance.disableMovement = true;
                m_inventoryUIOpen = true;
                // "open" the inventory
                inventoryUIAnimation.Play("a_Inventory_Open");
                GameManager.instance.timeSlider.gameObject.SetActive(false);
                //GameManager.instance.inventoryUI.SetActive(true);
            }

            // if you are closing the inventoryUI
            else
            {
                GameManager.instance.disableMovement = false;
                m_inventoryUIOpen = false;

                // "close" the inventory
                inventoryUIAnimation.Play("a_Inventory_Close");
                GameManager.instance.timeSlider.gameObject.SetActive(true);
                //GameManager.instance.inventoryUI.SetActive(false);
            }
        }
    }

    private void NotebookHandler()
    {
        if(!m_inventoryUIOpen)
        {
            //Debug.Log("tried to open");
            if (!m_isOpen)
            {
                m_notebookOpen = true;
                GameManager.instance.disableMovement = true;
                GameManager.instance.noteBook.SetActive(true);
                m_isOpen = true;

                // the player input for the notebook
                TMP_InputField playerInput;
                playerInput = GameManager.instance.noteBook.transform.GetChild(0).GetComponent<TMP_InputField>();
                playerInput.ActivateInputField();
                //foreach (s_TimePeriod t in timePeriods)
                //{
                //    foreach (Transform _object in t.timePeriodContents.transform)
                //        if (_object.GetComponent<s_Interactable>() != null)
                //        {
                //            //Debug.Log(t.timePeriodContents.);
                //            //t.timePeriodContents.GetComponentInChildren<s_Interactable>().m_animActive = false;
                //            //t.timePeriodContents.GetComponentInChildren<s_Interactable>().ableToInteract = false;

                //            _object.GetComponent<s_Interactable>().m_animActive = false;
                //            _object.GetComponent<s_Interactable>().ableToInteract = false;
                //        }
                //}
                GameManager.instance.timeSlider.gameObject.SetActive(false);
            }

            // if you want to close the notebook
            else
            {
                m_notebookOpen = false;
                GameManager.instance.disableMovement = false;
                GameManager.instance.noteBook.SetActive(false);
                m_isOpen = false;
                GameManager.instance.timeSlider.gameObject.SetActive(true);
            }
        }
       
    }


    private void WalkHandler()
    {
        // if you can move
        if(!GameManager.instance.disableMovement)
        {
            // receive input for WASD, controller, or arrowkeys
            hAxis = Input.GetAxis("Horizontal");
            vAxis = Input.GetAxis("Vertical");

            // moves player using the rigidbody's velocity
            // !!KEEP CAMERA'S POSITION IN MIND!!
            m_rb.velocity = new Vector3(hAxis * Time.deltaTime, 0, vAxis * Time.deltaTime).normalized * speed;
            // make the new position the current position
            if (m_rb.velocity.magnitude > 0)
            {
                GetComponentInChildren<Animator>().SetBool("Walking", true);


                Quaternion qto = Quaternion.LookRotation(m_rb.velocity);
                transform.rotation = Quaternion.Slerp(transform.rotation, qto, turnSpeed * Time.deltaTime);
            }
            else
            {
                GetComponentInChildren<Animator>().SetBool("Walking", false);
            }
        }
    }
    #endregion

    #region<Collision Handling>
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Interactable")
        {
            var interactable = col.gameObject.GetComponent<s_Interactable>();
            if (interactable.envTrigger)
            {
                GameManager.instance.currentInteractable = col.gameObject.GetComponent<s_Interactable>();
                interactable.QKey.Invoke();
                interactable.gameObject.SetActive(false);
            }
            interactable.ableToInteract = true;
            m_inInteractableRange = true;
            m_currentInteractble = col.gameObject;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Interactable")
        {
            var interactable = col.gameObject.GetComponent<s_Interactable>();
            interactable.ableToInteract = false;
            //resets the interactable's animation
            interactable.m_animActive = false;
            interactable.interacting = false;
            GameManager.instance.currentInteractable = null;
            m_inInteractableRange = false;
            m_currentInteractble = null;
        }
    }

    #endregion
    }

