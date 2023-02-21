using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    Playing = 0,
    Complete = 1
};

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text rankMessage;
    public TMP_Text pickupMessage;
    public TMP_Text scoreMessage;
    public TMP_Text timeMessage;
    public Image pickupImage;
    public Button restartButton;
    private static int _score;
    private float _timer;
    private bool _pickupCollected;
    public static GameState gs;

    private void Start()
    {
        _pickupCollected = false;
        _timer = 99.0f;
        _score = 0;
        gs = GameState.Playing;
        restartButton.onClick.AddListener(OnRestartClicked);
    }

    private void Update()
    {
        if (gs == GameState.Playing)
        {
            // update timer, and set to 0 if it falls below
            if (_timer != 0.0f)
            {
                _timer -= Time.deltaTime;
                UpdateTimer();
            }
            if (_timer < 0.0f)
            {
                _timer = 0.0f;
            }
        }
        scoreText.text = "Score: " + _score;
    }
    
    public static void UpdateScore(int targetValue)
    {
        _score += targetValue;
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
        gs = GameState.Complete;
        // Lock player input (but not movement as the player flopping around could be fun)
        // Update UI to show results + a button to restart
        
        // hide playing elements
        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        // show end screen elements
        rankMessage.gameObject.SetActive(true);
        pickupMessage.gameObject.SetActive(true);
        scoreMessage.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        timeMessage.gameObject.SetActive(true);
        
        SetMessageText();
    }

    private void SetMessageText()
    {
        // rank
        if (_timer >= 79.0f)
        {
            rankMessage.text = "Based on your time...\n\nYour rank: Cheater!";
        }
        else if (_timer >= 69.0f)
        {
            rankMessage.text = "Based on your time...\n\nYour rank: S";
        }
        else if (_timer >= 59.0f)
        {
            rankMessage.text = "Based on your time...\n\nYour rank: A";
        }
        else if (_timer >= 49.0f)
        {
            rankMessage.text = "Based on your time...\n\nYour rank: B";
        }
        else if (_timer >= 39.0f)
        {
            rankMessage.text = "Based on your time...\n\nYour rank: C";
        }
        else if (_timer == 0.0f)
        {
            rankMessage.text = "Based on your time...\n\nYour rank: F";
        }
        
        // score
        scoreMessage.text = $"Score: {_score} / 500";
        // pickup
        pickupMessage.text = (_pickupCollected) ? "Pickup collected: Yes" : "Pickup collected: No";
        // time
        int timeRemaining = (int)((99.0f) - (99.0f - _timer));
        timeMessage.text = $"Time remaining: {timeRemaining}";
    }
}
