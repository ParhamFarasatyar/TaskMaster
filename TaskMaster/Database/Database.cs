using System.Text.Json;
namespace DataBase;
using System.Text.Json.Serialization;

public enum DataType { Users, Questions, Answers }

public static class Database
{
    private static readonly JsonSerializerOptions options = new()
    {
        WriteIndented = true,
        Converters =
        {
            new JsonStringEnumConverter()
        }
    };
    private static string GetPath(DataType type)
    {
        string dataFolder = Path.Combine(
            AppContext.BaseDirectory,
            "Data"
        );

        Directory.CreateDirectory(dataFolder);

        return Path.Combine(
            dataFolder,
            $"{type}.json"
        );
    }
    
    public static void Save<T>(T data, DataType type)
    {
        string path = GetPath(type);
        
        List<T> dataList;
        
        if (File.Exists(path))
        {
            dataList = Load<T>(type);
        }
        else
        {
            dataList = new List<T>();
        }
        
        dataList.Add(data);
        
        string json = JsonSerializer.Serialize(
            dataList,
            options
        );
        
        File.WriteAllText(
            path,
            json
        );
    }
    
    public static List<T> Load<T>(DataType type)
    {
        string path = GetPath(type);
        
        if (!File.Exists(path))
        {
            return new List<T>();
        }
        
        string json = File.ReadAllText(path);
        
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<T>();
        }
        
        return JsonSerializer.Deserialize<List<T>>(json, options)
               ?? new List<T>();
    }
    
    public static void Update<T>(
        List<T> data,
        DataType type)
    {
        string path = GetPath(type);
        
        string json = JsonSerializer.Serialize(
            data,
            options
        );
        
        File.WriteAllText(
            path,
            json
        );
    }
}