using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagalongBillboard : MonoBehaviour
{
    public Camera mainCamera;
    public bool followCamera;
    public float distance;
    public float xOffset;
    public float yOffset;

    // Start is called before the first frame update
    void LateUpdate()
    {
        if (followCamera)
        {
            transform.position = mainCamera.transform.position + mainCamera.transform.forward * distance + mainCamera.transform.right * xOffset + mainCamera.transform.up * yOffset;
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }
    }

}
