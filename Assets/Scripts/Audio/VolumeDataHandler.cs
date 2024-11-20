using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class VolumeDataHandler
{
    private static string _filePath = Application.persistentDataPath + "/volumeData.dat";

    // Guardar los datos en binario
    public static void SaveVolumeData(VolumeData data)
    {
        try
        {
            using (FileStream fileStream = new FileStream(_filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, data);
            }
            Debug.Log("Datos guardados exitosamente.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error al guardar los datos: {ex.Message}");
        }
    }

    // Cargar los datos desde el archivo binario
    public static VolumeData LoadVolumeData()
    {
        if (File.Exists(_filePath))
        {
            try
            {
                using (FileStream fileStream = new FileStream(_filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (VolumeData)formatter.Deserialize(fileStream);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error al cargar los datos: {ex.Message}");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró archivo de datos, creando valores predeterminados.");
        }

        // Devuelve un valor por defecto si no se encuentran datos
        return new VolumeData(1f, 1f, 1f);
    }
}
