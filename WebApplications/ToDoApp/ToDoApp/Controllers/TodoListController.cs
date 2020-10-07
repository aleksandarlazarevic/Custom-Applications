using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Helpers;
using ToDoApp.ViewModels;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class TodoListController : Controller
    {
        public IActionResult Index()
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            return View("Index", viewModel);
        }

        public IActionResult Edit(int id)
        {
            TodoListViewModel viewModel = new TodoListViewModel();
            viewModel.EditableItem = viewModel.TodoItems.FirstOrDefault(x => x.Id == id);
            return View("Index", viewModel);
        }

        public IActionResult Delete(int id)
        {
            ToDoListItem item = DbHelper.GetListItem(id);
            if (item != null) { DbHelper.Delete(item.Id); }
            return RedirectToAction("Index");
        }

        public IActionResult CreateUpdate(TodoListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.EditableItem.Id <= 0)
                {
                    viewModel.EditableItem.AddDate = DateTime.Now;
                    DbHelper.Insert(viewModel.EditableItem);
                }
                else
                {
                    ToDoListItem dbItem = DbHelper.GetListItem(viewModel.EditableItem.Id);
                    var result = TryUpdateModelAsync<ToDoListItem>(dbItem, "EditableItem");
                    DbHelper.Update(dbItem);
                }
                return RedirectToAction("Index");
            }
            else
                return View("Index", new TodoListViewModel());
        }

        public IActionResult ToggleIsDone(int id)
        {
                ToDoListItem item = DbHelper.GetListItem(id);
                if (item != null)
                {
                    item.IsDone = !item.IsDone;
                    DbHelper.Update(item);
                }
                return RedirectToAction("Index");
        }

    }
}
