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

    private Vector3 offset;
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //gameObject.GetComponentInChildren<UMovie>().StartAttack();

            //Vector3 screenPoint = Camera.main.WorldToScreenPoint(scanPos);


            offset = transform.position - Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }
    }

    void OnMouseDrag()
    {
        Debug.Log("Start Drag");
        Vector3 curr = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) + offset;
        transform.position = curr;
    }

    void OnMouseUp()
    {
        Debug.Log("Mouse UP!!");

        Vector3 curr = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 200));
        Ray ray = new Ray(curr, new Vector3(curr.x, curr.y, -10));

        Debug.DrawLine(ray.origin, ray.direction, Color.red, 1);

        RaycastHit s;
        int layerIndex = 1 << LayerMask.NameToLayer("Default");
        Physics.Raycast(ray, out s, layerIndex);
        Debug.Log( s.transform.name);
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
