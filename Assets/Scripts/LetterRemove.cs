namespace Letters
{
    
    using UnityEngine;
    using System.IO;
    using UnityEngine.UI;
    using System.Collections.Generic;
    

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

        //list of letters guessed correctly
        public List<string> lettersGuessed;

        //list of letters guessed incorrectly
        public List<string> lettersWrong;

        public List<Text> prefabSpawnedText;

        //attempts remaining spawn for counter
        public GameObject AttemptsNprefab;
        public Transform spawnLocationAttempts;
        public Text[] textDisplayAttempts;

        //attempts remaining
        public int attemptsRemaining;

        public List<Text> attemptsN;
        
        //player score to display
        public int playerScore;  

        //weather game is over 
        private bool gameOver;

        void Start()
        {
            playerScore = 0;
            gameOver = false;
            AttemptsRemaining(GetAttemptsRemaining());
            if (gameSpace == true)
            {
                SavePlay();
                attemptsRemaining = 6;
                AttemptsRemaining(GetAttemptsRemaining());
                
                return;
                
            }
        }

        //press button
        private void OnGUI()
        {

            if (!gameOver)
            {
                if (attemptsRemaining != 0)
                {
                    KeyPressed();
                }
                else
                {
                    Debug.Log("Game over");
                    gameOver = true;
                }
            }


        }

        /// <summary>
        /// when any letter key is pressed then allow for letter to move to the wrong or right pile point for the collection of letters
        /// </summary>
        void KeyPressed()
        {
            //check using var if something is active
            Event currentEvent = Event.current;

            //check the key code on the keyboard
            if (currentEvent.isKey && Input.GetKeyDown(currentEvent.keyCode) && currentEvent.keyCode.ToString().Length == 1 && char.IsLetter(currentEvent.keyCode.ToString()[0]))
            {
                

                    Debug.Log(currentEvent.keyCode);
                if (lettersGuessed.Contains(currentEvent.keyCode.ToString()))
                {
                    Debug.Log("letter already guessed");
                }
                else if (chosenWord.Contains((char)currentEvent.keyCode))
                {
                    
                    //check to see if letter guessed is in word
                    lettersGuessed.Add(currentEvent.keyCode.ToString());
                    for (int i = 0; i < chosenWord.Length; i++)
                    {
                        if (chosenWord[i].ToString().ToLower() == currentEvent.keyCode.ToString().ToLower())
                        {
                            prefabSpawnedText[i].text = currentEvent.keyCode.ToString();
                        }
                    }
                    //checks to see if condition for winning is met
                    WinCondition();
                }
                //adds letters wrong to a list for use as a text point to show letters guessed
                if (!chosenWord.Contains((char)currentEvent.keyCode))
                {
                    lettersWrong.Add(currentEvent.keyCode.ToString());
                    
                    attemptsRemaining -= 1;
                }
                //to show letter guessed was already guessed 
                if (lettersWrong.Contains(currentEvent.keyCode.ToString()))
                {
                    Debug.Log("letter already guessed attempt lost");
                }

            }
             
        }

        //saved words 
        void SavePlay()
        {
            //clears the prefab play point
            prefabSpawnedText.Clear();

            //get a word from a text file
            SelectTextFile(); //pulls fuction
            chosenWord = SplitTextFile(ReadTextFile()); //pulls funtion
            charaters = chosenWord.ToCharArray(); //changes word to an array of charaters
            Debug.Log(charaters.Length); //shows the amount of charaters in the debug log

            //shows the text display of teh chartater point
            textDisplay = new Text[charaters.Length];
            for (int i = 0; i < charaters.Length; i++)
            {
                Text currentLetter = Instantiate(characterDisplayPrefab,spawanLocation).GetComponentInChildren<Text>();
                textDisplay[i] = currentLetter;
                prefabSpawnedText.Add(currentLetter);
            }


        }
        //choses a random word from a random difficulty
        void SelectTextFile()
        {
            
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

        private int GetAttemptsRemaining()
        {
            return attemptsRemaining;
        }

        //attempts remaining
        //void AttemptsRemaining()
        //{
        //   foreach (char lettersWrong in )
        //    {

        //    }

        //    //if (attemptsRemaining == 0) 
        //    //{
        //    //    Debug.Log("this now work");
        //    //}

        //}

        //this dooesnt currently work proporly
        void AttemptsRemaining(int attemptsRemaining)
        {
            //clears the display on 
            foreach (Text t in attemptsN)
            {
                Destroy(t.gameObject);
            }
            attemptsN.Clear();

            

            // Display remaining attempts
            string attemptsText = attemptsRemaining.ToString();
            Text newText = Instantiate(AttemptsNprefab, spawnLocationAttempts).GetComponentInChildren<Text>();
            newText.text = attemptsText;
            Text[] texts = new Text[attemptsRemaining];
            textDisplay = texts;
            attemptsN.Add(newText);

            if (attemptsRemaining <= 0)
            {
                Debug.Log("Game Over! The word was: " + chosenWord);
                
            }
        }




        //win condidtion
        void WinCondition()
        {
            foreach (char c in chosenWord)
            {
                if (!lettersGuessed.Contains(c.ToString().ToUpper()) && !lettersGuessed.Contains(c.ToString().ToLower()))
                {
                    return;
                }
            }

            Debug.Log("You Win!");
            gameOver = true;
            playerScore += 1;
            
        }

        ////continue to next word?
        //void AfterWin()
        //{
            
        //}


    }
}