using System.Threading.Tasks;
using IDAProject.Web.Admin.Managers;
using IDAProject.Web.Admin.Managers.Attributes;
using IDAProject.Web.Admin.Models.Common;
using IDAProject.Web.Admin.Models.Interfaces.Managers;
using IDAProject.Web.Admin.Models.ViewModels;
using IDAProject.Web.Admin.Models.ViewModels.IDA;
using IDAProject.Web.Models.General;
using IDAProject.Web.Models.General.Enums;
using Microsoft.AspNetCore.Mvc;

namespace IDAProject.Web.Admin.Controllers
{
    [Route("[controller]")]
    [CustomAuthorization((int)AspNetRoles.Any, (int)AspNetFeatures.Any)]
    public class IDAController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMasterDataManager _masterDataManager;
        private readonly IIdaTasksManager _idaTasksManager;
        private readonly ITasksPlanningsManager _tasksPlanningsManager;
        private readonly IEmployeesManager _employeesManager;
        public IDAController(ILogger<IDAController> logger, IAccountManager accountManager, IConfiguration configuration, IMasterDataManager masterDataManager, IIdaTasksManager idaTasksManager, IEmployeesManager employeesManager, ITasksPlanningsManager tasksPlanningsManager) : base(accountManager, logger)
        {
            _configuration = configuration;
            _masterDataManager = masterDataManager;
            _idaTasksManager = idaTasksManager;
            _employeesManager = employeesManager;
            _tasksPlanningsManager = tasksPlanningsManager;
        }


        [HttpGet("index", Name = RouteNames.IDA_Index)]
        public async Task<IActionResult> Index()
        {
            var viewModel = new IDAViewModel();
            var user = GetCurrentUser();

            var today = DateTime.Now.Date.ToString("dd.MM.yyyy");
            var taskPlannings = await _tasksPlanningsManager.SearchTasksPlanningsAsync(
    new Web.Models.RequestModels.TasksPlannings.SearchTasksPlanningsParams
    {
        UserId = user.Id,
        PlanDateForRowNumber = today,
        Finished = false
    }
);

            // Napravi listu sa stvarnim planovima
            var taskPlanningsList = taskPlannings.Payload
                .Select(x => new GenericSelectOption
                {
                    Value = x.Id,
                    Description = x.PlanNo.Value.ToString() ?? ""
                })
                .ToList();

            // Dodaj opciju sa 0 na kraj
            taskPlanningsList.Add(new GenericSelectOption
            {
                Value = 0,
                Description = "0" // tekst koji želiš
            });

            // Postavi svojstvo na novu listu
            viewModel.TaskPlannings = taskPlanningsList;
            viewModel.Projects = await _masterDataManager.GetSelectOptionsByTableAsync("Projects", "Description");
            viewModel.Tasks = await _idaTasksManager.GetUncompletedTasks(false,user.Id);
            viewModel.ProjectTasks = await _idaTasksManager.GetUncompletedTasks(true, user.Id);
            viewModel.ActivityTypes = await _masterDataManager.GetSelectOptionsByTableAsync("ActivityTypes", "Name");
            viewModel.PlanStatuses = await _masterDataManager.GetSelectOptionsByTableAsync("PlanStatuses", "Name");
            viewModel.RegularActivities = await _masterDataManager.GetSelectOptionsByTableAsync("RegularActivities", "Name");
            viewModel.User = user;
            var employeePhoto = (await _employeesManager.GetEmployeeByIdAsync(user.EmployeeId)).Payload.Photo;
            viewModel.ImageSource = employeePhoto;
            viewModel.TodayForActivityTable = DateTime.Now.Date.ToString("dd.MM.yyyy");
            var nextDay = DateTime.Now.Date.AddDays(1);

            // preskoči vikend
            while (nextDay.DayOfWeek == DayOfWeek.Saturday || nextDay.DayOfWeek == DayOfWeek.Sunday)
            {
                nextDay = nextDay.AddDays(1);
            }

            viewModel.Today = DateTime.Now.ToString("dd.MM.yyyy");
            viewModel.NextDay = nextDay;
            return View(viewModel);
        }
        [HttpGet("indexView", Name = RouteNames.IDA_IndexView)]
        public async Task<IActionResult> IndexView()
        {
            var viewModel = new IDAViewModel();
            var user = GetCurrentUser();
            var today = DateTime.Now.Date.ToString("dd.MM.yyyy");
            var taskPlannings = await _tasksPlanningsManager.SearchTasksPlanningsAsync(
    new Web.Models.RequestModels.TasksPlannings.SearchTasksPlanningsParams
    {
        UserId = user.Id,
        CreatedDate = today,
        Finished = false
    }
);

            // Napravi listu sa stvarnim planovima
            var taskPlanningsList = taskPlannings.Payload
                .Select(x => new GenericSelectOption
                {
                    Value = x.Id,
                    Description = x.PlanNo.Value.ToString() ?? ""
                })
                .ToList();

            // Dodaj opciju sa 0 na kraj    
            taskPlanningsList.Add(new GenericSelectOption
            {
                Value = 0,
                Description = "0" // tekst koji želiš
            });

            // Postavi svojstvo na novu listu
            viewModel.TaskPlannings = taskPlanningsList;
            viewModel.Projects = await _masterDataManager.GetSelectOptionsByTableAsync("Projects", "Description");
            viewModel.Tasks = await _idaTasksManager.GetUncompletedTasks(false,user.Id);
            viewModel.ProjectTasks = await _idaTasksManager.GetUncompletedTasks(true, user.Id);
            viewModel.ActivityTypes = await _masterDataManager.GetSelectOptionsByTableAsync("ActivityTypes", "Name");
            viewModel.PlanStatuses = await _masterDataManager.GetSelectOptionsByTableAsync("PlanStatuses", "Name");
            viewModel.RegularActivities = await _masterDataManager.GetSelectOptionsByTableAsync("RegularActivities", "Name");
            viewModel.User = user;
            var employeePhoto = (await _employeesManager.GetEmployeeByIdAsync(user.EmployeeId)).Payload.Photo;
            viewModel.ImageSource = employeePhoto;
            viewModel.Today = DateTime.Now.Date.ToString("dd.MM.yyyy");
            return View("ViewIndex",viewModel);
        }
        [HttpGet("planNewDay", Name = RouteNames.IDA_PlanNewDay)]
        public async Task<IActionResult> PlanNewDay()
        {
            var viewModel = new IDAViewModel();
            viewModel.Projects = await _masterDataManager.GetSelectOptionsByTableAsync("Projects", "Description");
            var user = GetCurrentUser();
            viewModel.ProjectTasks = await _masterDataManager.GetSelectOptionsByTableAsync("IdaTasks", "Name");
            viewModel.Tasks = await _idaTasksManager.GetUncompletedTasks(false, user.Id);
            viewModel.ProjectTasks = await _idaTasksManager.GetUncompletedTasks(true, user.Id);
            viewModel.ActivityTypes = await _masterDataManager.GetSelectOptionsByTableAsync("ActivityTypes", "Name");
            viewModel.PlanStatuses = await _masterDataManager.GetSelectOptionsByTableAsync("PlanStatuses", "Name");
            viewModel.RegularActivities = await _masterDataManager.GetSelectOptionsByTableAsync("RegularActivities", "Name");
            viewModel.User = user;
            var employeePhoto = (await _employeesManager.GetEmployeeByIdAsync(user.EmployeeId)).Payload.Photo;
            viewModel.ImageSource = employeePhoto;
            var nextDay = DateTime.Now.Date.AddDays(1);

            // preskoči vikend
            while (nextDay.DayOfWeek == DayOfWeek.Saturday || nextDay.DayOfWeek == DayOfWeek.Sunday)
            {
                nextDay = nextDay.AddDays(1);
            }

            viewModel.Today = nextDay.ToString("dd.MM.yyyy");

            return View("Index", viewModel);
        }

        [HttpGet("statistics", Name = RouteNames.IDA_StatisticView)]
        public async Task<IActionResult> StatisticView()
        {
            var viewModel = new IDAViewModel();
            var user = GetCurrentUser();
            var today = DateTime.Now.Date.ToString("dd.MM.yyyy");
            var taskPlannings = await _tasksPlanningsManager.SearchTasksPlanningsAsync(
    new Web.Models.RequestModels.TasksPlannings.SearchTasksPlanningsParams
    {
        UserId = user.Id,
        CreatedDate = today,
        Finished = false
    }
);

            // Napravi listu sa stvarnim planovima
            var taskPlanningsList = taskPlannings.Payload
                .Select(x => new GenericSelectOption
                {
                    Value = x.Id,
                    Description = x.PlanNo.Value.ToString() ?? ""
                })
                .ToList();

            // Dodaj opciju sa 0 na kraj    
            taskPlanningsList.Add(new GenericSelectOption
            {
                Value = 0,
                Description = "0" // tekst koji želiš
            });

            // Postavi svojstvo na novu listu
            viewModel.TaskPlannings = taskPlanningsList;
            viewModel.Projects = await _masterDataManager.GetSelectOptionsByTableAsync("Projects", "Description");
            viewModel.Tasks = await _idaTasksManager.GetUncompletedTasks(false, user.Id);
            viewModel.ProjectTasks = await _idaTasksManager.GetUncompletedTasks(true, user.Id);
            viewModel.ActivityTypes = await _masterDataManager.GetSelectOptionsByTableAsync("ActivityTypes", "Name");
            viewModel.PlanStatuses = await _masterDataManager.GetSelectOptionsByTableAsync("PlanStatuses", "Name");
            viewModel.RegularActivities = await _masterDataManager.GetSelectOptionsByTableAsync("RegularActivities", "Name");
            viewModel.User = user;
            var employeePhoto = (await _employeesManager.GetEmployeeByIdAsync(user.EmployeeId)).Payload.Photo;
            viewModel.ImageSource = employeePhoto;
            viewModel.Today = DateTime.Now.Date.ToString("dd.MM.yyyy");
            return View("StatisticView", viewModel);
        }
        [HttpGet("IDAViewers", Name = RouteNames.IDA_Viewers)]
        public async Task<IActionResult> IDAViewersView()
        {
            var viewModel = new IDAViewModel();
            var user = GetCurrentUser();
            var today = DateTime.Now.Date.ToString("dd.MM.yyyy");
            var taskPlannings = await _tasksPlanningsManager.SearchTasksPlanningsAsync(
    new Web.Models.RequestModels.TasksPlannings.SearchTasksPlanningsParams
    {
        UserId = user.Id,
        CreatedDate = today,
        Finished = false
    }
);

            // Napravi listu sa stvarnim planovima
            var taskPlanningsList = taskPlannings.Payload
                .Select(x => new GenericSelectOption
                {
                    Value = x.Id,
                    Description = x.PlanNo.Value.ToString() ?? ""
                })
                .ToList();

            // Dodaj opciju sa 0 na kraj    
            taskPlanningsList.Add(new GenericSelectOption
            {
                Value = 0,
                Description = "0" // tekst koji želiš
            });

            // Postavi svojstvo na novu listu
            viewModel.TaskPlannings = taskPlanningsList;
            viewModel.Projects = await _masterDataManager.GetSelectOptionsByTableAsync("Projects", "Description");
            viewModel.Tasks = await _idaTasksManager.GetUncompletedTasks(false, user.Id);
            viewModel.ProjectTasks = await _idaTasksManager.GetUncompletedTasks(true, user.Id);
            viewModel.ActivityTypes = await _masterDataManager.GetSelectOptionsByTableAsync("ActivityTypes", "Name");
            viewModel.PlanStatuses = await _masterDataManager.GetSelectOptionsByTableAsync("PlanStatuses", "Name");
            viewModel.RegularActivities = await _masterDataManager.GetSelectOptionsByTableAsync("RegularActivities", "Name");
            viewModel.User = user;
            var employeePhoto = (await _employeesManager.GetEmployeeByIdAsync(user.EmployeeId)).Payload.Photo;
            viewModel.ImageSource = employeePhoto;
            viewModel.Today = DateTime.Now.Date.ToString("dd.MM.yyyy");
            viewModel.Employees = await _employeesManager.GetEmployeesAsSelectOptionsAsync();
            return View("IDAViewersView", viewModel);
        }
        [HttpGet("projectsAndTasksView", Name = RouteNames.IDA_ProjectsAndTasksView)]
        public async Task<IActionResult> IdaProjectsAndTasksView()
        {
            var viewModel = new IDAViewModel();
            var user = GetCurrentUser();
            var today = DateTime.Now.Date.ToString("dd.MM.yyyy");
            var taskPlannings = await _tasksPlanningsManager.SearchTasksPlanningsAsync(
    new Web.Models.RequestModels.TasksPlannings.SearchTasksPlanningsParams
    {
        UserId = user.Id,
        CreatedDate = today,
        Finished = false
    }
);

            // Napravi listu sa stvarnim planovima
            var taskPlanningsList = taskPlannings.Payload
                .Select(x => new GenericSelectOption
                {
                    Value = x.Id,
                    Description = x.PlanNo.Value.ToString() ?? ""
                })
                .ToList();

            // Dodaj opciju sa 0 na kraj    
            taskPlanningsList.Add(new GenericSelectOption
            {
                Value = 0,
                Description = "0" // tekst koji želiš
            });

            // Postavi svojstvo na novu listu
            viewModel.TaskPlannings = taskPlanningsList;
            viewModel.Projects = await _masterDataManager.GetSelectOptionsByTableAsync("Projects", "Description");
            viewModel.Tasks = await _masterDataManager.GetSelectOptionsByTableAsync("IdaTasks", "Name");
            viewModel.ProjectTasks = await _idaTasksManager.GetUncompletedTasks(true, user.Id);
            viewModel.ActivityTypes = await _masterDataManager.GetSelectOptionsByTableAsync("ActivityTypes", "Name");
            viewModel.PlanStatuses = await _masterDataManager.GetSelectOptionsByTableAsync("PlanStatuses", "Name");
            viewModel.RegularActivities = await _masterDataManager.GetSelectOptionsByTableAsync("RegularActivities", "Name");
            viewModel.User = user;
            var employeePhoto = (await _employeesManager.GetEmployeeByIdAsync(user.EmployeeId)).Payload.Photo;
            viewModel.ImageSource = employeePhoto;
            viewModel.Today = DateTime.Now.Date.ToString("dd.MM.yyyy");
            viewModel.Employees = await _employeesManager.GetEmployeesAsSelectOptionsAsync();
            return View("IDAProjectsAndTasksView", viewModel);
        }
    }
}
