using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour {
    public float rotationSpeed;
    public float minSpeed, maxSpeed;

    public GameObject ateroidExplosion;
    public GameObject sheepExplosion;

    float size;

    public float sizeMin;
    public float sizeMax;

    // Start is called before the first frame update
    void Start () {
        size = Random.Range (sizeMin, sizeMax);

        Rigidbody Asteroid = GetComponent<Rigidbody> ();
        Asteroid.angularVelocity = Random.insideUnitSphere * rotationSpeed;
        float speed = Random.Range (minSpeed, maxSpeed);

        Asteroid.velocity = new Vector3 (0, 0, -speed);

        transform.localScale *= size;
    }
    //При столкновении с другими коллайдерами (other)
    private void OnTriggerEnter (Collider other) {
        if (other.tag == "GameBorder" || other.tag == "Coin") {
            return;
        }

        // Destroy (other.gameObject);
        Destroy (gameObject);

        GameObject explosion = Instantiate (ateroidExplosion, transform.position, Quaternion.identity);
        explosion.transform.localScale *= size;

        // if (other.tag == "Player") {
        //     Instantiate (sheepExplosion, other.transform.position, Quaternion.identity);
        // }
        if (other.tag == "PlayerLaser") {
            GameControlScript.instance.increaseScore (5);
        }

    }

    // Update is called once per frame
    void Update () {

    }
}