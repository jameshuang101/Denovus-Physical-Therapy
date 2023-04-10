using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.Events;
using System;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FishNew : MonoBehaviour
{
    public Vector3 currentWayPoint;
    private Transform fishMouth;
    private Transform bobTransform;
    private Transform rodTransform;
    public bool selfCaught = false;
    public float distToWayPoint;
    private FishSpawner fishSpawner;
    private float speed = 2f;
    public float distToBob;
    private GameObject bob;
    private BobBehaviour bobBehaviour;
    public float distBobToRod;
    public AudioSource exitSplashAudio;
    public GameObject exitSplashEffect;

    // Start is called before the first frame update
    void Start()
    {
        fishSpawner = GameObject.Find("GameMaster").GetComponent<FishSpawner>();
        fishMouth = transform.Find("FishMouth");
        bob = GameObject.Find("FishingBob");
        rodTransform = GameObject.Find("AnchorPoint").transform;
        bobBehaviour = bob.GetComponent<BobBehaviour>();
        bobTransform = GameObject.Find("FishingBob").transform;
        int rand = UnityEngine.Random.Range(0, fishSpawner.wayPoints.Count); // give it a random waypoint to move to
        currentWayPoint = fishSpawner.wayPoints[rand];
        fishSpawner.wayPoints.RemoveAt(rand); //Make waypoint unavailable for other fish
    }

    // Update is called once per frame
    void Update()
    {
        distToBob = Vector3.Distance(bobTransform.position, fishMouth.position);
        distToWayPoint = Vector3.Distance(transform.position, currentWayPoint);
        if (!selfCaught)
        {
            
            Vector3 direction = currentWayPoint - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
            transform.LookAt(currentWayPoint);

            if (!fishSpawner.caught)
            {
                if (distToBob <= 0.75f) // if the fishing bob is near the fish 
                {
                    currentWayPoint = bobTransform.position;
                }
                if (distToBob <= 0.3f) // if theyre close enough, start the OnReel
                {
                    selfCaught = true;
                    fishSpawner.caught = true;
                    fishSpawner.removeBobAsWaypoint();
                    transform.SetParent(bobTransform);
                    transform.localPosition = new Vector3(0, -2.75f, 0);
                    transform.localEulerAngles = new Vector3(-90f, 180f, 0);
                    bobBehaviour.onReel = true;
                    bobBehaviour.waitingPoseReel = true;
                    bobBehaviour.hideInstruction();
                    bobBehaviour.createNewInstruction();
                    exitSplashAudio.Play();
                    Destroy(Instantiate(exitSplashEffect, transform.position, transform.rotation).gameObject, 1f);
                }
            }

            if (distToWayPoint < 0.05f) //If very close to waypoint
            {
                int rand = UnityEngine.Random.Range(0, fishSpawner.wayPoints.Count); // give it a random waypoint to move to
                fishSpawner.wayPoints.Add(currentWayPoint); //Make waypoint available again
                currentWayPoint = fishSpawner.wayPoints[rand];
                fishSpawner.wayPoints.RemoveAt(rand); //Make waypoint unavailable for other fish
            }
        }
    }
}
