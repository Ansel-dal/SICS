using SICS.Server.Models;
using SICS.Server.RepositorioSICS.Contrato;

namespace SICS.Server.RepositorioSICS.Implementacion
{
    public class DashBoardRepositorio : IDashBoardRepositorio
    {
        private readonly SicsContext _dbContext;

        public DashBoardRepositorio(SicsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> PrestamosPendientes()
        {
            try
            {
                IQueryable<Prestamo> query = _dbContext.Prestamos;
                int total = query.Where(p => p.IdEstadoPrestamo == 1).Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> PrestamosRegistrados()
        {
            try
            {
                IQueryable<Prestamo> query = _dbContext.Prestamos;
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalConsumidores()
        {
            try
            {
                IQueryable<Consumidor> query = _dbContext.Consumidors;
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalItems()
        {
            try
            {
                IQueryable<Item> query = _dbContext.Items;
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }
    }
}
