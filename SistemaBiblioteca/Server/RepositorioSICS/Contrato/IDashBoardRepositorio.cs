namespace SICS.Server.RepositorioSICS.Contrato
{
    public interface IDashBoardRepositorio
    {
        Task<int> TotalItems();
        Task<int> TotalConsumidores();
        Task<int> PrestamosRegistrados();
        Task<int> PrestamosPendientes();
    }
}
