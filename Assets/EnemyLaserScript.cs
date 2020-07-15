using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserScript : MonoBehaviour {
    public float speed;
    // Start is called before the first frame update
    void Start () {
        // GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, -speed);
        // GetComponent<Rigidbody> ().velocity = Vector3.forward * speed;
        // transform.position += transform.forward * Time.deltaTime * speed;
    }

    // Update is called once per frame
    void Update () {
        // transform.position += transform.forward * Time.deltaTime * speed;
        // transform.position += transform.forward * Time.deltaTime * movementSpeed;
    }

    //При столкновении с другими коллайдерами (other)
    private void OnTriggerEnter (Collider other) {
        if (other.tag == "GameBorder" || other.tag == "PlayerLaser" || other.tag == "EnemySheep" || other.tag == "Asteroid" || other.tag == "Coin") {
            return;
        }

        Destroy (gameObject);

        // if (other.tag == "Player") {
        //     Instantiate (sheepExplosion, other.transform.position, Quaternion.identity);
        // }
    }
}