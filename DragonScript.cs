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

    public bool isInverse;

    private bool gameOver;
    private int score;
    private float soundVolume;

    public GameObject GameOverPanel;
    public GameObject NewHighScore;

    public Text scoreText;
    public Text gameOverScoreText;
    public Text gameOverHighScoreText;
    public Text SlowMotionText;
    public Text InvincibleText;

    public AudioSource dieAudio;
    public AudioSource flyAudio;
    public AudioSource scoreAudio;

    public GameObject PauseMenu;

    public bool isInvincible = false;
    public bool isSlow = false;

    private int haveInvincibleforSeconds = 0;
    private int haveSlowMotionforSeconds = 0;

    int currentLevel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOver = false;
        score = 0;
        soundVolume = PlayerPrefs.GetFloat("SoundVolume");
        Time.timeScale = 1.0f;

        currentLevel = PlayerPrefs.GetInt("currentLevel");

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
            float targetRotation = Mathf.Lerp(0.0f, 60.0f, 1.0f - (1.0f / (1.0f + Mathf.Exp(rb.velocity.y))));
            Quaternion targetQuaternion = Quaternion.Euler(0, 0, targetRotation);

            if (isInverse)
            {
            targetRotation = Mathf.Lerp(0.0f, -90.0f, 1.0f - (1.0f / (1.0f + Mathf.Exp(-rb.velocity.y))));
            targetQuaternion = Quaternion.Euler(0, 180, targetRotation);
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, Time.deltaTime * rotationSmoothness);
        }
        // Smoothly rotate the Dragon downwards when going down
        else
        {
            float targetRotation = Mathf.Lerp(0.0f, -25.0f, 1.0f - (1.0f / (1.0f + Mathf.Exp(-rb.velocity.y))));
            Quaternion targetQuaternion = Quaternion.Euler(0, 0, targetRotation);

            if (isInverse)
            {
                targetRotation = Mathf.Lerp(0.0f, 90.0f, 1.0f - (1.0f / (1.0f + Mathf.Exp(rb.velocity.y))));
                targetQuaternion = Quaternion.Euler(0, 180, targetRotation);
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQuaternion, Time.deltaTime * rotationSmoothness);
        }

        if(transform.localPosition.y < -500f)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, -500f);
        }

        if(transform.localPosition.y > 500f)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, 500f);
        }

        if(haveSlowMotionforSeconds > 0)
        {
            SlowMotionText.gameObject.SetActive(true);
            SlowMotionText.text = "Slow Motion : " + haveSlowMotionforSeconds;
        }
        else
        {
            SlowMotionText.gameObject.SetActive(false);
        }
        
        if(haveInvincibleforSeconds > 0)
        {
            InvincibleText.gameObject.SetActive(true);
            InvincibleText.text = "Invincible : " + haveInvincibleforSeconds;
        }
        else
        {
            InvincibleText.gameObject.SetActive(false);
        }

    }

    void RotateDragon(float angle)
    {
        if (isInverse)
        {
        transform.rotation = Quaternion.Euler(0, 180, angle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("DiePoint") && !isInvincible){
            gameOver = true;
            dieAudio.Play();
            GameOver();
        }
        if(other.CompareTag("ScorePoint")){
            score++;
            scoreAudio.Play();
            other.gameObject.tag = "Untagged";
        }
        if (other.CompareTag("InvinciblePowerUp"))
        {
            isInvincible = true;
            other.gameObject.SetActive(false);
            haveInvincibleforSeconds += 10;
            Invoke("InvincibleDisabler", 1f);
        }
        if (other.CompareTag("TimePowerUp"))
        {
            isSlow = true;
            other.gameObject.SetActive(false);
            Time.timeScale = 0.5f;
            haveSlowMotionforSeconds += 5;
            Invoke("SlowMotionDisabler", 1f);
        }
    }

    void SlowMotionDisabler()
    {
        if (haveSlowMotionforSeconds > 1)
        {
            haveSlowMotionforSeconds--;
            Invoke("SlowMotionDisabler", 1f);
            return;
        }
        haveSlowMotionforSeconds = 0;
        isSlow = false;
        Time.timeScale = 1.0f;
    }

    void InvincibleDisabler()
    {
        if(haveInvincibleforSeconds > 1)
        {
            haveInvincibleforSeconds--;
            Invoke("InvincibleDisabler", 1f);
            return;
        }

        haveInvincibleforSeconds = 0;
        isInvincible = false;
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
