using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[SerializeField]
public class Inventory : MonoBehaviour,ISaveManager
{
    public static Inventory Instance { get; private set; }
    public List<itemData> StartEquipmentLIst;
    public List<InventoryItem> Inventoryitem;
    public Dictionary<itemData, InventoryItem> InventoryDictionary;
    public List<InventoryItem> StashInventoryitem;
    public Dictionary<itemData, InventoryItem> StashInventoryDictionary;
    public List<InventoryItem> EquipmentItem;
    public Dictionary<ItemData_equirment, InventoryItem> equipmentDictionary;
    public Transform InventorySlotParents;
    public Transform StashInventorySlotParents;
    public Transform EquirMentInventroySlotParents;
    public Transform UISlotParents;
    public UIitemSlot[] itemSlot;
    public UIitemSlot[] StashitemSlot;
    public UI_equipmentSlot[] EquipMentSlot;
    private float lastuseTime;
    private float lastUseArmorTime;
   public float FlaskCoolDown;
    private float ArmorCoolDown;
    public UI_StatSlot[] uislot;
    //public string[] assetsNames;//NameId
    [Header("Data base")]
   public List<itemData> itemDataBase;//所有的equipmentData
    public List<InventoryItem> loadedItems;
    public List<ItemData_equirment> loadedEquipment;
    // Start is called before the first frame update
    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void Start()
    {
        Inventoryitem = new List<InventoryItem>();
        InventoryDictionary = new Dictionary<itemData, InventoryItem>();
        itemSlot = InventorySlotParents.GetComponentsInChildren<UIitemSlot>();
        StashInventoryitem = new List<InventoryItem>();
        StashInventoryDictionary = new Dictionary<itemData, InventoryItem>();
        StashitemSlot = StashInventorySlotParents.GetComponentsInChildren<UIitemSlot>();
        EquipMentSlot = EquirMentInventroySlotParents.GetComponentsInChildren<UI_equipmentSlot>();
        EquipmentItem = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_equirment, InventoryItem>();
        uislot = UISlotParents.GetComponentsInChildren<UI_StatSlot>();
        StartEquipment();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartEquipment()
    {
        foreach (ItemData_equirment item in loadedEquipment)
        {
            EquipMent(item);
        }
        if (loadedItems.Count > 0)//载入文件存在才调用
        {
            foreach (InventoryItem item in loadedItems)
            {
                for (int i = 0; i < item.StackSizes; i++)
                {
                    Additem(item.data);
                }
            }

            return;
        }
        for (int i = 0; i < StartEquipmentLIst.Count; i++)
        {
            if(StartEquipmentLIst[i]!=null)
            Additem(StartEquipmentLIst[i]);
        }
    }
    public void UpdateUi()
    {
        for (int i = 0; i < EquipMentSlot.Length; i++)
        {
            EquipMentSlot[i].CleanUpSlot();
        }
        foreach (KeyValuePair<ItemData_equirment, InventoryItem> item in equipmentDictionary)
        {
            for (int i = 0; i < EquipMentSlot.Length; i++)
            {
                if (EquipMentSlot[i].equirmenttype == item.Key.equirmenttype)
                {
                    EquipMentSlot[i].UpdateItemSlot(item.Value);
                }
            }
        }
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].CleanUpSlot();
        }
        for (int i = 0; i < StashitemSlot.Length; i++)
        {
            StashitemSlot[i].CleanUpSlot();
        }
        for (int i = 0; i < Inventoryitem.Count; i++)
        {
            itemSlot[i].UpdateItemSlot(Inventoryitem[i]);
        }
        for (int i = 0; i < StashInventoryitem.Count; i++)
        {
            StashitemSlot[i].UpdateItemSlot(StashInventoryitem[i]);
        }
        UpdateStatUI();
    }

    public void UpdateStatUI()
    {
        for (int i = 0; i < uislot.Length; i++)
        {
            uislot[i].UpdataStatValueString();
        }
    }

    public void EquipMent(itemData _item)
    {
        ItemData_equirment newequirment = _item as ItemData_equirment;
        InventoryItem newitem = new InventoryItem(_item);
        foreach (KeyValuePair<ItemData_equirment, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.equirmenttype == newequirment.equirmenttype)
            {
                UnequirItem(item.Key);
                InventoryAdd(item.Key);
                break;
            }
        }
            EquipmentItem.Add(newitem);
            equipmentDictionary.Add(newequirment, newitem);
            newequirment.AddModify();
            RemoveItem(_item);

    }

   public void UnequirItem(ItemData_equirment item)
    {
        if (equipmentDictionary.TryGetValue(item, out InventoryItem value))
        {
            EquipmentItem.Remove(value);
            equipmentDictionary.Remove(item);
            item.RemoveModify();
        }
    }

    public void  Additem(itemData _item)
    {
        if (_item.itemtype == itemType.Material)
        {
            StashAdd(_item);

        }
        if (_item.itemtype == itemType.Equipment)
        {
            InventoryAdd(_item);
        }
        UpdateUi();
    }

    private void StashAdd(itemData _item)
    {   
            if (StashInventoryDictionary.TryGetValue(_item, out InventoryItem stash))
            {
                stash.AddStack();
            }
            else
            {
            InventoryItem newitem = new InventoryItem(_item);
                StashInventoryitem.Add(newitem);
                StashInventoryDictionary.Add(_item, newitem);        
            }
        
    }

    private void InventoryAdd(itemData _item)
    {
        if (InventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            if (canAdditem())
            {
                InventoryItem newItem = new InventoryItem(_item);
                Inventoryitem.Add(newItem);
                InventoryDictionary.Add(_item, newItem);
            }
        }
    }

    public void RemoveItem(itemData _item)
    {
        if (InventoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if(value.StackSizes<=1)
            {
                Inventoryitem.Remove(value);
                InventoryDictionary.Remove(_item);
            }
            else
            {
                value.RemoveStack();
            }
                
        }
        if (StashInventoryDictionary.TryGetValue(_item, out InventoryItem stash))
        {
            if (stash.StackSizes <= 1)
            {
                StashInventoryitem.Remove(stash);
                StashInventoryDictionary.Remove(_item);
            }
            else
            {
                stash.RemoveStack();
            }

        }
        UpdateUi();
    }
    public List<InventoryItem> GetEquirments => EquipmentItem;
    public ItemData_equirment GetEquipment(equirmentType type)
    {
        ItemData_equirment equirment = null;
        foreach (KeyValuePair<ItemData_equirment, InventoryItem> item in equipmentDictionary)
        {
            if(item.Key.equirmenttype==type)
            {
                equirment = item.Key;
            }
        }
        return equirment;
    }
    public void UseFlask()
    {
        ItemData_equirment equirment = GetEquipment(equirmentType.Flask);
           if(equirment == null)
            return;
        bool Canuse = Time.time > lastuseTime + FlaskCoolDown;
        if(Canuse)
        {
            FlaskCoolDown = equirment.cooldown;
            equirment.ExecuteitemEffect(null);
            lastuseTime = Time.time;
        }
        else
        {
            PlayerManager.instance.player.fX.creatText("cooldown");
            return;
        }
    }
    public void UseArmor()
    {
        ItemData_equirment equirment = GetEquipment(equirmentType.armor);
        if (equirment == null)
            return;
        bool Canuse = Time.time > lastUseArmorTime +ArmorCoolDown;
        if (Canuse)
        {
            ArmorCoolDown = equirment.cooldown;
            equirment.ExecuteitemEffect(PlayerManager.instance.player.transform);
           lastUseArmorTime = Time.time;
        }
        else
        {
            return;
        }
    }
    public bool canAdditem()
    {
        if (Inventoryitem.Count >=itemSlot.Length)
        {
            return false;
        }
        else
            return true;
    }
    public bool CanCraft(ItemData_equirment _itemToCraft, List<InventoryItem> _requiredMaterials)
    {
        List<InventoryItem> materialsToRemove = new List<InventoryItem>();

        for (int i = 0; i < _requiredMaterials.Count; i++)
        {
            if (StashInventoryDictionary.TryGetValue(_requiredMaterials[i].data, out InventoryItem stashValue))//判断数量是否足够
            {
                if (stashValue.StackSizes < _requiredMaterials[i].StackSizes)
                {
                    Debug.Log("not enough materials");
                    return false;
                }
                else
                {
                    materialsToRemove.Add(stashValue);
                }

            }
            else
            {
                Debug.Log("not enough materials");
                return false;
            }
        }


        for (int i = 0; i < materialsToRemove.Count; i++)
        {
            RemoveItem(materialsToRemove[i].data);
        }

        Additem(_itemToCraft);
        Debug.Log("Here is your item " + _itemToCraft.name);

        return true;
    }
    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, int> pair in _data.inventory)//比较文件里保存data和所有data的，相同就保存在loadedItems里
        {
            foreach (var item in itemDataBase)
            {
                if (item != null && item.itemId == pair.Key)
                {
                    InventoryItem itemToLoad = new InventoryItem(item);
                    itemToLoad.StackSizes = pair.Value;//因为要保存的是两个变量故采用此方法，这也是InventoryItem存在的原因，需要保存数量

                    loadedItems.Add(itemToLoad);
                }
            }
        }

        foreach (string loadedItemId in _data.equipmentId)
        {
            foreach (var item in itemDataBase)
            {
                if (item != null && item.itemId == loadedItemId)
                {
                    loadedEquipment.Add(item as ItemData_equirment);
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.equipmentId.Clear();

        foreach (KeyValuePair<itemData, InventoryItem> pair in InventoryDictionary)//遍历存储
        {
            _data.inventory.Add(pair.Key.itemId, pair.Value.StackSizes);
        }

        foreach (KeyValuePair<itemData, InventoryItem> pair in StashInventoryDictionary)
        {
            _data.inventory.Add(pair.Key.itemId, pair.Value.StackSizes);
        }

        foreach (KeyValuePair<ItemData_equirment, InventoryItem> pair in equipmentDictionary)
        {
            _data.equipmentId.Add(pair.Key.itemId);
        }
    }
    #if UNITY_EDITOR
    [ContextMenu("fill up itemdata")]
    public void FillupItemdatabase() => itemDataBase = new List<itemData>(GetItemDataBase());
    private List<itemData> GetItemDataBase()//获得所有的equipmentData的IdName和data的函数
    {
       List<itemData> itemDataBase = new List<itemData>();
        string[] assetsNames = AssetDatabase.FindAssets("", new[] { "Assets/itemData/Item" });//这是根据unity的文件夹走的，也就是拿到了所有的Equipment的文件名IdName
        foreach (string SOName in assetsNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);//这是通过找到的文件名拿到对应的位置
            var itemData = AssetDatabase.LoadAssetAtPath<itemData>(SOpath);//这是实打实的通过位置转换拿到相应的数据
            if(itemData!=null)
            itemDataBase.Add(itemData);//将数据填到itemDataBase里
        }

        return itemDataBase;
    }
#endif
}


