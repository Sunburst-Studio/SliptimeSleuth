using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class s_Sentence 
{
    public string name;
    public Sprite portrait;
    [TextArea(3, 10)]
    public string sentenceText;
}
