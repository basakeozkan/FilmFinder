using SQLite;
using System.Collections.Generic;
using System.IO;

namespace FilmFinder;

public class HistoryDatabase
{
    SQLiteConnection db;

    public HistoryDatabase()
    {
        string path = Path.Combine(FileSystem.AppDataDirectory, "history.db");
        db = new SQLiteConnection(path);
        db.CreateTable<HistoryFilm>();
    }

    public void Add(string filmName)
    {
        db.Insert(new HistoryFilm { Name = filmName });
    }

    public List<HistoryFilm> GetLastFour()
    {
        return db.Table<HistoryFilm>()
            .OrderByDescending(x => x.Id)
            .Take(4)
            .ToList();
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
    [Ignore] 
    public string ImagePath { get; set; }
}