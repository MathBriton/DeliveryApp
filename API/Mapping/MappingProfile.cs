using AutoMapper;
using Domain.Entities;
using Application.DTOs;

namespace API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeamentos
        CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        CreateMap<Restaurante, RestauranteDTO>().ReverseMap();
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Pedido, PedidoDTO>().ReverseMap();
        CreateMap<ItemPedido, ItemPedidoDTO>().ReverseMap();
    }
}