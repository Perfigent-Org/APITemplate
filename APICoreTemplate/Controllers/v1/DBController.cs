using APICoreTemplate.Core.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace APICoreTemplate.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DBController : ControllerBase
    {
        private readonly IServerDataFactory _data;
        public DBController(IServerDataFactory data)
        {
            _data = data;
        }

        [HttpGet]
        public async Task<IActionResult> CheckConnection(CancellationToken cancel = default)
        {
            try
            {
                using (var data = await _data.Create(cancel))
                {
                    await data.AssertConnected();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(true);
        }
    }
}
