using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdvancedStats : MonoBehaviour
{

    public GameObject spaceButton, fishButton, skellyButton;
    public Text text;
    private float skellyThumb, skellyIndex, skellyMiddle, skellyRing, skellyPinky;
    public bool SpaceOn, FishOn;

    //private void Awake()
    //{
    //    // Only for repeated testing, reset values in preferences file on app close to show values changing next session
    //    PlayerPrefs.SetFloat("ThumbForceSkelly", 0f);
    //    PlayerPrefs.SetFloat("IndexForceSkelly", 0f);
    //    PlayerPrefs.SetFloat("MiddleForceSkelly", 0f);
    //    PlayerPrefs.SetFloat("RingForceSkelly", 0f);
    //    PlayerPrefs.SetFloat("PinkyForceSkelly", 0f);
    //}

    // Start is called before the first frame update
    void Start()
    {
        spaceButton.GetComponent<Interactable>().InteractableStateChanged.AddListener(InitiateSpaceEvent);
        
        fishButton.GetComponent<Interactable>().InteractableStateChanged.AddListener(InitiateFishEvent);

        skellyButton.GetComponent<Interactable>().InteractableStateChanged.AddListener(InitiateSkellyEvent);

        skellyThumb = PlayerPrefs.GetFloat("ThumbForceSkelly", 0f);
        skellyIndex = PlayerPrefs.GetFloat("IndexForceSkelly", 0f);
        skellyMiddle = PlayerPrefs.GetFloat("MiddleForceSkelly", 0f);
        skellyRing = PlayerPrefs.GetFloat("RingForceSkelly", 0f);
        skellyPinky = PlayerPrefs.GetFloat("PinkyForceSkelly", 0f);
    }

    void Update()
    {
        if (SpaceOn)
        {
            text.text = "Monday 5:13 pm\n"
            + "Exercise: Space Invaders\n"
            + "Difficulty: Easy\n"
            + "Time to beat: 130 sec\n\n"
            + "Average pinch forces:\n"
            + "\tIndex:\t13 lbs\n"
            + "\tMiddle:\t14 lbs\n"
            + "\tRing:\t10 lbs\n"
            + "\tPinky:\t8 lbs";
        }
        else if (FishOn)
        {
            text.text = "Monday 5:16 pm\n"
            + "Exercise: Fishing\n"
            + "Difficulty: Easy\n"
            + "Time to beat: 63 sec\n\n"
            + "Average flex percents:\n"
            + "\tIndex:\t83%\n"
            + "\tMiddle:\t55%\n"
            + "\tRing:\t48%\n"
            + "\tPinky:\t64%";
        }
    }

    void InitiateSpaceEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
        {
            text.text = "Monday 5:13 pm\n"
            + "Exercise: Space Invaders\n"
            + "Difficulty: Easy\n"
            + "Time to beat: 130 sec\n\n"
            + "Average Pinch Forces:\n"
            + "\tIndex:\t2.1 lbs\n"
            + "\tMiddle:\t1.8 lbs\n"
            + "\tRing:\t2.9 lbs\n"
            + "\tPinky:\t1.6 lbs";
        }
    }

    void InitiateFishEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
        {
            text.text = "Monday 5:16 pm\n"
            + "Exercise: Fishing\n"
            + "Difficulty: Easy\n"
            + "Time to beat: 63 sec\n\n"
            + "Average Flex Percents:\n"
            + "\tIndex:\t83%\n"
            + "\tMiddle:\t55%\n"
            + "\tRing:\t48%\n"
            + "\tPinky:\t64%";
        }
    }

    void InitiateSkellyEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ActionState)
        {
            skellyThumb = PlayerPrefs.GetFloat("ThumbForceSkelly", 0f);
            skellyIndex = PlayerPrefs.GetFloat("IndexForceSkelly", 0f);
            skellyMiddle = PlayerPrefs.GetFloat("MiddleForceSkelly", 0f);
            skellyRing = PlayerPrefs.GetFloat("RingForceSkelly", 0f);
            skellyPinky = PlayerPrefs.GetFloat("PinkyForceSkelly", 0f);

            if (skellyIndex > 0f)
            {
                text.text = "Tuesday 10:13 pm\n"
            + "Exercise: Bone Blaster\n"
            + "Difficulty: Easy\n\n"
            + "Average Pinch Porces:\n"
            + "\tThumb:\t" + skellyThumb.ToString("F1") + " lbs\n"
            + "\tIndex:\t" + skellyIndex.ToString("F1") + " lbs\n"
            + "\tMiddle:\t" + skellyMiddle.ToString("F1") + " lbs\n"
            + "\tRing:\t" + skellyRing.ToString("F1") + " lbs\n"
            + "\tPinky:\t" + skellyPinky.ToString("F1") + " lbs";
            }
            else
                text.text = "Exercise not\ncompleted yet";

        }
    }
}
