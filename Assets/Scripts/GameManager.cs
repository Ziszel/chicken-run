using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public static int TotalScore;
    public static bool GemCollected;
    public static float BestTime;

    private void Awake()
    {
        // If an Instance exists (character select screen ran before), do not create another
        // This stops duplication of this class on reloading of the scene
        if (Instance != null)
        {
            // destroy the newly created game object on scene load as one already exists
            Destroy(gameObject);
            return;
        }

        TotalScore = 0;
        GemCollected = false;
        BestTime = 99.0f;

        Instance = this;
        DontDestroyOnLoad(gameObject); // don't destroy this game object when loading a scene
    }
    
    // This method is called by the level manager after the player returns from scene: LevelOne
    public static void UpdateGameState(int latestScore, bool gemCollected, float latestTime)
    {
        // Update the TotalScore
        TotalScore += latestScore;
        
        // check if gem has been collected(this code would need significant refactoring if creating more than one level)
        if (gemCollected)
        {
            GemCollected = true;
        }

        // I could have the GemCollected variable reset if the player does not capture the gem, however
        // these are actually meant to persist so this code is unneeded, and it would be more obvious why
        // with more levels and more gems.
        /*if (!gemCollected)
        {
            GemCollected = false; 
        }*/
        
        if (latestTime < BestTime)
        {
            int timeResult = 99 - Mathf.CeilToInt(latestTime);
            BestTime = timeResult;
        }
    }

    public static void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
