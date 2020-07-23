using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Jump(GameObject other)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = true;
        
        for (int i = 0; i < 10; i++) {
            other.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 110,transform.position);
            yield return new WaitForSeconds(0.01f);
        }
        
        print("Delay");
        em.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("hit");
        StartCoroutine(Jump(collision.gameObject));
    }
}
