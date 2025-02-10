namespace Domain.Entities;

public class Pedido
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public List<ItemPedido> Itens { get; set; } = new();
    public decimal Total => Itens.Sum(i => i.Quantidade * i.Produto.Preco);
}