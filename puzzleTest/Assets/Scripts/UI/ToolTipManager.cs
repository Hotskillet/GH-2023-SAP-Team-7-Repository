using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


// Detect if player is using gamepad or keyboard
public class ToolTipManager : Singleton<ToolTipManager>
{
    public PlayerInput playerInput;
    public Vector2 offset;
    public TextMeshProUGUI controlsTipText;
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
        Vector3 newPosition = new Vector3 (evt.objectPosition.x + offset.x,
                                            evt.objectPosition.y + offset.y,
                                            evt.objectPosition.z);
        tipWindow.gameObject.transform.position = newPosition;
        tipWindow.gameObject.SetActive(true);
    }

    void HideTip(HideInteractTip evt)
    {
        tipText.text = string.Empty;
        tipWindow.gameObject.SetActive(false);
    }


    void OnDestroy()
    {
        EvtSystem.EventDispatcher.RemoveListener<ShowInteractTip>(ShowTip);
        EvtSystem.EventDispatcher.RemoveListener<HideInteractTip>(HideTip);
    }
}
