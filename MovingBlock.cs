using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float xMove;
    public float yMove;
    public float zMove;
    public float xSpeed;
    public float ySpeed;
    public float zSpeed;

    private float xNeg;
    private float yNeg;
    private float zNeg;

    private float xPos;
    private float yPos;
    private float zPos;
    private Vector3 startPos;

    public GameObject player;
    private float effectFactor;
    private float lastEffectFactor;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        xNeg = -Mathf.Abs(xSpeed);
        yNeg = -Mathf.Abs(ySpeed);
        zNeg = -Mathf.Abs(zSpeed);
        xPos = Mathf.Abs(xSpeed);
        yPos = Mathf.Abs(ySpeed);
        zPos = Mathf.Abs(zSpeed);

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
                effectFactor = 2-(Vector3.Distance(GetComponent<Collider>().ClosestPoint(player.transform.position), player.transform.position)/5);
            }
            else
            {
                effectFactor = Mathf.Pow((Vector3.Distance(GetComponent<Collider>().ClosestPoint(player.transform.position), player.transform.position)) / 5, timeslow);
            }

            transform.position += new Vector3(xSpeed, ySpeed, zSpeed)*effectFactor / 10;
        }
        else
        {
            transform.position += new Vector3(xSpeed, ySpeed, zSpeed) / 10;
        }
        
        if (transform.position.x > startPos.x + xMove)
        {
            xSpeed = xNeg;
        }
        if (transform.position.x < startPos.x) {
            xSpeed = xPos;
        }
        if (transform.position.y > startPos.y + yMove || transform.position.y < startPos.y)
        {
            ySpeed = yNeg;
        }
        if (transform.position.y < startPos.y)
        {
            ySpeed = yPos;
        }
        if (transform.position.z > startPos.z + zMove || transform.position.z < startPos.z)
        {
            zSpeed *= -1;
        }
        if (transform.position.z < startPos.z)
        {
            zSpeed = zPos;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        collision.rigidbody.AddForce(new Vector3(xSpeed, ySpeed, zSpeed) * effectFactor*50);
    }

}
