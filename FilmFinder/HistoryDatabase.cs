using SQLite;

namespace FilmFinder;

public class HistoryDatabase
{
    SQLiteConnection db;

    public HistoryDatabase()
    {
        string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "history.db");

        db = new SQLiteConnection(path);
        db.CreateTable<HistoryFilm>();
    }

    public void Add(string filmName)
    {
        db.Insert(new HistoryFilm { Name = filmName });
    }

    public List<HistoryFilm> GetAll()
    {
        return db.Table<HistoryFilm>().ToList();
    }

    public void Clear()
    {
        db.DeleteAll<HistoryFilm>();
    }
}

public class HistoryFilm
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
}