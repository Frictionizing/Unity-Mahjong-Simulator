using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Flower2 : MonoBehaviour
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
		for(int i=0;i<4; i++) {
			if(TurnTracker.sets[i,4].gameObject.activeSelf) {
				if(LatestDiscards.counter > 0 && !TileGenerator.redraw2)
					flowers[i].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID()-1];
				else
					flowers[i].sprite = images[42];
			}
		}

		if(!TurnTracker.sets[TurnTracker.turn,4].gameObject.activeSelf) {
			for(int i=0;i<4; i++) 
				flowers[i].sprite = images[42];
		}

		if(!TurnTracker.sets[TurnTracker.turn,4].gameObject.activeSelf && LatestDiscards.stream[LatestDiscards.counter].getFlower()) {
				for(int i=0;i<4; i++) 
					flowers[i].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter].getID()-1];
		}
    }
}
