using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;

public class PanelListener : MonoBehaviour
{
    public UnityEvent actionEvent;
    public GameObject panelButton;

    void Start()
    {
        panelButton.GetComponent<Interactable>().InteractableStateChanged.AddListener(InitiateEvent);
    }

    void InitiateEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
            actionEvent.Invoke();
    }
}
