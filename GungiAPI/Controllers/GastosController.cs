using Microsoft.AspNetCore.Mvc;
using GungiAPI.Data;
using GungiAPI.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class GastosController : ControllerBase
{
    private readonly GungiDb _db;

    public GastosController(GungiDb db)
    {
        _db = db;
    }

    [HttpGet("detalhados")]
    public async Task<IActionResult> GetDetalhados()
    {
        var gastos = await _db.Gastos.ToListAsync();
        return Ok(gastos);
    }

    [HttpGet("resumo-mensal")]
    public async Task<IActionResult> GetResumoMensal()
    {
        var hoje = DateTime.UtcNow;
        var primeiroDia = new DateTime(hoje.Year, hoje.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var ultimoDia = primeiroDia.AddMonths(1).AddDays(-1);
Console.WriteLine(primeiroDia);
        var resumo = await _db.Gastos
            .Where(g => g.DataLancamento >= primeiroDia && g.DataLancamento <= ultimoDia)
            .GroupBy(g => g.Categoria)
            .Select(grupo => new
            {
                Categoria = grupo.Key,
                Mes = primeiroDia.ToString("MM/yyyy"),
                Total = grupo.Sum(g => g.Valor)
            })
            .ToListAsync();

        return Ok(resumo);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Gasto gasto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _db.Gastos.Add(gasto);
        await _db.SaveChangesAsync();
        return Ok(gasto);
    }
}