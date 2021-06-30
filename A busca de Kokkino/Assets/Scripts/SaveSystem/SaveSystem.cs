using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    // Fazer o save (transformar em binário e serializar para o "/testesave")
    public static void saveData(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/testesave";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    // Fazer o load (abrir o arquivo no "/testesave" e deserializar para usar no jogo)
    public static PlayerData loadData()
    {
        string path = Application.persistentDataPath + "/testesave";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else              // Caso não ache a pasta para salvar  
        {
            Debug.LogError("Não foi encontrado na pasta" + path);
            return null;
        }

    }
}

