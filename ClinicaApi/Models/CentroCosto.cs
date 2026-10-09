using System.Text.Json.Serialization;

namespace ClinicaApi.Models
{
    public class CentroCosto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        [JsonIgnore] // Esto le dice a Swagger que ignore este campo al crear
        public ICollection<TransaccionInventario> Transacciones { get; set; } = new List<TransaccionInventario>();
    }
}
