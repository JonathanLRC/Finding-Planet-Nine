using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

public class DataBase : MonoBehaviour
{
    public static DataBase ins;
    void Awake()
    {
        ins = this;
        XmlDataBase = new XMLDB();
    }

    public class XMLDB
    {
        public GameData gameDB;
        public LevelDatabase levelDB;
        public ScreenDatabase screenDB;
        public SoundDatabase soundDB;
        public SpriteDatabase spriteDB;
        public PlayerData playerDB;
        public PowerUPDatabase powerupDB;
        public ObstacleDatabase obstacleDB;
        public ProyectileDatabase proyectileDB;
        public AlienDatabase alienDB;
    }

    XMLDB XmlDataBase;

    public void SaveAll()
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
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/game_data.xml", FileMode.Create);
        serializer.Serialize(stream, XmlDataBase.gameDB);
        stream.Close();
    }
    public void SaveLevelData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(LevelDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/level_data.xml", FileMode.Create);
        serializer.Serialize(stream, XmlDataBase.levelDB);
        stream.Close();
    }
    public void SaveScreenData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ScreenDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/screen_data.xml", FileMode.Create);
        serializer.Serialize(stream, XmlDataBase.screenDB);
        stream.Close();
    }
    public void SaveSoundData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SoundDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/sound_data.xml", FileMode.Create);
        serializer.Serialize(stream, XmlDataBase.soundDB);
        stream.Close();
    }
    public void SaveSpriteData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SpriteDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/sprite_data.xml", FileMode.Create);
        serializer.Serialize(stream, XmlDataBase.spriteDB);
        stream.Close();
    }
    public void SavePlayerData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/player_data.xml", FileMode.Create);
        serializer.Serialize(stream, XmlDataBase.playerDB);
        stream.Close();
    }
    public void SavePowerUPData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PowerUPDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/powerup_data.xml", FileMode.Create);
        serializer.Serialize(stream, XmlDataBase.powerupDB);
        stream.Close();
    }
    public void SaveObstacleData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ObstacleDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/obstacle_data.xml", FileMode.Create);
        serializer.Serialize(stream, XmlDataBase.obstacleDB);
        stream.Close();
    }
    public void SaveProyectileData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ProyectileDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/proyectile_data.xml", FileMode.Create);
        serializer.Serialize(stream, XmlDataBase.proyectileDB);
        stream.Close();
    }
    public void SaveAlienData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AlienDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/alien_data.xml", FileMode.Create);
        serializer.Serialize(stream, XmlDataBase.alienDB);
        stream.Close();
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
    }
    public void LoadGameData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/game_data.xml", FileMode.Open);
        XmlDataBase.gameDB = serializer.Deserialize(stream) as GameData;
        stream.Close();
    }
    public void LoadLevelData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(LevelDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/level_data.xml", FileMode.Open);
        XmlDataBase.levelDB = serializer.Deserialize(stream) as LevelDatabase;
        stream.Close();
    }
    public void LoadScreenData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ScreenDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/screen_data.xml", FileMode.Open);
        XmlDataBase.screenDB = serializer.Deserialize(stream) as ScreenDatabase;
        stream.Close();
    }
    public void LoadSoundData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SoundDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/sound_data.xml", FileMode.Open);
        XmlDataBase.soundDB = serializer.Deserialize(stream) as SoundDatabase;
        stream.Close();
    }
    public void LoadSpriteData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SpriteDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/sprite_data.xml", FileMode.Open);
        XmlDataBase.spriteDB = serializer.Deserialize(stream) as SpriteDatabase;
        stream.Close();
    }
    public void LoadPlayerData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/player_data.xml", FileMode.Open);
        XmlDataBase.playerDB = serializer.Deserialize(stream) as PlayerData;
        stream.Close();
    }
    public void LoadPowerUPData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(PowerUPDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/powerup_data.xml", FileMode.Open);
        XmlDataBase.powerupDB = serializer.Deserialize(stream) as PowerUPDatabase;
        stream.Close();
    }
    public void LoadObstacleData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ObstacleDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/obstacle_data.xml", FileMode.Open);
        XmlDataBase.obstacleDB = serializer.Deserialize(stream) as ObstacleDatabase;
        stream.Close();
    }
    public void LoadProyectileData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ProyectileDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/proyectile_data.xml", FileMode.Open);
        XmlDataBase.proyectileDB = serializer.Deserialize(stream) as ProyectileDatabase;
        stream.Close();
    }
    public void LoadAlienData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(AlienDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingFiles/XML/alien_data.xml", FileMode.Open);
        XmlDataBase.alienDB = serializer.Deserialize(stream) as AlienDatabase;
        stream.Close();
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

public class Dimensions
{
    public int heigh;
    public int width;
}

[System.Serializable]
public class Sprite
{
    public int id;
    public string texture;
    public Dimensions dimensions;
}

[System.Serializable]
public class SpriteDatabase
{
    [XmlArray("SpriteInfo")]
    public List<Sprite> list = new List<Sprite>();
}

public enum Effect
{
    Speed,
    Immunity,
    Strenght
}

[System.Serializable]
public class PowerUP : Sprite
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
public class PlayerData : Sprite
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
public class Obstacle : Sprite
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
public class Proyectile : Sprite
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
public class Alien : Sprite
{
    public int resistance;
}

[System.Serializable]
public class AlienDatabase
{
    [XmlArray("AlienInfo")]
    public List<Alien> list = new List<Alien>();
}
