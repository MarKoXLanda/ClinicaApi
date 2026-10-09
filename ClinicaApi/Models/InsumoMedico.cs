using System.Text.Json.Serialization;

namespace ClinicaApi.Models
{
    public class InsumoMedico
    {
        public int Id { get; set; }
        public string SKU { get; set; } = string.Empty; // Codigo interno o de barras estandar
        public string Nombre { get; set; } = string.Empty;
        public int StockGlobal { get; set; }
        public decimal PrecioUnitario { get; set; }

        [JsonIgnore]
        public ICollection<TransaccionInventario> Transacciones { get; set; } = new List<TransaccionInventario>();
    }
}
