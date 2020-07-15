using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public GameObject sheepExplosion;
    public float minSpeed, maxSpeed; //пределы скорости *
    float speed;
    Rigidbody EnemySheep;
    public GameObject LaserShot2;
    public Transform LaserGun;
    public float shotDelay = 1f; //задержка по времени выстрела *
    float nextShotTime; //время выстрела
    int bulletSpeed = 50; //скорость пули *

    // int countDifficultyScore = 0;

    Vector3 lastPosition; //предыдущая позиция

    public GameObject dropCoin;

    Transform player;

    // Start is called before the first frame update
    void Start () {
        EnemySheep = GetComponent<Rigidbody> ();
        speed = Random.Range (minSpeed, maxSpeed);

        EnemySheep.velocity = new Vector3 (0, 0, -speed);

        lastPosition = transform.position;
        // InvokeRepeating (Shot, 0f, 1f);
        difficultyIncrease ();
    }

    void Update () {
        //******
        if (player == null) {
            GameObject go = GameObject.Find ("Player(Clone)");

            if (go != null) {
                player = go.transform;
            }
        }

        if (player == null) {
            return;
        }

        Vector3 dir = player.position - transform.position;
        Vector3 currentDirection = transform.position - lastPosition;
        dir.Normalize ();

        float yAngle = Mathf.Atan2 (dir.x, dir.z) * Mathf.Rad2Deg - 180;
        EnemySheep.velocity = dir / dir.magnitude * speed;
        transform.rotation = Quaternion.Euler (0, yAngle, 0);
        //*****
        SelfGuidedShot (dir);

    }

    void Shot () {
        if (Time.time > nextShotTime) {
            Instantiate (LaserShot2, LaserGun.position, Quaternion.identity);
            nextShotTime = Time.time + shotDelay;
        }
    }

    void SelfGuidedShot (Vector3 dir) {
        if (Time.time > nextShotTime) {
            GameObject lazerEnemyShot = Instantiate (LaserShot2, LaserGun.position, Quaternion.identity);

            lazerEnemyShot.GetComponent<Rigidbody> ().velocity = dir / dir.magnitude * bulletSpeed;
            float angle = Vector3.SignedAngle (Vector3.back, dir, Vector3.up);
            lazerEnemyShot.transform.Rotate (0, angle, 0);
            nextShotTime = Time.time + shotDelay;
        }
    }

    void difficultyIncrease () {
        if (GameControlScript.instance.score > 50) {
            Debug.Log ("test");
            bulletSpeed += (GameControlScript.instance.score - 50) / 5;
            minSpeed += (GameControlScript.instance.score - 50) / 5;
            maxSpeed += (GameControlScript.instance.score - 50) / 5;
        }
    }

    //При столкновении с другими коллайдерами (other)
    private void OnTriggerEnter (Collider other) {
        if (other.tag == "GameBorder" || other.tag == "EnemyLaser" || other.tag == "Coin") {
            return;
        }

        // Destroy (other.gameObject);
        Destroy (gameObject);

        if (other.tag == "PlayerLaser") {
            GameControlScript.instance.increaseScore (10);
        }

        Instantiate (sheepExplosion, transform.position, Quaternion.identity);

        Instantiate (dropCoin, transform.position, Quaternion.identity);
        // if (other.tag == "Player") {
        //     Instantiate (sheepExplosion, other.transform.position, Quaternion.identity);
        // }
    }
}