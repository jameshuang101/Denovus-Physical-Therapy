using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightParent : MonoBehaviour
{
    private Transform RayTool;
    private BoxCollider collider;
    public Color highlightedColor;
    private Color ogColor;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        RayTool = GameObject.Find("RightHandAnchor").transform.Find("OVRHandPrefab").gameObject.GetComponent<OVRHand>().PointerPose;
        collider = GetComponent<BoxCollider>();
        ogColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(RayTool.position, RayTool.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (GameObject.ReferenceEquals(hit.collider.gameObject, this.gameObject))
            {
                image.color = highlightedColor;
            }
        }
        else
        {
            image.color = ogColor;
        }
    }
}
