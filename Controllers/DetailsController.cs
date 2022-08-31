using Microsoft.AspNetCore.Mvc;
using MyTasks.Services;
using System.Threading.Tasks;

namespace MyTasks.Controllers
{
    public class DetailsController : Controller
    {
        #region Props
        private readonly IMyTaskService myTaskService;
        #endregion

        #region Ctor
        public DetailsController(IMyTaskService myTaskService)
        {
            this.myTaskService = myTaskService;
        }
        #endregion

        #region Methods
        public IActionResult Index(int taskId)
        {
            var task = myTaskService.GetATask(taskId);
            
            return View(task);
        }

        #endregion
    }
}
