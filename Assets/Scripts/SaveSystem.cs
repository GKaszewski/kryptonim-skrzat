using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;

public class VersionBinder : SerializationBinder {
    public override Type BindToType(string assemblyName, string typeName) {
        if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName)) {
            Type type = null;
            assemblyName = Assembly.GetExecutingAssembly().FullName;
            type = Type.GetType($"{typeName}, {assemblyName}");
            return type;
        }
        
        return null;
    }
}

public class SaveSystem {
    public static string Path { get; set; } = "Data/SaveData.dat";
    
    public static void SaveData(GameData data) {
        if (!Directory.Exists($"{Application.dataPath}/Data")) {
            Directory.CreateDirectory($"{Application.dataPath}/Data");
        }
        
        Path = $"{Application.dataPath}/{Path}";

        using var stream = new FileStream(Path, FileMode.Create);
        var formatter = new BinaryFormatter {
            Binder = new VersionBinder()
        };
        formatter.Serialize(stream, data);
        stream.Flush();
    }
    
    public static GameData LoadData() {
        Path = $"{Application.dataPath}/{Path}";
        
        if (!File.Exists(Path)) {
            return null;
        }
        
        using var stream = new FileStream(Path, FileMode.Open);
        var formatter = new BinaryFormatter {
            Binder = new VersionBinder()
        };
        return (GameData) formatter.Deserialize(stream);
    }
}