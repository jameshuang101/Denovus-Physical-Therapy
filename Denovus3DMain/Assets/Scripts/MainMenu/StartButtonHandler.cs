using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;

public class StartButtonHandler : MonoBehaviour
{
    public GameObject playButton, settingsButton, progressButton, backButtonTask, backButtonProgress;
    public UnityEvent playActionEvent, settingsActionEvent, progressActionEvent, backEventTask, backEventProgress;

    // Start is called before the first frame update
    void Start()
    {
        playButton.GetComponent<Interactable>().InteractableStateChanged.AddListener(InitiatePlayEvent);
        settingsButton.GetComponent<Interactable>().InteractableStateChanged.AddListener(InitiateSettingsEvent);
        progressButton.GetComponent<Interactable>().InteractableStateChanged.AddListener(InitiateProgressEvent);
        backButtonTask.GetComponent<Interactable>().InteractableStateChanged.AddListener(InitiateBackEventTask);
        backButtonProgress.GetComponent<Interactable>().InteractableStateChanged.AddListener(InitiateBackEventProgress);
    }

    // Update is called once per frame
    void InitiatePlayEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
            playActionEvent.Invoke();
    }

    void InitiateSettingsEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
            settingsActionEvent.Invoke();
    }

    void InitiateProgressEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
            progressActionEvent.Invoke();
    }

    void InitiateBackEventTask(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
            backEventTask.Invoke();
    }

    void InitiateBackEventProgress(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
            backEventProgress.Invoke();
    }
}
