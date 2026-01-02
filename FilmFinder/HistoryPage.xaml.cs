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
        // Son 9 filmi çekiyoruz
        var history = historyDb.GetLastNine();
        HistoryList.ItemsSource = null;
        HistoryList.ItemsSource = history;
    }

    private void OnClearHistoryClicked(object sender, EventArgs e)
    {
        historyDb.Clear();
        LoadHistory();
    }
}