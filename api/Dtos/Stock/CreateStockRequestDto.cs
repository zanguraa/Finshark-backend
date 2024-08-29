namespace api.Dtos.Stock;

public class CreateStockRequestDto
{
    public string Symbol { get; set; } = string.Empty;
    public string Companyname { get; set; } = String.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = String.Empty;
    public long MarketCap { get; set; }
}