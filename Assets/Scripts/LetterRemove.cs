namespace Letters
{
    
    using UnityEngine;
    using System.IO;
    using UnityEngine.UI;
    using System.Collections.Generic;
    using TMPro;

    public class LetterRemove : MonoBehaviour
    {
        /// <summary>
        /// have a text file with wordas
        /// load words into an array
        /// select a random word
        /// debug how may letters in word
        /// see if word contains the letter a
        /// </summary>

       //dificulty
        public string[] difficulty;
        public string selectedDifficulty;
        public int selectedDifficultyIndex;
        
        //find file for difficulty
        string filePath;

        //find word in difficulty
        public string chosenWord;
        public char[] charaters;

        //display prefabs
        public GameObject characterDisplayPrefab;
        public Transform spawanLocation;
        public Text[] textDisplay;
        public GameObject gameSpace;

        public List<string> lettersGuessed;
        public List<string> lettersWrong;

        public List<Text> prefabSpawnedText;
        
        public int playerScore;  

        void Start()
        {
            if (gameSpace == true)
            {
                SavePlay();
                //WinCondition();
                return;
                
            }
        }

        //press button
        private void OnGUI()
        {
           
            KeyPressed();
        }

        void KeyPressed()
        {
            Event currentEvent = Event.current;

            if (currentEvent.isKey && Input.GetKeyDown(currentEvent.keyCode) && currentEvent.keyCode.ToString().Length == 1 && char.IsLetter(currentEvent.keyCode.ToString()[0]))
            {
                

                    Debug.Log(currentEvent.keyCode);
                if (lettersGuessed.Contains(currentEvent.keyCode.ToString()))
                {
                    Debug.Log("letter already guessed");
                }
                else if (chosenWord.Contains((char)currentEvent.keyCode))
                {
                    Debug.Log("hi");

                    lettersGuessed.Add(currentEvent.keyCode.ToString());
                    for (int i = 0; i < chosenWord.Length; i++)
                    {
                        if (chosenWord[i].ToString().ToLower() == currentEvent.keyCode.ToString().ToLower())
                        {
                            prefabSpawnedText[i].text = currentEvent.keyCode.ToString();
                        }
                    }
                }
                if (!chosenWord.Contains((char)currentEvent.keyCode))
                {
                    lettersWrong.Add(currentEvent.keyCode.ToString());
                }
                if (lettersWrong.Contains(currentEvent.keyCode.ToString()))
                {
                    Debug.Log("letter already guessed");
                }

            }
             
        }

        //saved words 
        void SavePlay()
        {
            prefabSpawnedText.Clear();
            SelectTextFile();
            chosenWord = SplitTextFile(ReadTextFile());
            charaters = chosenWord.ToCharArray();
            Debug.Log(charaters.Length);
            textDisplay = new Text[charaters.Length];
            for (int i = 0; i < charaters.Length; i++)
            {
                Text currentLetter = Instantiate(characterDisplayPrefab,spawanLocation).GetComponentInChildren<Text>();
                textDisplay[i] = currentLetter;
                prefabSpawnedText.Add(currentLetter);
            }

            //if (chosenWord.Contains('a'))
            //{
            //    Debug.Log("there is an a");
            //    foreach (char letter in charaters)
            //    {
            //        if (letter == 'a')
            //        {
            //            Debug.Log("a was found");
            //        }
            //    }

        }
        void SelectTextFile()
        {
            //WordsAsset.;
            selectedDifficultyIndex = Random.Range(0, difficulty.Length);
            selectedDifficulty = difficulty[selectedDifficultyIndex];
            filePath = $"{Application.dataPath}/Words/{selectedDifficulty}.txt";
            Debug.Log(filePath);
        }

        //file path find
        string ReadTextFile()
        {
            return File.ReadAllText(filePath);
        }

        //read words in the file
        private string SplitTextFile(string words)
        {
            string[] wordsInFile = words.Split('|');
            string chooseWord = wordsInFile[Random.Range(0, wordsInFile.Length)];
            return chooseWord;
        }

        //see what happens to win
        //void PlayerSuccess()
        //{
        //    if (prefabSpawnedText.ToString == lettersGuessed.ToString())
        //    {

        //    }
           
        //}

    }
}