namespace Domain.Entities;

public class Restaurante
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public List<Produto> Cardapio { get; set; } = new();
}