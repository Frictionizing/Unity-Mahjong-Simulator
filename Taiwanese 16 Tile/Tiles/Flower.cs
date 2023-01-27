using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Displays The currently drawn tile.

public class Flower : MonoBehaviour
{
	public Image[] i;
	public static Image[] flowers = new Image[4];
	public Sprite[] images = new Sprite[43];
	//public TileGenerator.Tile[] e = TileGenerator.Mahjong_Set;
    // Start is called before the first frame update
    void Start()
    {
		for(int j=0; j<4; j++) {
			flowers[j] = i[j].GetComponent<Image>();
		}
    }

    // Update is called once per frame
    void Update()
    {
		/*
		if(!TileGenerator.sup) {
		if(TileGenerator.Mahjong_Set[TileGenerator.master].getSuit() == "萬")
			flowers.color = Color.red;
		else if(TileGenerator.Mahjong_Set[TileGenerator.master].getSuit() == "條")
			flowers.color = new Color(0, 255, 0);
		else if(TileGenerator.Mahjong_Set[TileGenerator.master].getSuit() == "飾")
			flowers.color = new Color(0, 179, 255);
		else
			flowers.color = Color.white;
		} else {
			if(TileGenerator.Mahjong_SetReverse[TileGenerator.reversecounter].getSuit() == "萬")
			   flowers.color = Color.red;
		    else if(TileGenerator.Mahjong_SetReverse[TileGenerator.reversecounter].getSuit() == "條")
		    	flowers.color = new Color(0, 255, 0);
		    else if(TileGenerator.Mahjong_SetReverse[TileGenerator.reversecounter].getSuit() == "飾")
		    	flowers.color = new Color(0, 179, 255);
		    else
		    	flowers.color = Color.white;
		}
		*/



	    for(int i=0; i<4; i++){
			//Debug.Log(!TileGenerator.sup + " " + !LatestDiscards.update);
			if(!TurnTracker.isDrawn || i != TurnTracker.turn || TurnTracker.sets[i,4].gameObject.activeSelf || TurnTracker.sets[i,0].interactable == true)
				flowers[i].sprite = images[42];
			else {
				if(/*!TileGenerator.sup || */ !LatestDiscards.update )
					flowers[TurnTracker.turn].sprite = images[(int)TileGenerator.Mahjong_Set[TileGenerator.master].getID()-1];
				else
					flowers[TurnTracker.turn].sprite = images[(int)TileGenerator.Mahjong_SetReverse[TileGenerator.reversecounter].getID()-1];
			} 
		}
		
    }
}
