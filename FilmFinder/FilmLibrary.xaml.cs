using System.Collections.Generic;
using System.Linq;

namespace FilmFinder;

public partial class FilmLibrary : ContentPage
{
    public FilmLibrary()
    {
        InitializeComponent();

        LibraryList.ItemsSource = MovieService.AllFilms.OrderBy(x => x.Name).ToList();
    }
    
    Film _currentSelectedFilm;
    
    async void OnFilmTapped(object sender, EventArgs e)
    {
        var button = sender as Button;
        var film = button?.CommandParameter as Film;
        if (film == null) return;

        _currentSelectedFilm = film;

        DetailName.Text = film.Name;
        DetailMood.Text = film.DisplayMood;
        DetailTheme.Text = film.Theme;
        DetailTime.Text = film.LengthforLibrary;
        FilmPoster.Source = film.ImagePath;

        bool isWatched = Preferences.Get(film.Name + "_watched", false);
        WatchStatusBtn.Source = isWatched ? "watched.png" : "unwatched.png";

        DetailPanel.IsVisible = true;
        DetailPanel.Scale = 0.5; 
        DetailPanel.Opacity = 0;
        BackgroundOverlay.IsVisible = true;
        await Task.Delay(30);
        await Task.WhenAll(
            BackgroundOverlay.FadeTo(0.6, 250),
            DetailPanel.FadeTo(1, 150), 
            DetailPanel.ScaleTo(1.0, 250, Easing.SpringOut)
        );
    }

    async void OnCloseDetailClicked(object sender, EventArgs e)
    {
        await DetailPanel.ScaleTo(0.5, 200, Easing.SpringIn);
        DetailPanel.IsVisible = false;
        await BackgroundOverlay.FadeTo(0, 250); 
        BackgroundOverlay.IsVisible = false;
        LibraryList.ItemsSource = null;
        LibraryList.ItemsSource = MovieService.AllFilms.OrderBy(x => x.Name).ToList();
    }

    void OnWatchStatusClicked(object sender, EventArgs e)
    {
        if (_currentSelectedFilm == null) return;
        
        bool isWatched = Preferences.Get(_currentSelectedFilm.Name + "_watched", false);
        
        bool newStatus = !isWatched;
        Preferences.Set(_currentSelectedFilm.Name + "_watched", newStatus);
        
        WatchStatusBtn.Source = newStatus ? "watched.png" : "unwatched.png";
    }
}