namespace ClinicaApi.Models
{
    public class CentroCosto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        // Relacion: Un centro de costo tiene muchas transacciones
        public ICollection<TransaccionInventario> Transacciones { get; set; } = new List<TransaccionInventario>();
    }
}
