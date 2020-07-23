using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaTest : MonoBehaviour
{
    public GameObject player;
    private Mesh mesh;
    private int clock;
    private MeshCollider MC;
    private Mesh shMesh;
    private Vector3[] verticesStart;
    private Vector3[] vertices;
    // Start is called before the first frame update
    void Start()
    {
        MC = GetComponent<MeshCollider>();

        player = GameObject.Find("Player");
        clock = 0;
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        verticesStart = vertices;
        //print(vertices);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;


        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i] = Vector3.zero;
        }

        MC.sharedMesh = mesh;

        mesh.vertices = vertices;
    }
}
