namespace Domain.Entities;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int RestauranteId { get; set; }
    public Restaurante Restaurante { get; set; } = null!;
}