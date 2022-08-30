﻿using MyTasks.Models;

namespace MyTasks.Services
{
    public interface IMyTaskService
    {
        public void AddNewTask(TaskModel task);
        public List<TaskModel> GetAllTasks();
    }
}
