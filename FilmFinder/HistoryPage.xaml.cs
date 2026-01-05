namespace FilmFinder;

public partial class HistoryPage : ContentPage
{
    HistoryDatabase historyDb = new HistoryDatabase();

    public HistoryPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadHistory();
    }

    void LoadHistory()
    {
        var history = historyDb.GetLastFive();

        if (history != null && history.Any())
        {
            foreach (var film in history)
            {
                film.ImagePath = film.Name.Replace("'", "")
                    .Replace(" ", "")
                    .Replace(":", "")
                    .ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    .Replace("ı", "i") + ".jpg";
            }
        }
        
        HistoryList.ItemsSource = null;
        HistoryList.ItemsSource = history;
    }

    private void OnClearHistoryClicked(object sender, EventArgs e)
    {
        historyDb.Clear();
        LoadHistory();
    }
}