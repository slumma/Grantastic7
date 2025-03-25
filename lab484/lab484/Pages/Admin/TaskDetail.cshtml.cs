using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace lab484.Pages.Admin
{
    public class TaskDetailModel : PageModel
    {
        public int taskID { get; set; }
        public void OnGet(int taskID)
        {
            this.taskID = taskID;
        }
    }
}
