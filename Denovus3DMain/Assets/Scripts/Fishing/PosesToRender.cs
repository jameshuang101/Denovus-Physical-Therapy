using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PosesToRender : MonoBehaviour
{
    //public SpriteRenderer spriteRenderer; 
    public GameObject IndexDown;
    public GameObject MiddleDown;
    public GameObject RingDown;
    public GameObject PinkyDown;
    public GameObject ThumbDown;
    //public bool renderSprite = false;
    public int poseNum = 0;
    
    public int randO;
    public TextMeshPro textToRender;
    public TextMeshPro instructionText;
     //textmeshPro = GetComponent<TextMeshPro>();
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }

    public int getCurrentPose(int prevPose) // set a random pose for the user to do varying with flexing each individual digit on the hand
    {
        while (randO == prevPose)
        {
            randO = UnityEngine.Random.Range(5, 10);// set a random pose within 1-5
        }
        if (randO == 6)
        {
            IndexDown.GetComponent<SpriteRenderer>().enabled = true; // render the image to show the user which pose
            textToRender.GetComponent<MeshRenderer>().enabled = true;
            instructionText.GetComponent<MeshRenderer>().enabled = true;
            textToRender.SetText("Index");
            poseNum = 6;
           
            return poseNum; // return a pose integer for them accomplish
        }
        else if (randO == 7)
        {
            MiddleDown.GetComponent<SpriteRenderer>().enabled = true;
            textToRender.GetComponent<MeshRenderer>().enabled = true;
            instructionText.GetComponent<MeshRenderer>().enabled = true;
            textToRender.SetText("Middle");
            poseNum = 7;
          
            return poseNum;
        }
        else if (randO == 8)
        {
            RingDown.GetComponent<SpriteRenderer>().enabled = true;
            textToRender.GetComponent<MeshRenderer>().enabled = true;
            instructionText.GetComponent<MeshRenderer>().enabled = true;
            textToRender.SetText("Ring");
            poseNum = 8;

            return poseNum;
        }
        else if (randO == 9)
        {
            PinkyDown.GetComponent<SpriteRenderer>().enabled = true;
            textToRender.GetComponent<MeshRenderer>().enabled = true;
            instructionText.GetComponent<MeshRenderer>().enabled = true;
            textToRender.SetText("Pinky");
            poseNum = 9;

            return poseNum;
        }
        else if (randO == 5)
        {
            ThumbDown.GetComponent<SpriteRenderer>().enabled = true;
            textToRender.GetComponent<MeshRenderer>().enabled = true;
            instructionText.GetComponent<MeshRenderer>().enabled = true;
            textToRender.SetText("Thumb");
            poseNum = 5;

            return poseNum;
        }
        else
        {
            return -1; // just in case. Throw an error. Shouldnt happen though
        }

    }

    public void deleteAllPoses()
    {
        IndexDown.GetComponent<SpriteRenderer>().enabled = false;
        MiddleDown.GetComponent<SpriteRenderer>().enabled = false;
        RingDown.GetComponent<SpriteRenderer>().enabled = false;
        PinkyDown.GetComponent<SpriteRenderer>().enabled = false;
        ThumbDown.GetComponent<SpriteRenderer>().enabled = false;
        textToRender.GetComponent<MeshRenderer>().enabled = false;
        instructionText.GetComponent<MeshRenderer>().enabled = false;
    }

    public void changeInstructionText(string text)
    {
        instructionText.SetText(text);
    }

    public void showWaitText()
    {
        textToRender.SetText("Waiting for a bite...");
        textToRender.GetComponent<MeshRenderer>().enabled = true;
    }
}
