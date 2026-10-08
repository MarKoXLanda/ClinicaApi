namespace ClinicaApi.Models
{
    public class TransaccionInventario
    {
        public int Id { get; set; }
        public int InsumoMedicoId { get; set; }
        public int CentroCostoId { get; set; }

        public int Cantidad { get; set; } // Positivo para entradas, negativo para salidas (retiros)
        public DateTime FechaTransaccion { get; set; } = DateTime.UtcNow;
        public string UsuarioResponsable { get; set; } = string.Empty; 

        // Propiedades de navegación de EF Core
        public InsumoMedico Insumo { get; set; } = null!;
        public CentroCosto CentroCosto { get; set; } = null!;
    }
}
