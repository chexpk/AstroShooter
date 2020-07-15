using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour {
    public float speed;
    // Start is called before the first frame update
    void Start () {
        // GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, speed);
    }

    // Update is called once per frame
    void Update () {
        transform.position += Vector3.forward * Time.deltaTime * speed;
    }

    //При столкновении с другими коллайдерами (other)
    private void OnTriggerEnter (Collider other) {
        if (other.tag == "GameBorder" || other.tag == "EnemyLaser" || other.tag == "Player" || other.tag == "Coin") {
            return;
        }

        Destroy (gameObject);

        // if (other.tag == "Player") {
        //     Instantiate (sheepExplosion, other.transform.position, Quaternion.identity);
        // }
    }
}