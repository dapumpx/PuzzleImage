using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DataManager.getInstance().initData();

        foreach(NpcVO n in DataManager.getInstance().npcDB.Values)
        {   
            GameObject npc = NpcManager.Instance.buildNCP(n.id);
            npc.transform.parent = transform.parent;
            Vector3 tmp = npc.transform.position;
            tmp.x += Random.value*3;
            //tmp.y -= Random.value*3;
            npc.transform.position = tmp;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
