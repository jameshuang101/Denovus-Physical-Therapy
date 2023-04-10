using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rigidBody;
    public GameObject waterPlane;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public Rigidbody Rayline;
    public Vector3 directrayold;
    public float counter;
    public bool OntheReel = false;
    public AudioSource bobHitWaterAudio;
    private bool bobHit;

    // Start is called before the first frame update
    void Start()
    {
       // directrayold = Rayline.transform.position;
    }

    // Update is called once per frame
   void FixedUpdate()
    {
        // directrayold = Rayline.transform.position;
        if (transform.position.y < waterPlane.transform.position.y && OntheReel == false)
        {
            if (counter==0)
            {
                directrayold = Rayline.transform.position;
            }
            
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerged) * displacementAmount;
            rigidBody.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), ForceMode.Acceleration);
            //Vector3 directrayold = Rayline.transform.position;
            counter = counter + 1f;
            if (counter==30)
            {
                Vector3 directraynew = Rayline.transform.position;
                Vector3 directray = (directraynew - directrayold) / Time.deltaTime;
                directrayold = directraynew;
                rigidBody.AddForce(0.007f * directray, ForceMode.Impulse);
                counter = 0f;
            }
        }

        if (transform.position.y <= (waterPlane.transform.position.y + .4f))
        {
            if (!bobHit)
            {
                bobHitWaterAudio.Play();
                bobHit = true;
            }
        }
        else if (transform.position.y >= (waterPlane.transform.position.y + 1f))
            bobHit = false;
    }
}
