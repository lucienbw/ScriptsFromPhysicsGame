using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    public bool blue;
    private Renderer rend;
    Rigidbody rb;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Blue()
    {
        if (!blue)
        {
            GetComponent<ConstantForce>().force = new Vector3(0, -9.8f * rb.mass, 0);
            blue = true;
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", new Color(0, 0.455f, 1, 1));

            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
        }
    }

    public void Orange()
    {
        if (blue)
        {
            GetComponent<ConstantForce>().force = new Vector3(0, 9.8f * rb.mass, 0);
            blue = false;
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", new Color(1, 0.278f, 0, 1));


            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
        }
    }
}
