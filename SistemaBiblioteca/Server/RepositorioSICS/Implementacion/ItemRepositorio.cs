using Microsoft.EntityFrameworkCore;
using SICS.Server.Models;
using SICS.Server.RepositorioSICS.Contrato;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Implementacion
{
    public class ItemRepositorio : IItemRepositorio
    {
        private readonly SicsContext _dbContext;

        public ItemRepositorio(SicsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IQueryable<Item>> Consultar(Expression<Func<Item, bool>> filtro = null)
        {
            IQueryable<Item> queryEntidad = filtro == null ? _dbContext.Items : _dbContext.Items.Where(filtro);
            return queryEntidad;
        }

        public async Task<Item> Crear(Item entidad)
        {
            try
            {
                _dbContext.Set<Item>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(Item entidad)
        {
            try
            {
                _dbContext.Items.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(Item entidad)
        {
            try
            {
                _dbContext.Items.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Item>> Lista()
        {
            try
            {
                return await _dbContext.Items.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Item> Obtener(Expression<Func<Item, bool>> filtro = null)
        {
            try
            {
                var a = await _dbContext.Items.Where(filtro).FirstOrDefaultAsync();
                return a;
            }
            catch
            {
                throw;
            }
        }
    }
}
