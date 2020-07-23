using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDilation : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rb;
    private Vector3 trueVelocity;
    private Vector3 trueAngularVelocity;
    private float effectFactor;
    private float lastEffectFactor;
    // Start is called before the first frame update
    void Start()
    {
        lastEffectFactor = 1;
        effectFactor = 1;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(GetComponent<Collider>().ClosestPoint(player.transform.position), player.transform.position) < 5)
        {
            lastEffectFactor = effectFactor;
            float timeslow = player.GetComponent<BallController>().timeSlow;
            if (timeslow == -1)
            {
                effectFactor = 2 - (Vector3.Distance(GetComponent<Collider>().ClosestPoint(player.transform.position), player.transform.position) / 5);
            }
            else
            {
                effectFactor = Mathf.Pow((Vector3.Distance(GetComponent<Collider>().ClosestPoint(player.transform.position), player.transform.position)) / 5, timeslow);
            }
            rb.velocity = rb.velocity * effectFactor  / lastEffectFactor;
            //trueVelocity = rb.velocity / effectFactor;

            rb.angularVelocity = rb.angularVelocity * effectFactor * effectFactor / lastEffectFactor / lastEffectFactor;
            //trueAngularVelocity = rb.angularVelocity / effectFactor;

        }
        else
        {
            trueVelocity = rb.velocity;
            trueAngularVelocity = rb.angularVelocity;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //rb.velocity = collision.relativeVelocity;
    } 
}
