using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBinding.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerBaseController : Controller
    {
    }
}