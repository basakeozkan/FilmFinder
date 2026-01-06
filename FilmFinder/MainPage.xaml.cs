using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace FilmFinder;

public partial class MainPage : ContentPage
{
    HistoryDatabase historyDb = new HistoryDatabase();
    List<Film> films;
    int currentQuestionIndex = 0;
    public ObservableCollection<Question> Questions { get; set; }

    public MainPage()
    {
        InitializeComponent();
        
        films = MovieService.AllFilms;
        
        Questions = new ObservableCollection<Question>
        {
            new Question { QuestionText = "Bugün neye ihtiyacın var?", Options = new List<string> { "Kahkaha atmak", "Heyecanlanmak", "Duygulanmak", "Kafa yormak", "Rahatlamak" } },
            new Question { QuestionText = "Şu anda hangi tema sana hitap ediyor?", Options = new List<string> { "Aşk", "Aile Draması", "Gençlik", "Aksiyon", "Korku", "Polisiye" } },
            new Question { QuestionText = "Hızlıca izlemelik mi, yoksa uzun soluklu bir film mi tercihin?", Options = new List<string> { "Kısa", "Orta", "Uzun" } }
        };

        BindingContext = this;
    }

    protected override void OnAppearing() { base.OnAppearing(); ShowQuestion(); }

    private void ShowQuestion()
    {
        if (Questions == null || Questions.Count <= currentQuestionIndex) return;
        var question = Questions[currentQuestionIndex];
        QuestionLabel.Text = question.QuestionText;
        
        PreviousButton.IsVisible = (currentQuestionIndex > 0);
       
        NextButton.IsVisible = (currentQuestionIndex < Questions.Count - 1);
        
        AnswerPicker.SelectedIndexChanged -= OnAnswerPickerSelectedIndexChanged;
        AnswerPicker.ItemsSource = null;
        AnswerPicker.ItemsSource = question.Options;
        if (!string.IsNullOrEmpty(question.SelectedOption)) AnswerPicker.SelectedItem = question.SelectedOption;
        else AnswerPicker.SelectedIndex = -1;
        AnswerPicker.SelectedIndexChanged += OnAnswerPickerSelectedIndexChanged;
    }

    private void OnAnswerPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (AnswerPicker.SelectedIndex != -1)
            Questions[currentQuestionIndex].SelectedOption = AnswerPicker.SelectedItem.ToString();
    }

    private void OnPreviousQuestionClicked(object sender, EventArgs e) { if (currentQuestionIndex > 0) { currentQuestionIndex--; ShowQuestion(); } }
    private void OnNextQuestionClicked(object sender, EventArgs e) { if (currentQuestionIndex < Questions.Count - 1) { currentQuestionIndex++; ShowQuestion(); } }

    private void OnFindFilmClicked(object sender, EventArgs e)
    {
        string sMood = Questions[0].SelectedOption;
        string sTheme = Questions[1].SelectedOption;
        string sLength = Questions[2].SelectedOption;

        List<Film> matchedFilms = new List<Film>();
        foreach (var film in films)
        {
            bool match = true;
            if (!string.IsNullOrEmpty(sMood) && film.Mood != sMood) match = false;
            if (!string.IsNullOrEmpty(sTheme) && film.Theme != sTheme) match = false;
            if (!string.IsNullOrEmpty(sLength) && film.LengthforMain != sLength) match = false;
            if (Preferences.Get(film.Name + "_watched", false)) match = false;
            if (match) matchedFilms.Add(film);
        }

        if (matchedFilms.Count > 0)
        {
            Random rnd = new Random();
            var selectedFilm = matchedFilms[rnd.Next(matchedFilms.Count)];
            
        
            // yazı
            ResultLabel.Text = selectedFilm.Name;
            
            FilmPoster.Source = selectedFilm.ImagePath;

            // ekran değişimi
            QuestionsPanel.IsVisible = false;
            ResultPanel.IsVisible = true;

            // historye kaydet
            historyDb.Add(selectedFilm.Name);
        }
        else 
        { 
            DisplayAlert("Uyarı", "Uygun film bulunamadı.", "Tamam");
        }
    }
    
    private void OnResetClicked(object sender, EventArgs e)
    {
        // ekran değişimi
        ResultPanel.IsVisible = false;
        QuestionsPanel.IsVisible = true;

        // eski hale dönme
        currentQuestionIndex = 0;
        foreach (var q in Questions) q.SelectedOption = null;
        ShowQuestion();
    }
}

public class Question { public string QuestionText { get; set; } public List<string> Options { get; set; } public string SelectedOption { get; set; } }
