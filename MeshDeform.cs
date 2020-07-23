using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeform : MonoBehaviour
{
    public static GameObject player;
    public static GameObject[] players = new GameObject[5];
    public static float[] effectScales = new float[5];
    private Mesh mesh;
    private int clock;
    private MeshCollider MC;
    private Mesh shMesh;
    public static float effectScale;

    private Vector3[] verticesStart;
    private Vector3[] vertices;
    // Start is called before the first frame update
    void Start()
    {
        effectScale = 0;
        for (int i = 0; i < 5; i++) {
            effectScales[i] = 0;
        }
        MC = GetComponent<MeshCollider>();
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        players[0] = player;
        clock = 0;
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        verticesStart = vertices;
        /*
        for (var i = 0; i < vertices.Length; i++)
        {
            verticesStart[i] = vertices[i] - transform.position; 
        }
        */
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        Matrix4x4 m = Matrix4x4.Rotate(rotation);
        Matrix4x4 inv = m.inverse;


        clock++;
        if (clock == 2) {
            clock = 0;
        }
        vertices = mesh.vertices;
        //Vector3[] normals = mesh.normals;
        Vector3 sum = Vector3.zero;
        for (var i = 0; i < vertices.Length; i++)
        {
            sum = Vector3.zero;
            for (int j = 0; j < 5; j++)
            {
                
                if (players[j] != null)
                {
                    if (Vector3.Distance(transform.TransformPoint(verticesStart[i]), players[j].transform.position) < 8)
                    {
                        //sum += ((effectScales[j]) * ((players[j].transform.position - transform.TransformPoint(verticesStart[i])).normalized * (Vector3.Distance(transform.TransformPoint(verticesStart[i]), players[j].transform.position) - 8) / 6));
                        sum += ((MeshDeform.effectScales[j]) * ((players[j].transform.position - transform.TransformPoint(verticesStart[i])).normalized * (Vector3.Distance(transform.TransformPoint(verticesStart[i]), players[j].transform.position) - 8) / 6));

                    }
                }
            }
            //sum = ((MeshDeform.effectScale) * ((player.transform.position - transform.TransformPoint(verticesStart[i])).normalized * (Vector3.Distance(transform.TransformPoint(verticesStart[i]), player.transform.position) - 8) / 6));
            vertices[i] =
                                transform.TransformPoint(verticesStart[i])
                            + sum;
            vertices[i] = transform.InverseTransformPoint(vertices[i]);
        }

        
        MC.sharedMesh = mesh;
  
        mesh.vertices = vertices;
    }


    void SwitchFocus(GameObject focus) {
        player = focus;
    }
}
