using Microsoft.EntityFrameworkCore;
using SICS.Server.Models;
using SICS.Server.RepositorioSICS.Contrato;
using System.Linq;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Implementacion
{
    public class EntregaRepositorio : IEntregaRepositorio
    {
        private readonly SicsContext _dbContext;

        public EntregaRepositorio(SicsContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IQueryable<Entrega>> Consultar(Expression<Func<Entrega, bool>> filtro = null)
        {
            IQueryable<Entrega> queryEntidad = filtro == null ? _dbContext.Entregas : _dbContext.Entregas.Where(filtro);
            return queryEntidad;
        }

        public async Task<Entrega> Crear(Entrega entidad)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                int CantidadDigitos = 5;
                try
                {
                    NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.First(x => x.Tipo == "Entrega");

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbContext.NumeroCorrelativos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();


                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string codigo = ceros + correlativo.UltimoNumero.ToString();
                    codigo = correlativo.Prefijo + codigo.Substring(codigo.Length - CantidadDigitos, CantidadDigitos);

                    entidad.Codigo = codigo;

                    await _dbContext.Entregas.AddAsync(entidad);
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

        public async Task<bool> Editar(Entrega entidad)
        {
            try
            {
                _dbContext.Entregas.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(Entrega entidad)
        {
            try
            {
                _dbContext.Entregas.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Entrega>> Lista()
        {

            try
            {
                return await _dbContext.Entregas.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Entrega> Obtener(Expression<Func<Entrega, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.Entregas.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
