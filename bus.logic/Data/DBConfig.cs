using SQLite;

namespace bus.logic.Data;

public class DBConfig
{
    const string Database1file = "notehub-local.db";
    internal const SQLiteOpenFlags flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
    internal static string DatabasePath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        Database1file
        );
}