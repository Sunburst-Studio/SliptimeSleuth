using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class s_Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public TextMeshProUGUI nameDisplay;
    public Image portraitDisplay;
    public s_Sentence[] sentences;
    private int index;
    public float typingSpeed;
    private Animator m_anim;

    public GameObject m_enterPrompt;

    [SerializeField]
    private bool isTyping;
    private Coroutine m_textCoroutine;

    public void BeginDialogue()
    {
        GameManager.instance.textActive = true;
        m_anim = GetComponent<Animator>();
        textDisplay.text = "";
        m_textCoroutine = StartCoroutine("Type");
        m_enterPrompt.SetActive(false);
        GameManager.instance.timeSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index].sentenceText)
        {
            m_enterPrompt.SetActive(true);
        }
        nameDisplay.text = sentences[index].name;
        portraitDisplay.sprite = sentences[index].portrait;

        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.textActive &&
            !GameManager.instance.player.m_notebookOpen)
        {
            if (isTyping)
            {
                StopCoroutine(m_textCoroutine);
                isTyping = false;
                textDisplay.text = sentences[index].sentenceText;
            }
            else
            {
                NextSentence();
            }
        }
    }

    public IEnumerator Type()
    {
        isTyping = true;
        foreach(char letter in sentences[index].sentenceText.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    public void NextSentence()
    {
        m_enterPrompt.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StopCoroutine(m_textCoroutine);
            m_textCoroutine = StartCoroutine("Type");
        }
        else
        {
            GameManager.instance.textActive = false;
            m_anim.SetBool("TextActive", false);
            textDisplay.text = "";
            m_enterPrompt.SetActive(false);
            index = 0;
            GameManager.instance.timeSlider.gameObject.SetActive(true);
            GameManager.instance.disableMovement = false;
        }
    }
}
