using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Security.Cryptography;
using System.Text;

public class DataBase : MonoBehaviour
{
    public static DataBase ins;
    void Awake()
    {
        if (ins == null)
        {
            ins = this;
            ins.LoadAllData();
            DontDestroyOnLoad(this);
        }
        else if (ins != this)
        {
            Destroy(gameObject);
        }
    }

    public class XMLDB
    {
        public GameData gameDB = new GameData();
        public LevelDatabase levelDB = new LevelDatabase();
        public ScreenDatabase screenDB = new ScreenDatabase();
        public SoundDatabase soundDB = new SoundDatabase();
        public SpriteDatabase spriteDB = new SpriteDatabase();
        public PlayerData playerDB = new PlayerData();
        public PowerUPDatabase powerupDB = new PowerUPDatabase();
        public ObstacleDatabase obstacleDB = new ObstacleDatabase();
        public ProyectileDatabase proyectileDB = new ProyectileDatabase();
        public AlienDatabase alienDB = new AlienDatabase();
    }

    public XMLDB XmlDataBase = new XMLDB();
    bool encryptedDatabase = true;

    public void UpdateScore(int new_score)
    {
        Debug.Log("Current score = " + XmlDataBase.gameDB.score);
        if (new_score+1 > XmlDataBase.gameDB.score)
        {
            Debug.Log("Updating score, new_score = " + new_score+1);
            XmlDataBase.gameDB.score = new_score+1;
            UpdateGameData();
        }
    }
    public void SaveAndEncryptAllData()
    {
        bool enc = encryptedDatabase;
        encryptedDatabase = true;
        SaveAllData();
        encryptedDatabase = enc;
    }
    public void SaveAllData()
    {
        SaveGameData();
        SaveLevelData();
        SaveScreenData();
        SaveSoundData();
        SaveSpriteData();
        SavePlayerData();
        SavePowerUPData();
        SaveObstacleData();
        SaveProyectileData();
        SaveAlienData();
    }
    public void SaveGameData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        if (encryptedDatabase)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, XmlDataBase.gameDB, emptyNamespaces);
                byte[] b = Aes_Encryptor.AesEncryptor.EncryptString(stream.ToString());
                File.WriteAllBytes(Application.dataPath + "/StreamingFiles/XML/game_data.dat", b);
            }
        }
        else
        {
            FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/game_data.xml", FileMode.Create);
            serializer.Serialize(stream, XmlDataBase.gameDB);
            stream.Close();
        }
    }
    public void SaveLevelData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(LevelDatabase));
        if (encryptedDatabase)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, XmlDataBase.levelDB, emptyNamespaces);
                byte[] b = Aes_Encryptor.AesEncryptor.EncryptString(stream.ToString());
                File.WriteAllBytes(Application.dataPath + "/StreamingFiles/XML/level_data.dat", b);
            }
        }
        else
        {
            FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/level_data.xml", FileMode.Create);
            serializer.Serialize(stream, XmlDataBase.levelDB);
            stream.Close();
        }
    }
    public void SaveScreenData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ScreenDatabase));
        if (encryptedDatabase)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, XmlDataBase.screenDB, emptyNamespaces);
                byte[] b = Aes_Encryptor.AesEncryptor.EncryptString(stream.ToString());
                File.WriteAllBytes(Application.dataPath + "/StreamingFiles/XML/screen_data.dat", b);
            }
        }
        else
        {
            FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/screen_data.xml", FileMode.Create);
            serializer.Serialize(stream, XmlDataBase.screenDB);
            stream.Close();
        }
    }
    public void SaveSoundData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SoundDatabase));
        if (encryptedDatabase)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, XmlDataBase.soundDB, emptyNamespaces);
                byte[] b = Aes_Encryptor.AesEncryptor.EncryptString(stream.ToString());
                File.WriteAllBytes(Application.dataPath + "/StreamingFiles/XML/sound_data.dat", b);
            }
        }
        else
        {
            FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/sound_data.xml", FileMode.Create);
            serializer.Serialize(stream, XmlDataBase.soundDB);
            stream.Close();
        }
    }
    public void SaveSpriteData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SpriteDatabase));
        if (encryptedDatabase)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, XmlDataBase.spriteDB, emptyNamespaces);
                byte[] b = Aes_Encryptor.AesEncryptor.EncryptString(stream.ToString());
                File.WriteAllBytes(Application.dataPath + "/StreamingFiles/XML/sprite_data.dat", b);
            }
        }
        else
        {
            FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/sprite_data.xml", FileMode.Create);
            serializer.Serialize(stream, XmlDataBase.spriteDB);
            stream.Close();
        }
    }
    public void SavePlayerData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        if (encryptedDatabase)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, XmlDataBase.playerDB, emptyNamespaces);
                byte[] b = Aes_Encryptor.AesEncryptor.EncryptString(stream.ToString());
                File.WriteAllBytes(Application.dataPath + "/StreamingFiles/XML/player_data.dat", b);
            }
        }
        else
        {
            FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/player_data.xml", FileMode.Create);
            serializer.Serialize(stream, XmlDataBase.playerDB);
            stream.Close();
        }
    }
    public void SavePowerUPData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PowerUPDatabase));
        if (encryptedDatabase)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, XmlDataBase.powerupDB, emptyNamespaces);
                byte[] b = Aes_Encryptor.AesEncryptor.EncryptString(stream.ToString());
                File.WriteAllBytes(Application.dataPath + "/StreamingFiles/XML/powerup_data.dat", b);
            }
        }
        else
        {
            FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/powerup_data.xml", FileMode.Create);
            serializer.Serialize(stream, XmlDataBase.powerupDB);
            stream.Close();
        }
    }
    public void SaveObstacleData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ObstacleDatabase));
        if (encryptedDatabase)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, XmlDataBase.obstacleDB, emptyNamespaces);
                byte[] b = Aes_Encryptor.AesEncryptor.EncryptString(stream.ToString());
                File.WriteAllBytes(Application.dataPath + "/StreamingFiles/XML/obstacle_data.dat", b);
            }
        }
        else
        {
            FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/obstacle_data.xml", FileMode.Create);
            serializer.Serialize(stream, XmlDataBase.obstacleDB);
            stream.Close();
        }
    }
    public void SaveProyectileData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ProyectileDatabase));
        if (encryptedDatabase)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, XmlDataBase.proyectileDB, emptyNamespaces);
                byte[] b = Aes_Encryptor.AesEncryptor.EncryptString(stream.ToString());
                File.WriteAllBytes(Application.dataPath + "/StreamingFiles/XML/proyectile_data.dat", b);
            }
        }
        else
        {
            FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/proyectile_data.xml", FileMode.Create);
            serializer.Serialize(stream, XmlDataBase.proyectileDB);
            stream.Close();
        }
    }
    public void SaveAlienData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AlienDatabase));
        if (encryptedDatabase)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, XmlDataBase.alienDB, emptyNamespaces);
                byte[] b = Aes_Encryptor.AesEncryptor.EncryptString(stream.ToString());
                File.WriteAllBytes(Application.dataPath + "/StreamingFiles/XML/alien_data.dat", b);
            }
        }
        else
        {
            FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/alien_data.xml", FileMode.Create);
            serializer.Serialize(stream, XmlDataBase.alienDB);
            stream.Close();
        }
    }
    public void LoadAllData()
    {
        LoadGameData();
        LoadLevelData();
        LoadScreenData();
        LoadSoundData();
        LoadSpriteData();
        LoadPlayerData();
        LoadPowerUPData();
        LoadObstacleData();
        LoadProyectileData();
        LoadAlienData();
        Debug.Log("AllDataLoaded");
    }
    public void LoadGameData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        Stream stream;
        if (encryptedDatabase)
        {
            byte[] b = File.ReadAllBytes(Application.dataPath + "/StreamingFiles/XML/game_data.dat");
            string contents = Aes_Encryptor.AesEncryptor.DecryptBytes(b);
            stream = GenerateStreamFromString(contents);
        }
        else
        {
            stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/game_data.xml", FileMode.Open);
        }
        XmlDataBase.gameDB = serializer.Deserialize(stream) as GameData;
        stream.Close();
    }
    public void LoadLevelData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(LevelDatabase));
        Stream stream;
        if (encryptedDatabase)
        {
            byte[] b = File.ReadAllBytes(Application.dataPath + "/StreamingFiles/XML/level_data.dat");
            string contents = Aes_Encryptor.AesEncryptor.DecryptBytes(b);
            stream = GenerateStreamFromString(contents);
        }
        else
        {
            stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/level_data.xml", FileMode.Open);
        }
        XmlDataBase.levelDB = serializer.Deserialize(stream) as LevelDatabase;
        stream.Close();
    }
    public void LoadScreenData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ScreenDatabase));
        Stream stream;
        if (encryptedDatabase)
        {
            byte[] b = File.ReadAllBytes(Application.dataPath + "/StreamingFiles/XML/screen_data.dat");
            string contents = Aes_Encryptor.AesEncryptor.DecryptBytes(b);
            stream = GenerateStreamFromString(contents);
        }
        else
        {
            stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/screen_data.xml", FileMode.Open);
        }
        XmlDataBase.screenDB = serializer.Deserialize(stream) as ScreenDatabase;
        stream.Close();
    }
    public void LoadSoundData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SoundDatabase));
        Stream stream;
        if (encryptedDatabase)
        {
            byte[] b = File.ReadAllBytes(Application.dataPath + "/StreamingFiles/XML/sound_data.dat");
            string contents = Aes_Encryptor.AesEncryptor.DecryptBytes(b);
            stream = GenerateStreamFromString(contents);
        }
        else
        {
            stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/sound_data.xml", FileMode.Open);
        }
        XmlDataBase.soundDB = serializer.Deserialize(stream) as SoundDatabase;
        stream.Close();
    }
    public void LoadSpriteData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SpriteDatabase));
        Stream stream;
        if (encryptedDatabase)
        {
            byte[] b = File.ReadAllBytes(Application.dataPath + "/StreamingFiles/XML/sprite_data.dat");
            string contents = Aes_Encryptor.AesEncryptor.DecryptBytes(b);
            stream = GenerateStreamFromString(contents);
        }
        else
        {
            stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/sprite_data.xml", FileMode.Open);
        }
        XmlDataBase.spriteDB = serializer.Deserialize(stream) as SpriteDatabase;
        stream.Close();
    }
    public void LoadPlayerData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        Stream stream;
        if (encryptedDatabase)
        {
            byte[] b = File.ReadAllBytes(Application.dataPath + "/StreamingFiles/XML/player_data.dat");
            string contents = Aes_Encryptor.AesEncryptor.DecryptBytes(b);
            stream = GenerateStreamFromString(contents);
        }
        else
        {
            stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/player_data.xml", FileMode.Open);
        }
        XmlDataBase.playerDB = serializer.Deserialize(stream) as PlayerData;
        stream.Close();
    }
    public void LoadPowerUPData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PowerUPDatabase));
        Stream stream;
        if (encryptedDatabase)
        {
            byte[] b = File.ReadAllBytes(Application.dataPath + "/StreamingFiles/XML/powerup_data.dat");
            string contents = Aes_Encryptor.AesEncryptor.DecryptBytes(b);
            stream = GenerateStreamFromString(contents);
        }
        else
        {
            stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/powerup_data.xml", FileMode.Open);
        }
        XmlDataBase.powerupDB = serializer.Deserialize(stream) as PowerUPDatabase;
        stream.Close();
    }
    public void LoadObstacleData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ObstacleDatabase));
        Stream stream;
        if (encryptedDatabase)
        {
            byte[] b = File.ReadAllBytes(Application.dataPath + "/StreamingFiles/XML/obstacle_data.dat");
            string contents = Aes_Encryptor.AesEncryptor.DecryptBytes(b);
            stream = GenerateStreamFromString(contents);
        }
        else
        {
            stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/obstacle_data.xml", FileMode.Open);
        }
        XmlDataBase.obstacleDB = serializer.Deserialize(stream) as ObstacleDatabase;
        stream.Close();
    }
    public void LoadProyectileData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ProyectileDatabase));
        Stream stream;
        if (encryptedDatabase)
        {
            byte[] b = File.ReadAllBytes(Application.dataPath + "/StreamingFiles/XML/proyectile_data.dat");
            string contents = Aes_Encryptor.AesEncryptor.DecryptBytes(b);
            stream = GenerateStreamFromString(contents);
        }
        else
        {
            stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/proyectile_data.xml", FileMode.Open);
        }
        XmlDataBase.proyectileDB = serializer.Deserialize(stream) as ProyectileDatabase;
        stream.Close();
    }
    public void LoadAlienData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AlienDatabase));
        Stream stream;
        if (encryptedDatabase)
        {
            byte[] b = File.ReadAllBytes(Application.dataPath + "/StreamingFiles/XML/alien_data.dat");
            string contents = Aes_Encryptor.AesEncryptor.DecryptBytes(b);
            stream = GenerateStreamFromString(contents);
        }
        else
        {
            stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/alien_data.xml", FileMode.Open);
        }
        XmlDataBase.alienDB = serializer.Deserialize(stream) as AlienDatabase;
        stream.Close();
    }
    public void DeleteAllData()
    {
        DeleteGameData();
        DeleteLevelData();
        DeleteScreenData();
        DeleteSoundData();
        DeleteSpriteData();
        DeletePlayerData();
        DeletePowerUPData();
        DeleteObstacleData();
        DeleteProyectileData();
        DeleteAlienData();
    }
    public void DeleteGameData()
    {
        if (encryptedDatabase)
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/game_data.xml");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/game_data.xml.meta");
        }
        else
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/game_data.dat");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/game_data.dat.meta");
        }
    }
    public void DeleteLevelData()
    {
        if (encryptedDatabase)
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/level_data.xml");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/level_data.xml.meta");
        }
        else
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/level_data.dat");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/level_data.dat.meta");
        }
    }
    public void DeleteScreenData()
    {
        if (encryptedDatabase)
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/screen_data.xml");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/screen_data.xml.meta");
        }
        else
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/screen_data.dat");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/screen_data.dat.meta");
        }
    }
    public void DeleteSoundData()
    {
        if (encryptedDatabase)
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/sound_data.xml");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/sound_data.xml.meta");
        }
        else
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/sound_data.dat");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/sound_data.dat.meta");
        }
    }
    public void DeleteSpriteData()
    {
        if (encryptedDatabase)
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/sprite_data.xml");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/sprite_data.xml.meta");
        }
        else
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/sprite_data.dat");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/sprite_data.dat.meta");
        }
    }
    public void DeletePlayerData()
    {
        if (encryptedDatabase)
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/player_data.xml");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/player_data.xml.meta");
        }
        else
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/player_data.dat");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/player_data.dat.meta");
        }
    }
    public void DeletePowerUPData()
    {
        if (encryptedDatabase)
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/powerup_data.xml");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/powerup_data.xml.meta");
        }
        else
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/powerup_data.dat");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/powerup_data.dat.meta");
        }
    }
    public void DeleteObstacleData()
    {
        if (encryptedDatabase)
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/obstacle_data.xml");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/obstacle_data.xml.meta");
        }
        else
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/obstacle_data.dat");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/obstacle_data.dat.meta");
        }
    }
    public void DeleteProyectileData()
    {
        if (encryptedDatabase)
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/proyectile_data.xml");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/proyectile_data.xml.meta");
        }
        else
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/proyectile_data.dat");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/proyectile_data.dat.meta");
        }
    }
    public void DeleteAlienData()
    {
        if (encryptedDatabase)
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/alien_data.xml");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/alien_data.xml.meta");
        }
        else
        {
            File.Delete(Application.dataPath + "/StreamingFiles/XML/alien_data.dat");
            File.Delete(Application.dataPath + "/StreamingFiles/XML/alien_data.dat.meta");
        }
    }
    public void UpdateAllData()
    {
        UpdateGameData();
        UpdateLevelData();
        UpdateScreenData();
        UpdateSoundData();
        UpdateSpriteData();
        UpdatePlayerData();
        UpdatePowerUPData();
        UpdateObstacleData();
        UpdateProyectileData();
        UpdateAlienData();
        Debug.Log("All Data Updated");
    }
    public void UpdateGameData()
    {
        DeleteGameData();
        SaveGameData();
    }
    public void UpdateLevelData()
    {
        DeleteLevelData();
        SaveLevelData();
    }
    public void UpdateScreenData()
    {
        DeleteScreenData();
        SaveScreenData();
    }
    public void UpdateSoundData()
    {
        DeleteSoundData();
        SaveSoundData();
    }
    public void UpdateSpriteData()
    {
        DeleteSpriteData();
        SaveSpriteData();
    }
    public void UpdatePlayerData()
    {
        DeletePlayerData();
        SavePlayerData();
    }
    public void UpdatePowerUPData()
    {
        DeletePowerUPData();
        SavePowerUPData();
    }
    public void UpdateObstacleData()
    {
        DeleteObstacleData();
        SaveObstacleData();
    }
    public void UpdateProyectileData()
    {
        DeleteProyectileData();
        SaveProyectileData();
    }
    public void UpdateAlienData()
    {
        DeleteAlienData();
        SaveAlienData();
    }
    public static MemoryStream GenerateStreamFromString(string value)
    {
        return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
    }
}

[System.Serializable]
public class GameData
{
    public int id;
    public int status;
    public int score;
}

[System.Serializable]
public class Screen
{
    public int id;
    public string title;
}

[System.Serializable]
public class ScreenDatabase
{
    [XmlArray("ScreenInfo")]
    public List<Screen> list = new List<Screen>();
}

[System.Serializable]
public class Level
{
    public int number;
    public int difficulty;
    public int duration;
    public List<ObstacleType> Obstacles;
}

[System.Serializable]
public class LevelDatabase
{
    [XmlArray("LevelInfo")]
    public List<Level> list = new List<Level>();
}

[System.Serializable]
public class Sound
{
    public int id;
    public string file;
    public int volume;
    public int duration;
}

[System.Serializable]
public class SoundDatabase
{
    [XmlArray("SoundInfo")]
    public List<Sound> list = new List<Sound>();
}

[System.Serializable]
public class Dimensions
{
    public int heigh { get; set; }
    public int width { get; set; }
}

[System.Serializable]
public class SpriteTx
{
    public int id;
    public string texture;
    public Dimensions dimensions { get; set; }
}

[System.Serializable]
public class SpriteDatabase
{
    [XmlArray("SpriteInfo")]
    public List<SpriteTx> list = new List<SpriteTx>();
}

public enum Effect
{
    Speed,
    Immunity,
    Strength,
    TripleShoot
}

[System.Serializable]
public class PowerUP : SpriteTx
{
    public Effect effect;
}

[System.Serializable]
public class PowerUPDatabase
{
    [XmlArray("PowerUPInfo")]
    public List<PowerUP> list = new List<PowerUP>();
}
[System.Serializable]
public class PlayerData : SpriteTx
{
    public Vector2 velocity;
    public int hp;
}

public enum ObstacleType
{
    Asteroid,
    SpaceTrash,
    Star,
    ExtraAlien,
    BlackHole,
    ShootingStar,
    Alien
}

[System.Serializable]
public class Obstacle : SpriteTx
{
    public int resistance;
    public ObstacleType type;
}

[System.Serializable]
public class ObstacleDatabase
{
    [XmlArray("ObstacleInfo")]
    public List<Obstacle> list = new List<Obstacle>();
}

[System.Serializable]
public class Proyectile : SpriteTx
{
    public Vector2 velocity;
}

[System.Serializable]
public class ProyectileDatabase
{
    [XmlArray("ProyectileInfo")]
    public List<Proyectile> list = new List<Proyectile>();
}

[System.Serializable]
public class Alien : SpriteTx
{
    public int resistance;
}

[System.Serializable]
public class AlienDatabase
{
    [XmlArray("AlienInfo")]
    public List<Alien> list = new List<Alien>();
}

namespace Aes_Encryptor
{
    public class AesEncryptor
    {
        public static byte[] EncryptString(string original)
        {
            using (Aes myAes = Aes.Create())
            {
                Debug.Log("Encrypting: " + original);
                myAes.Key = Encoding.ASCII.GetBytes("FPNais99@dsADIJ#");
                myAes.IV = Encoding.ASCII.GetBytes("1923[*dkof32+FPN");
                byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);
                return encrypted;
            }
        }
        public static string DecryptBytes(byte[] encrypted)
        {
            using (Aes myAes = Aes.Create())
            {
                myAes.Key = Encoding.ASCII.GetBytes("FPNais99@dsADIJ#");
                myAes.IV = Encoding.ASCII.GetBytes("1923[*dkof32+FPN");
                string decrypted = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);
                Debug.Log("Decrypted: " + decrypted);
                return decrypted;
            }
        }
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            
            return encrypted;
        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            
            string plaintext = null;
            
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }
    }
}