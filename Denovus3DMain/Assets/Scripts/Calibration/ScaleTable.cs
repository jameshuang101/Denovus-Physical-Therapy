using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTable : MonoBehaviour
{
    private GameObject table;
    public Renderer tableRenderer;
    private Mesh tableMesh;
    private float tableLength;
    private float tableHeight;
    private float tableWidth;

    void Start()
    {
        table = GameObject.Find("Table");
        MeshFilter tableMeshFilter = (MeshFilter)table.GetComponent("MeshFilter");
        tableMesh = tableMeshFilter.mesh;
        tableRenderer = table.GetComponent<Renderer>();
        tableLength = tableMesh.bounds.size.x * table.transform.localScale.x;
        tableHeight = tableMesh.bounds.size.y * table.transform.localScale.y;
        tableWidth = tableMesh.bounds.size.z * table.transform.localScale.z;
        tableRenderer.enabled = false;
    }

    void Scale(Vector3 leftCorner, Vector3 rightCorner)
    {
        float distance = Vector3.Distance(leftCorner, rightCorner);
        float height = (leftCorner.y + rightCorner.y) / 2;
        Vector3 midpoint = new Vector3((leftCorner.x + rightCorner.x) / 2, 0f, (leftCorner.z + rightCorner.z) / 2);
        table.transform.localScale = new Vector3(distance / tableMesh.bounds.size.x, height / tableMesh.bounds.size.y, distance / tableMesh.bounds.size.x);
        PlayerPrefs.SetFloat("TableXScale", table.transform.localScale.x);
        PlayerPrefs.SetFloat("TableYScale", table.transform.localScale.y);
        PlayerPrefs.SetFloat("TableZScale", table.transform.localScale.z);
        float newTableLength = tableMesh.bounds.size.x * table.transform.localScale.x;
        float newTableWidth = tableMesh.bounds.size.z * table.transform.localScale.z;
        Vector3 leftCornerProjection = new Vector3(leftCorner.x, 0, leftCorner.z);
        Plane plane = new Plane(leftCorner, rightCorner, leftCornerProjection);
        Vector3 normalVec = plane.normal;
        Vector3 unitNormalVec = plane.normal / plane.normal.magnitude;
        table.transform.position = new Vector3(midpoint.x - (newTableLength / 2) * unitNormalVec.x, 0, midpoint.z - (newTableWidth / 2) * unitNormalVec.z);
        table.transform.LookAt(normalVec);
        tableRenderer.enabled = true;
    }

    public void ScalerPressed(Vector3 leftCornerPosition, Vector3 rightCornerPosition)
    {
        Scale(leftCornerPosition, rightCornerPosition);
        StartCoroutine(DelayScalerPressed());
    }

    IEnumerator DelayScalerPressed()
    {
        yield return new WaitForSecondsRealtime(3);
        GameObject.Find("SceneManager").GetComponent<SceneSwitcher>().SwitchCallibrationToMain();
    }
}
