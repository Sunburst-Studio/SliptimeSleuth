using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class s_MasterMH : MonoBehaviour
{

    [SerializeField]
    private s_Dialogue m_dialogueHandler;
    private Animator d_anim;
    s_Inventory inventory;
    s_Player player;
    public GameObject pickUpIndicator;
    public GameObject objectives;
    public GameObject objectiveObject;

    public GameObject[] nightstandKillList;
    public GameObject[] nightstandRevealList;
    public GameObject nightStand;

    public GameObject[] coffeeKillList;
    public GameObject[] coffeeRevealList;

    private void Start()
    {
        player = FindObjectOfType<s_Player>();
    }

    //Sets GameObject inactive
    public void HideObject(GameObject objToHide)
    {
        //Debug.Log("bingbong");
        //Debug.Log(objToHide);
        objToHide.SetActive(false);
    }

    //Sets GameObject active
    public void UnhideObject(GameObject objToHide)
    {
        objToHide.SetActive(true);
    }

    //Destroys GameObject
    public void DestroyObject(GameObject objToDestroy)
    {
        Destroy(objToDestroy);
    }

    //Instantly changes Player position
    public void TeleportPlayer (Transform pos)
    {
        GameManager.instance.player.transform.position = pos.position;
    }

    //FOR DYLAN UWU
    public void PickUpItem(GameObject gameObjectName)
    {
        name = gameObjectName.name;
        //Debug.Log(name);
        GameManager.instance.inventory.AddItem(name);
        pickUpIndicator = GameObject.Find("Pick Up Image");
        pickUpIndicator.SetActive(false);
        pickUpIndicator.SetActive(true);

        //Sprite _pickUpIndicator = pickUpIndicator.GetComponent<Image>().sprite;
        //Debug.Log(gameObjectName);
        //Debug.Log(gameObjectName.GetComponent<s_Item>().m_title);
        Sprite _newImage = GameManager.instance.inventory.ReturnImageSprite(name);
        //_pickUpIndicator = _newImage;
        pickUpIndicator.GetComponent<Image>().sprite = _newImage;
    }

    public void AltPickUpItem(string gameObjectName)
    {
        name = gameObjectName;
        //Debug.Log(name);
        GameManager.instance.inventory.AddItem(name);
        pickUpIndicator = GameObject.Find("Pick Up Image");
        pickUpIndicator.SetActive(false);
        pickUpIndicator.SetActive(true);

        //Sprite _pickUpIndicator = pickUpIndicator.GetComponent<Image>().sprite;
        //Debug.Log(gameObjectName);
        //Debug.Log(gameObjectName.GetComponent<s_Item>().m_title);
        Sprite _newImage = GameManager.instance.inventory.ReturnImageSprite(name);
        //_pickUpIndicator = _newImage;
        pickUpIndicator.GetComponent<Image>().sprite = _newImage;
    }

    public void StartDialogue()
    {
        Debug.Log(pickUpIndicator);
        Debug.Log("Starting Dialogue");
        m_dialogueHandler = FindObjectOfType<s_Dialogue>();
        d_anim = m_dialogueHandler.gameObject.GetComponent<Animator>();
        m_dialogueHandler.sentences = GameManager.instance.currentInteractable.interactbleDialogue;
        d_anim.SetBool("TextActive", true);
        m_dialogueHandler.BeginDialogue();
        GameManager.instance.disableMovement = true;

    }

    public void OpenSafe()
    {
        GameObject m_safePanel;
        GameObject m_realSafePanel;

        m_safePanel = FindObjectOfType<s_Safe>().gameObject;
        m_realSafePanel = m_safePanel.transform.GetChild(0).gameObject;
        m_realSafePanel.SetActive(true);
    }

    // HAVE THE DOORWAY METHOD IN ITEMSLOTS AND THEN REFER THIS
    public void SwitchCaseUseItem(string item)
    {
        string theItem = item;
        //Debug.Log(item); 
        string _object;
        _object = GameManager.instance.objectAction;
        
        // these will work if you have the needed item
        switch (_object)
        {
            case "Dirt Plot":
                Debug.Log(theItem);
                if(theItem.Equals("Pile of Acorns")) PlantTree();
            break;

            case "Hallway":
                if(theItem.Equals("Kitty Kamera")) TakePhoto();
                break;

            case "Cup of Coffee":
                if (theItem.Equals("Laxative Bottle")) PourLaxatives();
                break;

            case "Wife's Nightstand":
                if (theItem.Equals("Polaroid Picture")) PlaceEvidence();
                break;

            default:
                break;
        }
    }

    public void TeleportCamera(Transform pos)
    {
        Camera.main.gameObject.transform.position = pos.position;
    }

    public void AddObjective(string objective)
    {
        bool hasMatch = false;

        objectives = GameObject.Find("Objective Holder");

        foreach (Transform g in objectives.transform)
        {
            //Debug.Log(g);
            if (g.gameObject.GetComponent<TextMeshProUGUI>().text != objective)
            {
                hasMatch = false;
            }
            else
            {
                hasMatch = true;
            }
        }

        if (!hasMatch)
        {
            var newObjective = Instantiate(objectiveObject, objectives.transform.position,
            Quaternion.identity, objectives.transform);
            newObjective.GetComponent<TextMeshProUGUI>().text = objective;
        }
    }

    public void CrossOffObjective(string objective)
    {
        foreach (Transform g in objectives.transform)
        {
            if (g.gameObject.GetComponent<TextMeshProUGUI>().text == objective)
            {
                g.gameObject.SetActive(false);
                g.gameObject.SetActive(true);
                g.gameObject.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            }
        }
    }

    #region<SPECIFIC FUNCTIONS>
    public void PlantTree()
    {
        if(GameManager.instance.inventory.HasItem("Pile of Acorns"))
        {
            // close the inventory
            player.AlternativeInventoryUIHandler();

            UnhideObject(GameManager.instance.timeHolder.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).gameObject);
            UnhideObject(GameManager.instance.timeHolder.transform.GetChild(2).transform.GetChild(0).gameObject);
            GameManager.instance.timeHolder.transform.GetChild(0).transform.GetChild(0).GetComponent<s_Interactable>().useItemObject.SetActive(false);
            GameManager.instance.inventory.UseItem("Pile of Acorns");
        }
    }

    public void TakePhoto()
    {
        if (GameManager.instance.inventory.HasItem("Kitty Kamera"))
        {
            // close the inventory
            player.AlternativeInventoryUIHandler();
            AltPickUpItem("Polaroid Picture");
            HideObject(GameManager.instance.timeHolder.transform.GetChild(0).transform.GetChild(3).transform.GetChild(1).gameObject);
            GameManager.instance.inventory.UseItem("Kitty Kamera");
            AddObjective("-Place the affair evidence on the  wife's nightstand at the right time");
        }
    }

    public void PourLaxatives()
    {
        if (GameManager.instance.inventory.HasItem("Laxative Bottle"))
        {
            // close the inventory
            player.AlternativeInventoryUIHandler();

            HideObject(GameManager.instance.timeHolder.transform.GetChild(2).transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject);
            HideObject(GameManager.instance.timeHolder.transform.GetChild(2).transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject);

            GameManager.instance.timeHolder.transform.GetChild(2).transform.GetChild(3).gameObject.transform.GetChild(1).tag = "Untagged";

            GameManager.instance.inventory.UseItem("Laxative Bottle");

            foreach (GameObject g in coffeeKillList)
            {
                g.SetActive(false);
            }
            foreach (GameObject g in coffeeRevealList)
            {
                g.SetActive(true);
            }
        }
    }


    public void PlaceEvidence()
    {
        foreach (GameObject g in nightstandKillList)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in nightstandRevealList)
        {
            g.SetActive(true);
        }

        GameManager.instance.inventory.UseItem("Polaroid Picture");
        GameManager.instance.player.AlternativeInventoryUIHandler();
        nightStand.tag = "Untagged";
        Destroy(nightStand.transform.GetChild(0).gameObject);
        CrossOffObjective("-Place the affair evidence on the  wife's nightstand at the right time");
    }
    #endregion
}
