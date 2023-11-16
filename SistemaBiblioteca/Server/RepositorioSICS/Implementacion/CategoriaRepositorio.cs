using Microsoft.EntityFrameworkCore;
using SICS.Server.Models;
using SICS.Server.RepositorioSICS.Contrato;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Implementacion
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {

        private readonly SicsContext _dbContext;

        public CategoriaRepositorio(SicsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Categoria>> Lista()
        {
            try
            {
                return await _dbContext.Categoria.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

       
        public async Task<Categoria> Obtener(Expression<Func<Categoria, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.Categoria.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Eliminar(Categoria entidad)
        {
            try
            {
                _dbContext.Categoria.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Categoria> Crear(Categoria entidad)
        {
            try
            {
                _dbContext.Set<Categoria>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(Categoria entidad)
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
        public async Task<IQueryable<Categoria>> Consultar(Expression<Func<Categoria, bool>> filtro = null)
        {
            IQueryable<Categoria> queryEntidad = filtro == null ? _dbContext.Categoria : _dbContext.Categoria.Where(filtro);
            return queryEntidad;
        }
    }
}
