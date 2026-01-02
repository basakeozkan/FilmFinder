using System.Collections.Generic;
using System.Linq;

namespace FilmFinder;

public partial class FilmLibrary : ContentPage
{
    public FilmLibrary()
    {
        InitializeComponent();

        // filmler
        var allFilms = new List<Film>
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
            new Film { Name = "Mulholland Drive", Mood = "Kafa yormak", Theme = "Korku", Length = "Uzun" },
            new Film { Name = "Donnie Darko", Mood = "Kafa yormak", Theme = "Gençlik", Length = "Orta" },
            new Film { Name = "Shutter Island", Mood = "Kafa yormak", Theme = "Polisiye", Length = "Uzun" }
        };

        
        var displayList = allFilms.Select(f => new Film 
        {
            Name = f.Name,
            Theme = f.Theme,
            // değişim
            Mood = f.Mood == "Kahkaha atmak" ? "Eğlenceli" : 
                   f.Mood == "Duygulanmak" ? "Duygu Dolu" : 
                   f.Mood == "Heyecanlanmak" ? "Dinamik" : 
                   f.Mood == "Kafa yormak" ? "Psikolojik" :
                   f.Mood == "Rahatlamak" ? "Çerezlik" : f.Mood,
            
            Length = f.Length == "Kısa" ? "<= 90 dk" :
                f.Length == "Orta" ? "90 - 120 dk" :
                f.Length == "Uzun" ? ">= 120 dk" : f.Length
                   
                   
        }).OrderBy(x => x.Name).ToList();
        
        LibraryList.ItemsSource = displayList;
    }
}