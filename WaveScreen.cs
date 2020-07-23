using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (Vector3.Distance(other.transform.position, GetComponent<BoxCollider>().ClosestPoint(other.transform.position) + (transform.up / 100)) > Vector3.Distance(other.transform.position, GetComponent<BoxCollider>().ClosestPoint(other.transform.position) - (transform.up / 100)))
        {
            if (Vector3.Angle(other.GetComponent<Rigidbody>().velocity,transform.up*-1) < 90)
            {
                other.gameObject.SendMessage("Orange");
            }
        }
        else {
            if (Vector3.Angle(other.GetComponent<Rigidbody>().velocity, transform.up) < 90)
            {
                other.gameObject.SendMessage("Blue");
            }
            
            
        }
        //print("success");
        //other.gameObject.SendMessage("Orange");
    }
}
