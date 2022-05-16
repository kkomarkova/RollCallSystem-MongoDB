namespace RollCallSystem_MongoDB.Models
{
    public class RollCallDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;

        public string TrophiesCollectionName { get; set; } = null!;
        public string SubjectsCollectionName { get; set; } = null!;
        public string LessonsCollectionName { get; set; } = null!;

    }
}
