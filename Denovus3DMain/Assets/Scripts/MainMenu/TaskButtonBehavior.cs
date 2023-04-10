using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using UnityEngine.UI;

public class TaskButtonBehavior : MonoBehaviour
{
    private GameObject scnManager;
    public GameObject ex1Button, ex2Button, ex3Button, ex4Button, easyButton, medButton, hardButton;
    public Slider slider;

    void Start()
    {
        scnManager = GameObject.Find("SceneManager");
        ex1Button.GetComponent<Interactable>().InteractableStateChanged.AddListener(ex1InitiateEvent);
        ex2Button.GetComponent<Interactable>().InteractableStateChanged.AddListener(ex2InitiateEvent);
        ex3Button.GetComponent<Interactable>().InteractableStateChanged.AddListener(ex3InitiateEvent);
        ex4Button.GetComponent<Interactable>().InteractableStateChanged.AddListener(ex4InitiateEvent);
        easyButton.GetComponent<Interactable>().InteractableStateChanged.AddListener(easyInitiateEvent);
        medButton.GetComponent<Interactable>().InteractableStateChanged.AddListener(medInitiateEvent);
        hardButton.GetComponent<Interactable>().InteractableStateChanged.AddListener(hardInitiateEvent);
    }

    void ex1InitiateEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
            scnManager.GetComponent<SceneSwitcher>().SwitchMainToExercise1();
    }

    void ex2InitiateEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
            scnManager.GetComponent<SceneSwitcher>().SwitchMainToExercise2();
    }

    void ex3InitiateEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
            scnManager.GetComponent<SceneSwitcher>().SwitchMainToExercise3();
    }

    void ex4InitiateEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
            scnManager.GetComponent<SceneSwitcher>().SwitchMainToExercise4();
    }

    void easyInitiateEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
            slider.value = 1;
    }

    void medInitiateEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
            slider.value = 2;
    }

    void hardInitiateEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
            slider.value = 3;
    }
}
