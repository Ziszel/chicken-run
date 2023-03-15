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
    public TMP_Text charDescText;
    public Button startButton;
    public Button changeCharacterButton;
    public Button purchaseCharacter;
    [Header("Player Models")]
    public GameObject playerOne;
    public GameObject playerTwo;

    private void Start()
    {
        GameManager.SetSelectedCharacter("Gallagher");
        charDescText.text = "Gallagher is still living it, like it's 1993. We're gonna live forever baby.";
        playerTwo.SetActive(false);
        startButton.onClick.AddListener(OnStartClicked);
        changeCharacterButton.onClick.AddListener(OnCharChangeClicked);
        purchaseCharacter.onClick.AddListener(OnPurchaseClicked);
        // Set text to equal their GameManager variable values
        SetTextValues();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckForRotation();
        CheckForCharUnlocked();
    }

    private void OnStartClicked()
    {
        // Use the game manager to control scene selection   
        GameManager.LoadScene("LevelOne");
    }

    private void OnCharChangeClicked()
    {
        if (playerOne.activeInHierarchy)
        {
            GameManager.SetSelectedCharacter("Hobo");
            playerOne.SetActive(false);
            playerTwo.SetActive(true);
            charDescText.text = "Hobo Bill has a beard and doesn't like to wash.";
        }
        else if (playerTwo.activeInHierarchy)
        {
            GameManager.SetSelectedCharacter("Gallagher");
            playerTwo.SetActive(false);
            playerOne.SetActive(true);
            charDescText.text = "Gallagher is still living large like it's 1994. We're gonna live forever baby.";
        }
    }

    private void OnPurchaseClicked()
    {
        if (GameManager.TotalScore >= 1000)
        {
            GameManager.UnlockGallagher();
            SetTextValues();
        }
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

    private void CheckForCharUnlocked()
    {
        if (playerOne.activeInHierarchy)
        {
            startButton.gameObject.SetActive(true);
            purchaseCharacter.gameObject.SetActive(false);
        }
        else if (playerTwo.activeInHierarchy)
        {
            if (GameManager.GallagherUnlocked)
            {
                startButton.gameObject.SetActive(true);
                purchaseCharacter.gameObject.SetActive(false);
            }
            else
            {
                startButton.gameObject.SetActive(false);
                purchaseCharacter.gameObject.SetActive(true);
            }
        }
    }
}
