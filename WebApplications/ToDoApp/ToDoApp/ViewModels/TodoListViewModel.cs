using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Helpers;
using ToDoApp.Models;

namespace ToDoApp.ViewModels
{
    public class TodoListViewModel
    {
        public List<ToDoListItem> TodoItems { get; set; }
        public ToDoListItem EditableItem { get; set; }
        public TodoListViewModel()
        {
            this.EditableItem = new ToDoListItem();
            this.TodoItems = DbHelper.GetAllListItems().ToList();
        }

    }
}
