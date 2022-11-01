using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public class SAVE_manager : MonoBehaviour
{
    public static SAVE_manager Instance {get; set;}
    public Items Items;
    int idgen;

    void Start()
    {
        Instance = this;
        LoadData();
    }

    public void AddItem(GameObject obj)
    {
        Item item = new Item();
        item.ItemID = Items.Objects.Count + idgen + ToString();
        idgen++;
        obj.GetComponent<ObjInfo>().SaveID = item.ItemID;
        item.ItemPosition = obj.transform.position;
        item.ItemRotation = obj.transform.rotation;
        Items.Objects.Add(item);
    }

    public void RemoveItem(string itemID)
    {
        Item item = Items.Objects.Where(p => p.ItemID == itemID).First();
        Items.Objects.Remove(item);
    }

    public void SaveData()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Items));
        FileStream stream = new FileStream(Application.dataPath + "/PebblesSaves/Save_1.xml", FileMode.Create);
        xmlSerializer.Serialize(stream, Items);
        stream.Close();
    }

    public void LoadData()
    {
        if(!File.Exists(Application.dataPath + "/PebblesSaves/Save_1.xml")) return;
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Items));
        FileStream stream = new FileStream(Application.dataPath + "/PebblesSaves/Save_1.xml", FileMode.Open);
        Items = xmlSerializer.Deserialize(stream) as Items;
        stream.Close();

        /*foreach(Item item in Items.Objects)
        {
            GameObject go = Instantiate(item.ItemObj, item.ItemPosition, item.ItemRotation);
        }*/
    }

}

[System.Serializable]
public class Items
{
    public List<Item> Objects = new List<Item>();
}

[System.Serializable]
public class Item
{
    public string ItemID;
    public Vector3 ItemPosition;
    public Quaternion ItemRotation;
}
