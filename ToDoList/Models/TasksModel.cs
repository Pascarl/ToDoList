using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace ToDoList.Models
{
    [Table("Tasks")]
    public partial class TasksModel : ObservableObject
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Completed")]
        public bool Completed { get; set; }

        [Column("Time")]
        public DateTime time { get; set; }

    }
}
