using FalconeOne.BLL.Interfaces;
using FalconOne.Models.Dtos.Tags;
using Microsoft.AspNetCore.Mvc;

namespace FalconOne.API.Controllers.Tags
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : BaseSecureController
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("tag-entity")]
        public async Task<IActionResult> Tag(TagEntityDto model, CancellationToken cancellationToken)
        {
            var result = await _tagService.TagEntityAsync(model, cancellationToken);

            return result ? Ok() : BadRequest();
        }
    }
}
