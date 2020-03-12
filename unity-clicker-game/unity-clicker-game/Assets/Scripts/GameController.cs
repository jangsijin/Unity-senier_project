using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Advertisements;

public class GameController : MonoBehaviour
{

    public Text TextGold;
    public Transform Tap1Content;
    public Transform Tap2Content;
    public Transform Tap5Content;

    public static GameController Instance;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        TextGold.text = DataController.Instance.gameData.Gold.ToString();
        StartCoroutine(StartCollectGold());
        DataController.Instance.GetStatList();
        DataController.Instance.GetQuestList();
        DataController.Instance.GetItemList();
       // DataController.Instance.GetDugeonList();
        InitTap();
        InitTap1();
        //InitTap2();

        //#if UNITY_ANDROID || UNITY_EDITOR
        //안드로이드 Advertisement.Initialize("게임아이디");

        //#elif UNITY_IOS
        //아이폰Advertisement.Initialize("게임 아이디");

        //#endif

        //ShowRewardedVideo();
    }

    IEnumerator StartCollectGold()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            DataController.Instance.gameData.Gold += DataController.Instance.gameData.GoldPerSec;
            TextGold.text = DataController.Instance.gameData.Gold.ToString();
            DataController.Instance.SaveGameData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DataController.Instance.gameData.Gold += DataController.Instance.gameData.GoldPerSec;
            TextGold.text = DataController.Instance.gameData.Gold.ToString();
        }
    }
    public void UpgradeCollectGold()
    {
        int cost = 0;
        int currentLevel = DataController.Instance.gameData.CollectGoldLevel;

        Stat stat = null;

        Quest quest = null;

        //Dugeon dugeon = null;

        //List<Dungeon> dugeonList = DataController.Instance.GetDugeonList().DugeonList;

        List<Quest> quesstList = DataController.Instance.GetQuestList().QuestList;

        List<Stat> statList = DataController.Instance.GetStatList().StatList;

        foreach (Stat s in statList)
        {
            if (s.Type == "CollectGold")
            {
                stat = s;
            }
        }

        if (stat.CostUpgradeAmountOp == "*")
        {
            cost = DataController.Instance.gameData.CollectGoldLevel * stat.CostUpgradeAmount;
        }
        else
        {
            cost = stat.CostUpgradeAmount;
        }

        //int Cost = DataController.Instance.gameData.CollectGoldLevel * DataController.Instance.gameData.CollectGoldLevel;

        if (DataController.Instance.gameData.Gold < cost)
        {
            return;
        }
        DataController.Instance.gameData.CollectGoldLevel += 1;
        DataController.Instance.gameData.GoldPerSec =
            DataController.Instance.gameData.CollectGoldLevel * stat.UpgradeAmount;
        DataController.Instance.gameData.Gold -= cost;

        RefreshTap();
        RefreshTap1();
    }

    public void UgradeDamage()
    {
        int cost = 0;
        int currentLevel = DataController.Instance.gameData.DamageLevel;

        Stat stat = null;

        List<Stat> statList = DataController.Instance.GetStatList().StatList;

        foreach (Stat s in statList)
        {
            if (s.Type == "StatDamage")
            {
                stat = s;
            }
        }

        if (stat.CostUpgradeAmountOp == "*")
        {
            cost = DataController.Instance.gameData.DamageLevel * stat.CostUpgradeAmount;
        }
        else
        {
            cost = stat.CostUpgradeAmount;
        }

        //int Cost = DataController.Instance.gameData.CollectGoldLevel * DataController.Instance.gameData.CollectGoldLevel;

        if (DataController.Instance.gameData.Gold < cost)
        {
            return;
        }
        DataController.Instance.gameData.DamageLevel += 1;
        DataController.Instance.gameData.Damage =
            DataController.Instance.gameData.DamageLevel * stat.UpgradeAmount;
        DataController.Instance.gameData.Gold -= cost;

        RefreshTap();
    }

    public void UgradeHealth()
    {
        int cost = 0;
        int currentLevel = DataController.Instance.gameData.HealthLevel;

        Stat stat = null;

        List<Stat> statList = DataController.Instance.GetStatList().StatList;

        foreach (Stat s in statList)
        {
            if (s.Type == "StatHealth")
            {
                stat = s;
            }
        }

        if (stat.CostUpgradeAmountOp == "*")
        {
            cost = DataController.Instance.gameData.HealthLevel * stat.CostUpgradeAmount;
        }
        else
        {
            cost = stat.CostUpgradeAmount;
        }

        //int Cost = DataController.Instance.gameData.CollectGoldLevel * DataController.Instance.gameData.CollectGoldLevel;

        if (DataController.Instance.gameData.Gold < cost)
        {
            return;
        }
        DataController.Instance.gameData.HealthLevel += 1;
        DataController.Instance.gameData.Health =
            DataController.Instance.gameData.HealthLevel * stat.UpgradeAmount;
        DataController.Instance.gameData.Gold -= cost;

        RefreshTap();
    }

    public void InitTap()
    {
        StatData statData = DataController.Instance.GetStatList();

        int i = 0;

        foreach (Stat stat in statData.StatList)
        {
            GameObject item = Resources.Load("Prefabs/Item") as GameObject;

            GameObject obj = Instantiate(item, Tap1Content);

            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, -60f + (i * -110f));


            //obj.GetComponent<RectTransform>().rect= rect;

            obj.name = stat.Type;

            obj.GetComponent<TapItem>().StatType = stat.Type;

            i++;

        }

        RefreshTap();
    }

    public void InitTap1()
    {
        QuestData questData = DataController.Instance.GetQuestList();

        int i = 0;

        foreach (Quest quest in questData.QuestList)
        {
            GameObject quest1 = Resources.Load("Prefabs/Quest") as GameObject;

            GameObject obj = Instantiate(quest1, Tap2Content);

            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 90f + (i * -110f));

            //obj.GetComponent<RectTransform>().rect= rect;

            obj.name = quest.Type;

            obj.GetComponent<TapQuest>().QuestType = quest.Type;

            i++;

        }
        RefreshTap1();

    }

    /*public void InitTap2()
    {
        DugeonData dugeonData = DataController.Instance.GetDugeonList();

        int i = 0;

        foreach (Dugeon dugeon in dugeonData.DugeonList)
        {
            GameObject dugeon1 = Resources.Load("Prefabs/Dugeon") as GameObject;

            GameObject obj = Instantiate(dugeon1, Tap5Content);

            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 90f + (i * -110f));

            //obj.GetComponent<RectTransform>().rect= rect;

            obj.name = dugeon.Type;

            obj.GetComponent<TapDugeon>().DugeonType = dugeon.Type;

            i++;

        }
        RefreshTap2();

    }*/

    public void RefreshTap()
    {
        TapItem[] items = Tap1Content.GetComponentsInChildren<TapItem>();

        foreach (TapItem item in items)
        {
            GameObject obj = item.gameObject;

            Stat stat = null;

            List<Stat> statList = DataController.Instance.GetStatList().StatList;

            foreach (Stat s in statList)
            {
                if (s.Type == obj.name)
                {
                    stat = s;
                }
            }

            if (stat == null)
            {
                continue;
            }

            Text[] texts = obj.GetComponentsInChildren<Text>();

            foreach (Text text in texts)
            {
                if (text.tag == "Description")
                {
                    int currentLevel = 0;
                    int cost = 0;

                    if (stat.Type == "CollectGold")
                    {
                        currentLevel = DataController.Instance.gameData.CollectGoldLevel;
                        if (stat.CostUpgradeAmountOp == "*")
                        {
                            cost = DataController.Instance.gameData.CollectGoldLevel * stat.CostUpgradeAmount;
                        }
                        else
                        {
                            cost = stat.CostUpgradeAmount;
                        }
                    }
                    else if (stat.Type == "StatDamage")
                    {
                        currentLevel = DataController.Instance.gameData.DamageLevel;
                        if (stat.CostUpgradeAmountOp == "*")
                        {
                            cost = DataController.Instance.gameData.DamageLevel * stat.CostUpgradeAmount;
                        }
                        else
                        {
                            cost = stat.CostUpgradeAmount;
                        }
                    }
                    else if (stat.Type == "StatHealth")
                    {
                        currentLevel = DataController.Instance.gameData.HealthLevel;
                        if (stat.CostUpgradeAmountOp == "*")
                        {
                            cost = DataController.Instance.gameData.HealthLevel * stat.CostUpgradeAmount;
                        }
                        else
                        {
                            cost = stat.CostUpgradeAmount;
                        }
                    }
                    String upgradeText = string.Format("{0}\n현재: {1} 가격 : {2} Gold",
                        stat.Name,
                        currentLevel,
                        cost
                       );
                    text.text = upgradeText;
                }
            }
        }

    }

    public void RefreshTap1()
    {
        TapQuest[] quests = Tap2Content.GetComponentsInChildren<TapQuest>();

        foreach (TapQuest quest1 in quests)
        {
            GameObject obj = quest1.gameObject;

            Quest quest = null;

            List<Quest> questList = DataController.Instance.GetQuestList().QuestList;

            foreach (Quest q in questList)
            {
                if (q.Type == obj.name)
                {
                    quest = q;
                }
            }

            if (quest == null)
            {
                continue;
            }

            Text[] texts = obj.GetComponentsInChildren<Text>();

            foreach (Text text in texts)
            {
                if (text.tag == "Description")
                {
                    int currentLevel = 0;
                    int cost = 0;

                    String upgradeText = string.Format("{0}\n현재: {1} 가격 : {2} Gold",
                        quest.Name,
                        currentLevel,
                        cost
                       );
                    text.text = upgradeText;
                }
            }
        }

    }

    /*public void RefreshTap2()
    {
        TapDugeon[] dugeons = Tap5Content.GetComponentsInChildren<TapDugeon>();

        foreach (TapDugeon dugeon1 in dugeons)
        {
            GameObject obj = dugeon1.gameObject;

            Dugeon dugeon = null;

            List<Dugeon> dugeonList = DataController.Instance.GetDugeonList().DugeonList;

            foreach (Dugeon d in dugeonList)
            {
                if (d.Type == obj.name)
                {
                    dugeon = d;
                }
            }

            if (dugeon == null)
            {
                continue;
            }

            Text[] texts = obj.GetComponentsInChildren<Text>();

            foreach (Text text in texts)
            {
                if (text.tag == "Description")
                {
                    int currentLevel = 0;
                    int cost = 0;

                    String upgradeText = string.Format("{0}\n현재: {1} 가격 : {2} Gold",
                        dugeon.Name,
                        currentLevel,
                        cost
                       );
                    text.text = upgradeText;
                }
            }
        }

    }*/

    void ShowRewardedVideo()
    {
        var options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show("rewardedVideo", options);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video-complete - offera reward to the player");
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("Video was skipped - DO NOT reward the player");
        }
        else if (result == ShowResult.Failed)
        {
            Debug.LogError("Video failed to show");
        }
    }
}
