using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialogue : Singleton<Dialogue>
{
    public GameObject box;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;


    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        box.SetActive(false);
        //StartDialogue();
    }

    public void StartDialogue()
    {
        box.SetActive(true);
        print("hello!!!!!");
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // type each char 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return null;
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }else{
            textComponent.text = string.Empty;
            box.SetActive(false);
        }
    }

    public bool LineFullyShown(){
        return textComponent.text == lines[index];
    }
    public void ShowFullLine(){
        StopAllCoroutines();
        textComponent.text = lines[index];
        return;
    }
}
