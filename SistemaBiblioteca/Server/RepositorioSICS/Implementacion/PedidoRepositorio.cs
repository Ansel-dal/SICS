using Microsoft.EntityFrameworkCore;
using SICS.Server.Models;
using SICS.Server.RepositorioSICS.Contrato;
using System.Linq;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Implementacion
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly SicsContext _dbContext;

        public PedidoRepositorio(SicsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IQueryable<Pedido>> Consultar(Expression<Func<Pedido, bool>> filtro = null)
        {
            IQueryable<Pedido> queryEntidad = filtro == null ? _dbContext.Pedidos : _dbContext.Pedidos.Where(filtro);          
            return queryEntidad;
        }

        public async Task<Pedido> Crear(Pedido entidad)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                int CantidadDigitos = 5;
                try
                {
                    NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.First(x => x.Tipo == "Pedido");

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbContext.NumeroCorrelativos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();


                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string codigo = ceros + correlativo.UltimoNumero.ToString();
                    codigo = correlativo.Prefijo + codigo.Substring(codigo.Length - CantidadDigitos, CantidadDigitos);

                    //entidad.Codigo = codigo;

                    await _dbContext.Pedidos.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();

                    return entidad;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> Editar(Pedido entidad)
        {
            try
            {
                _dbContext.Pedidos.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(Pedido entidad)
        {
            try
            {
                _dbContext.Pedidos.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Pedido>> Lista()
        {

            try
            {
                return await _dbContext.Pedidos.Include(p => p.ProductosPedidos).ThenInclude(pp => pp.IdProductoNavigation).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Pedido> Obtener(Expression<Func<Pedido, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.Pedidos.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
