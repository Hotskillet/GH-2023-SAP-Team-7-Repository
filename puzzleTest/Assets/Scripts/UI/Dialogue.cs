using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialogue : Singleton<Dialogue>
{
    public GameObject box;
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    public bool showing;

    private string[] lines;
    private int index;

    private Coroutine updateCheck;


    void Awake()
    {
        EvtSystem.EventDispatcher.AddListener<ContinueDialogue>(Next);
        EvtSystem.EventDispatcher.AddListener<UpdateNewLines>(NewLines);
    }

    // Start is called before the first frame update
    void Start()
    {
        showing = false;
        textComponent.text = string.Empty;
        EvtSystem.EventDispatcher.Raise<UpdateDialogueState>(new UpdateDialogueState() {state = false});
        box.SetActive(false);
        lines = null;
        //StartDialogue();
    }

    public void NewLines(UpdateNewLines evt)
    {
        if (updateCheck == null)
        {
            updateCheck = StartCoroutine(UpdateLines(evt.moreLines));
        }
        StartDialogue();
    }

    IEnumerator UpdateLines(string[] newLines)
    {
        lines = newLines;
        yield return new WaitForSeconds(0.5f);
        updateCheck = null;
    }

    public void StartDialogue()
    {
        box.SetActive(true);
        EvtSystem.EventDispatcher.Raise<UpdateDialogueState>(new UpdateDialogueState() {state = true});
        showing = true;
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

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }else{
            textComponent.text = string.Empty;
            EvtSystem.EventDispatcher.Raise<UpdateDialogueState>(new UpdateDialogueState() {state = false});
            box.SetActive(false);
            showing = false;
        }
    }

    private bool LineFullyShown(){
        return textComponent.text == lines[index];
    }
    private void ShowFullLine(){
        StopAllCoroutines();
        textComponent.text = lines[index];
        return;
    }

    void Next(ContinueDialogue evt)
    {
        if (LineFullyShown()){
            NextLine();
        }else{
            ShowFullLine();
        }
    }

    void OnDestroy()
    {
        EvtSystem.EventDispatcher.RemoveListener<ContinueDialogue>(Next);
        EvtSystem.EventDispatcher.RemoveListener<UpdateNewLines>(NewLines);
    }
}
