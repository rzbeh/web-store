using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyEshop.Controllers
{
    [Authorize]
    public class TestController : Controller
    {

        public string test1()
        {
            return "test 1";
        }    

        public string test2()
        {
            return "test2" ;
        }
    }
}
