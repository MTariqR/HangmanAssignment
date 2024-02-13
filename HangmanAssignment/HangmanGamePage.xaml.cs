using System.Text;
using System.ComponentModel;

namespace HangmanAssignment;

public partial class HangmanGamePage : ContentPage, INotifyPropertyChanged
{

	private string guessLetter;
	public string GuessLetter 
	{
		get => GuessLetter;
		set
		{
			guessLetter = value;
			OnPropertyChanged();
		}
	}

	private List<char> letters = new();
	private List<char> guessed = new();
	public List<char> Letters
	{
		get => letters;
		set
		{
			letters = value;
			OnPropertyChanged();
		}
	}

	private string result;
	public string Result
	{
		get => result;
		set 
		{
			result = value;
			OnPropertyChanged();
		}
	}

	private string incorrectGuesses;
	public string IncorrectGuesses
	{
		get => incorrectGuesses;
		set
		{
			incorrectGuesses = value;
			OnPropertyChanged();
		}
	}

	private string currentImage = "hang1.png";
	public string CurrentImage
	{
		get => currentImage;
		set 
		{
			currentImage = value; 
			OnPropertyChanged(); 
		}
	}


	List<string> wordList = new() 
	{
		"code",
		"csharp",
		"laptop",
		"colab",
		"samsung",
	};

	private string answer = "";
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
        }

        GuessLetter = sb.ToString();
    }

	private void CheckWin()
	{
		if (GuessLetter.ToString() == answer)
		{
			Result = "You Win!!!";
			DisableButtons();
		}
	}

	private void DisableButtons()
	{
        foreach (var child in LettersContainer.Children)
        {
            var btn = child as Button;
			if (btn != null)
			{
				btn.IsEnabled = false;
			}
        }
    }

	private void EnableButtons()
	{
        foreach (var child in LettersContainer.Children)
        {
            var btn = child as Button;
			if (btn != null)
			{
				btn.IsEnabled = true;
			}
        }
    }

	private void UpdateStatus()
	{
		IncorrectGuesses = guessed.ToString();
	}

	private int mistakes = 1;
	private int maxwrong = 8;
    private void CheckLose()
    {
        if (mistakes == maxwrong)
        {
            Result = "You Lost!!";
            DisableButtons();
        }
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
    public HangmanGamePage()
	{
		InitializeComponent();
		Letters.AddRange("abcdefghijklmnopqrstuvwxyz");
		BindingContext = this;
		PickWord();
		CheckWord(answer, guessed);
	}

	private void ButtonClick(object sender, EventArgs e)
	{
		var button = sender as Button;
		if(button != null) 
		{
			var letter = button.Text;
			button.IsEnabled= false;
			Turn(letter[0]);
		}
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
}
