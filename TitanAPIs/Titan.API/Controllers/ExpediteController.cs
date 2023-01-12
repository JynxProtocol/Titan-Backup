using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Titan.API.Functions;
using Titan.Filters;
using Titan.Models.Expedite;

namespace Titan.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Feature("Expedite")]
    [Produces("application/json")]
    public class ExpediteController : ControllerBase
    {
        public ExpediteController(TitanContext titanContext, IServiceScopeFactory scopeFactory)
        {
            Titan = titanContext;
            ServiceScopeFactory = scopeFactory;
        }
        public TitanContext Titan { get; private set; }
        public IServiceScopeFactory ServiceScopeFactory { get; private set; }

        /// <response code="200">Succesfully returns expedite status</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ExpediteStatusDTO))]
        public async Task<ExpediteStatusDTO> GetExpediteStatus()
        {
            return await Titan.NewExpedites.AsNoTracking()
                .Select(info => new ExpediteStatusDTO
                {
                    LastComplete = info.LastRunTime.ToString() ?? "Never",
                    Progress = info.Progress,
                    Status = info.Status,
                    SkippedRecords = info.SkippedRecords,
                })
                .FirstAsync();
        }

        /// <response code="200">Expedite started</response>
        /// <response code="409">
        /// Expedite is already running (only one expedite can ran at once)
        /// </response>
        [HttpPost]
        [Feature("RunExpedite")]
        public ActionResult StartExpedite()
        {
            // strictly one expedite only can run at a time
            if (!Startup.ExpediteLock.Wait(0))
            {
                return Conflict();
            }
            else
            {
                // execute expedite after we return and the client gets a response
                // kind of like a queued task, but handled by MVC
                HttpContext.Response.OnCompleted(() =>
                {
                    try
                    {
                        // run expedite
                        using (var scope = this.ServiceScopeFactory.CreateScope())
                        {
                            var Expedite = new Expedite(scope);

                            Expedite.RunExpedite();
                        }
                    }
                    finally
                    {
                        // exit lock
                        Startup.ExpediteLock.Release();
                    }

                    return Task.CompletedTask;
                });

                return Ok();
            }
        }
    }
}
