using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{

    public GameObject cam;
    public GameObject pivot;
    public GameObject zoom;
    public GameObject goalSprite;
    public GameObject canvas;
    //public GameObject camTrack;
    private float scroll;
    private Rigidbody rb;
    public float speed;
    private Vector3 offset;
    private bool blue;
    private Renderer rend;

    private Vector3 startPos;
    private Vector3 camStart;
    private Vector3 mouseTemp;

    private int levelnum;

    public GameObject readyText;
    public float timeSlow;

    //public static float[] effectScales = new float[5];

    private bool ready;
    private bool goal;

    private bool[] spaceIn = new bool[5];
    private bool[] spaceOut = new bool[5];
    private bool[] spaceProgress = new bool[5];

    private Vector3 velocity = Vector3.zero;
    private Color startColor;
    // Start is called before the first frame update
    void Start()
    {
        timeSlow = 0;
        pivot = GameObject.Find("CameraTracker").transform.Find("pivot").gameObject;
        scroll = 0;
        spaceIn[0] = false;
        spaceOut[0] = false;
        levelnum = 2;
        startPos = transform.position;
        goalSprite.SetActive(false);
        ready = false;
        goal = false;
        //ready = true;
        blue = true;
        readyText.transform.position -= Vector3.right* Screen.width/1.5f;
        rb = GetComponent<Rigidbody>();
        offset = cam.transform.position - transform.position;

        rend = GetComponent<Renderer>();
        camStart = cam.transform.eulerAngles;

        startColor = rend.material.color;

        StartCoroutine(Ready());
        GetComponent<ConstantForce>().force = new Vector3(0, -9.8f * rb.mass, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.y) > 30) {
            StartCoroutine(SpaceNorm(0));
            transform.position = startPos;
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", startColor);
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
            if (int.Parse(BackgroundMusic.sceneNum) <= 11)
            {
                blue = true;
                

            }
            GetComponent<ConstantForce>().force = new Vector3(0, -9.8f * rb.mass, 0);
            rb.velocity = Vector3.zero;
            cam.transform.eulerAngles = camStart;
        }
        cam.transform.position = transform.position + offset;
        //BREAKPOINT
        if (!ready) {
            readyText.transform.position += Vector3.right * Screen.width/130;
            MeshDeform.effectScales[0] = 0;
            return; }
        Destroy(readyText);


        scroll += Input.mouseScrollDelta.y;
        if (pivot.transform.localEulerAngles.x < 70 && Input.mouseScrollDelta.y > 0)
        {
            pivot.transform.localEulerAngles = new Vector3(pivot.transform.localEulerAngles.x + (Input.mouseScrollDelta.y * 4), pivot.transform.localEulerAngles.y, pivot.transform.localEulerAngles.z);
        }
        if (pivot.transform.localEulerAngles.x > 1 && Input.mouseScrollDelta.y < 0)
        {
            pivot.transform.localEulerAngles = new Vector3(pivot.transform.localEulerAngles.x + (Input.mouseScrollDelta.y * 4), pivot.transform.localEulerAngles.y, pivot.transform.localEulerAngles.z);
        }
        if (pivot.transform.localEulerAngles.x > 300) {
            pivot.transform.localEulerAngles = new Vector3(0, pivot.transform.localEulerAngles.y, pivot.transform.localEulerAngles.z);
        }
        if (pivot.transform.localEulerAngles.x > 75)
        {
            pivot.transform.localEulerAngles = new Vector3(72, pivot.transform.localEulerAngles.y, pivot.transform.localEulerAngles.z);
        }
        if (!goal && ready)
        {
            zoom.transform.localPosition = new Vector3(zoom.transform.localPosition.x, zoom.transform.localPosition.y, 1 * -(pivot.transform.localEulerAngles.x / 10 + 4));
            print(pivot.transform.localEulerAngles.x / (10 + 4));
        }


        if (Input.GetKeyDown("h"))
        {
            StartCoroutine(SpaceOut(0));
        }
        if (Input.GetKeyDown("j"))
        {
            StartCoroutine(SpaceIn(0));
        }
        if (Input.GetKeyDown("y"))
        {
            cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x + 180, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
        }

        if (Input.GetKeyDown("right")) {
            if (!blue)
            {
                StartCoroutine(CamLeft());
            }
            else
            {
                StartCoroutine(CamRight());
            }
        }
        if (Input.GetKeyDown("left"))
        {
            if (blue)
            {
                StartCoroutine(CamLeft());
            }
            else {
                StartCoroutine(CamRight());
            }
        }
        

        if ( Input.GetKey("d")) {
            rb.AddForce((cam.transform.right- cam.transform.right.y*Vector3.up) * speed);
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            rb.AddForce((cam.transform.right - cam.transform.right.y * Vector3.up) * -speed);
        }
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            rb.AddForce((cam.transform.forward - cam.transform.forward.y * Vector3.up) * speed);
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            rb.AddForce((cam.transform.forward - cam.transform.forward.y * Vector3.up) * -speed);
        }
        
        if (blue)
        {
            cam.transform.RotateAround(transform.position, Vector3.up, (Input.GetAxis("Mouse X")) / 1);
            /*
            if (Input.mousePosition.x > (Screen.width / 2) + 80)
            {
                cam.transform.RotateAround(transform.position, Vector3.up, ((Input.mousePosition.x - Screen.width / 2) / 80));
            }
            if (Input.mousePosition.x < (Screen.width / 2) - 80)
            {
                cam.transform.RotateAround(transform.position, Vector3.up, ((Input.mousePosition.x - Screen.width / 2) / 80));
            }
            */
        }
        else {
            cam.transform.RotateAround(transform.position, Vector3.up*-1, (Input.GetAxis("Mouse X")) / 1);
            /*
            if (Input.mousePosition.x > (Screen.width / 2) + 80)
            {
                cam.transform.RotateAround(transform.position, Vector3.up, -1*((Input.mousePosition.x - Screen.width / 2) / 80));
            }
            if (Input.mousePosition.x < (Screen.width / 2) - 80)
            {
                cam.transform.RotateAround(transform.position, Vector3.up, -1*((Input.mousePosition.x - Screen.width / 2) / 80));
            }
            */
        }

        //cam.transform.RotateAround(transform.position, Vector3.right * -1, (Input.mousePosition.x - mouseTemp.x) / 5);

        mouseTemp = Input.mousePosition;
    }
    private void LateUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && blue) {
            GetComponent<ConstantForce>().force = new Vector3(0, 9.8f*rb.mass, 0);
            blue = false;
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", new Color(1, 0.278f, 0, 1));
            

            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
            StartCoroutine( CamSpin());
        }
        if (other.gameObject.layer == 9 && !blue)
        {
            GetComponent<ConstantForce>().force = new Vector3(0, -9.8f*rb.mass, 0);
            blue = true;
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", new Color(0, 0.455f, 1, 1));

            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
            StartCoroutine(CamSpin());
        }
        if (other.gameObject.layer == 10) {
            //Collect Object
            Destroy(other.gameObject);
            goalSprite.SetActive(true);
            StartCoroutine(Goal());
        }
        if (other.gameObject.layer == 11)
        {
            //Collect Object
            Destroy(other.gameObject);

        }
        if (other.gameObject.layer == 12)
        {
            StartCoroutine(SpaceOut(0));
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", new Color(0, 1, 1, 1));

            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
        }
        if (other.gameObject.layer == 13)
        {
            StartCoroutine(SpaceIn(0));
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", new Color(1, 0, 0, 1));

            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
        }
        if (other.gameObject.layer == 14)
        {
            StartCoroutine(SpaceNorm(0));
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", startColor);
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
        }
        if (other.gameObject.layer == 15) {
            timeSlow = 1;
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", new Color(0.1f, 0, 1, 1));

            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
        }
        if (other.gameObject.layer == 16)
        {
            timeSlow = 0;
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", startColor);
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
        }
        if (other.gameObject.layer == 17)
        {
            timeSlow = -1;
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", new Color(1, 0.2f, 0, 1));

            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
        }

    }

    IEnumerator CamSpin()
    {
        GetComponent<AudioSource>().Play();
        for (float f = 0; f < 180; f += 9f)
        {
            cam.transform.RotateAround(cam.transform.position, cam.transform.forward, 9f);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator CamRight()
    {
        for (float f = 0; f < 90; f += 5)
        {
            cam.transform.RotateAround(transform.position, Vector3.up, 5);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator CamLeft()
    {
        for (float f = 0; f < 90; f += 5)
        {
            cam.transform.RotateAround(transform.position, Vector3.up, -5);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator Ready()
    {
        zoom.transform.localPosition += Vector3.forward * -90;
        for (float f = 0; f < 180; f += 1)
        {
            cam.transform.RotateAround(transform.position, Vector3.up, -2);
            yield return new WaitForFixedUpdate();
            zoom.transform.localPosition += Vector3.forward * 0.5f;
        }
        ready = true;
        cam.transform.position = camStart;
        mouseTemp = Input.mousePosition;

}
    IEnumerator Goal()
    {
        goal = true;
        for (float f = 0; f < 180; f += 1)
        {
            cam.transform.RotateAround(transform.position, Vector3.up, -2);
            yield return new WaitForFixedUpdate();
            zoom.transform.localPosition -= Vector3.forward * 0.5f;
        }
        SceneManager.LoadScene("Level-" + BackgroundMusic.sceneNum );

    }
    IEnumerator SpaceOut(int i)
    {
        if (!spaceProgress[i])
        {
            spaceProgress[i] = true;
            if (spaceOut[i] == false && spaceIn[i] == true)
            {
                for (float f = 0; f < 20; f += 1)
                {
                    MeshDeform.effectScales[i] += 0.1f;
                    MeshDeform.effectScale += 0.1f;
                    yield return new WaitForFixedUpdate();
                }
            }
            else if (spaceOut[i] == false)
            {
                for (float f = 0; f < 20; f += 1)
                {
                    MeshDeform.effectScales[i] += 0.05f;
                    MeshDeform.effectScale += 0.05f;
                    yield return new WaitForFixedUpdate();
                }
            }
            spaceOut[i] = true;
            spaceIn[i] = false;
            spaceProgress[i] = false;
        }
    }
    IEnumerator SpaceIn(int i)
    {
        if (!spaceProgress[i])
        {
            spaceProgress[i] = true;
            if (spaceIn[i] == false && spaceOut[i] == true)
            {
                for (float f = 0; f < 20; f += 1)
                {
                    MeshDeform.effectScales[i] -= 0.1f;
                    MeshDeform.effectScale -= 0.1f;
                    yield return new WaitForFixedUpdate();
                }
                spaceOut[i] = false;
            }
            else if (spaceIn[i] == false)
            {
                for (float f = 0; f < 20; f += 1)
                {
                    MeshDeform.effectScales[i] -= 0.05f;
                    MeshDeform.effectScale -= 0.05f;
                    yield return new WaitForFixedUpdate();
                }
            }
            spaceIn[i] = true;
            spaceOut[i] = false;
            spaceProgress[i] = false;
        }
        
    }
    IEnumerator SpaceNorm(int i)
    {
        if (!spaceProgress[i])
        {
            spaceProgress[i] = true;
            if (spaceIn[i] == false && spaceOut[i] == true)
            {
                for (float f = 0; f < 20; f += 1)
                {
                    MeshDeform.effectScales[i] -= 0.05f;
                    MeshDeform.effectScale -= 0.05f;
                    yield return new WaitForFixedUpdate();
                }
                
            }
            else if (spaceIn[i] == true && spaceOut[i] == false)
            {
                
                for (float f = 0; f < 20; f += 1)
                {
                    MeshDeform.effectScales[i] += 0.05f;
                    MeshDeform.effectScale += 0.05f;
                    yield return new WaitForFixedUpdate();
                }
            }
            spaceIn[i] = false;
            spaceOut[i] = false;
            spaceProgress[i] = false;
        }

    }


    public void Blue() {
        if (!blue)
        {
            GetComponent<ConstantForce>().force = new Vector3(0, -9.8f * rb.mass, 0);
            blue = true;
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", new Color(0, 0.455f, 1, 1));

            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", new Color(0.5f, 0.5f, 0.5f, 0.5f));
            StartCoroutine(CamSpin());
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
            StartCoroutine(CamSpin());
        }
    }

    public void SpaceControl(int i,int j) {
        switch (i)
        {
            case 1:
                StartCoroutine(SpaceIn(j));
                break;
            case 2:
                StartCoroutine(SpaceOut(j));
                break;
        }
    }


}
