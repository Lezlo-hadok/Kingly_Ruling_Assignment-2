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
        public string[] difficulty; //creates array for difficulty
        public string selectedDifficulty; //variable used to select difficluty
        public int selectedDifficultyIndex;//selected difficulty varible
        public static int chosenDifficulty = 0;
        //find file for difficulty
        string filePath;

        //find word in difficulty
        public string chosenWord; // word chosen from difficluty
        public char[] charaters; // to have charaters seperate from chosen word as a charater array

        //display prefabs
        public GameObject characterDisplayPrefab; //prefab for display
        public Transform spawanLocation; //Spawn location area
        public Text[] textDisplay; // text to be displayed as an array
        public GameObject gameSpace; //the gamepspace

        //list of letters guessed correctly
        public List<string> lettersGuessed;//creates a list for letters guessed that is used to check agaisnt the letters that are correct

        //list of letters guessed incorrectly
        public List<string> lettersWrong; //list for checking the letters guessed that are wrong (will not be displayed)

        public List<Text> prefabSpawnedText; //list for what the text displayed will say as seprate letters

        
        //attempts remaining
        public int attemptsRemaining; //number of attemps remaining
        public Text attemptsText; //for displaying attempts
        
        //player score to display
        public static int playerScore; // player score varible
        public Text scoreText; //for displaying varible

        //weather game is over 
        private bool gameOver; //see if game is over

        //win and continue
        private bool hasWon; //see if game has been won

        //show win and lose screen
        public GameObject winScreen; //for showing win screen
        public GameObject loseScreen; //for showing win screen

        //Continue button
        public Button btnContinue; //button varible to add code to

        private void Start()//on game start
        {
            
            this.winScreen.SetActive(false);//set the win screen to off 
            this.loseScreen.SetActive(false);//set the lose screen to off
            gameOver = false; //set game over to off
            AttemptsRemaining(GetAttemptsRemaining()); //display the number of attemps remaining as a number
            PlayerScoreChange(GetPlayerScore()); //display the current score from words in difficulty selected
            
            if (hasWon)// open loop //see if the player has won the game
            {
                NewWord();//attenpt point
            }//end loop

            if (gameSpace == true)//open loop //see if the gamespace is there
            {
                SavePlay();//function for the words to spawn and be chosen from difficulty
                
                
                return;
                
            }//end loop
            if (Input.GetKeyDown(KeyCode.Space)) //attempt point
            {
                Debug.Log("you pressed continue");
                SavePlay();

            }



            

        }

        //press button
        private void OnGUI() //open void for when a gui ellement is input
        {

            if (!gameOver)//open loop //check if game over and pplayer has lost
            {
                if (attemptsRemaining != 0)//open loop// check to see if attemps remaining is not zero
                {
                    KeyPressed();//fucntion for pressing keys
                    
                }//end loop
                else //open loop
                {
                    Debug.Log("Game over");
                    gameOver = true;
                    this.loseScreen.SetActive(true);
                    playerScore = 0;
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
                    AttemptsRemaining(GetAttemptsRemaining());

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

            attemptsRemaining = 6;
            AttemptsRemaining(GetAttemptsRemaining());
        }
        //choses a random word from a random difficulty
        void SelectTextFile()
        {
            
            selectedDifficultyIndex = chosenDifficulty;
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

        //shows attempts dislpay
        void AttemptsRemaining(int attemptsRemaining)
        {
            attemptsText.text = attemptsRemaining.ToString();
        }


        private int GetPlayerScore()
        {
            return playerScore;
        }

        void PlayerScoreChange(int playerScore)
        {
            scoreText.text = playerScore.ToString();
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
            hasWon = true;
            playerScore += 1;
            PlayerScoreChange(GetPlayerScore());
            AfterWin();
            
        }

        //continue to next word?
        void AfterWin()
        {
            if (hasWon)
            {
                this.winScreen.SetActive(true);
                NewWord();
            }
        }

        void NewWord()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("you pressed continue");
                SavePlay();
                
            }
        }

        //void ContinueButton()
        //{
        //    Button btn = btnContinue.GetComponet<Button>();
        //    btn.onClick.AddListener(OnContinueClick);
        //}

        //void OnContinueClick()
        //{
        //    Debug.Log("Button clicked");
        //}

    }
}