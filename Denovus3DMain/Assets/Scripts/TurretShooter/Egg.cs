using UnityEngine;

public class Egg : MonoBehaviour
{
    private bool squeeze;
    private int squeezeValue;

    // Start is called before the first frame update
    void Start()
    {
        squeezeValue = PlayerPrefs.GetInt("Squeeze", 1);
        if (squeezeValue == 1)
            squeeze = true;
        else
            squeeze = false;

        if (!squeeze)
            Destroy(this.gameObject);
    }

    //void Update()
    //{
    //    if (!GameObject.FindGameObjectWithTag("RightHand").GetComponent<OVRHand>().IsTracked)
    //        transform.position = new Vector3(11.307f, 2.274f, -7.619f);
    //}
}
