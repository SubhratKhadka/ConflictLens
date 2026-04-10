namespace Lafda.Dtos;

public class EmbeddingResponseDto
{
    public List<float> Embedding { get; set; } = null!;
    public int Dimension { get; set; }
}