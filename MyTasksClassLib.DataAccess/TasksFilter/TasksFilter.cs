using MyTasksClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.DataAccess.TasksFilter
{
    public static class TasksFilter
    {
        public static IQueryable<TaskModel> GetOptionTasks(this IQueryable<TaskModel> existingQuery,
            int? taskOption)
        {
            return existingQuery.Where(task => taskOption == null ? task.Status != -1 :
                taskOption == 3 ? task.DueDate.Date < DateTime.Now.Date && task.Status != 2 :
                task.Status == taskOption);
        }

        public static IQueryable<TaskModel> GetPriorityFilterTasks(this IQueryable<TaskModel> existingQuery,
            int taskHighPriority, int taskMediumPriority, int taskLowPrioriry)
        {
            if (taskHighPriority == taskMediumPriority && taskMediumPriority == taskLowPrioriry &&
                taskLowPrioriry == 0) return existingQuery;

            return existingQuery.Where(task => (
                task.Priority == 0 && taskHighPriority == 1) ||
                (task.Priority == 1 && taskMediumPriority == 1) ||
                (task.Priority == 2 && taskLowPrioriry == 1));
        }

        public static IQueryable<TaskModel> OrderTasks(this IQueryable<TaskModel> existingQuery,
            int orderOption)
        {
            if (orderOption == 0) return existingQuery.OrderByDescending(task => task.Id);

            return existingQuery.OrderBy(task => task.Id);
        }

        public static IQueryable<TaskModel> GetSearchOptionTasks(this IQueryable<TaskModel> existingQuery,
            int srOption)
        {
            return existingQuery.Where(task => srOption == 0 ? task.Status != -1 :
                    srOption == 4 ? (task.DueDate.Date < DateTime.Now.Date) &&
                    task.Status != 2 : task.Status == srOption - 1);
        }

        public static IQueryable<TaskModel> GetSearchPriorityFilterTasks(this IQueryable<TaskModel> existingQuery,
            int srPriority)
        {
            return existingQuery.Where(task => srPriority == 0 ? task.Priority != -1 : task.Priority == srPriority - 1);
        }

        public static IQueryable<TaskModel> GetPatternMatchingTasks(this IQueryable<TaskModel> existingQuery,
            string srText)
        {
            return existingQuery.Where(task => task.MyTask.Contains(srText) ||
                        task.Description.Contains(srText));
        }
    }
}
