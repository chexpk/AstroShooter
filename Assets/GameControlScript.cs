using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlScript : MonoBehaviour {
    public UnityEngine.UI.Text scoreText;
    public UnityEngine.UI.Button startButton;
    public GameObject menu;
    public GameObject restart;
    public UnityEngine.UI.Text restartText;
    public UnityEngine.UI.Button restartButton;

    public bool isStarted = false;
    public bool isPlayerLive = true;

    public GameObject PlayerPrefab;

    public int score = 0;
    public static GameControlScript instance;

    private void Start () {
        instance = this;
        startButton.onClick.AddListener (
            delegate {
                menu.SetActive (false);
                isStarted = true;
                restart.SetActive (false);
                Instantiate (PlayerPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
            }
        );
        restartButton.onClick.AddListener (
            delegate {
                isStarted = true;
                restart.SetActive (false);
                Instantiate (PlayerPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
                score = 0;
                scoreText.text = "Score: " + score;
            }
        );
    }

    public void increaseScore (int increment) {
        score += increment;
        scoreText.text = "Score: " + score;
    }

    public void showRestartButton () {
        isStarted = false;
        restart.SetActive (true);
        restartText.text = "Your score is " + score;
        if (score > 300) {
            restartText.text = "Ого!!! Счёт " + score + "! Ты огонь!";
        }
    }

}