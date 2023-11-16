using AutoMapper;
using SICS.Server.Models;
using SICS.Shared;
using System.Globalization;

namespace SICS.Server.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Usuario
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>()
            .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(src => true)
                );
            #endregion Usuario

            #region Consumidor
            CreateMap<Consumidor, ConsumidorDTO>();
            CreateMap<ConsumidorDTO, Consumidor>()
                 .ForMember(destino =>
                    destino.Estado,
                    opt => opt.MapFrom(src => true)
                );
            #endregion Consumidor

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<CategoriaDTO, Categoria>()
                .ForMember(destino =>
                    destino.Estado,
                    opt => opt.MapFrom(src => true)
                );
            #endregion Categoria


            #region EstadoPrestamo
            CreateMap<EstadoPrestamo, EstadoPrestamoDTO>();
            CreateMap<EstadoPrestamoDTO, EstadoPrestamo>()
                 .ForMember(destino =>
                    destino.Estado,
                    opt => opt.MapFrom(src => true)
                );
            #endregion EstadoPrestamo

            #region Prestamo
            CreateMap<Prestamo, PrestamoDTO>();
            CreateMap<PrestamoDTO, Prestamo>()
                 .ForMember(destino =>
                    destino.Estado,
                    opt => opt.MapFrom(src => true)
                );
            #endregion Prestamo

            #region Item
            CreateMap<Item, ItemDTO>();
            CreateMap<ItemDTO, Item>()
                 .ForMember(destino =>
                    destino.Estado,
                    opt => opt.MapFrom(src => true)
                );
            #endregion Item

            #region Pedido
            CreateMap<Pedido, PedidoDTO>();
            CreateMap<PedidoDTO, Pedido>();
            #endregion Pedido

            #region Producto
            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoDTO, Producto>()
                 .ForMember(destino =>
                    destino.Estado,
                    opt => opt.MapFrom(src => true)
                );
            #endregion Producto

            #region ProductoPedido
            CreateMap<ProductosPedido, ProductosPedidoDTO>();
            CreateMap<ProductosPedidoDTO, ProductosPedido>();
            #endregion ProductoPedido

            #region 


            CreateMap<EstadoPedido, EstadoPedidoDTO>();
            CreateMap<EstadoPedidoDTO, EstadoPedido>();
            #endregion EstadoPedido

            //le puse otro nombre para la clase del lado del cliente
            #region Identificadores
            CreateMap<NumeroCorrelativo, IdentificadoresDTO>();
            CreateMap<IdentificadoresDTO, NumeroCorrelativo>();
            #endregion Identificadores

            #region Entrega
            CreateMap<Entrega, EntregaDTO>();
            CreateMap<EntregaDTO, Entrega>();
            #endregion Entrega



        }
    }
}
