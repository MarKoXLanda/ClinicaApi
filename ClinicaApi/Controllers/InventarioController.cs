using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicaApi.Data;
using ClinicaApi.Models;



namespace ClinicaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly ClinicaDbContext _context;

        public InventarioController(ClinicaDbContext context)
        {
            _context = context;
        }

        // POST: api/inventario/registrar-salida
        [HttpPost("registrar-salida")]
        public async Task<IActionResult> RegistrarSalida([FromBody] SalidaRequest request)
        {
            // transaccional de Db2
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var insumo = await _context.Insumos
                    .FirstOrDefaultAsync(i => i.SKU == request.SKU);

                if (insumo == null)
                    return NotFound(new { mensaje = "Insumo no encontrado." });

                if (insumo.StockGlobal < request.Cantidad)
                    return BadRequest(new { mensaje = "Stock insuficiente para realizar el retiro." });

                // 1. Restamos el stock global
                insumo.StockGlobal -= request.Cantidad;

                // 2. Registramos el movimiento para la auditoria (valor negativo = salida)
                var nuevaTransaccion = new TransaccionInventario
                {
                    InsumoMedicoId = insumo.Id,
                    CentroCostoId = request.CentroCostoId,
                    Cantidad = -request.Cantidad,
                    FechaTransaccion = DateTime.UtcNow,
                    UsuarioResponsable = request.UsuarioWeb
                };

                _context.Transacciones.Add(nuevaTransaccion);

                // 3. Guardamos los cambios en Db2
                await _context.SaveChangesAsync();

                // 4. Confirmamos que ambas operaciones fueron exitosas
                await transaction.CommitAsync();

                return Ok(new { mensaje = "Salida de inventario registrada con éxito." });
            }
            catch (Exception ex)
            {
                // Si ocurre CUALQUIER error, Db2 deshace todo para proteger la integridad
                await transaction.RollbackAsync();
                return StatusCode(500, new { mensaje = "Error interno procesando la transacción.", detalle = ex.Message });
            }
        }

        // POST: api/inventario/crear-centro
        [HttpPost("crear-centro")]
        public async Task<IActionResult> CrearCentro([FromBody] CentroCosto centro)
        {
            _context.CentrosCosto.Add(centro);
            await _context.SaveChangesAsync();
            return Ok(centro);
        }

        // POST: api/inventario/crear-insumo
        [HttpPost("crear-insumo")]
        public async Task<IActionResult> CrearInsumo([FromBody] InsumoMedico insumo)
        {
            _context.Insumos.Add(insumo);
            await _context.SaveChangesAsync();
            return Ok(insumo);
        }


    }

    // Objeto que recibe los datos desde la interfaz web/escritorio
    public class SalidaRequest
    {
        public string SKU { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public int CentroCostoId { get; set; }
        public string UsuarioWeb { get; set; } = string.Empty;
    }
}
