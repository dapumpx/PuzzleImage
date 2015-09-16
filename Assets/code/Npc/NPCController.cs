using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour {

    public bool flagWalk;

    public bool flagMoveLeft;

    public float destX;

    public float upSpeed;

    public float oriY;

    public Vector2 oriPoint;

    public NpcVO npc;
	// Use this for initialization
	void Start () {
	
	}
	
    void Awake()
    {
        oriY = transform.position.y;
        Debug.Log("Awake");
    }

    public void init(NpcVO npc)
    {
        this.npc = npc;

        gameObject.GetComponentInChildren<UMovie>().init(npc);

        //Debug.Log("URL::" + "ui/npc/body/" + npc.body.ToString());
        //Debug.Log(gameObject.transform.FindChild("body").GetComponent<SpriteRenderer>().sprite);

        gameObject.transform.FindChild("body").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("ui/npc/body/" + npc.body.ToString());
    }

	// Update is called once per frame
	void Update () {
	    if(flagWalk)
        {
            if(flagMoveLeft)
            {
                Vector3 tmp = transform.position;
                tmp.x -= 0.02f;

                if (tmp.y <= oriY) upSpeed = 0.02f;
                else if (tmp.y >= oriY + 0.1f) upSpeed = -0.02f;
                tmp.y += upSpeed;

                if (tmp.x < destX)
                {
                    tmp.x = destX;
                    flagWalk = false;
                    tmp.y = oriY;
                }

                transform.position = tmp;
            }
        }
	}

    public void setPosition(int posX, int posY)
    {
        if(oriPoint == null)
        {
            oriPoint = new Vector2(posX, posY);
        }
        else
        {
            oriPoint.x = posX;
            oriPoint.y = posY;
        }
        Vector3 tmp = transform.position;
        tmp.y = -0.8f + 0.4f * posY;
        transform.position = tmp;
    }

    public void walk(int step)
    {
        flagMoveLeft = true;

        if (flagMoveLeft)
        {
            destX = transform.position.x - step * 0.6f;
        }

        flagWalk = true;
    }
}
