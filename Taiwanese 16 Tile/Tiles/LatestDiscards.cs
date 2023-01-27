using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Displays the last 7 discards and records total discard tally.

public class LatestDiscards : MonoBehaviour
{
	public GameObject[] discord;
	public Text[] discard;
	
	public static int[,] discardcount = new int[3,9];
	public static string[]  honor = {"E東", "S南", "W西", "N北", "F發", "Z中", "P白"};
	public static string[]        honor2 = {"東", "南", "西", "北", "發", "中", "白"};

	//public static string[]        number = {"一", "二", "三", "四", "五", "六", "七", "八", "九"};
	public static string[]        number = {"1", "2", "3", "4", "5", "6", "7", "8", "9"};
	public static int[]  honorcount = new int[7];
	
	public static int count;
	public static int turntable = 0;
	public static bool e = false;
	private bool f = true;
	//private bool g = false;
	
	public static bool[] interact = {false, false, false, false};
	public static bool[] interact2 = {false, false, false, false};
	public static bool interact3 = false;
	
	public Button[] Chis;
	Button[] c = new Button[12];
	//public static Button[][] cf = {}
	Text[] t = new Text[12];
	public Button diu;
	Button d;
	public Button pong;
	Button p;
	public Button kong;
	Button k;
	

	public static bool kongHand = false;
	
	public static float[,] pongCounter = new float[4,50];
	public static TileGenerator.Tile[] stream = new TileGenerator.Tile[144];
	public static int counter = 0;
	public static bool s = false;
	public static bool se = true;
	public static bool update = false;
	
	public Image[] pile = new Image[64];
	public static Image[] pile2 = new Image[64];
	public static int pileCount = 0;

    // Start is called before the first frame update
    void Start()
    {
		pileCount = 0;
		counter = 0;
		s = false;
		se = true;
		update = false;
		f = true;
		e = false;
		turntable = 0;
        for(int i=0; i<7; i++){
			discard[i] = discord[i].GetComponent<Text>();
			discard[i].text = "";
			honorcount[i] = 0;
		}
		

	for(int i=0; i<64; i++){
			pile2[i] = pile[i];
			pile[i].enabled = false;
		}

		for(int i=0; i<12; i++){
			c[i] = Chis[i].GetComponent<Button>();
			t[i] = Chis[i].GetComponentInChildren<Text>();
			c[i].interactable = false;
			t[i].text = "";
		}
		
		for(int i=0; i<12; i++){
		//	cf[0] = c[i];
		}
		
		for (int i=0; i<3; i++) {
			for(int j=0; j<9; j++) {
				discardcount[i,j] = 0;
			}
		}
		
		for(int j=0; j<4; j++) {
			for(int i=0; i<36; i++)
				pongCounter[j,i] = 0;
		}
		
		for(int i=0; i<144; i++){
			stream[i] = new TileGenerator.Tile("", 0, false, false, 45);
		}
		
		c[0].onClick.AddListener(() => TaskSpecialPong(3));
		c[3].onClick.AddListener(() => TaskSpecialPong(3));
		c[6].onClick.AddListener(() => TaskSpecialPong(3));
		c[9].onClick.AddListener(() => TaskSpecialPong(3));
		
		c[1].onClick.AddListener(() => TaskSpecialPong(2));
		c[4].onClick.AddListener(() => TaskSpecialPong(2));
		c[7].onClick.AddListener(() => TaskSpecialPong(2));
		c[10].onClick.AddListener(() => TaskSpecialPong(2));
		
		c[2].onClick.AddListener(() => TaskSpecialPong(1));
		c[5].onClick.AddListener(() => TaskSpecialPong(1));
		c[8].onClick.AddListener(() => TaskSpecialPong(1));
		c[11].onClick.AddListener(() => TaskSpecialPong(1));	
    }

	public static void recordPile(){
		if(counter > 0){
				pile2[pileCount].sprite = RiverTiles.images2[parseTile()]; 
				pile2[pileCount++].enabled = true;
			}
	}

	static int parseTile(){
		return (int) stream[counter - 1].getID()-1;
	}



    // Update is called once per frame
    void Update()
    {
		if(f) {
		   count = 143 - TileGenerator.master;
		   f = false;
		}
		
		if(count != (143 - TileGenerator.master)) {
		   e = true;	
		   se = false;
		}
		
		if(TurnTracker.newTurn) {
			//recordPile();
			TurnTracker.newTurn = false;
			TurnTracker.turnone = false;
			counter++;
			if(turntable != 7)
				turntable++;
			count--;
			e = false;
			s = false;
			recordTile();	
		}
		
		if(!update)
			stream[counter] = TileGenerator.Mahjong_Set[TileGenerator.master];
		else 
		    stream[counter] = TileGenerator.Mahjong_SetReverse[TileGenerator.reversecounter];
		
		if(counter > 0) {

			if(counter <= 7) {
			    for(int i=0; i<counter; i++) {
				    discard[i].text = stream[counter - 1 - i].playNum2();
			    	colorChanger(i);
		    	}
			}	
			else {
			    for(int i=0; i<7; i++) {
				    discard[i].text = stream[counter - 1 - i].playNum2();
			    	colorChanger(i);
		    	}
			}	
		}
		//Debug.Log("Stream: " + stream[counter].playNum2() + "\n                 Counter: " + counter);
		//Debug.Log(turntable + " d");
		
		for(int k=0; k<4; k++){
		    for(int i=0; i<50; i++){
			   pongCounter[k,i] = 0;
		    }
	    }
		

		for(int i=0; i<16; i++){
			TurnTracker.turn = TurnTracker.turn%4;
			pongCounter[0, (int) TileGenerator.Player_1[i].getID()]++;
			pongCounter[1, (int) TileGenerator.Player_2[i].getID()]++;
			pongCounter[2, (int) TileGenerator.Player_3[i].getID()]++;
			pongCounter[3, (int) TileGenerator.Player_4[i].getID()]++;
		}	
	
		
		if(counter > 0) {
			for(int i=0; i<4; i++) {
				if(pongCounter[i, (int) stream[counter - 1].getID()]>= 2) {
					if(pongCounter[i, (int) stream[counter - 1].getID()]== 3)
						interact2[i] = true;
					interact[i] = true;
				}
				else {
					interact2[i] = false;
					interact[i] = false;
				}
			}
		}
	
		e = false;	
		int j = 0;
		if(!TurnTracker.turnone && stream[counter - 1].getID() <= 27 )	{
			
			bool x = straightAlgorithm(pongCounter, stream[counter - 1].getID());
			bool[] activate = AlgorithmChoice(pongCounter, stream[counter - 1].getID());
	        if(activate[2] && x && TurnTracker.sets[TurnTracker.turn,4].gameObject.activeSelf 
			&& !TurnTracker.grace && !WinCondition.endGame && !TileGenerator.pong2 && !TileGenerator.kong2 && !TileGenerator.redraw2) {
				j = 0;
				for(int i=0; i<12; i+=3){
					if(j == TurnTracker.turn && i<12)
						c[i].interactable = true;
					j++;
				}
			} else {
				for(int i=0; i<12; i+=3)
					c[i].interactable = false;
			}
			if(activate[1] && x && TurnTracker.sets[TurnTracker.turn,4].gameObject.activeSelf 
			&& !TurnTracker.grace && !WinCondition.endGame && !TileGenerator.pong2 && !TileGenerator.kong2 && !TileGenerator.redraw2) {
				j = 0;
				for(int i=1; i<12; i+=3){
					if(j == TurnTracker.turn && i<12)
						c[i].interactable = true;
					j++;
				}
			} else {
				for(int i=1; i<12; i+=3)
					c[i].interactable = false;
			}
	
			if(activate[0] && x && TurnTracker.sets[TurnTracker.turn,4].gameObject.activeSelf 
			&& !TurnTracker.grace && !WinCondition.endGame && !TileGenerator.pong2 && !TileGenerator.kong2 && !TileGenerator.redraw2) {
				j = 0;
				for(int i=2; i<12; i+=3){
					if(j == TurnTracker.turn && i<12)
						c[i].interactable = true;
					j++;
				}
			} else {
				for(int i=2; i<12; i+=3)
					c[i].interactable = false;
			}
			
			if(TileGenerator.pongFlag > 0) {
				for(int i=0; i<12; i++)
					c[i].interactable = false;
			}
			
			for(int i=0; i<12; i++){
				t[i].text = "";
			}
			
			float IDchange = (stream[counter - 1].getID() % 10) + (int)((stream[counter - 1].getID() / 10));
			if(LatestDiscards.stream[LatestDiscards.counter - 1].getID() == 19)
				IDchange = 1;
			
			if(IDchange != 1 && IDchange != 2 && !TileGenerator.redraw2)  {
				j = 0;
				for(int i=0; i<12; i+=3){
					if(j == TurnTracker.turn && i<12) {
						//Debug.Log(IDchange + " I");
						t[i].text = number[(int)(IDchange - 2)-1] + "," + number[(int)(IDchange - 1)-1] + "" + (stream[counter - 1].getSuit()) + "吃";				
					}
					j++;
				}
			} else {
				for(int i=0; i<12; i+=3){
					if(j == TurnTracker.turn && i<12)
						t[i].text = "";
					j++;
				}
			}
			
			if(IDchange != 1 && IDchange != 9 && !TileGenerator.redraw2) {
				j = 0;
				for(int i=1; i<12; i+=3){
					if(j == TurnTracker.turn && i<12) {
					//	Debug.Log(IDchange + " I");
						t[i].text = number[(int)(IDchange-2)] + "," + number[(int)(IDchange)] + (stream[counter - 1].getSuit()) + "吃";
					}
					j++;
				}
			} else {
				for(int i=1; i<12; i+=3){
					if(j == TurnTracker.turn && i<12)
						t[i].text = "";
					j++;
				}
			}
			
			if(IDchange != 8 && IDchange != 9 && !TileGenerator.redraw2)  {
				j = 0;
				for(int i=2; i<12; i+=3){
					if(j == TurnTracker.turn && i<12) {
						//Debug.Log(IDchange + " I");
						t[i].text = "" + number[(int)(IDchange)] + "," + number[(int)(IDchange+1)] + (stream[counter - 1].getSuit()) + "吃";
					}
					j++;
				}
			} else {
				for(int i=2; i<12; i+=3){
					if(j == TurnTracker.turn && i<12)
						t[i].text = "";
					j++;
				}
			}
		} else {
			for(int i=0; i<12; i++){
				t[i].text = "";	
			}
		}				
		
		kongHand = false;	
		for(int i=0; i<35; i++){
			if(pongCounter[TurnTracker.turn, i] == 4)
				kongHand = true;	
		}
		
	}
	
	public static void reset() {
		for(int k=0; k<4; k++){
		    for(int i=0; i<50; i++){
			   pongCounter[k,i] = 0;
		    }
	    }
		

		for(int i=0; i<16; i++){
			TurnTracker.turn = TurnTracker.turn%4;
			pongCounter[0, (int) TileGenerator.Player_1[i].getID()]++;
			pongCounter[1, (int) TileGenerator.Player_2[i].getID()]++;
			pongCounter[2, (int) TileGenerator.Player_3[i].getID()]++;
			pongCounter[3, (int) TileGenerator.Player_4[i].getID()]++;
		}	
	}
	
	public static bool straightAlgorithm(float[,] x, float ID) {
		if(ID > 27)
			return false;
	    if(ID == 1 || ID == 10 || ID == 19) {
			if(x[TurnTracker.turn, (int)ID+1] > 0 && x[TurnTracker.turn, (int)ID+2] > 0)
				return true;
		}
		else if(ID == 9 || ID == 18 || ID == 27) {
			if(x[TurnTracker.turn, (int)ID-1] > 0 && x[TurnTracker.turn, (int)ID-2] > 0)
				return true;
		}
		else if(ID == 2 || ID == 11 || ID == 20) {
			if((x[TurnTracker.turn, (int)ID-1] > 0 && x[TurnTracker.turn, (int)ID+1] > 0) || (x[TurnTracker.turn, (int)ID+1] > 0 && x[TurnTracker.turn, (int)ID+2] > 0))
				return true;
		}	
		else if(ID == 8 || ID == 17 || ID == 26) {
			if((x[TurnTracker.turn, (int)ID+1] > 0 && x[TurnTracker.turn, (int)ID-1] > 0) || (x[TurnTracker.turn, (int)ID-1] > 0 && x[TurnTracker.turn, (int)ID-2] > 0))
				return true;
		}
		else {
			if((x[TurnTracker.turn, (int)ID+1] > 0 && x[TurnTracker.turn, (int)ID+2] > 0) || (x[TurnTracker.turn, (int)ID-1] > 0 && x[TurnTracker.turn, (int)ID+1] > 0) || (x[TurnTracker.turn, (int)ID-1] > 0 && x[TurnTracker.turn, (int)ID-2] > 0) )
		       return true;
		}
		return false;
	}
	
	public static int straightAlgorithmCombos(float[,] x, float ID) {
        int i = 0;
	    if(ID == 1 || ID == 10 || ID == 19 || ID == 9 || ID == 18 || ID == 27) {
			return 1; 
		}
		else if(ID == 2 || ID == 11 || ID == 20) {
			if(x[TurnTracker.turn, (int)ID-1] > 0 && x[TurnTracker.turn, (int)ID+1] > 0)
				i++;
			if(x[TurnTracker.turn, (int)ID+1] > 0 && x[TurnTracker.turn, (int)ID+2] > 0)
				i++;
		}	
		else if(ID == 8 || ID == 17 || ID == 26) {
			if(x[TurnTracker.turn, (int)ID+1] > 0 && x[TurnTracker.turn, (int)ID-1] > 0)
				i++;
			if(x[TurnTracker.turn, (int)ID-1] > 0 && x[TurnTracker.turn, (int)ID-2] > 0)
				i++;
		}
		else {
			if(x[TurnTracker.turn, (int)ID+1] > 0 && x[TurnTracker.turn, (int)ID+2] > 0)
		        i++;
		    if(x[TurnTracker.turn, (int)ID-1] > 0 && x[TurnTracker.turn, (int)ID+1] > 0)
				i++;
			if(x[TurnTracker.turn, (int)ID-1] > 0 && x[TurnTracker.turn, (int)ID-2] > 0)
				i++;
		}
		return i;
	}
	
	public static int AlgorithmFlag(float[,] x, float ID) {
	    if(ID == 1 || ID == 10 || ID == 19)
			return 1;
		
		if(ID == 9 || ID == 18 || ID == 27)
			return 3;
			
		if(x[TurnTracker.turn, (int)ID-1] > 0 && x[TurnTracker.turn, (int)ID+1] > 0)
			return 2;
		
		if(x[TurnTracker.turn, (int)ID+1] > 0 && x[TurnTracker.turn, (int)ID+2] > 0)
			return 1;
	
		if(x[TurnTracker.turn, (int)ID-1] > 0 && x[TurnTracker.turn, (int)ID-2] > 0)
			return 3;
		
		return 6;
	}
	
	public static bool[] AlgorithmChoice(float[,] x, float ID) {
		bool[] setOf = new bool[3];
		bool i = false , j = false , k = false;
	    if(ID == 1 || ID == 10 || ID == 19)
			i = true;
		
		if(ID == 9 || ID == 18 || ID == 27)
			k = true;
			
		if(x[TurnTracker.turn, (int)ID-1] > 0 && x[TurnTracker.turn, (int)ID+1] > 0)
			j = true;
		
		if(x[TurnTracker.turn, (int)ID+1] > 0 && x[TurnTracker.turn, (int)ID+2] > 0)
			i = true;
	
		if(x[TurnTracker.turn, (int)ID-1] > 0 && x[TurnTracker.turn, (int)ID-2] > 0)
			k = true;
		
		if(ID == 1 || ID == 10 || ID == 19) {
			j = false;
			k = false;
		}
		
		if(ID == 2 || ID == 11 || ID == 20) 
			k = false;
		
		if(ID == 9 || ID == 18 || ID == 27) {
			j = false;
			i = false;
		}
		
		if(ID == 8 || ID == 17 || ID == 26) 
			i = false;
		
		setOf[0] = i;
		setOf[1] = j;
		setOf[2] = k;
		return setOf;
	}
	
	
	void TaskDiu() {
		Action.toss = true;
		Action.swap = false;
	}
	
	void TaskPong() {
		TileGenerator.tossing = true;
		TileGenerator.pong = true;
		RiverTiles.isActivate = true;
	}
	
	void TaskSpecialPong(int i) {
		TileGenerator.tossing = true;
		TileGenerator.pongFlag = i;
		RiverTiles.isActivate = true;
		if(i == 3)
			RiverTiles.Chi1 = true;
		else if(i == 2)
			RiverTiles.Chi2 = true;
		else if(i == 1)
			RiverTiles.Chi3 = true;
	}
	
	void TaskKong() {
		TileGenerator.kong = true;
		RiverTiles.isActivate = true;
		RiverTiles.isKong = true;
	}
	
	void colorChanger(int i) {
		if(stream[counter - 1 - i].getSuit() == "萬")
			discard[i].color = Color.red;
		else if(stream[counter - 1 - i].getSuit()  == "條")
			discard[i].color = new Color(0, 200, 0);
		else if(stream[counter - 1 - i].getSuit()  == "飾")
			discard[i].color = Color.blue;
		else
			discard[i].color = Color.black;
	}
	
	public static void recordTile() {
		if(stream[counter - 1].getSuit() == "萬") 
			discardcount[0,stream[counter - 1].getNumber() - 1]++;
		else if(stream[counter - 1].getSuit() == "飾") 
			discardcount[1,stream[counter - 1].getNumber() - 1]++;
		else if(stream[counter - 1].getSuit() == "條") 
			discardcount[2,stream[counter - 1].getNumber() - 1]++;
	//	else if(stream[counter - 1].getSuit() == "花")
		//	TileGenerator.flower_count++;
		else{
		    for(int i=0; i<honor.Length; i++) {
                if(stream[counter - 1].getSuit() == honor2[i])     
					honorcount[i]++;
            }			
		}
	}
	
	public static void recordTilePong(int i) {
		if(stream[counter - 1].getSuit() == "萬") 
			discardcount[0,stream[counter - 1].getNumber() - 1] -= 1;
		else if(stream[counter - 1].getSuit() == "飾") 
			discardcount[1,stream[counter - 1].getNumber() - 1] -= 1;
		else if(stream[counter - 1].getSuit() == "條") 
			discardcount[2,stream[counter - 1].getNumber() - 1] -= 1;		
	}
	
	public static void recordTileChi(int i) {
		if(stream[counter - 1].getSuit() == "萬") {
			    discardcount[0,stream[counter - 1].getNumber()-1] -= 1;
	    	}
		    else if(LatestDiscards.stream[LatestDiscards.counter - 1].getSuit() == "飾") {
			    discardcount[1,stream[counter - 1].getNumber()-1] -= 1;
		    }  
		    else if(LatestDiscards.stream[LatestDiscards.counter - 1].getSuit() == "條") {
			    discardcount[2,stream[counter - 1].getNumber()-1] -= 1;
			}			
	
		/*
		if (i == 1) {
		    if(stream[counter - 1].getSuit() == "萬") {
			    discardcount[0,stream[counter - 1].getNumber()] += 1;
			//	discardcount[0,stream[counter - 1].getNumber() + 1] += 1;
	    	}
		    else if(LatestDiscards.stream[LatestDiscards.counter - 1].getSuit() == "飾") {
			    discardcount[1,stream[counter - 1].getNumber()] += 1;
			//	discardcount[1,stream[counter - 1].getNumber() + 1] += 1;
			
		    }  
		    else if(LatestDiscards.stream[LatestDiscards.counter - 1].getSuit() == "條") {
			    discardcount[2,stream[counter - 1].getNumber()] += 1;
			//	discardcount[2,stream[counter - 1].getNumber() + 1] += 1;
			}				
	    }   
		if (i == 2) {
		    if(LatestDiscards.stream[LatestDiscards.counter - 1].getSuit() == "萬") {
			    discardcount[0,stream[counter - 1].getNumber()] += 1;
			//	discardcount[0,stream[counter - 1].getNumber() - 2] += 1;
	    	}
		    else if(LatestDiscards.stream[LatestDiscards.counter - 1].getSuit() == "飾") {
			    discardcount[1,stream[counter - 1].getNumber()] += 1;
			//	discardcount[1,stream[counter - 1].getNumber() - 2] += 1;
			
		    }  
		    else if(LatestDiscards.stream[LatestDiscards.counter - 1].getSuit() == "條") {
			    discardcount[2,stream[counter - 1].getNumber()] += 1;
				//discardcount[2,stream[counter - 1].getNumber() - 2] += 1;
			}				
	    }   
		if (i == 3) {
		    if(LatestDiscards.stream[LatestDiscards.counter - 1].getSuit() == "萬") {
			//   discardcount[0,stream[counter - 1].getNumber() -2] += 1;
			//	discardcount[0,stream[counter - 1].getNumber() -3] += 1;
	    	}
		    else if(LatestDiscards.stream[LatestDiscards.counter - 1].getSuit() == "飾") {
			    discardcount[1,stream[counter - 1].getNumber() - 2] += 1;
				discardcount[1,stream[counter - 1].getNumber() - 3] += 1;
			
		    }  
		    else if(LatestDiscards.stream[LatestDiscards.counter - 1].getSuit() == "條") {
			    discardcount[2,stream[counter - 1].getNumber() - 2] += 1;
				discardcount[2,stream[counter - 1].getNumber() - 3] += 1;
			}				
	    }
		*/		
	}
}
