using Letters;
using System.IO;

using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneScripts : MonoBehaviour
{
    [System.Serializable]
    
    public class SaveData
    {
        public int chosenDifficulty;
        public int playerScore;
        public int attemptsRemaining;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ChangeSceneByName(string sceneName)
    {
      SceneManager.LoadScene(sceneName);  //pick a scene by the name of the scene 
        Debug.Log("this worked");
    }

    // Update is called once per frame
    public void ChangeSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);//change the scene by the chosen index
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reload current when activiated scene
        ResetGuess();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); //reloads current scene
    }
    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
    public void SelectDif(int i)
    {
        LetterRemove.chosenDifficulty = i; //selects the difficulty in the range of a number
    }
    public void ResetScore()
    {
        LetterRemove.playerScore = 0; //set the player score to 0 
    }
    public void ResetGuess()//resets the guess when scene resets for a new word
    {
        LetterRemove.attemptsRemaining = 6;//sets attempts to 6
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame();
    }

    public void LoadGame()
    {
        SaveSystem.LoadGame();
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static class SaveSystem
    {
        private static string savePath = Application.persistentDataPath + "/savefile.json";

        public static void SaveGame()
        {
            SaveData data = new SaveData
            {
                chosenDifficulty = LetterRemove.chosenDifficulty,
                playerScore = LetterRemove.playerScore,
                attemptsRemaining = LetterRemove.attemptsRemaining
            };

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(savePath, json);
            Debug.Log("Game saved to: " + savePath);
        }

        public static void LoadGame()
        {
            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                LetterRemove.chosenDifficulty = data.chosenDifficulty;
                LetterRemove.playerScore = data.playerScore;
                LetterRemove.attemptsRemaining = data.attemptsRemaining;

                Debug.Log("Game loaded.");
            }
            else
            {
                Debug.LogWarning("Save file not found.");
            }
        }
    }
}
