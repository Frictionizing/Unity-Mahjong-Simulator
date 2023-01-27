using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayedTiles : MonoBehaviour
{
	public Image[] Current;
	public Image[] P1;
	public Image[] P2;
	public Image[] P3;
	public Image[] P4;
	public Image[] P5;
	public Image[] River;
	public Image[] Claim;
	public static Image[] c = new Image[9];

	public Image[,] h = new Image[6,5];

	private int[] Set1 = new int[5];
	public static bool start = false;


	public static int[] agg = new int[34];
	public static int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
    	for(int i=0; i<5; i++){
    		h[0,i] = Current[i];
    		h[1,i] = P1[i];
    		h[2,i] = P2[i];
    		h[3,i] = P3[i];
    		h[4,i] = P4[i];
    		h[5,i] = P5[i];
    	}

    	for(int i=0; i<Claim.Length; i++)
    	{
    		c[i] = Claim[i];
    	}

    	foreach(Image i in h)
    	{
    		i.sprite = TileSet.tile[0];
    	}
    }

    // Update is called once per frame
    void Update()
    {
    	if(start){
    		start = !start;
    		counter = 0;
    		//Claim
			for(int i=0; i<9; i++){
    			c[i].enabled = false;
    		}

    		//River
    		for(int i=0; i<4; i++){
    			agg[counter] = BSTurnTracker.Set.Peek().getID();
    			counter++;
    			River[i].sprite = TileSet.tile[BSTurnTracker.Set.Dequeue().getID()];
    		}

    		//Tiles of Players
    		for(int i=5; i>=0; i--) 
    		{
    			for(int j=0; j<5; j++)
    			{
    				if(i - BSTurnTracker.ID < 0) {
    					agg[counter] = BSTurnTracker.Set.Peek().getID();
    					counter++;
    					h[BSTurnTracker.max-i-1,j].sprite = TileSet.tile[BSTurnTracker.Set.Dequeue().getID()];
    				}
    				else{
    					agg[counter] = BSTurnTracker.Set.Peek().getID();
    					counter++;
    					h[((-BSTurnTracker.ID)+i)%BSTurnTracker.max,j].sprite = TileSet.tile[BSTurnTracker.Set.Dequeue().getID()];
    				}
    			}
    		}
    	}
    }
}
