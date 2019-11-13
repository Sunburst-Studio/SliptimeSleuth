using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <READ ME>
    /// 
    /// - make sure to drag scene's notebook into the gamemanager's spot
    /// - make sure to drag scene's inventoryUI into the gamemanager's spot
    /// - make sure to give the inventory its parent inventory
    /// - make sure to drag scene's tooltip into the gamemanager's spot
    /// - make sure tooltip starts off inactive in canvas inventoryui panel
    /// - WHEN DRAGGING AN INTERACTABLE INTO A SCENE, CHANGE THE GAME OBJECT'S NAME TO THE ITEM'S 
    /// - make sure to add the needed scriptable objects in the Game Manager's database
    /// - make sure to add Time Holder
    /// - make sure to drag in a masterMH 
    /// - just ditto this for everything in the drag in region
    /// 
    /// DON'T DESTROY ON LOAD IS COMMENTED OUT :'D
    /// 
    /// MAYBE PUT IN THE CHANGE SCENE FUNCTION
    /// - make sure you make all scriptable objects are turned to false in ResetSO();
    /// - make sure to clear the notebook's writing
    /// - make sure to clear the inventory
    /// 
    /// 
    /// 
    /// </summary>

    #region<Initializers>
    #region<Misc. Initializers>
    // initialize  a static GameManager instance so we ensure we only have ONE GameManager
    public static GameManager instance = null;

    // initialize what will be the cached main camera to make performance better
    public GameObject mainCamera;

    // the player
    public s_Player player;

    // toggle for when the player is in another menu or not
    public bool disableMovement;

    // the current inventory
    public s_Inventory inventory;

    // all of the items in the game are held here. the add item function will refer to this list
    public s_Item[] database;

    public s_MasterMH masterMH;

    [SerializeField]
    private s_AudioManager audioManager;

    #region<Drag In>
    [Header("~~~DRAG IN AREA BEGIN~~~")]
    // the notebook players can use to write in
    public GameObject noteBook;

    // the inventoryUI the player uses
    public GameObject inventoryUI;

    // the tool tip for items
    public GameObject toolTip;

    // the parent time holder
    public GameObject timeHolder;

    // a restart point for when caught by people
    public GameObject restartPoint;

    

    [Header("~~~DRAG IN AREA END~~~")]
    #endregion

    // is the text active or not?
    public bool textActive;

    // the current interactble the player inspects
    public s_Interactable currentInteractable;

    //  the big kahuna time slider boi
    public s_Timeslider timeSlider;

    // the global case you're trying to use an item with with an interaction
    public string objectAction;

    #endregion

    #region<SaveSystem Initializers>
    // the level players are up to
    [HideInInspector]
    public int level;

    // the player's current amount of money
    [HideInInspector]
    public int currency;
    #endregion
    #endregion

    private void Awake()
    {
        #region<GameManager Instance Handling>
        // if there is no GameManager present, make this one the instance
        if (instance == null)
        {
            instance = this;
        }

        // if the established instance is not the only existing GameManager, Highlander it (there can only be one!)
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        // make sure the established instance is not destroyed throughout the game's runntime and cause a new false GameManager to be created/established
        //DontDestroyOnLoad(this.gameObject);
        #endregion

        //!!! MAKE SURE THE FOLLOW GAMEOBJECTS ARE IN THE HIERARCHY OF THE SCENE !!!//
        FindEverything();
        //Debug.Log("entered");

        // !!! ADDED HERE TO MAKE SURE THE SCRIPTABLE OBJECT'S BOOL VALUES AREN'T PERMA. KEEP AN EYE FOR THAT WHEN DEBUGGING WEIRD ITEM BEHAVIOR !!! ///
        ResetSO();


    }

    private void Start()
    {
        // play the music
        //audioManager.GetComponent<AudioSource>().Play();
        audioManager = s_AudioManager.instance;
        if(audioManager == null)
        {
            Debug.Log("BINGBONGBINGBONG: NO AUDIO MANAGER FOUND IN SCENE");
        }
    }

    #region <Save Load System>
    // !!!Be sure to call FindPlayer() right before calling Save() !!!
    public void Save()
    {
        s_SaveSystem.SaveGame(player);
    }

    public void Load()
    {
        // the following returns a script_PlayerData, so we want to take that data and load it into our game. script_PlayerData data is like a package where we will take the goodies from
        s_PlayerData data = s_SaveSystem.LoadGame();

        // In the event where the LoadGame() function is called on a new game with 0 progress/saved data
        if (data == null)
        {

        }

        // !!!THIS IS WHERE THE LOADING OCCURS. HAVE THIS COMMENTED OUT DURING DEVELOPMENT TO ENSURE NO WEIRD BEHAVIORS AND EASIER TESTING!!!
        else
        {
            //level = data.level;
            //currency = data.currency;
        }
    }
    #endregion

    #region<Find Cacheing>
    public void FindEverything()
    {
        FindPlayer();
        FindInventory();
        FindCamera();
        FindDatabase();
        FindTimeSlider();
    }

    public void FindPlayer()
    {
        // cache the player
        player = FindObjectOfType<s_Player>();
    }

    public void FindTimeSlider()
    {
        timeSlider = FindObjectOfType<s_Timeslider>();
    }

    public void FindInventory()
    {
        inventory = FindObjectOfType<s_Inventory>();
    }

    public void FindCamera()
    {
        // cache the mainCamera. EVERY "Find" METHOD IS A TAXING CALL, SO MAKE SURE TO CACHE THOSE 
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void FindDatabase()
    {
        //database = FindObjectOfType<s_Database>();
    }

    #endregion

    public void DebugTest()
    {
        //Debug.Log();
    }

    public void ResetSO()
    {
        foreach (s_Item i in GameManager.instance.database)
        {
            i.m_inInventory = false;
        }
    }

    public void PlayAudio(string soundName)
    {
        audioManager.PlaySound(soundName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Application.Quit();
        }
    }
}
