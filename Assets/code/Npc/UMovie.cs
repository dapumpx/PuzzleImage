using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class UMovie : MonoBehaviour {
    public List<Sprite> lSprites;
    public float fSep = 0.05f;
    private SpriteRenderer shower;
    private int curFrame = 0;
    private float fDelta = 0;
    public bool playAttacking = false;
    
    void Start () {
        
	}
	
	void Awake()
    {
        shower = GetComponent<SpriteRenderer>();

        //shower.sprite = Resources.Load<Sprite>("ui/npc/1/weapon/1");
    }

    public void init(NpcVO npc)
    {
        shower.sprite = Resources.Load<Sprite>("ui/npc/weapon/" + npc.weapon.ToString() + "/1");

        lSprites = new List<Sprite>();

        for (int i = 1; i <= npc.weaponFrameLength; i++)
        {
            lSprites.Add(Resources.Load<Sprite>("ui/npc/weapon/" + npc.weapon.ToString() + "/" + i.ToString()));
        }
    }

    public void Play(int iFrame)
    {
        if (iFrame >= FrameCount)
        {
            iFrame = 0;
            playAttacking = false;
        }
        shower.sprite = lSprites[iFrame];
        curFrame = iFrame;

        if (dMovieEvents.ContainsKey(iFrame))
        {
            foreach (delegateMovieEvent del in dMovieEvents[iFrame])
            {
                del();
            }
        }
    }
    
    public void StartAttack()
    {
        playAttacking = true;
    }

    public int FrameCount
    {
        get
        {
            return lSprites.Count;
        }
    }

    void Update()
    {
        fDelta += Time.deltaTime;
        if (playAttacking)
        {
            if (fDelta > fSep)
            {
                fDelta = 0;
                curFrame++;
                Play(curFrame);
            }
        }
    }

    public delegate void delegateMovieEvent();
    private Dictionary<int, List<delegateMovieEvent>> dMovieEvents = new Dictionary<int, List<delegateMovieEvent>>();
    public void RegistMovieEvent(int frame, delegateMovieEvent delEvent)
    {
        if (!dMovieEvents.ContainsKey(frame))
        {
            dMovieEvents.Add(frame, new List<delegateMovieEvent>());
        }
        dMovieEvents[frame].Add(delEvent);
    }
    public void UnregistMovieEvent(int frame, delegateMovieEvent delEvent)
    {
        if (!dMovieEvents.ContainsKey(frame))
        {
            return;
        }
        if (dMovieEvents[frame].Contains(delEvent))
        {
            dMovieEvents[frame].Remove(delEvent);
        }
    }
}
