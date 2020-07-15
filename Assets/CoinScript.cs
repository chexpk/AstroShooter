using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    public int dieDelay = 5; // через сколько секунд исчезнет
    public float rotationSpeed; // скорость вращения

    // Start is called before the first frame update
    void Start () {

        Rigidbody rb = GetComponent<Rigidbody> ();
        // rb.angularVelocity = Random.insideUnitSphere * rotationSpeed;
        rotationSpeed = rb.angularVelocity.magnitude * Mathf.Rad2Deg;

        rb.angularVelocity = new Vector3 (0, Mathf.PI * 2, 0);

    }

    // Update is called once per frame
    void Update () {
        Invoke ("Die", dieDelay);
    }

    void Die () {

        Destroy (gameObject);

    }
}