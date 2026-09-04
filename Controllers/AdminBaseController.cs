using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBinding.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBaseController : Controller
    {
    }
}