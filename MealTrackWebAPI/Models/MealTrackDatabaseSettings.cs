namespace MealTrackWebAPI.Models
{
    public class MealTrackDatabaseSettings : IMealTrackDatabaseSettings
    {
        public string[] CollectionNames { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMealTrackDatabaseSettings
    {
        string[] CollectionNames { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}