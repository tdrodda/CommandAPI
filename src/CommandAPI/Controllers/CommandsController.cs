using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CommandAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return new string[] {"this", "is", "hard", "coded" };
		}
	}
	
}