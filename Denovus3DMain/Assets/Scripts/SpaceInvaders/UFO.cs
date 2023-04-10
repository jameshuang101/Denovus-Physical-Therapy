using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class UFO : MonoBehaviour
{
    private GameObject[] wayPoints;
    private int nextWayPointIndex;
    private float speed = 2;
    private Transform RayTool;
    private SphereCollider collider;
    private BTManager bTManager;
    private int forceThreshold;
    private waves Waves;
    private int lastWayPointIndex;
    private Vector3 lastWayPoint;
    private Vector3 nextWayPoint;
    private Vector3 flyAwayPosition = new Vector3(-25f, 50f, -50f);
    private bool flyingAway = false;
    private GameObject target;
    public Material aliensMaterial;
    public ParticleSystem alienDeathEffect;
    public Color _orange;

    public enum Finger
    {
        Index, Middle, Ring, Pinky
    };

    public Finger finger;
    
    void Start()
    {
        RayTool = GameObject.Find("RightHandAnchor").transform.Find("OVRHandPrefab").gameObject.GetComponent<OVRHand>().PointerPose;
        var wayPointsSorted = GameObject.FindGameObjectsWithTag("Waypoints").OrderBy( go => go.name ).ToList();
        wayPoints = wayPointsSorted.ToArray();
        lastWayPointIndex = wayPoints.Length - 1;
        lastWayPoint = wayPoints[lastWayPointIndex].transform.position;
        nextWayPointIndex = 0;
        nextWayPoint = wayPoints[0].transform.position;
        collider = GetComponent<SphereCollider>();
        target = gameObject.transform.Find("Target").gameObject;
        target.SetActive(false);
        bTManager = FindObjectOfType<BTManager>();
        _orange = new Color(1.0f, 0.5f, 0.0f);

        Waves = GameObject.Find("GameMaster").GetComponent<waves>();
        switch(Waves.globalDifficulty)
        {
            case 1:
                forceThreshold = TriggerValues.forceTrigger;
                break;
            case 2:
                forceThreshold = TriggerValues.forceTriggerMedium;
                break;
            case 3:
                forceThreshold = TriggerValues.forceTriggerHard;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        MoveUFO();
        RaycastHit hit;
        if (Physics.Raycast(RayTool.position, RayTool.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) )
        {
            if (GameObject.ReferenceEquals(hit.collider.gameObject, this.gameObject))
            {
                target.SetActive(true);
                switch (finger)
                {
                    case Finger.Index:
                        if (bTManager.sensorValue[1] > forceThreshold)
                        {
                            GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle();
                            DestroyUFO();
                        }
                        break;
                    case Finger.Middle:
                        if (bTManager.sensorValue[2] > forceThreshold)
                        {
                            GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle();
                            DestroyUFO();
                        }
                        break;
                    case Finger.Ring:
                        if (bTManager.sensorValue[3] > forceThreshold)
                        {
                            GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle();
                            DestroyUFO();
                        }
                        break;
                    case Finger.Pinky:
                        if (bTManager.sensorValue[4] > forceThreshold)
                        {
                            GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle();
                            DestroyUFO();
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                target.SetActive(false);
            }
        }
        /*
        //For testing
        switch (finger)
        {
            case Finger.Index:
                if (Waves.currentIndexValue > forceThreshold)
                {
                    Debug.Log("Destroying UFO");
                    DestroyUFO();
                }
                break;
            case Finger.Middle:
                if (Waves.currentMiddleValue > forceThreshold)
                    DestroyUFO();
                break;
            case Finger.Ring:
                if (Waves.currentRingValue > forceThreshold)
                    DestroyUFO();
                break;
            case Finger.Pinky:
                if (Waves.currentPinkyValue > forceThreshold)
                    DestroyUFO();
                break;
            default:
                break;
        }
        */
    }

    private void MoveUFO()
    {

        if (Vector3.Distance(transform.position, lastWayPoint) > 0.1f)
        {
            nextWayPoint = wayPoints[nextWayPointIndex].transform.position;
            Vector3 direction = nextWayPoint - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
        if (Vector3.Distance(transform.position, nextWayPoint) < 0.5f && nextWayPointIndex < lastWayPointIndex)
        {
            nextWayPointIndex++;
        }
        if (nextWayPointIndex == lastWayPointIndex && Vector3.Distance(transform.position, lastWayPoint) < 0.5f)
        {
            flyingAway = true;
            collider.enabled = false;
            transform.rotation = new Quaternion(0f, -90f, 0f, 0f);
        }
        if (flyingAway)
        {
            Vector3 direction = flyAwayPosition - transform.position;
            transform.Translate(direction.normalized * 10 * speed * Time.deltaTime, Space.World);
            if (Vector3.Distance(transform.position, flyAwayPosition) < 0.5f)
                DestroyUFOWithoutIncrement();
        }

    }

    public void DestroyUFO()
    {
        Waves.UFOsDestroyed++;

        switch (finger)
        {
            case Finger.Index:
                aliensMaterial.SetColor("_Color", Color.magenta);
                Destroy((Instantiate(alienDeathEffect, transform.position, transform.rotation)).gameObject, 1f);
                break;
            case Finger.Middle:
                aliensMaterial.SetColor("_Color", Color.yellow);
                Destroy((Instantiate(alienDeathEffect, transform.position, transform.rotation)).gameObject, 1f);
                break;
            case Finger.Ring:
                aliensMaterial.SetColor("_Color", _orange);
                Destroy((Instantiate(alienDeathEffect, transform.position, transform.rotation)).gameObject, 1f);
                break;
            case Finger.Pinky:
                aliensMaterial.SetColor("_Color", Color.grey);
                Destroy((Instantiate(alienDeathEffect, transform.position, transform.rotation)).gameObject, 1f);
                break;
            default:
                break;
        }

        GameObject.Find("SaveManager").GetComponent<SaveSensors>().toggle();

        Destroy(gameObject);
    }

    public void DestroyUFOWithoutIncrement()
    {
        Destroy(gameObject);
    }
}
