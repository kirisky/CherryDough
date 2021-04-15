using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CherryDough.Application.Interface;
using CherryDough.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CherryDough.Services.Api.Controllers
{
    [Authorize]
    public class ShowcaseController : ApiController
    {
        private readonly IShowcaseAppService _showcaseAppService;

        public ShowcaseController(IShowcaseAppService showcaseAppService)
        {
            _showcaseAppService = showcaseAppService;
        }

        [AllowAnonymous]
        [HttpGet("showcase-dashboard")]
        public async Task<IEnumerable<ShowcaseViewModel>> Get()
        {
            return await _showcaseAppService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("showcase-dashboard/{category}")]
        public async Task<IEnumerable<ShowcaseViewModel>> Get(Guid id)
        {
            return await _showcaseAppService.GetById(id);
        }

        [AllowAnonymous]
        [HttpPost("showcase-dashboard")]
        public async Task<IActionResult> Post([FromBody] ShowcaseViewModel showcaseViewModel)
        {
            await _showcaseAppService.AddItemAsync(showcaseViewModel);
            return !ModelState.IsValid
                ? ShowcaseResponse(ModelState)
                : ShowcaseResponse(await _showcaseAppService.GetAll());
        }

        [AllowAnonymous]
        [HttpPut("showcase-dashboard")]
        public async Task<IActionResult> Put([FromBody] ShowcaseViewModel showcaseViewModel)
        {
            return ModelState.IsValid
                ? ShowcaseResponse(await _showcaseAppService.UpdateItemAsync(showcaseViewModel))
                : ShowcaseResponse(ModelState);
        }

        [AllowAnonymous]
        [HttpDelete("showcase-dashboard")]
        public async Task<IActionResult> Delete([FromBody] ShowcaseViewModel showcaseViewModel)
        {
            return ShowcaseResponse(await _showcaseAppService.RemoveItemAsync(showcaseViewModel));
        }
    }
}