using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryV3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly DataContext _context;
    private readonly string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Invoices");

    /// <summary>
    /// Initializes a new instance of the <see cref="InvoiceController"/> class.
    /// </summary>
    /// <param name="dataContext">DataContext of the database.</param>
    public InvoiceController(DataContext dataContext)
    {
        _context = dataContext;

        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Invoice API Running...");
    }

    [HttpPost("Upload/{id}")]
    public async Task<IActionResult> Upload(int id, IFormFile file)
    {
        // Get the extension of the uploaded file.
        var ext = Path.GetExtension(file.FileName);

        // Get the item that is uploaded to.
        var item = await _context.Items.FindAsync(id);

        // If the item does not exist return badrequest.
        if (item is null)
        {
            return BadRequest("Item not found.");
        }

        // Filename as it will be saved on the server.
        var fileName = Guid.NewGuid().ToString() + ext;

        // Save the file to the server.
        using (Stream fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create, FileAccess.Write))
        {
            file.CopyTo(fileStream);
        }
        
        // If there is still an old invoice, remove it.
        if (item.Invoice is not null)
        {
            System.IO.File.Delete(Path.Combine(uploadPath, item.Invoice));
        }
        
        // Save file name of invoice.
        item.Invoice = fileName;
        
        // Update database.
        await _context.SaveChangesAsync();
        
        return Ok("Upload of invoice succeeded.");
    }

    [HttpGet("Download/{id}")]
    public async Task<IActionResult> Download(int id)
    {
        // Get item.
        var item = await _context.Items.FindAsync(id);

        // If item cannot be found return bad request.
        if (item is null)
            return BadRequest("Item not found.");

        // If item has no invoice return bad request.
        if (item.Invoice is null)
            return BadRequest("Item has no invoice.");

        // Get item and download it.
        var path = Path.Combine(uploadPath, item.Invoice);
        var stream = System.IO.File.OpenRead(path);
        return new FileStreamResult(stream, "application/octet-stream")
        {
            FileDownloadName = item.Invoice,
        };
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // Get item.
        var item = await _context.Items.FindAsync(id);

        // If item cannot be found return bad request.
        if (item is null)
            return BadRequest("Item not found.");

        if (item.Invoice is not null)
        {
            // Remove item.
            System.IO.File.Delete(Path.Combine(uploadPath, item.Invoice));
            item.Invoice = null;
            await _context.SaveChangesAsync();
        }

        return Ok("Deleted invoice successfully");
    }
}
