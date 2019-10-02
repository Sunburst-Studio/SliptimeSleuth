using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public GameObject StartUI;
    public GameObject LoadingUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
            StartUI.SetActive(false);
            LoadingUI.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Application.Quit();
        }
    }
}
