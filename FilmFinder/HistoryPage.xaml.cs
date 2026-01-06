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
        UpdateClearButtonVisibility();
        base.OnAppearing();
        LoadHistory();
    }

    void LoadHistory()
    {
        var history = historyDb.GetLastFour();

        if (history != null && history.Any())
        {
            foreach (var film in history)
            {
                film.ImagePath = film.Name.Replace("'", "")
                    .Replace(" ", "")
                    .Replace("é", "e")
                    .Replace(":", "")
                    .Replace("ı", "i")
                    .Replace("10", "ten")
                    .ToLower(System.Globalization.CultureInfo.InvariantCulture)
                    .Replace("ı", "i") + ".jpg";
            }
        }
        
        HistoryList.ItemsSource = null;
        HistoryList.ItemsSource = history;
        UpdateClearButtonVisibility();
    }

    private void OnClearHistoryClicked(object sender, EventArgs e)
    {
        historyDb.Clear();
        LoadHistory();
        UpdateClearButtonVisibility();
    }
    
    void UpdateClearButtonVisibility()
    {
        if (HistoryList.ItemsSource is System.Collections.ICollection list)
        {
            ClearHistoryBtn.IsVisible = list.Count > 0;
        }
        else
        {
            ClearHistoryBtn.IsVisible = false;
        }
    }
    
}