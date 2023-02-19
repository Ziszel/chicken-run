using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum GameState
{
    Playing = 0,
    Complete = 1
};

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text endMessage;
    public TMP_Text rankMessage;
    public TMP_Text gemMessage;
    public TMP_Text scoreMessage;
    public Image pickupImage;
    public Button restartButton;
    private static int _score;
    private float _timer;
    private bool _pickupCollected;
    private GameState _gs;

    private void Start()
    {
        _pickupCollected = false;
        _timer = 99.0f;
        _score = 0;
        _gs = GameState.Playing;
        restartButton.onClick.AddListener(OnRestartClicked);
    }

    private void Update()
    {
        if (_gs == GameState.Playing)
        {
            _timer -= Time.deltaTime;
            UpdateTimer();   
        }
    }
    
    public void UpdateScore(int targetValue)
    {
        _score += targetValue;
        scoreText.text = "Score: " + _score;
    }

    private void UpdateTimer()
    {
        int timerString = (int)Math.Round(_timer);
        timerText.text = timerString.ToString();
    }

    public void UpdatePickup()
    {
        _pickupCollected = true;
        pickupImage.gameObject.SetActive(true);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnRestartClicked()
    {
        RestartLevel();
    }

    public void CompleteLevel()
    {
        // Change game state to stop timer and score
        _gs = GameState.Complete;
        // Lock player input (but not movement as the player flopping around could be fun)
        // Update UI to show results + a button to restart
        
        // hide playing elements
        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        // show end screen elements
        endMessage.gameObject.SetActive(true);
        rankMessage.gameObject.SetActive(true);
        gemMessage.gameObject.SetActive(true);
        scoreMessage.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
}
