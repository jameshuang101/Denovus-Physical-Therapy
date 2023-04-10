using System.Collections;
using UnityEngine;

public class BobBehaviour : MonoBehaviour
{
    public bool castable = true;
    public bool reeling = false;
    public bool onReel = false;
    public FishSpawner fishSpawner;
    private Transform fishingRod;
    private float speed = 5f;
    private Vector3 offset = new Vector3(0f, -0.4f, 0);
    private Vector3 localOffset = new Vector3(0f, -0.4f, 0);
    public Vector3 direction;
    public Renderer castArea;
    public Score scoreText;
    private bool CR_running = false;
    public AudioSource reelingAudio;

    private BTManager bTManager;
    public PosesToRender poses;
    public int waitingPose;
    public bool waitingPoseReel;
    private Rigidbody rb;
    private float thrust = 20f;
    private bool toggled;

    void Start()
    {
        fishingRod = GameObject.Find("AnchorPoint").transform;
        bTManager = FindObjectOfType<BTManager>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        GetComponent<Floater>().enabled = false;
        
        transform.parent = fishingRod;
        transform.localPosition = localOffset;
        createNewInstruction();
    }

    void Update()
    {
        if(castable) //If bob is castable, wait for user to trigger then cast bob and make it uncastable
        {
            transform.parent = fishingRod;
            transform.localPosition = localOffset;
            switch (waitingPose)
            {
                case 5:
                    if (bTManager.sensorValue[5] < fishSpawner.thumbTrigger && bTManager.sensorValue[5] != -999) //bTManager.sensorValue[5] < fishSpawner.thumbTrigger
                    {
                        transform.parent = null;
                        GetComponent<Rigidbody>().useGravity = true;
                        GetComponent<Floater>().enabled = true;
                        Vector3 direction = transform.forward;
                        rb.AddForce(-0.5f*direction.x * thrust, 0, 0.866f*direction.z, ForceMode.Impulse);
                        StartCoroutine(waitCheckBobLocation());
                        castable = false;
                        hideInstruction();
                        poses.showWaitText();
                        fishSpawner.caught = true;
                    }
                    break;
                case 6:
                    if (bTManager.sensorValue[6] < fishSpawner.indexTrigger && bTManager.sensorValue[6] != -999) //bTManager.sensorValue[6] < fishSpawner.indexTrigger
                    {
                        transform.parent = null;
                        GetComponent<Rigidbody>().useGravity = true;
                        GetComponent<Floater>().enabled = true;
                        Vector3 direction = transform.forward;
                        rb.AddForce(-0.5f * direction.x * thrust, 0, 0.866f * direction.z, ForceMode.Impulse);
                        StartCoroutine(waitCheckBobLocation());
                        castable = false;
                        hideInstruction();
                        poses.showWaitText();
                        fishSpawner.caught = true;
                    }
                    break;
                case 7:
                    if (bTManager.sensorValue[7] < fishSpawner.middleTrigger && bTManager.sensorValue[7] != -999) //bTManager.sensorValue[7] < fishSpawner.middleTrigger
                    {
                        transform.parent = null;
                        GetComponent<Rigidbody>().useGravity = true;
                        GetComponent<Floater>().enabled = true;
                        Vector3 direction = transform.forward;
                        rb.AddForce(-0.5f * direction.x * thrust, 0, 0.866f * direction.z, ForceMode.Impulse);
                        StartCoroutine(waitCheckBobLocation());
                        castable = false;
                        hideInstruction();
                        poses.showWaitText();
                        fishSpawner.caught = true;
                    }
                    break;
                case 8:
                    if (bTManager.sensorValue[8] < fishSpawner.ringTrigger && bTManager.sensorValue[8] != -999) //bTManager.sensorValue[8] < fishSpawner.ringTrigger
                    {
                        transform.parent = null;
                        GetComponent<Rigidbody>().useGravity = true;
                        GetComponent<Floater>().enabled = true;
                        Vector3 direction = transform.forward;
                        rb.AddForce(-0.5f * direction.x * thrust, 0, 0.866f * direction.z, ForceMode.Impulse);
                        StartCoroutine(waitCheckBobLocation());
                        castable = false;
                        hideInstruction();
                        poses.showWaitText();
                        fishSpawner.caught = true;
                    }
                    break;
                case 9:
                    if (bTManager.sensorValue[9] < fishSpawner.pinkyTrigger && bTManager.sensorValue[9] != -999) //bTManager.sensorValue[9] < fishSpawner.pinkyTrigger
                    {
                        transform.parent = null;
                        GetComponent<Rigidbody>().useGravity = true;
                        GetComponent<Floater>().enabled = true;
                        Vector3 direction = transform.forward;
                        rb.AddForce(-0.5f * direction.x * thrust, 0, 0.866f * direction.z, ForceMode.Impulse);
                        StartCoroutine(waitCheckBobLocation());
                        castable = false;
                        hideInstruction();
                        poses.showWaitText();
                        fishSpawner.caught = true;
                    }
                    break;
                default:
                    break;
            }
        }

        if(onReel)
        {
            switch(waitingPose)
            {
                case 5:
                    if (bTManager.sensorValue[5] < fishSpawner.thumbTrigger && bTManager.sensorValue[5] != -999) //bTManager.sensorValue[5] < fishSpawner.thumbTrigger
                        reeling = true;
                    else
                        reeling = false;
                    break;
                case 6:
                    if (bTManager.sensorValue[6] < fishSpawner.indexTrigger && bTManager.sensorValue[6] != -999) //bTManager.sensorValue[6] < fishSpawner.indexTrigger
                        reeling = true;
                    else
                        reeling = false;
                    break;
                case 7:
                    if (bTManager.sensorValue[7] < fishSpawner.middleTrigger && bTManager.sensorValue[7] != -999) //bTManager.sensorValue[7] < fishSpawner.middleTrigger
                        reeling = true;
                    else
                        reeling = false;
                    break;
                case 8:
                    if (bTManager.sensorValue[8] < fishSpawner.ringTrigger && bTManager.sensorValue[8] != -999) //bTManager.sensorValue[8] < fishSpawner.ringTrigger
                        reeling = true;
                    else
                        reeling = false;
                    break;
                case 9:
                    if (bTManager.sensorValue[9] < fishSpawner.pinkyTrigger && bTManager.sensorValue[9] != -999) //bTManager.sensorValue[9] < fishSpawner.pinkyTrigger
                        reeling = true;
                    else
                        reeling = false;
                    break;
                default:
                    reeling = false;
                    break;
            }
        }

        if(reeling)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Floater>().enabled = false;
            direction = fishingRod.position + offset - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            float dist = Vector3.Distance(transform.position, fishingRod.position + offset);
            poses.changeInstructionText("Keep going!");

            if (!toggled)
            {
                GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle();
                toggled = true;
            }

            if (!reelingAudio.isPlaying)
                reelingAudio.Play();

            if (dist < 0.1f)
            {
                foreach(Transform child in transform)
                {
                    if(child.tag == "Fish")
                        Destroy(child.gameObject);
                }
                reeling = false;
                onReel = false;
                castable = true;
                waitingPoseReel = false;
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Floater>().enabled = false;
                transform.SetParent(fishingRod);
                transform.localPosition = localOffset;
                hideInstruction();
                createNewInstruction();
                fishSpawner.numFish--;
                scoreText.setScore(fishSpawner.numFish);
                reelingAudio.Stop();
            }
        }
        else
        {
            if (toggled)
            {
                GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle();
                toggled = false;
            }
        }

    }

    private IEnumerator waitCheckBobLocation()
    {
        CR_running = true;
        yield return new WaitForSecondsRealtime(2);
        if(castArea.bounds.Contains(transform.position))
        {
            fishSpawner.caught = false;
            fishSpawner.addBobAsWaypoint();
            //Debug.Log("Bob in play area");
        }
        else
        {
            reeling = false;
            onReel = false;
            castable = true;
            waitingPoseReel = false;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Floater>().enabled = false;
            transform.SetParent(fishingRod);
            transform.localPosition = localOffset;
            hideInstruction();
            createNewInstruction();
            //Debug.Log("Bob not in play area");
        }
        CR_running = true;
    }

    public void createNewInstruction()
    {
        if(waitingPoseReel)
        {
            poses.changeInstructionText("Reel In");
        }
        else
        {
            poses.changeInstructionText("Cast Rod");
        }
        waitingPose = poses.getCurrentPose(waitingPose);
    }

    public void hideInstruction()
    {
        poses.deleteAllPoses();
    }

    public void endBob()
    {
        transform.SetParent(fishingRod);
        transform.localPosition = localOffset;
        hideInstruction();
        enabled = false;
    }
}
