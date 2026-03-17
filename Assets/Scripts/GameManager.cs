using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float gameTime = 60f;
    private float timer;
    public TextMeshProUGUI timerText;
    bool isGameOver = false;

    public Canvas gameOvercanvas;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScore;
    
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        timer = gameTime;
    }

    void Update()
    {
        if (!isGameOver)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timer).ToString();

            if (timer <= 0)
            {
                timer = 0;
                isGameOver = true;

                gameOvercanvas.enabled = true;
                Time.timeScale = 0f;

                finalScore.text = "Your Score: " + score;
            }
        }
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
