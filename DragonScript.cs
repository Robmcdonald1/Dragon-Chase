using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragonScript : MonoBehaviour
{
    public float jumpForce = 5.0f;
    public float rotationSmoothness = 2.0f;
    private Rigidbody2D rb;

    private bool gameOver;
    private int score;
    private float soundVolume;

    public GameObject GameOverPanel;
    public GameObject NewHighScore;

    public Text scoreText;
    public Text gameOverScoreText;
    public Text gameOverHighScoreText;

    public AudioSource dieAudio;
    public AudioSource flyAudio;
    public AudioSource scoreAudio;

    public GameObject PauseMenu;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOver = false;
        score = 0;
        soundVolume = PlayerPrefs.GetFloat("SoundVolume");

        dieAudio.volume = soundVolume;
        flyAudio.volume = soundVolume;
        scoreAudio.volume = soundVolume;
    }

  

    void FixedUpdate(){
        scoreText.text = "Score : " + score.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
            RotateDragon(-30.0f);
            flyAudio.Play();
        }

        // Smoothly rotate the Dragon upwards when going up
        if (rb.velocity.y > 0)
        {
            float targetRotation = Mathf.Lerp(0.0f, -90.0f, 1.0f - (1.0f / (1.0f + Mathf.Exp(-rb.velocity.y))));
            Quaternion targetQuaternion = Quaternion.Euler(0, 180, targetRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, Time.deltaTime * rotationSmoothness);
        }
        // Smoothly rotate the Dragon downwards when going down
        else
        {
            float targetRotation = Mathf.Lerp(0.0f, 90.0f, 1.0f - (1.0f / (1.0f + Mathf.Exp(rb.velocity.y))));
            Quaternion targetQuaternion = Quaternion.Euler(0, 180, targetRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, Time.deltaTime * rotationSmoothness);
        }
    }

    void RotateDragon(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 180, angle);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("DiePoint")){
            gameOver = true;
            dieAudio.Play();
            GameOver();
        }
        if(other.CompareTag("ScorePoint")){
            score++;
            scoreAudio.Play();
            other.gameObject.tag = "Untagged";
        }
    }

    void GameOver(){
        Time.timeScale = 0f;
        GameOverPanel.SetActive(true);
        int highscore = PlayerPrefs.GetInt("HighScore");
        if(score > highscore){
            PlayerPrefs.SetInt("HighScore", score);
            NewHighScore.SetActive(true);
        }
        else{
            NewHighScore.SetActive(false);
        }
        gameOverHighScoreText.text = "HighScore : " + PlayerPrefs.GetInt("HighScore").ToString();
        gameOverScoreText.text = "Score : " + score.ToString();
    }

    public void PlayAgain(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void MainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame(){
        if(GameOverPanel.activeSelf){
            return;
        }
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
    }
    public void ResumeGame(){
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }
}
