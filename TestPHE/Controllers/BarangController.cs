using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TestPHE.Models;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]

public class BarangController : ControllerBase
{
    private readonly DbBarangContext _context;

    public BarangController(DbBarangContext context)
    {
        _context = context;
    }

    [HttpGet("GetBarangAll")]
    public async Task<ActionResult<IEnumerable<TblBarang>>> GetBarangAll()
    {
        return await _context.TblBarangs.ToListAsync();
    }

    [HttpGet("GetBarangById/{id}")]
    public async Task<ActionResult<TblBarang>> GetBarangById(int id)
    {
        var barang = await _context.TblBarangs.FindAsync(id);
        if (barang == null)
            return NotFound();

        return barang;
    }

    [HttpPost("InsertBarang")]
    public async Task<ActionResult<TblBarang>> InsertBarang(TblBarang barang)
    {
        _context.TblBarangs.Add(barang);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBarangById), new { id = barang.Id }, barang);
    }

    [HttpPatch("UpdateBarang/{id}")]
    public async Task<IActionResult> UpdateBarang(int id, [FromBody] JsonElement patchData)
    {
        var barang = await _context.TblBarangs.FindAsync(id);
        if (barang == null)
            return NotFound();

        if (patchData.TryGetProperty("nama", out var nama))
            barang.Nama = nama.GetString();

        if (patchData.TryGetProperty("stok", out var stok))
            barang.Stok = stok.GetInt32();

        if (patchData.TryGetProperty("harga", out var harga))
            barang.Harga = harga.GetInt32();

        await _context.SaveChangesAsync();

        return Ok(barang);
    }

    [HttpDelete("DeleteBarang/{id}")]
    public async Task<IActionResult> DeleteBarang(int id)
    {
        var barang = await _context.TblBarangs.FindAsync(id);
        if (barang == null)
            return NotFound();

        _context.TblBarangs.Remove(barang);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
