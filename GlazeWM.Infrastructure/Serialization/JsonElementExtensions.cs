using System;
using System.Text.Json;

namespace GlazeWM.Infrastructure.Serialization
{
  public static class JsonElementExtensions
  {
    /// <summary>
    /// Get JSON property where property name has been converted according to naming policy.
    /// </summary>
    private static JsonElement GetConvertedProperty(
      this JsonElement element,
      string propertyName,
      JsonSerializerOptions options)
    {
      // Convert name according to given naming policy.
      var convertedName =
        options.PropertyNamingPolicy?.ConvertName(propertyName) ?? propertyName;

      return element.GetProperty(convertedName);
    }

    public static string GetStringProperty(
      this JsonElement element,
      string propertyName,
      JsonSerializerOptions options)
    {
      return element.GetConvertedProperty(propertyName, options).GetString();
    }

    public static int GetInt64Property(
      this JsonElement element,
      string propertyName,
      JsonSerializerOptions options)
    {
      return element.GetConvertedProperty(propertyName, options).GetInt32();
    }

    public static double GetDoubleProperty(
      this JsonElement element,
      string propertyName,
      JsonSerializerOptions options)
    {
      return element.GetConvertedProperty(propertyName, options).GetDouble();
    }

    public static T GetEnumProperty<T>(
      this JsonElement element,
      string propertyName,
      JsonSerializerOptions options) where T : struct, Enum
    {
      T value;
      Enum.TryParse<T>(element.GetProperty(propertyName).GetString(), out value);
      return value;
    }

    private static string ConvertName(string propertyName, JsonSerializerOptions options)
    {
      return options.PropertyNamingPolicy?.ConvertName(propertyName) ?? propertyName;
    }
  }
}