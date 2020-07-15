using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepScript : MonoBehaviour {
    Rigidbody Ship;
    public float speed;
    public float tilt;
    public float xMin, xMax, zMin, zMax;

    public GameObject LaserShot;
    public Transform LaserGun;
    public Transform RightLaserGun;
    public Transform LeftLaserGun;

    public float shotDelay; //задержка выстрела
    public float sideShotDelay = 0.15f;
    public float sideShotSize = 0.25f;

    float nextShotTime; //время следующего выстрела

    public GameObject sheepExplosion;

    public int shield = 0;      //количество собранных "щитов"

    public GameObject Shield;
    public Transform ShieldPosition;

    private Touch touch;
    private float speedModifier; // должно уменьшать скорость перемещения при управлении с тачскрина

    GameObject spheres;

    // Start is called before the first frame update
    void Start () {
        Ship = GetComponent<Rigidbody> ();
        speedModifier = 0.1f;
        spheres = GameObject.Find ("Shield");
    }

    // Update is called once per frame
    void Update () {
        if (!GameControlScript.instance.isStarted) {
            return;
        }

        // перемещение при управлении с тачскрина
        if (Input.touchCount > 0) {
            touch = Input.GetTouch (0);

            if (touch.phase == TouchPhase.Moved) {
                transform.position = new Vector3 (
                    transform.position.x + touch.deltaPosition.x * speedModifier,
                    transform.position.y,
                    transform.position.z + touch.deltaPosition.y * speedModifier);
            }
        }

        //перемещение при управлении с клавиатуры
        float moveVertical = Input.GetAxis ("Vertical"); // возвращает значение от -1 до +1. Если джостик, то дробь.
        float moveHorizontal = Input.GetAxis ("Horizontal");
        Ship.velocity = new Vector3 (moveHorizontal, 0, moveVertical) * speed;

        // наклоны при перемещении с клавиатуры
        float restrictedX = Mathf.Clamp (Ship.position.x, xMin, xMax);
        float restrictedZ = Mathf.Clamp (Ship.position.z, zMin, zMax);
        Ship.position = new Vector3 (restrictedX, 0, restrictedZ);

        Ship.rotation = Quaternion.Euler (Ship.velocity.z * tilt, 0, -Ship.velocity.x * tilt);

        shotMain ();
    }

    void shotMain () {
        if (Time.time > nextShotTime
            //  && Input.GetButton ("Fire1")
        ) {
            Instantiate (LaserShot, LaserGun.position, Quaternion.identity);
            nextShotTime = Time.time + shotDelay;

            if (shield > 2) {
                GameObject RightLaser = Instantiate (LaserShot, RightLaserGun.position, Quaternion.Euler (0, 45, 0));
                RightLaser.GetComponent<Rigidbody> ().velocity = new Vector3 (50, 0, 50);
                RightLaser.transform.localScale *= sideShotSize;

                GameObject LeftLaser = Instantiate (LaserShot, LeftLaserGun.position, Quaternion.Euler (0, -45, 0));
                LeftLaser.GetComponent<Rigidbody> ().velocity = new Vector3 (-50, 0, 50);
                LeftLaser.transform.localScale *= sideShotSize;

                nextShotTime = Time.time + shotDelay / 2;
            }
        }
    }

    //При столкновении с другими коллайдерами (other)
    private void OnTriggerEnter (Collider other) {
        if (other.tag == "GameBorder" || other.tag == "PlayerLaser") {
            return;
        }

        if (other.tag == "Coin") { // собираем "щиты", если щит появился, то отображаем щит
            shield += 1;
            spheres.transform.localScale = new Vector3 (1, 1, 1);

            if (shield > 3) {
                shield = 3;
            }
            Destroy (other.gameObject);
            return;
        }

        if (shield > 0) { // если щитов нет, то убираем отображение щита
            shield -= 1;
            if (shield == 0) {
                spheres.transform.localScale = new Vector3 (0, 0, 0);
            }
            return;
        }

        Destroy (gameObject);

        Instantiate (sheepExplosion, transform.position, Quaternion.identity);

        GameControlScript.instance.showRestartButton ();
    }
}