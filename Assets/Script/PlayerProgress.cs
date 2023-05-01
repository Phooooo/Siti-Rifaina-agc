using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;                    

[CreateAssetMenu(
    fileName = "Player Progress",
    menuName = "Game Kuis/Player Progress")]
public class PlayerProgress : ScriptableObject
{
    [System.Serializable]
    public struct MainData
    {
        public int koin;
        public Dictionary<string, int> progresLevel;
    }

    [SerializeField]
    private string _filename = "contoh.txt";

    [SerializeField]
    private string _startingLevelPackName = string.Empty;

    public MainData progresData = new MainData();


    public void SimpanProgres()
    {
        // // Sampel data
        // progresData.koin = 200;
        // if (progresData.progresLevel == null)
        //     progresData.progresLevel = new();
        // progresData.progresLevel.Add("Level Pack 1", 3);
        // progresData.progresLevel.Add("Level Pack 3", 5);

        //simpan starting data saat objek Dictinionary tidak ada saat dimuat 
        if (progresData.progresLevel == null)
        {
            progresData.progresLevel = new();
            progresData.koin = 0;
            progresData.progresLevel.Add(_startingLevelPackName, 1);
        }

        //informasi penyimpanan data 
#if UNITY_EDITOR
        string directory = Application.dataPath + "/Temporary";
#elif (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        string directory = Application.persistentDataPath + "/ProgresLokal/";
#endif  
        string path = directory + _filename;

        //membuat Directory Temporary
        if(!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been Created: " + directory);
        }

        //Membuat file baru
        if (File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }

       // var konten = $"{progresData.koin}\n";
       var fileStream = File.Open(path, FileMode.Open);
       //var formatter = new BinaryFormatter();

       fileStream.Flush();
        //formatter.Serialize(fileStream, progresData);
       //menyimpan data  ke dalam file menggunsakan binari writer
       var writer = new BinaryWriter(fileStream);

       writer.Write(progresData.koin);
       foreach (var i in progresData.progresLevel)
       {
            writer.Write(i.Key);
            writer.Write(i.Value);
       }

       //putuskan a;iran memori dengan file
       writer.Dispose();
       fileStream.Dispose();

       // foreach (var i in progresData.progresLevel)
       // {
       //     konten += $"{i.Key} {i.Value}\n";
       // }

        //File.WriteAllText(path, konten);

        Debug.Log($"{_filename} Berhasil Disimpan");
    }

    public bool MuatProgres()
    {
        //informasi penyimpanan data 
        string directory = Application.dataPath + "/Temporary/";
        string path = directory + _filename;
        
        var fileStream = File.Open(path, FileMode.OpenOrCreate);

        try
        {
            var reader = new BinaryReader(fileStream);

            try
            {
                progresData.koin = reader.ReadInt32();
                if (progresData.progresLevel == null)
                    progresData.progresLevel = new ();
                while (reader.PeekChar() != -1)
                {
                    var namaLevelPack = reader.ReadString();
                    var levelKe = reader.ReadInt32();
                    progresData.progresLevel.Add(namaLevelPack, levelKe);
                    Debug.Log($"{namaLevelPack}:{levelKe}");
                }
                //putuskan aliran memori dengan File
                reader.Dispose();
            }
            catch (System.Exception e)
            {

                Debug.Log($"ERROR: Terjadi kesalahan saat memuat progres\n{e.Message}");

                //putuskan aliran memori dengan File
                reader.Dispose();
                fileStream.Dispose();
                return false;
            }
            //memuat data dari file menggunakan binari formatter
            //var formatter = new BinaryFormatter();

            //progresData = (MainData)formatter.Deserialize(fileStream);

            //putuskan aliran memori dengan File
            fileStream.Dispose();

            Debug.Log($"{progresData.koin}; {progresData.progresLevel.Count}");

            return true;
        }
        catch (System.Exception e)
        {
            //putuskan aliran memori dengan File
            fileStream.Dispose();

            Debug.Log($"ERROR: Terjadi kesalahan saat memuat progres\n{e.Message}");

            return false;
        }
    }
}
