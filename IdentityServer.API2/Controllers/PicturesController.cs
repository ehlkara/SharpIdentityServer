using IdentityServer.API2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetPictures()
        {
            var pictures = new List<Picture>() { new Picture() { Id = 1, Name = "Natural picture", Url = "naturalpicture.jpg" },
            new Picture() { Id = 1, Name = "Elephant picture", Url = "elephantpicture.jpg" },
            new Picture() { Id = 1, Name = "Lion picture", Url = "lionpicture.jpg" },
            new Picture() { Id = 1, Name = "Dog picture", Url = "dogpicture.jpg" },
            new Picture() { Id = 1, Name = "Cat picture", Url = "catpicture.jpg" }};

            return Ok(pictures);
        }
    }
}
