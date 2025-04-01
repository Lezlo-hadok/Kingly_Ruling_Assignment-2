namespace Letters
{
    
    using UnityEngine;
    using System.IO;
    using UnityEngine.UI;
    

    public class LetterRemove : MonoBehaviour
    {
        /// <summary>
        /// have a text file with wordas
        /// load words into an array
        /// select a random word
        /// debug how may letters in word
        /// see if word contains the letter a
        /// </summary>

       
        public string[] difficulty;
        public string selectedDifficulty;
        public int selectedDifficultyIndex;

        string filePath;

        public string chosenWord;
        public char[] charaters;

        public GameObject characterDisplayPrefab;
        public Transform spawanLocation;
        public Text[] textDisplay;

        void Start()
        {
            SelectTextFile();
            chosenWord = SplitTextFile(ReadTextFile());
            charaters = chosenWord.ToCharArray();
            Debug.Log(charaters.Length);
            textDisplay = new Text[charaters.Length];
            for (int i = 0; i < charaters.Length; i++)
            {
                Text currentLetter = Instantiate(characterDisplayPrefab).GetComponentInChildren<Text>();
                textDisplay[i] = currentLetter;
            }

            if (chosenWord.Contains('a'))
            {
                Debug.Log("there is an a");
                foreach (char letter in charaters)
                {
                    if (letter == 'a')
                    {
                        Debug.Log("a was found");
                    }
                }
            }

        }
        void SelectTextFile()
        {
            //WordsAsset.;
            selectedDifficultyIndex = Random.Range(0, difficulty.Length);
            selectedDifficulty = difficulty[selectedDifficultyIndex];
            filePath = $"{Application.dataPath}/Words/{selectedDifficulty}.txt";
            Debug.Log(filePath);
        }

        string ReadTextFile()
        {
            return File.ReadAllText(filePath);
        }

        private string SplitTextFile(string words)
        {
            string[] wordsInFile = words.Split('|');
            string chooseWord = wordsInFile[Random.Range(0, wordsInFile.Length)];
            return chooseWord;
        }

    }
}