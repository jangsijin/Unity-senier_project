using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class DataController : MonoBehaviour
{

    //Singletoon class start!!
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }



    public int TimeAfterLastPlay
    {
        get
        {
            DateTime currentTime = DateTime.Now;
            DateTime lastPlayDate = GetLastPlayDate();

            return (int)currentTime.Subtract(lastPlayDate).TotalSeconds;
        }
    }


    void Start()
    {
        InvokeRepeating("UpdateLastPlayDate", 0f, 5f);
    }

    public string gameDataProjectFilePath = "game.json";

    //SingleTon class end

    public StatData statData;
    public ItemData itemData;
    public QuestData questData;
    //public DugeonData dugeonData;

    GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
            }
            return _gameData;
        }
    }



    public StatData GetStatList()
    {
        if (statData == null)
        {
            LoadStatData();
        }
        return statData;
    }
    public void LoadStatData()
    {
        TextAsset statJson = Resources.Load("MetaData/Stat") as TextAsset;
        Debug.Log(statJson.text);
        statData = JsonUtility.FromJson<StatData>(statJson.text);

        foreach (Stat stat in statData.StatList)
        {
            Debug.Log(stat.Name);
        }
    }
    public QuestData GetQuestList()
    {
        if (questData == null)
        {
            LoadQuestData();
        }
        return questData;
    }
    public void LoadQuestData()
    {
        TextAsset questJson = Resources.Load("MetaData/Quest") as TextAsset;
        Debug.Log(questJson.text);
        questData = JsonUtility.FromJson<QuestData>(questJson.text);

        foreach (Quest quest in questData.QuestList)
        {
            Debug.Log(quest.Name);
        }
    }

   /*public DugeonData GetDugeonList()
    {
        if (dugeonData == null)
        {
            LoadDugeonData();
        }
        return dugeonData;
    }
    public void LoadDugeonData()
    {
        TextAsset dugeonJson = Resources.Load("MetaData/Dugeon") as TextAsset;
        Debug.Log(dugeonJson.text);
        dugeonData = JsonUtility.FromJson<DugeonData>(dugeonJson.text);

        foreach (Dugeon dugeon in dugeonData.DugeonList)
        {
            Debug.Log(dugeon.Name);
        }
    }*/

    public ItemData GetItemList()
    {
        if (itemData == null)
        {
            LoadItemData();
        }
        return itemData;
    }
    public void LoadItemData()
    {
        TextAsset itemJson = Resources.Load("MetaData/Item") as TextAsset;
        Debug.Log(itemJson.text);
        itemData = JsonUtility.FromJson<ItemData>(itemJson.text);

        foreach (Item item in itemData.ItemList)
        {
            Debug.Log(item.Name);
        }
    }
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + gameDataProjectFilePath;


        if (System.IO.File.Exists(filePath))
        {
            Debug.Log("loaded");
            string dataAsJson = System.IO.File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            Debug.Log("Create new");

            _gameData = new GameData();
            _gameData.CollectGoldLevel = 1;
            _gameData.GoldPerSec = 1;
            _gameData.Gold = 0;
            _gameData.Health = 100;
            _gameData.Damage = 20;
            _gameData.Level = 1;
            _gameData.Exp = 0;
        }
    }
    public void SaveGameData()
    {

        string dataAsJson = JsonUtility.ToJson(gameData);

        string filePath = Application.persistentDataPath + gameDataProjectFilePath;
        System.IO.File.WriteAllText(filePath, dataAsJson);
    }

    DateTime GetLastPlayDate()
    {
        if (!PlayerPrefs.HasKey("Time"))
        {
            return DateTime.Now;
        }
        string timeBinaryInString = PlayerPrefs.GetString("Time");
        long timeBinaryInLong = Convert.ToInt64(timeBinaryInString);

        return DateTime.FromBinary(timeBinaryInLong);
    }

    void UpdateLastPlayDate()
    {
        _gameData.Gold += _gameData.GoldPerSec * TimeAfterLastPlay;
        PlayerPrefs.SetString("Time", DateTime.Now.ToBinary().ToString());
    }

    void OnApplicationQuit()
    {
        UpdateLastPlayDate();
    }
}
