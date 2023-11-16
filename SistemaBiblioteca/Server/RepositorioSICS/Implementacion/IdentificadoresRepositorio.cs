using Microsoft.EntityFrameworkCore;
using SICS.Server.Models;
using SICS.Server.RepositorioSICS.Contrato;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Implementacion
{


    public class IdentificadoresRepositorio : IIdentificadoresRepositorio
    {

        private readonly SicsContext _dbContext;

        public IdentificadoresRepositorio(SicsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<NumeroCorrelativo>> Lista()
        {
            try
            {
                return await _dbContext.NumeroCorrelativos.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<NumeroCorrelativo> Obtener(Expression<Func<NumeroCorrelativo, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.NumeroCorrelativos.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Eliminar(NumeroCorrelativo entidad)
        {
            try
            {
                _dbContext.NumeroCorrelativos.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<NumeroCorrelativo> Crear(NumeroCorrelativo entidad)
        {
            try
            {
                _dbContext.Set<NumeroCorrelativo>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(NumeroCorrelativo entidad)
        {
            try
            {
                _dbContext.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
