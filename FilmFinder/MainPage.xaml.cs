using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace FilmFinder;

public partial class MainPage : ContentPage
{
    HistoryDatabase historyDb;
    List<Film> films;
    string lastSuggestedFilm = null;
    int currentQuestionIndex = 0;
    
    public ObservableCollection<Question> Questions { get; set; }

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;

        historyDb = new HistoryDatabase();
        LoadHistory();

        // filmler
        films = new List<Film>
        {
            new Film { Name = "La La Land", Mood = "Duygulanmak", Theme = "Aşk", Length = "Uzun" },
            new Film { Name = "Léon: The Professional", Mood = "Duygulanmak", Theme = "Aksiyon", Length = "Orta" },
            new Film { Name = "Mystic River", Mood = "Duygulanmak", Theme = "Polisiye", Length = "Uzun" },
            new Film { Name = "Carrie", Mood = "Duygulanmak", Theme = "Korku", Length = "Orta" },
            new Film { Name = "Little Miss Sunshine", Mood = "Duygulanmak", Theme = "Aile Draması", Length = "Orta" },
            new Film { Name = "Lady Bird", Mood = "Duygulanmak", Theme = "Gençlik", Length = "Orta" },
            new Film { Name = "Kill Bill", Mood = "Heyecanlanmak", Theme = "Aksiyon", Length = "Orta" },
            new Film { Name = "Se7en", Mood = "Heyecanlanmak", Theme = "Polisiye", Length = "Uzun" },
            new Film { Name = "Nerve", Mood = "Heyecanlanmak", Theme = "Gençlik", Length = "Orta" },
            new Film { Name = "The Notebook", Mood = "Heyecanlanmak", Theme = "Aşk", Length = "Uzun" },
            new Film { Name = "The Impossible", Mood = "Heyecanlanmak", Theme = "Aile Draması", Length = "Orta" },
            new Film { Name = "Don't Breathe", Mood = "Heyecanlanmak", Theme = "Korku", Length = "Kısa" },
            new Film { Name = "Scary Movie", Mood = "Kahkaha atmak", Theme = "Korku", Length = "Kısa" },
            new Film { Name = "The Nice Guys", Mood = "Kahkaha atmak", Theme = "Polisiye", Length = "Orta" },
            new Film { Name = "How To Lose a Guy in 10 Days", Mood = "Kahkaha atmak", Theme = "Aşk", Length = "Orta" },
            new Film { Name = "Deadpool", Mood = "Kahkaha atmak", Theme = "Aksiyon", Length = "Orta" },
            new Film { Name = "Aile Arasında", Mood = "Kahkaha atmak", Theme = "Aile Draması", Length = "Orta" },
            new Film { Name = "Superbad", Mood = "Kahkaha atmak", Theme = "Gençlik", Length = "Orta" },
            new Film { Name = "Knives Out", Mood = "Kafa yormak", Theme = "Aile Draması", Length = "Uzun" },
            new Film { Name = "Before Sunrise", Mood = "Kafa yormak", Theme = "Aşk", Length = "Kısa" },
            new Film { Name = "Fight Club", Mood = "Kafa yormak", Theme = "Aksiyon", Length = "Uzun" },
            new Film { Name = "Scream", Mood = "Kafa yormak", Theme = "Korku", Length = "Orta" },
            new Film { Name = "Donnie Darko", Mood = "Kafa yormak", Theme = "Korku", Length = "Orta" },
            new Film { Name = "Shutter Island", Mood = "Kafa yormak", Theme = "Polisiye", Length = "Uzun" }
        };

        // sorular
        Questions = new ObservableCollection<Question>
        {
            new Question
            {
                QuestionText = "Tam şu an ne yapmak isterdin?",
                Options = new List<string> { "Kahkaha atmak", "Heyecanlanmak", "Duygulanmak", "Kafa yormak" }
            },
            new Question
            {
                QuestionText = "Tercih ettiğin bir tema var mı?",
                Options = new List<string> { "Aşk", "Aile Draması", "Gençlik", "Aksiyon", "Korku", "Polisiye" }
            },
            new Question
            {
                QuestionText = "Filmin süresi nasıl olmalı?",
                Options = new List<string> { "Kısa", "Orta", "Uzun" }
            }
        };
    }
    
 // başlangıçta soru gözükmesi
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ShowQuestion();
    }
    
    private void ShowQuestion()
    {
        if (Questions == null || Questions.Count <= currentQuestionIndex) return;

        var question = Questions[currentQuestionIndex];
        QuestionLabel.Text = question.QuestionText;

        
        AnswerPicker.SelectedIndexChanged -= OnAnswerPickerSelectedIndexChanged;

        AnswerPicker.ItemsSource = null;
        AnswerPicker.ItemsSource = question.Options;
        
        
        if (!string.IsNullOrEmpty(question.SelectedOption))
        {
            AnswerPicker.SelectedItem = question.SelectedOption;
        }
        else
        {
            AnswerPicker.SelectedIndex = -1;
        }
        
        AnswerPicker.SelectedIndexChanged += OnAnswerPickerSelectedIndexChanged;
    }

    private void OnAnswerPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (AnswerPicker.SelectedIndex != -1)
        {
            Questions[currentQuestionIndex].SelectedOption = AnswerPicker.SelectedItem.ToString();
        }
    }
    
    // ok butonlarının işlevi
    private void OnPreviousQuestionClicked(object sender, EventArgs e)
    {
        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            ShowQuestion();
        }
    }

    private void OnNextQuestionClicked(object sender, EventArgs e)
    {
        if (currentQuestionIndex < Questions.Count - 1)
        {
            currentQuestionIndex++;
            ShowQuestion();
        }
    }

    
    //film bul butonunun işlevi
    private void OnFindFilmClicked(object sender, EventArgs e)
    {
        string selectedMood = Questions[0].SelectedOption;
        string selectedTheme = Questions[1].SelectedOption;
        string selectedLength = Questions[2].SelectedOption;

        List<Film> matchedFilms = new List<Film>();

        
        //seçilenlere göre eşleştirme
        foreach (var film in films)
        {
            bool match = true;
            if (selectedMood != null && film.Mood != selectedMood) match = false;
            if (selectedTheme != null && film.Theme != selectedTheme) match = false;
            if (selectedLength != null && film.Length != selectedLength) match = false;

            if (match) matchedFilms.Add(film);
        }

        if (matchedFilms.Count == 0)
        {
            ResultLabel.Text = "Uygun film bulunamadı.";
        }
        else
        {
            Random rnd = new Random();
            string selectedFilm;
            do
            {
                selectedFilm = matchedFilms[rnd.Next(matchedFilms.Count)].Name;
            } while (selectedFilm == lastSuggestedFilm && matchedFilms.Count > 1);

            ResultLabel.Text = "Önerilen film: " + selectedFilm;
            lastSuggestedFilm = selectedFilm;
            historyDb.Add(selectedFilm);
            LoadHistory();
        }
    }

    //geçmiş
    void LoadHistory()
    {
        var history = historyDb.GetLastThree();
        if (history.Count == 0)
        {
            HistoryLabel.Text = "Henüz geçmiş yok.";
            return;
        }

        HistoryLabel.Text = "";
        foreach (var item in history)
        {
            HistoryLabel.Text += "• " + item.Name + "\n";
        }
    }

    //geçmişi temizle butonu işlevi
    private void OnClearHistoryClicked(object sender, EventArgs e)
    {
        historyDb.Clear();
        LoadHistory();
    }
}

// soru ve seçenekler
public class Question
{
    public string QuestionText { get; set; }
    public List<string> Options { get; set; }
    public string SelectedOption { get; set; }
}

//film için seçilecekler
public class Film
{
    public string Name { get; set; }
    public string Mood { get; set; }
    public string Theme { get; set; }
    public string Length { get; set; }
}