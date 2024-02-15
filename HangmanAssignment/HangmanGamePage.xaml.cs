using System.Text;
using System.ComponentModel;

namespace HangmanAssignment;

public partial class HangmanGamePage : ContentPage, INotifyPropertyChanged
{
    #region Variables

    private List<char> guessed = new();
	private string currentGuess;
	private string result;
    private string incorrectGuesses;
    private string currentImage = "hang1.png";
    private string answer = "";
    private int mistakes = 1;
    private int maxwrong = 8;

    #endregion

    #region Getter Methods
    public string CurrentGuess
    {
		get => currentGuess;
		set
		{
			currentGuess = value;
			OnPropertyChanged();
		}
	}

	public string Result
	{
		get => result;
		set 
		{
			result = value;
			OnPropertyChanged();
		}
	}

	public string IncorrectGuesses
	{
		get => incorrectGuesses;
		set
		{
			incorrectGuesses = value;
			OnPropertyChanged();
		}
	}

	public string CurrentImage
	{
		get => currentImage;
		set 
		{
			currentImage = value; 
			OnPropertyChanged(); 
		}
	}

    #endregion

    #region Buttons

    private void DisableButtons()
    {
        GuessButton.IsEnabled = false;
        GuessButton.IsVisible = false;
        EntryBox.IsEnabled = false;
        EntryBox.IsVisible = false;
    }

    private void EnableButtons()
    {
        GuessButton.IsEnabled = true;
        GuessButton.IsVisible = true;
        EntryBox.IsEnabled = true;
        EntryBox.IsVisible = true;
    }

    private void GuessClick(object sender, EventArgs e)
    {
        string letter = EntryBox.Text;
        EntryBox.Text = "";
        Turn(letter[0]);
    }

    private void ResetClick(object sender, EventArgs e)
    {
        mistakes = 1;
        guessed = new List<char>();
        CurrentImage = "hang1.jpg";
        PickWord();
        CheckWord(answer, guessed);
        Result = "";
        UpdateStatus();
        EnableButtons();
    }

    #endregion

    #region Game

    private void PickWord()
    {
        answer = wordList[new Random().Next(0, wordList.Count)];
    }

    private void CheckWord(string answer, List<char> guessed)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in answer)
        {
            if (guessed.Contains(c))
            {
                sb.Append(c);
            }
            else
            {
                sb.Append('_');
            }
            sb.Append(" ");
        }

        CurrentGuess = sb.ToString();
    }

    private void CheckWin()
    {
        if (CurrentGuess.Replace(" ", "") == answer)
        {
            Result = "You Win!!!";
            DisableButtons();
        }
    }

    private void CheckLose()
    {
        if (mistakes == maxwrong)
        {
            Result = "You Lost!!";
            DisableButtons();
            CurrentGuess = answer;
        }
    }

    private void UpdateStatus()
    {
        StringBuilder temp = new StringBuilder();
        foreach (char c in guessed)
        {
            if (!answer.Contains(c))
            {
                temp.Append(c);
            }
        }

        IncorrectGuesses = $"Incorrect Guesses: {temp.ToString()}";
    }

    private void Turn(char letter)
    {
        if (!guessed.Contains(letter))
        {
            guessed.Add(letter);

            if (answer.Contains(letter))
            {
                CheckWord(answer, guessed);
                CheckWin();
            }
            else
            {
                mistakes++;
                UpdateStatus();
                CheckLose();
                CurrentImage = $"hang{mistakes}.png";
            }
        }
    }

    #endregion

    #region Constructor 

    public HangmanGamePage()
    {
        InitializeComponent();
        BindingContext = this;
        PickWord();
        CheckWord(answer, guessed);
    }

    #endregion

    List<string> wordList = new() 
	{
		"CODE",
		"CSHARP",
		"LAPTOP",
		"COLAB",
		"SAMSUNG",
	};

	

    
    

	
}
