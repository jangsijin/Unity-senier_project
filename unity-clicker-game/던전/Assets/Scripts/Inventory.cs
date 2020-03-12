using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private OrderManager theOrder;
    private InventorySlot[] slots; //인벤토리 슬롯들

    private List<Item> inventoryItemList; //플레이어가 소지한 아이템 리스트
    private List<Item> inventoryTabList; //선택한 탭에 따라 다르게 보여질 아이템

    public Text Description_Text; //부연설명
    public string[] tabDescription; //탭부연설명

    public Transform tf; //슬롯 부모객체

    public GameObject go; //인벤토리 활성화 비활성화
    public GameObject[] selectedTabImages;

    private int selectedItem; //선택된 아이템             
    private int selectedTab; // 선택된 탭

    private bool activated; //  인벤토리 활성화시 트루
    private bool tabActivated; //탭 활성화시 트루
    private bool itemActivated; // 아이템 활성화시 트루
    private bool stopKeyInput; //키입력제한(소비할 때 질의가 나올텐데, 그때 키입력 방지)
    private bool preventExec; // 중복 실행 제한

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    // Use this for initialization
    void Start () {

        theOrder = FindObjectOfType<OrderManager>();
        inventoryItemList = new List<Item>();
        inventoryTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<InventorySlot>();

        inventoryList.Add(new Item(10001, "빨간 포션", "체력을 5씩 채워주는 마법의 물약", Item.ItemType.Use));
    }
	
    public void ShowTab()
    {
        selectedTab();
    }//탭활성화

    public void RemoveSlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
        Description_Text.text = tabDescription[selectedTab];
        StarCorountine(SelectedTabEffectCoroutine());
    }//인벤토리 슬롯 초기화

    public void SelectedTab()
    {
        StopAllCoroutines();
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f;
        for(int i = 0; i < selectedTabImages.Length; i++)
        {
            selectedTabImages[i].GetComponent<Image>().color = color;
        }
    }  //선택된 탭을 제외하고 다른모든 탭의 컬러 알파값 0으로조정

    IEnumerator SelectedTabEffectCoroutine()
    {
        while (tabActivated)
        {
            Color color = selectedTabImages[0].GetComponent<Image>().color;
            while(color.a < 0.5f)
            {
                color.a += 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);
        }
    } //선택된 탭 반짝임 효과

    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;

        switch (selectedTab)
        {
            case 0:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if(Item.ItemType.Use == inventoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryItemList[i]);
                    }
                }
                break;

            case 1:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Equip == inventoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryItemList[i]);
                    }
                }
                break;

            case 2:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (Item.ItemType.Quest == inventoryItemList[i].itemType)
                    {
                        inventoryTabList.Add(inventoryItemList[i]);
                    }
                }
                break;

        } //탭에따른 아이템분류 그것을 아이템 리스트에 추가

        for(int i =0; i < inventoryTabList.Count; i++)
        {
            slots[i] = gameObject.SetActive(true);
            slots[i].Additem(inventoryTabList[i]);
        } //인벤토리 탭리스트의 내용을 인벤토리 리스트의 추가
    }

    public void SelectedItem()
    {
        StopAllCoroutines();
        if(inventoryTabList.Count > 0)
        {
            Color color = slots[0].selcted_Item.GetComponent<Image>().color;
            color.a = 0f;
            for(int i = 0; i < inventoryItemList.Count; i++)
            {
                slots[i].selected_Item.GetComponent<Image>().color = color;
            }
            Description_Text.text = inventoryTabList[selectedItem].itemDescription;
            StartCoroutine(SelectedItemEffectCoroutine());
        }
        else
        {
            Description_Text.text = "해당 타입의 아이템을 소유하고 있지 않습니다.";
        }
    }

    IEnumerator SelectedItemEffectCoroutine()
    {
        while (itemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    // Update is called once per frame
    void Update () {
        if (!stopKeyInput)
        {
            if (Input.GetKeyDown(Keycode.I))
            {
                activated = !activated;
                if(activated)
                {
                    theOrder.NotMove();
                    go.SetActive(true);
                    selectedTab = 0;
                    tabActivated = true;
                    itemActivated = false;
                    ShowTab();
                }
                else
                {
                    StopAllCoroutines();
                    go.SetActive(false);
                    tabActivated = false;
                    itemActivated = false;
                    theOrder.Move();
                }
            }

            if (activated)
            {
                if (tabActivated)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if(selectedTab < selectedTabImages.Length -1)
                        {
                            selectedTab++;
                        }
                        else
                        {
                            selectedTab = 0;
                        }
                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedTab > 0)
                        {
                            selectedTab--;
                        }
                        else
                        {
                            selectedTab = selectedTabImages.Length - 1;
                        }
                        SelectedTab();
                    }
                    else if (Input.GetKeyDown(KeyCode.Z))
                    {
                        Color color = selectedTabImages[selectedTab].GetComponent<image>().color;
                        color.a = 0.25f;
                        selectedTabImages[selectedTab].GetComponent<image>().color = color;
                        itemActivated = true;
                        tabActivated = false;
                        preventExec = true;
                        ShowItem();
                    }
                } // 탭활성화 키입력 처리

                else if (itemActivated)
                { 
                    if(inventoryTabList.Count > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectedItem < inventoryTabList.Count - 2)
                                selectedItem += 2;
                            else
                                selectedItem %= 2;
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem > 1)
                                selectedItem -= 2;
                            else
                                selectedItem = inventoryTabList.Count - 1 - selectedItem;
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedItem < inventoryTabList.Cpunt - 1)
                                selectedItem++;
                            else
                                selectedItem = 0;
                            SelectedItem();

                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedItem > 0)
                                selectedItem--;
                            else
                                selectedItem = inventoryTabList.Count - 1;
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.Z) && !preventExec)
                        {
                            if (selectedTab == 0)//소모품
                            {
                                stopKeyInput = true; //물약을 마실거냐같은 선택ㅊ지 호출
                            }
                            else if (selectedTab == 1)
                            {

                            }
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        StopAllCoroutines();
                        itemActivated = false;
                        tabActivated = true;
                        ShowTab();
                    }
                }  // 아이템 활성화시 키입력 처리

                if (Input.GetKeyUp(KeyCode.Z)) // 중복실행방지
                    preventExec = false;
            }
        }
	}
}
