using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInBall : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        MeshDeform.players[1] = this.gameObject;
        player.GetComponent<BallController>().SpaceControl(1,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
