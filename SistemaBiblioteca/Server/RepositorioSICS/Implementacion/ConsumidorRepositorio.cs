using Microsoft.EntityFrameworkCore;
using SICS.Server.Models;
using SICS.Server.RepositorioSICS.Contrato;
using System.Linq;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Implementacion
{
    public class ConsumidorRepositorio : IConsumidorRepositorio
    {
        private readonly SicsContext _dbContext;

        public ConsumidorRepositorio(SicsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Consumidor>> Consultar(Expression<Func<Consumidor, bool>> filtro = null)
        {
            IQueryable<Consumidor> queryEntidad = filtro == null ? _dbContext.Consumidors : _dbContext.Consumidors.Where(filtro);
            return queryEntidad;
        }

        public async Task<Consumidor> Crear(Consumidor entidad)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                int CantidadDigitos = 5;
                try
                {
                    NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.First(x => x.Tipo == "Consumidor");

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbContext.NumeroCorrelativos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();


                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string codigo = ceros + correlativo.UltimoNumero.ToString();
                    codigo = correlativo.Prefijo + codigo.Substring(codigo.Length - CantidadDigitos, CantidadDigitos);

                    entidad.Codigo = codigo;

                    await _dbContext.Consumidors.AddAsync(entidad);
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

        public async Task<bool> Editar(Consumidor entidad)
        {
            try
            {
                _dbContext.Consumidors.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(Consumidor entidad)
        {
            try
            {
                _dbContext.Consumidors.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Consumidor>> Lista()
        {
            try
            {
                return await _dbContext.Consumidors.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Consumidor> Obtener(Expression<Func<Consumidor, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.Consumidors.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
