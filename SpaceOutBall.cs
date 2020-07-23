using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceOutBall : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        MeshDeform.players[2] = this.gameObject;
        player.GetComponent<BallController>().SpaceControl(2, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

}