using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmiterScript : MonoBehaviour {
    List<GameObject> prefabList = new List<GameObject> ();
    public GameObject Asteroid1;
    public GameObject Asteroid2;
    public GameObject Asteroid3;
    public GameObject EnemySheep;

    public float minDelay, maxDelay; //задержка между запусками

    float nextLaunchTime; // время следующего запуска
    // Start is called before the first frame update
    void Start () {
        prefabList.Add (Asteroid1);
        prefabList.Add (Asteroid2);
        prefabList.Add (Asteroid3);
        prefabList.Add (EnemySheep);

    }

    // Update is called once per frame
    void Update () {
        if (!GameControlScript.instance.isStarted) {
            return;
        }

        if (Time.time > nextLaunchTime) {
            // пора зупускать 
            int prefabIndex = Random.Range (0, 4);
            // Debug.Log (prefabIndex);
            float positionX = Random.Range (-transform.localScale.x / 2,
                transform.localScale.x / 2);
            Instantiate (prefabList[prefabIndex], new Vector3 (positionX, 0, transform.position.z), Quaternion.identity);
            nextLaunchTime = Time.time + Random.Range (minDelay, maxDelay);
        }
    }
}