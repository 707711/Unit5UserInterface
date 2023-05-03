using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    private float spawnRate = 1.0f;

    private int score;
    public TextMeshProUGUI scoreText;
    public int lives;
    public TextMeshProUGUI livesText;

    public TextMeshProUGUI gameOverText;

    public bool isGameActive;

    public Button restartButton;

    public GameObject titleScreen;

    public bool isGamePaused;

    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = false;
        isGamePaused = false;
        StartCoroutine(SpawnTarget());

        score = 0;
        UpdateScore(0);

     
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
       
        score = 0;
        lives = 0;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(3);

        titleScreen.gameObject.SetActive(false);
    }

    public void pauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            if(isGamePaused)
            {
                pauseScreen.gameObject.SetActive(true);
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
            else
            {
                pauseScreen.gameObject.SetActive(false);
                Time.timeScale = 1;
                AudioListener.pause = false;
            }
        }
    }

    public void GameOver()
    {
        if(lives <= 0)
        {
            gameOverText.gameObject.SetActive(true);
            isGameActive = false;

            restartButton.gameObject.SetActive(true);
        }
        //gameOverText.gameObject.SetActive(true);
        //isGameActive = false;

        //restartButton.gameObject.SetActive(true);
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        pauseGame();
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score:" + score;
    }

    public void UpdateLives( int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives:" + lives;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
