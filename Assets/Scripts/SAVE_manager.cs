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
    public ItemObjects ItemObjects;
    public AssignmentControlLoader AssignmentControlLoader;

    public PItem PItem;
    public PItemObjects PItemObjects;

    int idgen;

    public GameObject[] prefabLoaders;
    public string[] prefabTypes;

    public GameObject[] vpLoaders;
    public string[] vpTypes;

    public GameObject help;

    void Awake()
    {
        Instance = this;
        if(StateController.load){
            LoadData();
            help.SetActive(false);
        }
    }

    public void AddBlock(GameObject block){
        PItem pitem = new PItem();
        pitem.PItemID = Items.Objects.Count + idgen + ToString();
        pitem.PType =  block.GetComponent<VP_drag>().BlockType;
        block.GetComponent<VP_drag>().vpID = pitem.PItemID;
        idgen++;
        Items.Blocks.Add(pitem);

        PItemObj pitemobj = new PItemObj();
        pitemobj.PItemID = pitem.PItemID;
        pitemobj.PBlock = block;
        PItemObjects.BlockObj.Add(pitemobj);
    }

    public void AddItem(GameObject obj)
    {
        Item item = new Item();
        item.ItemID = Items.Objects.Count + idgen + ToString();
        idgen++;
        obj.GetComponent<ObjInfo>().SaveID = item.ItemID;
        item.ObjType = obj.GetComponent<ObjInfo>().objType;
        Items.Objects.Add(item);

        ItemObj itemobj = new ItemObj();
        itemobj.ItemID = item.ItemID;
        itemobj.buildPart = obj;
        ItemObjects.GObjects.Add(itemobj);
        
    }

    public void SetDay(bool day){
        Items.Day = day;
    }

    public void SetTransformsItems(){
        Items.spawnPos = GameObject.Find("SpawnPoint").transform.localPosition;
        Items.CameraPos = Camera.main.transform.localPosition;
        Items.CameraRot = Camera.main.transform.parent.rotation;
        Items.CameraZoom = Camera.main.transform.parent.position;
        for(int x=0; x < ItemObjects.GObjects.Count; x++){
            Items.Objects[x].ItemPosition = ItemObjects.GObjects[x].buildPart.transform.position;
            Items.Objects[x].ItemRotation = ItemObjects.GObjects[x].buildPart.transform.rotation;
            Items.Objects[x].col = ItemObjects.GObjects[x].buildPart.GetComponent<ObjInfo>().col;
            Items.Objects[x].transparent = ItemObjects.GObjects[x].buildPart.GetComponent<ObjInfo>().transparent;
            Items.Objects[x].scale = ItemObjects.GObjects[x].buildPart.GetComponent<ObjInfo>().scale;
            Items.Objects[x].ItemScale = ItemObjects.GObjects[x].buildPart.transform.localScale;

            Items.Objects[x].IsMerged = ItemObjects.GObjects[x].buildPart.GetComponent<ObjInfo>().isMerged;

            Items.Objects[x].connected = ItemObjects.GObjects[x].buildPart.GetComponent<ObjInfo>().connected;
            Items.Objects[x].connectionID = ItemObjects.GObjects[x].buildPart.GetComponent<ObjInfo>().connectionID;

            Items.Objects[x].assignment = ItemObjects.GObjects[x].buildPart.GetComponent<ObjInfo>().assignment;
            Items.Objects[x].let_in = ItemObjects.GObjects[x].buildPart.GetComponent<ObjInfo>().let_in;
            Items.Objects[x].let_out = ItemObjects.GObjects[x].buildPart.GetComponent<ObjInfo>().let_out;

            Items.Objects[x].ParentID = ItemObjects.GObjects[x].buildPart.GetComponent<ObjInfo>().ParentID;
            ItemObjects.GObjects[x].ParentID = Items.Objects[x].ParentID;

            
        }
        for (int v=0; v < PItemObjects.BlockObj.Count; v++){
            Items.Blocks[v].PItemPos = PItemObjects.BlockObj[v].PBlock.GetComponent<RectTransform>().localPosition;
            Items.Blocks[v].PItemScale = PItemObjects.BlockObj[v].PBlock.GetComponent<RectTransform>().localScale;

            Items.Blocks[v].Sin = PItemObjects.BlockObj[v].PBlock.GetComponent<VP_drag>().Sin;

            Items.Blocks[v].PITemParentID = PItemObjects.BlockObj[v].PBlock.GetComponent<VP_drag>().BParentID;
            PItemObjects.BlockObj[v].PITemParentID = Items.Blocks[v].PITemParentID;

            if(Items.Blocks[v].Sin == "Sloop"){
                LoopSize lp = new LoopSize();
                lp.ConnID = Items.Blocks[v].PITemParentID;
                lp.TotWidth = PItemObjects.BlockObj[v].PBlock.transform.parent.GetComponent<VP_loopSize>().totWidth;
                lp.Total = PItemObjects.BlockObj[v].PBlock.transform.parent.GetComponent<VP_loopSize>().total;
                lp.TotChildren = PItemObjects.BlockObj[v].PBlock.transform.parent.GetComponent<VP_loopSize>().totchildren;
                lp.Children = PItemObjects.BlockObj[v].PBlock.transform.parent.GetComponent<VP_loopSize>().children;
                Items.Blocks[v].sizes.Add(lp);

            }

            if(Items.Blocks[v].PType == "Rot"){
                Rot r = new Rot();
                r.Num_Rotations = PItemObjects.BlockObj[v].PBlock.GetComponent<RotScript>().num_rotations;
                r.Num_Speed = PItemObjects.BlockObj[v].PBlock.GetComponent<RotScript>().num_speed;
                r.LetterVal = PItemObjects.BlockObj[v].PBlock.GetComponent<RotScript>().letter.value;
                Items.Blocks[v].rots.Add(r);
            }

            if(Items.Blocks[v].PType == "Rot2"){
                Rot2 r2 = new Rot2();
                r2.Num_Rotations = PItemObjects.BlockObj[v].PBlock.GetComponent<RotScript2>().num_rotations;
                r2.Num_Speed = PItemObjects.BlockObj[v].PBlock.GetComponent<RotScript2>().num_speed;
                r2.LetterVal = PItemObjects.BlockObj[v].PBlock.GetComponent<RotScript2>().letter.value;
                r2.Num_Rotations2 = PItemObjects.BlockObj[v].PBlock.GetComponent<RotScript2>().num_rotations2;
                r2.Num_Speed2 = PItemObjects.BlockObj[v].PBlock.GetComponent<RotScript2>().num_speed2;
                r2.LetterVal2 = PItemObjects.BlockObj[v].PBlock.GetComponent<RotScript2>().letter2.value;
                Items.Blocks[v].rots2.Add(r2);
            }
            else if(Items.Blocks[v].PType == "Loop"){
                Items.Blocks[v].Iterations = PItemObjects.BlockObj[v].PBlock.GetComponent<LopScript>().iterations;
            }
        }

        Items.motorID = AssignmentControlLoader.motorID;
        Items.sensorID = AssignmentControlLoader.sensorID;
        Items.inTakes = AssignmentControlLoader.inTakes;
        Items.outTakes = AssignmentControlLoader.outTakes;
        
    }

    public void SetAssignments(bool mot, int indes, GameObject tar, int take){
        if(mot){
            if(tar != null)
                AssignmentControlLoader.motorID[indes] = tar.GetComponent<ObjInfo>().SaveID;
            else
             AssignmentControlLoader.motorID[indes] = null;

            AssignmentControlLoader.outTakes[indes] = take;
        }else{
            if(tar != null)
                AssignmentControlLoader.sensorID[indes] = tar.GetComponent<ObjInfo>().SaveID;
            else
             AssignmentControlLoader.sensorID[indes] = null;

            AssignmentControlLoader.inTakes[indes] = take;
        }
    }

    public void SetAssignmentsCount(bool mot, int count){
        if(mot){
            AssignmentControlLoader.motCount = count;
            Items.motCount = count;
        }else{
            AssignmentControlLoader.sensCount = count;
            Items.sensCount = count;
        }
    }

    public void RemoveItem(string itemID)
    {
    
        Item item = Items.Objects.Where(p => p.ItemID == itemID).First();
        Items.Objects.Remove(item);
        
        if(item.ParentID != null){
            Item itemp = Items.Objects.Where(p => p.ParentID == itemID).First();
            Items.Objects.Remove(itemp);
        }

        ItemObj itemobj = ItemObjects.GObjects.Where(q => q.ItemID == itemID).First();
        ItemObjects.GObjects.Remove(itemobj);
    }

    public void RemoveBlock(string itemID)
    {
        PItem block = Items.Blocks.Where(p => p.PItemID == itemID).First();
        Items.Blocks.Remove(block);

        PItemObj itemobj =  PItemObjects.BlockObj.Where(q => q.PItemID == itemID).First();
        PItemObjects.BlockObj.Remove(itemobj);
    }

    public void SaveData()
    {
        SetTransformsItems();
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Items));
        FileStream stream = new FileStream(Application.persistentDataPath + "Save_1.xml", FileMode.Create);
        xmlSerializer.Serialize(stream, Items);
        stream.Close();
    }

    public void LoadData()
    {
        if(!File.Exists(Application.persistentDataPath + "Save_1.xml")) return;
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Items));
        FileStream stream = new FileStream(Application.persistentDataPath + "Save_1.xml", FileMode.Open);
        Items = xmlSerializer.Deserialize(stream) as Items;
        stream.Close();

        idgen = Items.Objects.Count;
        GameObject.Find("SpawnPoint").transform.localPosition = Items.spawnPos;
        Camera.main.transform.localPosition = Items.CameraPos;
        Camera.main.transform.parent.rotation = Items.CameraRot;
        Camera.main.transform.parent.position = Items.CameraZoom;
        if(Items.Day){
            GameObject.Find("Light").GetComponent<DaylightControl>().ChangeDay();
        }

        foreach(Item item in Items.Objects)
        {
            GameObject spawnP = null;
            for(int i=0; i < prefabLoaders.Length; i ++){
                if(prefabTypes[i] == item.ObjType){
                    spawnP = prefabLoaders[i];
                }
            }
            GameObject go = Instantiate(spawnP, item.ItemPosition, item.ItemRotation);
            go.GetComponent<ObjInfo>().SaveID = item.ItemID;
            go.GetComponent<ObjInfo>().col = item.col;
            go.GetComponent<ObjInfo>().transparent = item.transparent;
            go.GetComponent<ObjInfo>().scale = item.scale;
            go.transform.localScale = item.ItemScale;

            go.GetComponent<ObjInfo>().isMerged = item.IsMerged;

            go.GetComponent<ObjInfo>().connected = item.connected;
            go.GetComponent<ObjInfo>().connectionID = item.connectionID;

            go.GetComponent<ObjInfo>().assignment = item.assignment;
            go.GetComponent<ObjInfo>().let_in = item.let_in;
            go.GetComponent<ObjInfo>().let_out = item.let_out;

            go.GetComponent<ObjInfo>().ParentID = item.ParentID;


            ItemObj itemobj = new ItemObj();
            itemobj.ItemID = item.ItemID;
            itemobj.ParentID = item.ParentID;
            itemobj.buildPart = go;
            ItemObjects.GObjects.Add(itemobj);
        }

        foreach (PItem pitem in Items.Blocks){
            GameObject spawnB = null;
            for(int i=0; i < vpLoaders.Length; i ++){
                if(vpTypes[i] == pitem.PType){
                    spawnB = vpLoaders[i];
                }
            }
            GameObject vp = Instantiate(spawnB);
            vp.transform.SetParent(GameObject.Find("Panel").transform);
            vp.GetComponent<RectTransform>().localPosition = pitem.PItemPos;
            vp.GetComponent<RectTransform>().localScale = pitem.PItemScale;
            vp.GetComponent<VP_drag>().vpID = pitem.PItemID;
            vp.GetComponent<VP_drag>().Sin = pitem.Sin;
            
            PItemObj pitemobj = new PItemObj();
            pitemobj.PItemID = pitem.PItemID;
            pitemobj.PBlock = vp;
            pitemobj.PITemParentID = pitem.PITemParentID;
            PItemObjects.BlockObj.Add(pitemobj);

        }

       FindParents();
       BlockParent();

        AssignmentControlLoader.motCount = Items.motCount;
        AssignmentControlLoader.sensCount = Items.sensCount;
        AssignmentControlLoader.motorID = Items.motorID;
        AssignmentControlLoader.sensorID = Items.sensorID;
        AssignmentControlLoader.inTakes = Items.inTakes;
        AssignmentControlLoader.outTakes = Items.outTakes;

        GameObject.Find("Inspector").GetComponent<AssignmentControl>().motorCount = AssignmentControlLoader.motCount;
        GameObject.Find("Inspector").GetComponent<AssignmentControl>().sensorCount = AssignmentControlLoader.sensCount;
        GameObject.Find("Inspector").GetComponent<AssignmentControl>().inTake = AssignmentControlLoader.inTakes;
        GameObject.Find("Inspector").GetComponent<AssignmentControl>().outTake = AssignmentControlLoader.outTakes;

        //GameObject.Find("Inspector").GetComponent<AssignmentControl>().motorID = AssignmentControlLoader.motorID;
        for(int m = 0; m < AssignmentControlLoader.motorID.Length; m++){
            if(AssignmentControlLoader.motorID[m].Contains("SAVE")){
                int pindx = 0;
                for(int k = 0; k < ItemObjects.GObjects.Count; k++){
                    if(ItemObjects.GObjects[k].ItemID == AssignmentControlLoader.motorID[m]){
                        pindx = k;
                    }
                }
                GameObject.Find("Inspector").GetComponent<AssignmentControl>().motors[m] = ItemObjects.GObjects[pindx].buildPart;
            }
        }

        //GameObject.Find("Inspector").GetComponent<AssignmentControl>().sensorID = AssignmentControlLoader.sensorID;
        for(int m = 0; m < AssignmentControlLoader.sensorID.Length; m++){
            if(AssignmentControlLoader.sensorID[m].Contains("SAVE")){
                int pindx = 0;
                for(int k = 0; k < ItemObjects.GObjects.Count; k++){
                    if(ItemObjects.GObjects[k].ItemID == AssignmentControlLoader.sensorID[m]){
                        pindx = k;
                    }
                }
                GameObject.Find("Inspector").GetComponent<AssignmentControl>().sensors[m] = ItemObjects.GObjects[pindx].buildPart;
            }
        }

        ChangeLoopSize();
        SetComponents();

    }

    void SetComponents(){
        for(int i = 0; i < Items.Blocks.Count; i++){

            if(Items.Blocks[i].PType == "Rot2"){
                PItemObjects.BlockObj[i].PBlock.GetComponent<RotScript2>().num_rotations = Items.Blocks[i].rots2[0].Num_Rotations;
                PItemObjects.BlockObj[i].PBlock.GetComponent<RotScript2>().num_speed = Items.Blocks[i].rots2[0].Num_Speed;
                PItemObjects.BlockObj[i].PBlock.GetComponent<RotScript2>().dval = Items.Blocks[i].rots2[0].LetterVal;

                PItemObjects.BlockObj[i].PBlock.GetComponent<RotScript2>().num_rotations2 = Items.Blocks[i].rots2[0].Num_Rotations2;
                PItemObjects.BlockObj[i].PBlock.GetComponent<RotScript2>().num_speed2 = Items.Blocks[i].rots2[0].Num_Speed2;
                PItemObjects.BlockObj[i].PBlock.GetComponent<RotScript2>().dval2 = Items.Blocks[i].rots2[0].LetterVal2;

                PItemObjects.BlockObj[i].PBlock.GetComponent<RotScript2>().SetText2();
            }
            else if(Items.Blocks[i].PType == "Rot"){
                PItemObjects.BlockObj[i].PBlock.GetComponent<RotScript>().num_rotations = Items.Blocks[i].rots[0].Num_Rotations;
                PItemObjects.BlockObj[i].PBlock.GetComponent<RotScript>().num_speed = Items.Blocks[i].rots[0].Num_Speed;
                PItemObjects.BlockObj[i].PBlock.GetComponent<RotScript>().dval = Items.Blocks[i].rots[0].LetterVal;
                PItemObjects.BlockObj[i].PBlock.GetComponent<RotScript>().SetText();
            }
            else if(Items.Blocks[i].PType == "Loop"){
                PItemObjects.BlockObj[i].PBlock.GetComponent<LopScript>().iterations = Items.Blocks[i].Iterations;
                PItemObjects.BlockObj[i].PBlock.GetComponent<LopScript>().SetIter();
            }
        

            
        }
    }

    void ChangeLoopSize(){
        for (int v=0; v < PItemObjects.BlockObj.Count; v++){
            if(Items.Blocks[v].Sin == "Sloop"){
                PItemObjects.BlockObj[v].PBlock.transform.parent.GetComponent<VP_loopSize>().totWidth = Items.Blocks[v].sizes[0].TotWidth;
                PItemObjects.BlockObj[v].PBlock.transform.parent.GetComponent<VP_loopSize>().total = Items.Blocks[v].sizes[0].Total;
                PItemObjects.BlockObj[v].PBlock.transform.parent.GetComponent<VP_loopSize>().totchildren = Items.Blocks[v].sizes[0].TotChildren;
                PItemObjects.BlockObj[v].PBlock.transform.parent.GetComponent<VP_loopSize>().children = Items.Blocks[v].sizes[0].Children;
                PItemObjects.BlockObj[v].PBlock.transform.parent.GetComponent<VP_loopSize>().grow();

                PItemObjects.BlockObj[v].PBlock.GetComponentInChildren<VP_shadow>().inSide = true;
                PItemObjects.BlockObj[v].PBlock.GetComponentInChildren<VP_shadow>().loopParent = PItemObjects.BlockObj[v].PBlock.transform.parent.parent.gameObject;
                PItemObjects.BlockObj[v].PBlock.GetComponentInChildren<VP_shadow>().isLoopParent = true;
            }
        }
    }
    void BlockParent(){
        for(int i= 0; i < PItemObjects.BlockObj.Count; i++){
            if(PItemObjects.BlockObj[i].PITemParentID.Contains("SAVE")){
                if(PItemObjects.BlockObj[i].PITemParentID == "SAVE_0"){
                    PItemObjects.BlockObj[i].PBlock.transform.SetParent(GameObject.Find("Start").transform);
                    GameObject.Find("Start").GetComponentInChildren<VP_shadow>().occupied = PItemObjects.BlockObj[i].PBlock;
                    GameObject.Find("Start").GetComponentInChildren<VP_shadow>().taken = true;
                }else{
                    int pindx = 0;
                    for(int k = 0; k < PItemObjects.BlockObj.Count; k++){
                        if(PItemObjects.BlockObj[k].PItemID == PItemObjects.BlockObj[i].PITemParentID){
                            pindx = k;
                        }
                        if(PItemObjects.BlockObj[pindx].PBlock.GetComponent<IfScript>() == null && PItemObjects.BlockObj[pindx].PBlock.GetComponent<LopScript>() == null){
                            PItemObjects.BlockObj[i].PBlock.transform.SetParent(PItemObjects.BlockObj[pindx].PBlock.transform);
                            PItemObjects.BlockObj[pindx].PBlock.GetComponentInChildren<VP_shadow>().occupied = PItemObjects.BlockObj[i].PBlock;
                            PItemObjects.BlockObj[pindx].PBlock.GetComponentInChildren<VP_shadow>().taken = true;
                        }else if(Items.Blocks[i].Sin == "Sloop"){
                            PItemObjects.BlockObj[i].PBlock.transform.SetParent(PItemObjects.BlockObj[pindx].PBlock.transform.Find("shad_Loop_in"));
                            PItemObjects.BlockObj[pindx].PBlock.GetComponentInChildren<VP_shadow>().occupied = PItemObjects.BlockObj[i].PBlock;
                            PItemObjects.BlockObj[pindx].PBlock.GetComponentInChildren<VP_shadow>().taken = true;
                        }else{
                            PItemObjects.BlockObj[i].PBlock.transform.SetParent(PItemObjects.BlockObj[pindx].PBlock.transform.Find("BackGround (1)"));
                            PItemObjects.BlockObj[pindx].PBlock.GetComponentInChildren<VP_shadow>().occupied = PItemObjects.BlockObj[i].PBlock;
                            PItemObjects.BlockObj[pindx].PBlock.GetComponentInChildren<VP_shadow>().taken = true;
                        }
                    }
                }
                PItemObjects.BlockObj[i].PBlock.transform.localPosition = Items.Blocks[i].PItemPos;
                PItemObjects.BlockObj[i].PBlock.transform.localScale = Items.Blocks[i].PItemScale;
                
            }
         }
    }

    void FindParents(){
         for(int i= 0; i < ItemObjects.GObjects.Count; i++){
            if(ItemObjects.GObjects[i].ParentID.Contains("SAVE")){
                int pindx = 0;
                for(int k = 0; k < ItemObjects.GObjects.Count; k++){
                    if(ItemObjects.GObjects[k].ItemID == ItemObjects.GObjects[i].ParentID){
                        pindx = k;
                    }
                }
                ItemObjects.GObjects[i].buildPart.transform.SetParent(ItemObjects.GObjects[pindx].buildPart.transform);
                ItemObjects.GObjects[i].buildPart.transform.localScale = Items.Objects[i].ItemScale;
                ItemObjects.GObjects[pindx].buildPart.AddComponent<FixedJoint>().connectedBody = ItemObjects.GObjects[i].buildPart.GetComponent<Rigidbody>();
            }else if(ItemObjects.GObjects[i].buildPart.GetComponent<ObjInfo>().connected){
                int cindx = 0;
                for(int k = 0; k < ItemObjects.GObjects.Count; k++){
                    if(ItemObjects.GObjects[i].buildPart.GetComponent<ObjInfo>().connectionID == ItemObjects.GObjects[i].ItemID){
                        cindx = k;
                    }
                }
                ItemObjects.GObjects[i].buildPart.transform.SetParent(ItemObjects.GObjects[cindx].buildPart.transform.GetChild(1));
                ItemObjects.GObjects[i].buildPart.transform.localScale = Items.Objects[i].ItemScale;

                ItemObjects.GObjects[i].buildPart.AddComponent<ConfigurableJoint>().connectedBody = ItemObjects.GObjects[cindx].buildPart.transform.GetChild(1).GetComponent<Rigidbody>();
                    ItemObjects.GObjects[i].buildPart.GetComponent<ConfigurableJoint>().anchor = new Vector3(0,0,0);
                    ItemObjects.GObjects[i].buildPart.GetComponent<ConfigurableJoint>().axis = new Vector3(0,0,0);
                    ItemObjects.GObjects[i].buildPart.GetComponent<ConfigurableJoint>().secondaryAxis = new Vector3(0,0,0);
                
                    ItemObjects.GObjects[i].buildPart.GetComponent<ConfigurableJoint>().xMotion = ConfigurableJointMotion.Locked;
                    ItemObjects.GObjects[i].buildPart.GetComponent<ConfigurableJoint>().yMotion = ConfigurableJointMotion.Locked;
                    ItemObjects.GObjects[i].buildPart.GetComponent<ConfigurableJoint>().zMotion = ConfigurableJointMotion.Locked;
                    ItemObjects.GObjects[i].buildPart.GetComponent<ConfigurableJoint>().angularXMotion = ConfigurableJointMotion.Free;
                    ItemObjects.GObjects[i].buildPart.GetComponent<ConfigurableJoint>().angularYMotion = ConfigurableJointMotion.Locked;
                    ItemObjects.GObjects[i].buildPart.GetComponent<ConfigurableJoint>().angularZMotion = ConfigurableJointMotion.Locked;


            }
        }
    }

}

[System.Serializable]
public class Items
{
    public List<Item> Objects = new List<Item>();
    public Vector3 CameraPos;
    public Vector3 CameraZoom;
    public Quaternion CameraRot;

    public int motCount;
    public int sensCount;
    public string[] motorID;
    public string[] sensorID;
    public int[] inTakes;
    public int[] outTakes;

    public Vector3 spawnPos;

    public bool Day;

    public List<PItem> Blocks = new List<PItem>();
    
}

[System.Serializable]
public class PItem
{
    public string PItemID;
    public string PType;
    public Vector3 PItemPos;
    public Vector3 PItemScale;
    public string Sin;
    public string PITemParentID;

    public List<LoopSize> sizes = new List<LoopSize>();

    public List<Rot> rots = new List<Rot>();
    public List<Rot2> rots2 = new List<Rot2>();

    public float Iterations;
}

[System.Serializable]
public class Rot2
{
    public float Num_Rotations;
    public float Num_Speed;
    public int LetterVal;

    public float Num_Rotations2;
    public float Num_Speed2;
    public int LetterVal2;
}

[System.Serializable]
public class Rot
{
    public float Num_Rotations;
    public float Num_Speed;
    public int LetterVal;
}

[System.Serializable]
public class LoopSize
{
    public float Iterations;
    public string ConnID;
    public float TotWidth;
    public float Total;
    public float Children;
    public float TotChildren;
}

[System.Serializable]
public class PItemObjects
{
    public List<PItemObj> BlockObj = new List<PItemObj>();
}

[System.Serializable]
public class PItemObj
{
    public string PItemID;
    public GameObject PBlock;

    public string PITemParentID;
}

[System.Serializable]
public class Item
{
    public string ItemID;
    public string ObjType;
    public Vector3 ItemPosition;
    public Quaternion ItemRotation;
    public Vector3 ItemScale;
    public bool IsMerged;

    public bool connected;
    public string connectionID;
    
    public string ParentID;

    public float col;
    public bool transparent;
    public float scale;
    public string assignment;
    public int let_in, let_out;

    
    
}


[System.Serializable]
public class ItemObjects
{
    public List<ItemObj> GObjects = new List<ItemObj>();
}

[System.Serializable]
public class ItemObj
{
    public string ItemID;
    public string ParentID;
    public GameObject buildPart;
}

[System.Serializable]
public class AssignmentControlLoader
{
    public int motCount;
    public int sensCount;
    public string[] motorID;
    public string[] sensorID;
    public int[] inTakes;
    public int[] outTakes;
}
