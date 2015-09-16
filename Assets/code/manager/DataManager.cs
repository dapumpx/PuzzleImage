using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class DataManager{
    public Dictionary<int, NpcVO> npcDB;

    private DataManager() { }
    static private DataManager _instance;
    static public DataManager getInstance()
    {
        if (_instance == null) _instance = new DataManager();
        return _instance;
    }

    public void initData()
    {
        TextAsset txtDb = null;

        npcDB = new Dictionary<int, NpcVO>();
        txtDb = Resources.Load<TextAsset>("data/npc");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtDb.text);
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("NPCConfig").ChildNodes;
        for(int i = 0; i < nodeList.Count; i++)
        {
            XmlElement xe = nodeList.Item(i) as XmlElement;

            if (xe == null) continue;

            int key = int.Parse(xe.GetAttribute("ID"));
            npcDB[key] = new NpcVO();
            for(int j = 0; j < xe.Attributes.Count; j++)
            {
                XmlAttribute attr = xe.Attributes[j];

                switch(attr.Name)
                {
                    case "ID":
                        npcDB[key].id = int.Parse(attr.Value);
                        break;

                    case "name":
                        npcDB[key].name = attr.Value;
                        break;

                    case "weaponFrameLength":
                        npcDB[key].weaponFrameLength = int.Parse(attr.Value);
                        break;

                    case "weapon":
                        npcDB[key].weapon = int.Parse(attr.Value);
                        break;

                    case "body":
                        npcDB[key].body = int.Parse(attr.Value);
                        break;
                }
            }
        }
    }
}
