using UnityEngine;

public class Table : MonoBehaviour
{
    public Material translucentMaterial;
    public Material opaqueMaterial;
    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material = translucentMaterial;
    }

    public void MakeTableOpaque()
    {
        rend.material = opaqueMaterial;
    }
}
