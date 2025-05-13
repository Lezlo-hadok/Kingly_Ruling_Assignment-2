namespace Letters //namespace
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
        public static string chosenWord; // word chosen from difficluty
        public char[] charaters; // to have charaters seperate from chosen word as a charater array

        //display prefabs
        public GameObject characterDisplayPrefab; //prefab for display
        public Transform spawanLocation; //Spawn location area
        public Text[] textDisplay; // text to be displayed as an array
        public GameObject gameSpace; //the gamepspace

        //list of letters guessed correctly
        public List<string> lettersGuessed;//creates a list for letters guessed that is used to check agaisnt the letters that are correct
        public string letterHold;

        //list of letters guessed incorrectly
        public List<string> lettersWrong; //list for checking the letters guessed that are wrong (will not be displayed)
        private string wrongLetter;//to string the list of wrong letters

        public List<Text> prefabSpawnedText; //list for what the text displayed will say as seprate letters

        
        //attempts remaining
        public static int attemptsRemaining; //number of attemps remaining
        public Text attemptsText; //for displaying attempts
        
        //player score to display
        public static int playerScore; // player score varible
        public Text scoreText; //for displaying varible

        //weather game is over 
        private bool gameOver; //see if game is over

        

        //show win and lose screen
        public GameObject _winScreen; //for showing win screen
        public GameObject _loseScreen; //for showing win screen

        //for letter display
        public Text _wrongGuesses;//for displaying letter wrong

        //for lost word display when player loses
        public Text _lostWord;//for displaying word guessed wrong

        //for displaying and holding letter
        public Text _letterConfirm;//display the letter for confirming
        private string pendingLetter = "";//string set to nothing


        private void Start()//on game start
        {
            
           
            this._winScreen.SetActive(false);//set the win screen to off 
            this._loseScreen.SetActive(false);//set the lose screen to off
            gameOver = false; //set game over to off
            AttemptsRemaining(GetAttemptsRemaining()); //display the number of attemps remaining as a number
            PlayerScoreChange(GetPlayerScore()); //display the current score from words in difficulty selected
            //GuessedWrongDisplay(GetGuessedDisplay());

            if (gameSpace == true)//open loop //see if the gamespace is there
            {
                SavePlay();//function for the words to spawn and be chosen from difficulty
                return;
                
            }//end loop
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
                    
                    gameOver = true; //see if game is not lost
                    this._loseScreen.SetActive(true); //turn on lose 
                    _lostWord.text = ($"Your word was: {chosenWord}"); //show what the word was on lose
                }
            }


        }

        /// <summary>
        /// when any letter key is pressed then allow for letter to move to the wrong or right pile point for the collection of letters
        /// </summary>
        //void KeyPressed()
        //{
        //    //check using var if something is active
        //    Event currentEvent = Event.current;

        //    //check the key code on the keyboard
        //    if (currentEvent.isKey && Input.GetKeyDown(currentEvent.keyCode) && currentEvent.keyCode.ToString().Length == 1 && char.IsLetter(currentEvent.keyCode.ToString()[0])) //open if for input of charater, current event key code, 
        //    {

        //        if (!gameOver)
        //        {
        //            Debug.Log(currentEvent.keyCode);
        //            if (lettersGuessed.Contains(currentEvent.keyCode.ToString()))
        //            {
        //                Debug.Log("letter already guessed");
        //            }
        //            else if (chosenWord.Contains((char)currentEvent.keyCode))
        //            {

        //                //check to see if letter guessed is in word
        //                lettersGuessed.Add(currentEvent.keyCode.ToString());
        //                for (int i = 0; i < chosenWord.Length; i++)
        //                {
        //                    if (chosenWord[i].ToString().ToLower() == currentEvent.keyCode.ToString().ToLower())
        //                    {
        //                        prefabSpawnedText[i].text = currentEvent.keyCode.ToString();

        //                    }
        //                }
        //                //checks to see if condition for winning is met
        //                WinCondition();
        //            }
        //            //adds letters wrong to a list for use as a text point to show letters guessed
        //            if (!chosenWord.Contains((char)currentEvent.keyCode))
        //            {
        //                lettersWrong.Add(currentEvent.keyCode.ToString());

        //                _wrongGuesses.text = "";
        //                foreach (string wrongLetters in lettersWrong)
        //                {
        //                    _wrongGuesses.text += (wrongLetters + " ");
        //                }

        //                attemptsRemaining -= 1;
        //                AttemptsRemaining(GetAttemptsRemaining());

        //            }
        //            //to show letter guessed was already guessed 
        //            if (lettersWrong.Contains(currentEvent.keyCode.ToString()))
        //            {
        //                Debug.Log("letter already guessed attempt lost");
        //            }
        //        }
        //    }

        //}

        void KeyPressed()
        {
            Event currentEvent = Event.current;//sets a varible for current event

            if (currentEvent.isKey && Input.GetKeyDown(currentEvent.keyCode))//when key is pressed
            {
                KeyCode key = currentEvent.keyCode;//key is the varible for the key pressed

                // Check for ENTER key
                if (key == KeyCode.Return || key == KeyCode.KeypadEnter)//checks enter key
                {
                    if (!gameOver && !string.IsNullOrEmpty(pendingLetter))//if game is not over and there is no letter in letter pending
                    {
                        string inputLetter = pendingLetter.ToLower();//have a letter stored in a private area 
                        _letterConfirm.text = ""; // clear letter display

                        if (lettersGuessed.Contains(inputLetter) || lettersWrong.Contains(inputLetter))//if letter is guessed withing teh two lists of letters guessed
                        {
                            Debug.Log("Letter already guessed.");//debug for game editor section to check if working
                            return;
                        }

                        if (chosenWord.ToLower().Contains(inputLetter))//see if the chosen word has the letter guessed
                        {
                            lettersGuessed.Add(inputLetter);//add the letter input to the lettes guessed list
                            for (int i = 0; i < chosenWord.Length; i++)//chosen word lengh check if the word is full/guessed and add one for every letter guessed correctly
                            {
                                if (chosenWord[i].ToString().ToLower() == inputLetter)//
                                {
                                    prefabSpawnedText[i].text = inputLetter.ToUpper();//display letter guessed as an upercase letter on the prefab
                                }
                            }
                            WinCondition();//run win condition function
                        }//end if
                        else
                        {
                            lettersWrong.Add(inputLetter);//add wrong guesses to this list
                            _wrongGuesses.text = string.Join(" ", lettersWrong);//
                            attemptsRemaining -= 1;//get rid of one attempt point
                            AttemptsRemaining(GetAttemptsRemaining());//change attempts remianing score
                        }

                        pendingLetter = ""; // clear after processing
                    }
                }
                // If letter key pressed (and not Enter), store the letter
                else if (key.ToString().Length == 1 && char.IsLetter(key.ToString()[0]))
                {
                    pendingLetter = key.ToString().ToLower();//set the pending letter to a lowercase letter
                    _letterConfirm.text = $"{pendingLetter}";//display letter in press to enter key
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
            textDisplay = new Text[charaters.Length]; //create text of the word in text
            for (int i = 0; i < charaters.Length; i++)//
            {
                
                Text currentLetter = Instantiate(characterDisplayPrefab,spawanLocation).GetComponentInChildren<Text>();//add text and ammount of charaters as the 
                textDisplay[i] = currentLetter; //
                prefabSpawnedText.Add(currentLetter); //
            }

            attemptsRemaining = 6;//set attemps remaining to 6
            AttemptsRemaining(GetAttemptsRemaining()); //display updated attemps remaining
            
        }

       

        //choses a random word from a random difficulty
        void SelectTextFile()
        {
            
            selectedDifficultyIndex = chosenDifficulty;//chosen difficulty from difficulty select to the name chosenDifficuty

            selectedDifficulty = difficulty[selectedDifficultyIndex];//selected difficulty index point

            filePath = $"{Application.dataPath}/Words/{selectedDifficulty}.txt"; //check the file path for difficulty location

            Debug.Log(filePath); //show the file path in debug log
        }

        //file path find and read text
        string ReadTextFile()
        {
            return File.ReadAllText(filePath);//reads the text in the chosen difficulty point
        }

        //read words in the file
        private string SplitTextFile(string words) //split the words
        {
            string[] wordsInFile = words.Split('|');//remove | from the file as a point between word
            string chooseWord = wordsInFile[Random.Range(0, wordsInFile.Length)]; //random word chosen from file seperated by |
            return chooseWord;//return the chosen word
        }

        private int GetAttemptsRemaining()//holds the varible attempsRemaining
        {

            return attemptsRemaining;//holds the varible
        }

        //shows attempts dislpay
        void AttemptsRemaining(int attemptsRemaining)
        {
            attemptsText.text = attemptsRemaining.ToString();//chnages the number of attempts remaining to a string to display on the attemptsText text
        }


        private int GetPlayerScore()//holds the varible playerScore
        {
            return playerScore;//holds the varible
        }

        void PlayerScoreChange(int playerScore)
        {
            scoreText.text = playerScore.ToString();//chnages the number of PlayerScore to a string to display scoreText text
        }
        
        //win condidtion
        void WinCondition()
        {
            foreach (char c in chosenWord)//checks all letters in word are filled in
            {
                if (!lettersGuessed.Contains(c.ToString().ToUpper()) && !lettersGuessed.Contains(c.ToString().ToLower()))
                {
                    return;
                }
            }

            Debug.Log("You Win!");//debug to see if woking
            gameOver = true;//set the game over to true to stop inputs from keyboard
            
            playerScore += 1;//add a point to player score
            PlayerScoreChange(GetPlayerScore());//change the number of the text in player score
            this._winScreen.SetActive(true); //set the winscreen to true to show the winscreen
            
            
        }

           
        
        
        
        


    }
}