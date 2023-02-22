using Microsoft.AspNetCore.Mvc;

namespace BCP.Controllers;

[ApiController]
[Route("api/attachment")]
public class AttachmentController : ControllerBase
{
    [HttpPost("temp")]
    public async Task<IActionResult> UploadAsync(IFormFile file)
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "temp");
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        
        var fileName = Path.Combine(Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));
        
        var fullPath = Path.Combine(filePath,fileName);

        await using (var fileSteam = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(fileSteam);
        }
        
        return Ok(fileName);
    }
}