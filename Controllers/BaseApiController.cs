using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // controller wordt vervangen door naam van class zonder het woord controller

    public class BaseApiController : ControllerBase
    {
        
    }
}