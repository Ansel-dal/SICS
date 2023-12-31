﻿using SICS.Server.Models;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Contrato
{
    public interface IPrestamoRepositorio
    {
        Task<List<Prestamo>> Lista();
        Task<Prestamo> Obtener(Expression<Func<Prestamo, bool>> filtro = null);
        Task<Prestamo> Crear(Prestamo entidad);
        Task<bool> Editar(Prestamo entidad);
        Task<bool> Eliminar(Prestamo entidad);
        Task<IQueryable<Prestamo>> Consultar(Expression<Func<Prestamo, bool>> filtro = null);
    }
}
