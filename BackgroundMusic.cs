using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update

    private string sceneName;
    public static string sceneNum;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        sceneName = SceneManager.GetActiveScene().name;
        sceneNum = sceneName.Split('-')[1];
        int temp = int.Parse(sceneNum);
        if (temp == 11) {
            GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Music2");
            GetComponent<AudioSource>().Play();
        }
        if (temp > 11 && GetComponent<AudioSource>().clip != (AudioClip)Resources.Load("Music2")) {
            GetComponent<AudioSource>().clip = (AudioClip)Resources.Load("Music2");
            GetComponent<AudioSource>().Play();
        }
        temp++;
        sceneNum = temp.ToString();
        print(sceneNum);
    }


}
