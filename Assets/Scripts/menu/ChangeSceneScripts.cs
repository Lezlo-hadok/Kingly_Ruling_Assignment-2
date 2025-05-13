using Letters;
using System.IO;

using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneScripts : MonoBehaviour
{
    [System.Serializable]
    
    public class SaveData //varible determain
    {
        public int chosenDifficulty;
        public int playerScore;
        public int attemptsRemaining;
        public string chosenWord;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);//loads the previous scene
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

    public void SaveGame()//attached to button
    {
        SaveSystem.SaveGame(); //save file
    }

    public void LoadGame()//attached to button
    {
        SaveSystem.LoadGame(); //load file
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static class SaveSystem
    {
        private static string savePath = Application.persistentDataPath + "/savefile.json"; //write the file and file save path

        public static void SaveGame()//save game funtion
        {
            SaveData data = new SaveData //save data varible and what varibles are going into the file
            {
                chosenDifficulty = LetterRemove.chosenDifficulty, //varible assign
                playerScore = LetterRemove.playerScore, //varible assign
                attemptsRemaining = LetterRemove.attemptsRemaining, //varible assign
                chosenWord = LetterRemove.chosenWord //varible assign
            };

            string json = JsonUtility.ToJson(data, true); //set the save data into the json file 
            File.WriteAllText(savePath, json); // write the save file
            Debug.Log("Game saved to: " + savePath); //debug where the save path is
        }

        public static void LoadGame()// load game funtion
        {
            if (File.Exists(savePath))// if the file exists in the save path
            {
                string json = File.ReadAllText(savePath);//read the file at the save path
                SaveData data = JsonUtility.FromJson<SaveData>(json);//convert data to a string

                LetterRemove.chosenDifficulty = data.chosenDifficulty; //set data to varible
                LetterRemove.playerScore = data.playerScore;//set data to varible
                LetterRemove.attemptsRemaining = data.attemptsRemaining;//set data to varible
                LetterRemove.chosenWord = data.chosenWord;//set data to varible

                Debug.Log("Game loaded.");//check game loaded
            }
            else
            {
                Debug.LogWarning("Save file not found.");//save file not found
            }
        }
    }
}
