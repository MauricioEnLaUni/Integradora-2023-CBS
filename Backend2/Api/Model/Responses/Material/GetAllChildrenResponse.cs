namespace Fictichos.Constructora.Dto;

public record MaterialChildren
{
    public List<MaterialCategoryDto> categories = new();
    public List<MaterialDto> material = new();
}