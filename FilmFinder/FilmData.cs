using System.Collections.Generic;
using System.Linq;

namespace FilmFinder
{
    public class Film
    {
        public string Name { get; set; }
        public string Mood { get; set; }
        public string Theme { get; set; }
        public int Duration { get; set; } 
        
        public bool IsWatched => Preferences.Get(this.Name + "_watched", false);
        
        public string DisplayMood => 
            Mood == "Kahkaha atmak" ? "Eğlenceli" : 
            Mood == "Duygulanmak" ? "Duygu Dolu" : 
            Mood == "Heyecanlanmak" ? "Dinamik" : 
            Mood == "Kafa yormak" ? "Psikolojik" :
            Mood == "Rahatlamak" ? "Çerezlik" : Mood;

        
        public string LengthforMain => Duration < 100 ? "Kısa" : (Duration <= 125 ? "Orta" : "Uzun");
        
        public string LengthforLibrary => $"{Duration} dk";
        
        public string ImagePath => Name.Replace("'", "")
                                       .Replace(" ", "")
                                       .Replace("é", "e")
                                       .Replace(":", "")
                                       .Replace("ı", "i")
                                       .Replace("10", "ten")
                                       .ToLower(System.Globalization.CultureInfo.InvariantCulture) + ".jpg";
    }
    
    public static class MovieService
    {
        public static List<Film> AllFilms = new List<Film>
        {
            new Film { Name = "La La Land", Mood = "Duygulanmak", Theme = "Aşk", Duration = 128 },
            new Film { Name = "Léon: The Professional", Mood = "Duygulanmak", Theme = "Aksiyon", Duration = 110 },
            new Film { Name = "Mystic River", Mood = "Duygulanmak", Theme = "Polisiye", Duration = 137 },
            new Film { Name = "Carrie", Mood = "Duygulanmak", Theme = "Korku", Duration = 98 },
            new Film { Name = "Little Miss Sunshine", Mood = "Duygulanmak", Theme = "Aile Draması", Duration = 101 },
            new Film { Name = "Lady Bird", Mood = "Duygulanmak", Theme = "Gençlik", Duration = 94 },
            new Film { Name = "Kill Bill", Mood = "Heyecanlanmak", Theme = "Aksiyon", Duration = 111 },
            new Film { Name = "Seven", Mood = "Heyecanlanmak", Theme = "Polisiye", Duration = 127 },
            new Film { Name = "Nerve", Mood = "Heyecanlanmak", Theme = "Gençlik", Duration = 96 },
            new Film { Name = "The Notebook", Mood = "Heyecanlanmak", Theme = "Aşk", Duration = 123 },
            new Film { Name = "The Impossible", Mood = "Heyecanlanmak", Theme = "Aile Draması", Duration = 114 },
            new Film { Name = "Don't Breathe", Mood = "Heyecanlanmak", Theme = "Korku", Duration = 88 },
            new Film { Name = "Scary Movie", Mood = "Kahkaha atmak", Theme = "Korku", Duration = 88 },
            new Film { Name = "The Nice Guys", Mood = "Kahkaha atmak", Theme = "Polisiye", Duration = 116 },
            new Film { Name = "How To Lose a Guy in 10 Days", Mood = "Kahkaha atmak", Theme = "Aşk", Duration = 116 },
            new Film { Name = "Deadpool", Mood = "Kahkaha atmak", Theme = "Aksiyon", Duration = 108 },
            new Film { Name = "Aile Arasında", Mood = "Kahkaha atmak", Theme = "Aile Draması", Duration = 124 },
            new Film { Name = "Superbad", Mood = "Kahkaha atmak", Theme = "Gençlik", Duration = 113 },
            new Film { Name = "We Need to Talk About Kevin", Mood = "Kafa yormak", Theme = "Aile Draması", Duration = 112 },
            new Film { Name = "Before Sunrise", Mood = "Kafa yormak", Theme = "Aşk", Duration = 105 },
            new Film { Name = "Fight Club", Mood = "Kafa yormak", Theme = "Aksiyon", Duration = 139 },
            new Film { Name = "Lost Highway", Mood = "Kafa yormak", Theme = "Korku", Duration = 134 },
            new Film { Name = "Donnie Darko", Mood = "Kafa yormak", Theme = "Gençlik", Duration = 113 },
            new Film { Name = "Shutter Island", Mood = "Kafa yormak", Theme = "Polisiye", Duration = 138 },
            new Film { Name = "Clueless", Mood = "Rahatlamak", Theme = "Gençlik", Duration = 97 },
            new Film { Name = "10 Things I Hate About You", Mood = "Rahatlamak", Theme = "Aşk", Duration = 97 },
            new Film { Name = "The Parent Trap", Mood = "Rahatlamak", Theme = "Aile Draması", Duration = 128 },
            new Film { Name = "Guardians of the Galaxy", Mood = "Rahatlamak", Theme = "Aksiyon", Duration = 121 },
            new Film { Name = "Knives Out", Mood = "Rahatlamak", Theme = "Polisiye", Duration = 130 },
            new Film { Name = "Coraline", Mood = "Rahatlamak", Theme = "Korku", Duration = 100 }
        };
    }
}