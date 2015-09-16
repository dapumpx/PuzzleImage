using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NpcManager {
    static private NpcManager _instance;

    private List<GameObject> listNpc;

    private NpcManager()
    {
        listNpc = new List<GameObject>();
    }

    public static NpcManager Instance
    {
        get
        {
            if (_instance == null) _instance = new NpcManager();
            return _instance;
        }
    }

    public GameObject buildNCP(int id)
    {
        GameObject npc = Object.Instantiate<GameObject>(Resources.Load<GameObject>("ui/npc/hero"));
        npc.GetComponent<NPCController>().init(DataManager.getInstance().npcDB[id]);
        return npc;
    }
}
