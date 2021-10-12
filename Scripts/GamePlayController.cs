using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    public BoxSpawner boxSpawner;

    public CameraFollow cameraScripts;

    [HideInInspector]
    public Box currentBox;
    int moveCount;

    [HideInInspector]
    public bool isGameOver;

    AudioSource backgroundSound;
    public Text scoreText;
    int score;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = true;
        boxSpawner.Spawn();
        backgroundSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectInput();
    }

    void DetectInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentBox.DropBox();
        }
    }

    public void SpawnNewBox()
    {
        boxSpawner.Spawn();
    }

    public void MoveCamera()
    {
        moveCount++;
        score++;
        scoreText.text = "Score :" + score;

        if (moveCount == 3)
        {
            moveCount = 0;
            cameraScripts.targetPos.y += 2f; //Camera 위치조정.
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
