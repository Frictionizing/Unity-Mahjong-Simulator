using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSet : MonoBehaviour
{
	public Sprite[] tiles;
	//0 Hidden, 1-9 Numbers, 10-18 Circles, 19-27 Bamboo, 28-34 Honors & Dragons, 35- 42 Joker, 43 Crossout
	public static Sprite[] tile = new Sprite[44];
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<tiles.Length; i++)
        	tile[i] = tiles[i];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
