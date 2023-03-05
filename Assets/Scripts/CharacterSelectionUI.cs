using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionUI : MonoBehaviour
{

    [Header("Character Selection Parameters")]
    [SerializeField] private float rotationSpeed;
    [Header("UI Elements")]
    public TMP_Text silverPointsText;
    public TMP_Text gemCollectedText;
    public TMP_Text bestTimeText;
    public Button startButton;
    [Header("Player Models")]
    public GameObject playerOne;
    public GameObject playerTwo;

    private void Start()
    {
        playerTwo.SetActive(false);
        startButton.onClick.AddListener(OnStartClicked);
        // Set text to equal their GameManager variable values
        SetTextValues();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckForRotation();
    }

    private void OnStartClicked()
    {
        // Use the game manager to control scene selection   
        GameManager.LoadScene("LevelOne");
    }

    private void CheckForRotation()
    {
        if (playerOne.activeInHierarchy)
        {
            RotatePlayerModel(playerOne);
        }

        if (playerTwo.activeInHierarchy)
        {
            RotatePlayerModel(playerTwo);
        }
    }

    private void RotatePlayerModel(GameObject gm)
    {
        gm.transform.eulerAngles += new Vector3(
            0.0f,
            rotationSpeed * Time.deltaTime,
            0.0f
            );
    }

    private void SetTextValues()
    {
        silverPointsText.text = "Silver Points: " + GameManager.TotalScore;
        gemCollectedText.text = (GameManager.GemCollected == true) ? "Gem Collected: Yes" : "Gem Collected: No";
        bestTimeText.text = "Best Time: " + (int)GameManager.BestTime;
    }
}
