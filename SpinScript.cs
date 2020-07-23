using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{

    public bool x;
    public bool y;
    public bool z;

    public float xSpeed;
    public float ySpeed;
    public float zSpeed;

    public GameObject player;
    private float effectFactor;
    private float lastEffectFactor;
    // Start is called before the first frame update
    void Start()
    {
        lastEffectFactor = 1;
        effectFactor = 1;
        player = GameObject.Find("Player");
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
            if (x)
            {
                transform.RotateAround(transform.position, Vector3.right, xSpeed*effectFactor);
            }
            if (y)
            {
                transform.RotateAround(transform.position, Vector3.up, ySpeed * effectFactor);
            }
            if (z)
            {
                transform.RotateAround(transform.position, Vector3.forward, zSpeed * effectFactor);
            }
        }
        else
        {
            if (x)
            {
                transform.RotateAround(transform.position, Vector3.right, xSpeed);
            }
            if (y)
            {
                transform.RotateAround(transform.position, Vector3.up, ySpeed);
            }
            if (z)
            {
                transform.RotateAround(transform.position, Vector3.forward, zSpeed);
            }
        }

    }

    private void OnCollisionStay(Collision collision)
    {

        collision.rigidbody.AddRelativeForce((collision.transform.position - GetComponent<Collider>().ClosestPoint(collision.transform.position))*50);
    }
}
