using UnityEngine;

public class DataManager<T>
{
    public string FileName { get; }

    public DataManager(string fileName)
    {
        FileName = fileName;
    }

    public void Save(T entity)
    {        
        string json = JsonUtility.ToJson(entity, true);

        FileUtilities.WriteToFile(FileName, json);
    }

    public T Load()
    {
        string json = FileUtilities.ReadFromFile(FileName);

        return JsonUtility.FromJson<T>(json);
    }
}
