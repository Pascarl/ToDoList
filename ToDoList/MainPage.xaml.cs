using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList
{
    public partial class MainPage : ContentPage
    {
        private Dbservice _dbservice;
        public DateTime DateTime = new DateTime();
        public MainPage(Dbservice dbservice)
        {
            InitializeComponent();
            _dbservice = dbservice;
            Task.Run(async () => todolist.ItemsSource = await _dbservice.GetAll());
            BindingContext = new TasksModel();
        }

        private void Clear()
        {
            tientry.Text = null;
            desentry.Text = null;
        }

        // Add task to list //

        public async void Addbtn(object  sender, EventArgs e)
        {
            if(tientry.Text == null | desentry.Text == null)
            {
                await DisplayAlert("Error", "please enter a title and description", "OK");
            }
            else
            {
                await _dbservice.Create(new TasksModel()
                {
                    Description = desentry.Text,
                    Title = tientry.Text,
                    Completed = false,
                    time = DateTime.UtcNow

                });

                await DisplayAlert("created", "Task added", "OK");
                todolist.ItemsSource = await _dbservice.GetAll();
                Clear();
            }
        }

        // select a task //
        public async void tapped(object sender, EventArgs e)
        {
             var task = todolist.SelectedItem as TasksModel;

            string choice = await DisplayActionSheet("Choose an option:", null, null, "Update", "Mark as done");
            switch(choice)
            {
                // update task information //

                case "Update":
                    string title = await DisplayPromptAsync("Fill in feild", "title", "OK", "CANCEL");
                    string descript = await DisplayPromptAsync("Fill in feild", "Description", "OK", "CANCEL");
                    task.Title = title;
                    task.Description = descript;
                    await _dbservice.Update(task);
                    todolist.ItemsSource = await _dbservice.GetAll();
                    Clear();
                    break;

                // Mark task as complete //

                case "Mark as done":
                    task.Completed = true;
                    await _dbservice.Delete(task);
                    todolist.ItemsSource = await _dbservice.GetAll();
                    Clear();
                    break;

            }
        }
    }

}
