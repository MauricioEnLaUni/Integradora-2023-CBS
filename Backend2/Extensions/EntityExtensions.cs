namespace Fictichos.Constructora.Repository;

internal static class EntityExtensions
{
    public static TDto To<TModel, TDto>(
        this TModel model)
    {
        var dto = Activator.CreateInstance<TDto>();
        var modelProps = typeof(TModel).GetProperties();
        var dtoProps = typeof(TDto).GetProperties();

        foreach (var dtoProp in dtoProps)
        {
            var modelProp = modelProps
                .FirstOrDefault(x => x.Name == dtoProp.Name);
            if (modelProp != null && modelProp.PropertyType == dtoProp.PropertyType)
            {
                dtoProp.SetValue(dto, modelProp.GetValue(model));
            }
        }

        return dto;
    }
}