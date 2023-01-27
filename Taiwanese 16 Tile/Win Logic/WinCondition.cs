using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
	
	public Button[] Win;
	public Image[] tile;
	public static Button[] wh = new Button[16];
	public static Button[] wh2 = new Button[16];
	public static Button[] wh3 = new Button[16];
	public static Button[] wh4 = new Button[16];
	public Sprite[] Images;
	public Text[] results;
	public static Button[] wn = new Button[4];
	public GameObject[] blackout;
	public Button[] endTiles; 
	public Button[] endTiles2; 
	public Button[] endTiles3; 
	public Button[] endTiles4; 
	public Button w;
	public static int modul;
	public static int modul2;
	public static int hold = 0;
	public static float score = 0;
	public static float point = 0;
	public static bool endGame = false;
	public static bool checkWin = false;
	float[,] winCounter = new float[4,50];
	public Text t;
	public Text te;
	public static TileGenerator.Tile[] hand;
	public static bool[] first = new bool[3];
	public static int remaining = 0;
	public static bool endoftiles = false;
    // Start is called before the first frame update
    void Start()
    {
		modul = 0;
		modul2=0;
		hold = 0;
		score = 0;
		point = 0;
		endGame = false;
		checkWin = false;
		remaining = 0;
		endoftiles = false;
		for(int i=0; i<4; i++) {
			wn[i] = Win[i].GetComponent<Button>();
			wn[i].interactable = false;
		}
		wn[0].onClick.AddListener(() => TaskCheckWin(0));
		wn[1].onClick.AddListener(() => TaskCheckWin(1));
		wn[2].onClick.AddListener(() => TaskCheckWin(2));
		wn[3].onClick.AddListener(() => TaskCheckWin(3));
	
	
		for(int i=0; i<16; i++) {
			wh[i] = endTiles[i].GetComponent<Button>();
			wh2[i] = endTiles2[i].GetComponent<Button>();
			wh3[i] = endTiles3[i].GetComponent<Button>();
			wh4[i] = endTiles4[i].GetComponent<Button>();
		}
    }

    // Update is called once per frame
    void Update()
    {
		TurnTracker.turn = TurnTracker.turn%4;
		for(int i=0; i<4; i++) {
			for(int j=0; j<4; j++) {
				if(!TurnTracker.sets[j,4].gameObject.activeSelf)
					hold = j;
			}
			modul = TurnTracker.turn-1;
			if(TurnTracker.turn == 0)
				modul = 3;
			
			if(TurnTracker.turnone) {
				if(TurnTracker.turn == i && TurnTracker.turnone && !TurnTracker.sets[TurnTracker.turn,4].gameObject.activeSelf  && (!LatestDiscards.stream[LatestDiscards.counter].getFlower() || TileGenerator.redraw2))
					wn[i].interactable = true;
			}

			modul2 = modul-1;
			if(modul == 0)
				modul2 = 3;

			if(TurnTracker.turn == i && (TurnTracker.sets[TurnTracker.turn,4].gameObject.activeSelf || (!TileGenerator.redraw2 && !TurnTracker.sets[TurnTracker.turn,4].gameObject.activeSelf && !LatestDiscards.stream[LatestDiscards.counter].getFlower())) && !TurnTracker.turnone)
				wn[i].interactable = true;
			if((TurnTracker.turn == i && !TurnTracker.sets[TurnTracker.turn,4].gameObject.activeSelf && LatestDiscards.stream[LatestDiscards.counter].getFlower() && !TurnTracker.turnone) || (TurnTracker.turn == i && TileGenerator.tossing))
				wn[i].interactable = false;

			if(TurnTracker.turn != i && !endGame && (modul != i || TileGenerator.tossing) && !TurnTracker.turnone && TurnTracker.sets[TurnTracker.turn,4].gameObject.activeSelf && !TileGenerator.redraw2) 
				wn[i].interactable = true;
			else if (TurnTracker.turn != i)
				wn[i].interactable = false;
		}
		
		if(checkWin || endoftiles) {
			//for(int i=0; i<4; i++) 
				blackout[0].SetActive(true);
		}
		
		if(endoftiles) {
			for(int j=0; j<16; j++) {
					wh[j].gameObject.SetActive(false);
					wh2[j].gameObject.SetActive(false);
					wh3[j].gameObject.SetActive(false);
					wh4[j].gameObject.SetActive(false);
			}
			for(int i=0; i<4; i++)
				tile[i].gameObject.SetActive(false);
			for(int i=0; i<12; i+=3) {
				results[i].text = "DRAW";		
				results[i+1].text = "";	
				results[i+2].text = "";					
			}
		}
		
		if(checkWin) {
			checkWin = false;
		
		for(int j=0; j<16; j++) {
			if(TileGenerator.Ppd[TurnTracker.turn, j].getID() == 45) {
				wh[j].gameObject.SetActive(false);
				wh2[j].gameObject.SetActive(false);
				wh3[j].gameObject.SetActive(false);
				wh4[j].gameObject.SetActive(false);
			} else {
				wh[j].image.sprite = Images[(int)TileGenerator.Ppd[TurnTracker.turn, j].getID()-1];
				wh2[j].image.sprite = Images[(int)TileGenerator.Ppd[TurnTracker.turn, j].getID()-1];
				wh3[j].image.sprite = Images[(int)TileGenerator.Ppd[TurnTracker.turn, j].getID()-1];
				wh4[j].image.sprite = Images[(int)TileGenerator.Ppd[TurnTracker.turn, j].getID()-1];
			}
		}
		
		for(int i=0; i<12; i+=3) 
			results[i].text = "Player " + (TurnTracker.turn+1) + " Asserts a Winning Hand";
		
		hand = collectLot();
		int numOfTiles = TileCount(hand);
		for(int j=0; j<4; j++) {
			for(int i=0; i<50; i++)
				winCounter[j,i] = LatestDiscards.pongCounter[j, i];
		}
		winCounter[TurnTracker.turn,(int)hand[16].getID()]++;
		for(int i=0; i<4; i++)
			tile[i].sprite = Images[(int)hand[16].getID()-1];
		InsertionSort();
		
		for(int i=0; i<17; i++){
			//if(winCounter[(int)hand[i].getID()] > 0)
			//	Debug.Log( i + ": " + winCounter[(int)hand[i].getID()]);
			//Debug.Log(hand[i].toString());
		}
		
		StraightCheck(true);
		StraightCheck(false);
		DoubleStraightCheck();
		StraightCheck(false);
		remaining = TileCount(hand);
		PongCheck(remaining);
		CheckIfLastPair();
		AddRiverPoints();
		
		for(int i=1; i<12; i+=3) 
			results[i].text = "Need 5.5 points to win -> " + point;
		
		for(int i=2; i<12; i+=3) {
			if(point >= 5.5f)
				results[i].text = "<- If that number bigger than 5.5 not my problem";
			else
				results[i].text = "my code says u lost 2 bad";
		}
		}  
    }

	void TaskCheckWin(int i) {
		TurnTracker.turn = i;
		endGame = true;
		checkWin = true;
	}
	
	void StraightCheck(bool edgeCase) {
		int k = 0;
		int a = 1;
		int b = 2;
		int c = 0;
        int g = 4;
		if(edgeCase)
			g = -1;
		for(int m=0; m<2; m++) {
			if(m == 1) {
				k = 16;
			    a = -1;
			    b = -2;
		    	c =  2;
			}
		for(int i=0; i<17; i++) {
			first = LatestDiscards.AlgorithmChoice(winCounter, hand[k].getID());
			if(((winCounter[TurnTracker.turn, (int)hand[k].getID()] == 1 || winCounter[TurnTracker.turn, (int)hand[k].getID()] == g) && (!edgeCase)) || 
			((edgeCase && ((i == 0) || i == 16)) && winCounter[TurnTracker.turn,(int)hand[k].getID()] == 1))  {
				//Debug.Log("Found Singular or Quad: " + hand[k].playNum());
				if(LatestDiscards.straightAlgorithm(winCounter, (int)hand[k].getID()) && first[c]) {
					Debug.Log("Found Singular with Straight: " + hand[k].playNum());
					float y = -1;
				    float z = -1;
					for(int j=16; j>=0; j--) {
				        if(hand[k].getID() + a == hand[j].getID()) 
					        y = j;
						if(hand[k].getID() + b == hand[j].getID())	   
							z = j;
					}
					int[] delete = {(int)y, (int)z, k};
					for(int d=0; d<3; d++){
						winCounter[TurnTracker.turn, (int)hand[delete[d]].getID()] -= 1;
						Debug.Log("Del: " + hand[delete[d]].playNum());
						hand[delete[d]] = new TileGenerator.Tile("", 1, false, false, 45);
	                }
					point++;
					if(m == 1)
						Debug.Log("Point in Reverse Straight");
					else
						Debug.Log("Point in Straight");
				}
			}
			if(m == 1)
					k--;
				else
					k++;
		}
		InsertionSort();
		}
	}
	
	void DoubleStraightCheck(){
		for(int i=0; i<12; i++) {
			if(winCounter[TurnTracker.turn, (int)hand[i].getID()] == 2 && winCounter[TurnTracker.turn, (int)hand[i+2].getID()] == 2 && winCounter[TurnTracker.turn, (int)hand[i+4].getID()] == 2 
			&& LatestDiscards.straightAlgorithm(winCounter, (int)hand[i].getID())) {
				for(int j=0; j<6; j+=2)
					winCounter[TurnTracker.turn, (int)hand[i+j].getID()] -= 2;
				for(int j=0; j<6; j++){
					Debug.Log("Del: " + hand[i+j].playNum());
				    hand[i+j] = new TileGenerator.Tile("", 1, false, false, 45);
				}
				point+=2;
				Debug.Log("2 Points in Double Straight");
			}
		}
		InsertionSort();
	}
	
	void PongCheck(int left) {
		for(int i=0; i<left; i++) {
			if(winCounter[TurnTracker.turn,(int)hand[i].getID()] == 3 && (int)hand[i].getID() != 45) {
				winCounter[TurnTracker.turn,(int)hand[i].getID()] -= 3;
				for(int j=0; j<3; j++){
					Debug.Log("Del: " + hand[i+j].playNum());
					hand[i+j] = new TileGenerator.Tile("", 1, false, false, 45);
				}
				point++;
				Debug.Log("Point in Pong");
			}
		}
		InsertionSort();
	}
	
	void CheckIfLastPair() {
		if((hand[0].getID() == hand[1].getID()) && hand[2].getID() == 45) {
			point+=0.5f;
			for(int i=0; i<2; i++) {
				Debug.Log("Del: " + hand[i].playNum());
				hand[i] = new TileGenerator.Tile("", 1, false, false, 45);
			}
			if(hand[2].getID() != 45)
				Debug.Log("BAD HAND: MORE THAN 2 TILES REMAINING...");
			Debug.Log("0.5 Points for a pair");
		}
	}
	
	void AddRiverPoints() {
		point += riverPoints();
		Debug.Log("Point(s) in river:" + riverPoints());
		Debug.Log("TOTAL POINTS: " + point + "/5.5 REQUIRED");
	}
	
	public TileGenerator.Tile[] collectLot(){
		TileGenerator.Tile[] hand = new TileGenerator.Tile[17];
		
		for(int i=0; i<16; i++){
			hand[i] = TileGenerator.Ppd[TurnTracker.turn, i];
		}
		    if(!TurnTracker.sets[TurnTracker.turn,4].gameObject.activeSelf)
			    hand[16] = LatestDiscards.stream[LatestDiscards.counter];
		    else
				hand[16] = LatestDiscards.stream[LatestDiscards.counter-1];
		return hand;
	}
	
	int TileCount(TileGenerator.Tile[] x) {
         int e = 0;
		for(int i=0; i<17; i++){
			if(x[i].getID() != 45)
				e++;
		}
		return e;
	}
	
	float riverPoints(){
		return (float)RiverTiles.rivercount[TurnTracker.turn];
	}
	
	public void InsertionSort() {
		int current = 0;
		TileGenerator.Tile temp;
		for(int i=1; i<=42; i++) {
			for(int j=0; j<17; j++) {
				if(hand[j].getID() == i){
				    temp = hand[current];
					hand[current++] = hand[j];
					hand[j] = temp;
                }					
			}
		}
	}
}
