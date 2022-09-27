﻿using MyTasksClassLib.Models.ViewModels;

namespace MyTasks.Services.IServices
{
    public interface IMyTasksService
    {
        Task AddNewTaskServiceAsync(AddNewViewModel newTask, string userId);
        Task<DetailViewModel> GetATaskDetailServiceAsync(int taskId);
        Task UpdateTaskServiceAsync(DetailViewModel updatedTask);
    }
}
