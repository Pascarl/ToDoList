using SQLite;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class Dbservice
    {
        private const string DB_NAME = "ToDoListDb.db3";
        private readonly SQLiteAsyncConnection _connection;

        public Dbservice()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<TasksModel>();
        }


        // Get all tasks as a list //
        public async Task<List<TasksModel>> GetAll()
        {
            return await _connection.Table<TasksModel>().ToListAsync();
        }

        // Create a task //
        public async Task Create(TasksModel tasksModel)
        {
            await _connection.InsertAsync(tasksModel);
        }

        // Update a task //

        public async Task Update(TasksModel tasksModel)
        {
            await _connection.UpdateAsync(tasksModel);
        }

        // Mark task as complete
        public async Task Delete(TasksModel tasksModel)
        {
            await _connection.DeleteAsync(tasksModel);
        }
    }
}
