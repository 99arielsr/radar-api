namespace radar_api.Services
{
  public class BuilderService<T>
  {
    public static T Builder(object objectDTO)
    {
      var obj = Activator.CreateInstance<T> ();

      foreach(var propertyDTO in objectDTO.GetType().GetProperties())
      {
        var propertyObj = obj?.GetType().GetProperty(propertyDTO.Name);
        if(propertyDTO is not null)
        {
          propertyDTO.SetValue(obj, propertyDTO.GetValue(objectDTO));
        }
      }

      return obj;
    }
  }
}
