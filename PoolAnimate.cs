using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAnimate : MonoBehaviour
{

    SkinnedMeshRenderer rend;
    Mesh mesh;
    private float weight;
    private bool forward;
    // Start is called before the first frame update
    void Start()
    {
        forward = true;
        weight = 0;
        rend = GetComponent<SkinnedMeshRenderer>();
        mesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (weight < 100 && forward)
        {
            weight += 1 - ((Mathf.Abs(50 - weight)) / 100);

        }
        else if (weight > 0)
        {
            weight -= 2 - ((Mathf.Abs(50 - weight)) / 30);
            forward = false;
        }
        else {
            forward = true;
        }

        rend.SetBlendShapeWeight(0, weight);

    }
}
