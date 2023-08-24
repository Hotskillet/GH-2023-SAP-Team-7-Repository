using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


// Detect if player is using gamepad or keyboard
public class ToolTipManager : Singleton<ToolTipManager>
{
    public Vector2 offset;
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;


    void Awake()
    {
        EvtSystem.EventDispatcher.AddListener<ShowInteractTip>(ShowTip);
        EvtSystem.EventDispatcher.AddListener<HideInteractTip>(HideTip);
    }

    void Start()
    {
        tipWindow.gameObject.SetActive(false);
    }

    void ShowTip(ShowInteractTip evt)
    {
        tipText.text = evt.info;
        Vector3 newPosition = new Vector3 (evt.obj.transform.position.x + offset.x,
                                            evt.obj.transform.position.y + offset.y,
                                            evt.obj.transform.position.z);
        gameObject.transform.position = newPosition;
        gameObject.transform.parent = evt.obj.transform;
        tipWindow.gameObject.SetActive(true);
    }

    void HideTip(HideInteractTip evt)
    {
        tipText.text = string.Empty;
        gameObject.transform.parent = null;
        tipWindow.gameObject.SetActive(false);
    }


    void OnDestroy()
    {
        EvtSystem.EventDispatcher.RemoveListener<ShowInteractTip>(ShowTip);
        EvtSystem.EventDispatcher.RemoveListener<HideInteractTip>(HideTip);
    }
}
