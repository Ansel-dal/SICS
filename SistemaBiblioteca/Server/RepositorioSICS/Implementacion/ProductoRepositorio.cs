using Microsoft.EntityFrameworkCore;
using SICS.Server.Models;
using SICS.Server.RepositorioSICS.Contrato;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Implementacion
{
    public class ProductoRepositorio : IProductoRepositorio
    {

        private readonly SicsContext _dbContext;

        public ProductoRepositorio(SicsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Producto>> Lista()
        {
            try
            {
                return await _dbContext.Productos.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

       
        public async Task<Producto> Obtener(Expression<Func<Producto, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.Productos.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Eliminar(Producto entidad)
        {
            try
            {
                _dbContext.Productos.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Producto> Crear(Producto entidad)
        {
            try
            {
                _dbContext.Set<Producto>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(Producto entidad)
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
        public async Task<IQueryable<Producto>> Consultar(Expression<Func<Producto, bool>> filtro = null)
        {
            IQueryable<Producto> queryEntidad = filtro == null ? _dbContext.Productos : _dbContext.Productos.Where(filtro);
            return queryEntidad;
        }
    }
}
