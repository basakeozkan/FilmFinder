namespace FilmFinder;

public partial class MainPage : ContentPage
{
    HistoryDatabase historyDb;
    List<Film> films;

    public MainPage()
    {
        InitializeComponent();
        
        historyDb = new HistoryDatabase();
        LoadHistory();

        
        films = new List<Film>
        {
            new Film { Name = "Little Miss Sunshine", Mood = "Duygulanmak", Theme = "Aile Draması", Length = "Orta" },
            new Film { Name = "Before Sunrise", Mood = "Kafa yormak", Theme = "Aşk", Length = "Kısa" },
            new Film { Name = "Kill Bill", Mood = "Heyecanlanmak", Theme = "Aksiyon", Length = "Uzun" },
            new Film { Name = "Carrie", Mood = "Heyecanlanmak", Theme = "Korku", Length = "Orta" },
            new Film { Name = "Blue Valentine", Mood = "Duygulanmak", Theme = "Aşk", Length = "Orta" },
            new Film { Name = "How To Lose a Guy in 10 Days", Mood = "Kahkaha atmak", Theme = "Aşk", Length = "Orta" },
            new Film { Name = "Deadpool", Mood = "Kahkaha atmak", Theme = "Aksiyon", Length = "Orta" },
            new Film { Name = "Aile Arasında", Mood = "Kahkaha atmak", Theme = "Aile Draması", Length = "Orta" },
            new Film { Name = "Fight Club", Mood = "Kafa yormak", Theme = "Aksiyon", Length = "Uzun" },
            new Film { Name = "Shutter Island", Mood = "Kafa yormak", Theme = "Polisiye", Length = "Uzun" }
        };
    }

    private void OnFindFilmClicked(object sender, EventArgs e)
    {
        string selectedMood = MoodPicker.SelectedItem as string;
        string selectedTheme = ThemePicker.SelectedItem as string;
        string selectedLength = LengthPicker.SelectedItem as string;

        List<Film> matchedFilms = new List<Film>();

        foreach (var film in films)
        {
            bool match = true;

            if (selectedMood != null && film.Mood != selectedMood)
                match = false;

            if (selectedTheme != null && film.Theme != selectedTheme)
                match = false;

            if (selectedLength != null && film.Length != selectedLength)
                match = false;

            if (match)
                matchedFilms.Add(film);
        }

        if (matchedFilms.Count == 0)
        {
            ResultLabel.Text = "Uygun film bulunamadı.";
        }
        else
        {
            Random rnd = new Random();
            int index = rnd.Next(matchedFilms.Count);
            ResultLabel.Text = "Önerilen film: " + matchedFilms[index].Name;
            
            historyDb.Add(matchedFilms[index].Name);
            LoadHistory();

        }
        
    }
    
    void LoadHistory()
    {
        var history = historyDb.GetAll();

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
    
    private void OnClearHistoryClicked(object sender, EventArgs e)
    {
        historyDb.Clear();
        LoadHistory();
    }


}

class Film
{
    public string Name;
    public string Mood;
    public string Theme;
    public string Length;
}
