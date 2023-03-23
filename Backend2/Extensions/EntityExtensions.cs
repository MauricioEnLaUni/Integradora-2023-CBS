namespace Fictichos.Constructora.Repository;

internal static class EntityExtensions
{
    public static TDto To<TModel, TDto>(
        this TModel model)
    {
        var dto = Activator.CreateInstance<TDto>();
        var modelProperties = typeof(TModel).GetProperties();
        var dtoProperties = typeof(TDto).GetProperties();

        foreach (var dtoProperty in dtoProperties)
        {
            var modelProperty = modelProperties.FirstOrDefault(x => x.Name == dtoProperty.Name);
            if (modelProperty != null && modelProperty.PropertyType == dtoProperty.PropertyType)
            {
                dtoProperty.SetValue(dto, modelProperty.GetValue(model));
            }
        }

        return dto;
    }
}