using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Generates, shuffles, and distributes tiles to all players.
//Automatically handles initial flowers

public class TileGenerator : MonoBehaviour
{
	public GameObject hello;
	public static Text helloworld;
	public GameObject flowerPile;
	public static Text flowers;
	public GameObject Rivers1;
	public static Text river1;
	
	public GameObject P_2;
	public static Text P2;
	public GameObject flowerPile_2;
	public static Text flowers_2;
	public GameObject Rivers2;
	public static Text river2;
	 
	public GameObject P_3;
	public static Text P3;
	public GameObject flowerPile_3;
	public static Text flowers_3;
	public GameObject Rivers3;
	public static Text river3;	
	
	public GameObject P_4;
	public static Text P4;
	public GameObject flowerPile_4;
	public static Text flowers_4;
	public GameObject Rivers4;
	public static Text river4;
	
	public Button diu;
	public Button liu;
	Button d;
	Button l;
	

	
	//public static Text[] fd = {flowers, flowers_2, flowers_3, flowers_4};
	
	
    public string[] suit3 = {"萬", "飾", "條", "東", "南", "西", "北", "發", "中", "白", "花"};
	
	public class Tile {
		string suit;
		int number;
		bool isHonor;
		bool isFlower;
		float id;
		
		public Tile(string suit, int number, bool isHonor, bool isFlower, float id) {
			this.suit     = suit;
			this.number   = number;
			this.isHonor  = isHonor;
			this.isFlower = isFlower;
			this.id       = id;
		}
		
		public string getSuit(){
			return suit;
		}
		public int getNumber(){
			return number;
		}
		public bool getHonor(){
			return isHonor;
		}
		public bool getFlower(){
			return isFlower;
		}
		public float getID(){
			return id;
		}
		
		public string toString(){
			return "(" + this.getSuit() + ", " + this.getNumber() + ", Honor: " + this.getHonor() + 
			       ", Flower: " + this.getFlower() + ", ID: " + this.getID() + ")";
		}
		
		public string playNum(){
			if (!this.getHonor())
				return " [" + this.getNumber() + this.getSuit() + "] ";
			else {
				/*
		       	string x = "";
                if(this.getSuit() == "東")
					x = "E";
				else if(this.getSuit() == "西")
					x = "W";
				else if(this.getSuit() == "南")
					x = "S";
				else if(this.getSuit() == "北")
					x = "N";
				else if(this.getSuit() == "中")
					x = "Z";
				else if(this.getSuit() == "發")
					x = "F";
				else if(this.getSuit() == "白")
					x = "P";
				*/
				return " [ " + this.getSuit() + " ] ";
				
			}	
		}
		
		public string playNum2(){
			if (!this.getHonor())
				return this.getNumber() + this.getSuit();
			else {
				/*
		       	string x = "";
                if(this.getSuit() == "東")
					x = "E";
				else if(this.getSuit() == "西")
					x = "W";
				else if(this.getSuit() == "南")
					x = "S";
				else if(this.getSuit() == "北")
					x = "N";
				else if(this.getSuit() == "中")
					x = "Z";
				else if(this.getSuit() == "發")
					x = "F";
				else if(this.getSuit() == "白")
					x = "P";
				*/
				return " " + this.getSuit() + " ";
				
			}	
		}
	}
	
	public Tile tempGO;
	public static Tile[] Mahjong_Set = new Tile[144];
	public static Tile[] Mahjong_SetReverse = new Tile[144];
	public static Tile[] Player_1    = new Tile[16];
	public static Tile[] Player_2    = new Tile[16];
	public static Tile[] Player_3    = new Tile[16];
	public static Tile[] Player_4    = new Tile[16];
	public static Tile[][] Pp        = {Player_1, Player_2, Player_3, Player_4};
	public static Tile[,] Ppd = new Tile[4,16];
	public static int current = 1;
	public static int master =  0;
	public static int master2 =  0;
	public static int flower_count = 0;
	
	public static bool swapping = false;
	public static bool tossing  = false;
	
	public static bool pong  = false;
	public static bool pong2  = false;
	
	public static bool kong  = false;
	public static bool kong2  = false;
	public static bool kong3  = false;
	
	public static bool chi = false;
	public static bool chi2 = false;
	
	public static int pongFlag = 0;
	
	public static bool redraw = false;
	public static bool gotFlower = false;
	
	public static bool hKong = false;
	public static bool hKong2 = false;
	
	public static bool one = false;
	public static int  multi = 0;
	public static bool redraw2 = false;
	
	public static int flcount = 0;
	//Mathf.Ceil((counter+1)/4)
	//Tile slab = new Tile("萬", 3, false, 1);
	public Button[] handB;
	public Button[] handC;
	public Button[] handD;
	public Button[] handE;
	//public static Button[,] handA;


	static Button[,] xd = new Button[4,16];
	Text[] xt = new Text[16];
	Sprite[,] st = new Sprite[4,16];
	public Text[] fd = {flowers, flowers_2, flowers_3, flowers_4};
	public Sprite[] images = new Sprite[43];
	
	public GameObject[] ft = new GameObject[8];
	public GameObject[] ft2 = new GameObject[8];
	public GameObject[] ft3 = new GameObject[8];
	public GameObject[] ft4 = new GameObject[8];
	//public GameObject[,] flowerz = {ft, ft2, ft3, ft4};
	Image[] flowy = new Image[8];
	Image[] flowy2 = new Image[8];
	Image[] flowy3 = new Image[8];
	Image[] flowy4 = new Image[8];
	
	public static int[] flstore = new int[4];
	
    // Start is called before the first frame update
    void Start()
    {
		current = 1;
	    master =  0;
	    master2 =  0;
	    flower_count = 0;
		swapping = false;
	    tossing  = false;
		flcount = 0;
	    pong  = false;
	    pong2  = false;
	
	   kong  = false;
	   kong2  = false;
		kong3 = false;
	
	   chi = false;
	   chi2 = false;
	
	   pongFlag = 0;
	 
	   redraw = false;
	   gotFlower = false;
	
	   hKong = false;
	   hKong2 = false;
	
	   one = false;
	   multi = 0;
	   redraw2 = false;
		//images[0] = Resources.Load<Sprite>("1-Wan");
        //public static Button[,] handA = {handB, handC, handD, handE};
        helloworld  = hello.GetComponent<Text>();
		P2          = P_2.GetComponent<Text>();
		P3          = P_3.GetComponent<Text>();
		P4          = P_4.GetComponent<Text>();
		flowers     = flowerPile.GetComponent<Text>();
		flowers_2   = flowerPile_2.GetComponent<Text>();
		flowers_3    = flowerPile_3.GetComponent<Text>();
		flowers_4    = flowerPile_4.GetComponent<Text>();
		

		for(int i=0; i<16; i++){
			xd[0,i] = handB[i].GetComponent<Button>();
		//	xt[i] = handA[i].GetComponentInChildren<Text>();
			st[0,i] = handB[i].GetComponent<Sprite>();
		}
		for(int i=0; i<16; i++){
			xd[1,i] = handC[i].GetComponent<Button>();
		//	xt[i] = handA[i].GetComponentInChildren<Text>();
			st[1,i] = handC[i].GetComponent<Sprite>();
		}for(int i=0; i<16; i++){
			xd[2,i] = handD[i].GetComponent<Button>();
		//	xt[i] = handA[i].GetComponentInChildren<Text>();
			st[2,i] = handD[i].GetComponent<Sprite>();
		}for(int i=0; i<16; i++){
			xd[3,i] = handE[i].GetComponent<Button>();
		//	xt[i] = handA[i].GetComponentInChildren<Text>();
			st[3,i] = handE[i].GetComponent<Sprite>();
		}
		
		
		
		for(int i=0; i<8; i++){
			flowy[i] = ft[i].GetComponent<Image>();
		}
		for(int i=0; i<8; i++){
			flowy2[i] = ft2[i].GetComponent<Image>();
		}
		for(int i=0; i<8; i++){
			flowy3[i] = ft3[i].GetComponent<Image>();
		}
		for(int i=0; i<8; i++){
			flowy4[i] = ft4[i].GetComponent<Image>();
		}
		
	    //if(images[0] == null)
			//Debug.Log("unity sux");
		
		//xd[0].image.sprite = images[0]; 
		
		for(int i=0; i<4; i++){
			xd[i,0].onClick.AddListener(() => TaskSwap(0));
			xd[i,1].onClick.AddListener(() => TaskSwap(1));
			xd[i,2].onClick.AddListener(() => TaskSwap(2));
			xd[i,3].onClick.AddListener(() => TaskSwap(3));
			xd[i,4].onClick.AddListener(() => TaskSwap(4));
			xd[i,5].onClick.AddListener(() => TaskSwap(5));
			xd[i,6].onClick.AddListener(() => TaskSwap(6));
			xd[i,7].onClick.AddListener(() => TaskSwap(7));
			xd[i,8].onClick.AddListener(() => TaskSwap(8));
			xd[i,9].onClick.AddListener(() => TaskSwap(9));
			xd[i,10].onClick.AddListener(() => TaskSwap(10));
			xd[i,11].onClick.AddListener(() => TaskSwap(11));
			xd[i,12].onClick.AddListener(() => TaskSwap(12));
			xd[i,13].onClick.AddListener(() => TaskSwap(13));
			xd[i,14].onClick.AddListener(() => TaskSwap(14));
			xd[i,15].onClick.AddListener(() => TaskSwap(15));
		}
		//xd[0].onClick.AddListener(TaskDiu);
		
		d = diu.GetComponent<Button>();
		d.onClick.AddListener(TaskDiu);
		l = liu.GetComponent<Button>();
		l.onClick.AddListener(TaskLiu);
		
		int counter     = 0; //Tile # in master mahjong set
		int suit_count  = 0; //Which suit being made
		bool honor  = false; //When to switch to Honor tiles
		bool flower = false; //When to switch to Flower tiles
		
	    //Text[] fd = {flowers, flowers_2, flowers_3, flowers_4};
		Text[] hand = {helloworld, P2, P3, P4};
		
		for(int i=0; i<35; i++){ //Which unique Tile
			if(i >= 27 && i != 34) {
				honor = true;
			    current = 0;	
			}
			
			else if(i == 34){
				honor = false;
				flower = true;
			}  
		   
		    if(i != 34) {
				for(int j=0; j<4; j++){    //4 Copies of each tile
					Mahjong_Set[counter++] = new Tile(suit3[suit_count], current%10, honor, flower, i+1);
				}
			} else {
				int k = i + 1;
				for(int j=0; j<8; j++){    //8 Flowers
					Mahjong_Set[counter++] = new Tile(suit3[suit_count], j+1, honor, flower, k++);
				}
			}						
				
			if(i < 27)
				current++;
			else
				current = 0;
			
			if(i>=27 && i!=34) {
				suit_count++;
			}
			
			if(current%10 == 0 && i<27) {
				suit_count++;
			    current = 1;	
			}
		}
		
	    Shuffle();
		for(int i=0; i<144; i++) {
		    Mahjong_SetReverse[i] = Mahjong_Set[143-i];
			//Debug.Log(Mahjong_Set[i].toString());
		}
		
		helloworld.text = "";
		P2.text = "";
	    P3.text = "";
		P4.text = "";
		flowers.text = "";
		flowers_2.text = "";
		flowers_3.text = "";
		flowers_4.text = "";
		
		
		counter = 0;
		Tile[] fl = new Tile[32];
		//int[] flstore = new int[4];
		for(int i=0; i<8; i++) { 
			ft[i].SetActive(false);
			ft2[i].SetActive(false);
			ft3[i].SetActive(false);
			ft4[i].SetActive(false);
		}
		
	//	flowerz[0,0] = f
		
		for(int j=0; j<4; j++) {
		    for(int i=0; i<16; i++) {
			    Pp[j][i] = Mahjong_Set[master++];
			    if(Pp[j][i].getFlower())
			    counter++;
		    }		
		    while(counter > 0){
			    for(int i=0; i<16; i++){
				    if(Pp[j][i].getFlower()) {
						//Debug.Log(j + " got a flower!");
						flower_count++;
						if(j == 0)  {
							ft[flcount].SetActive(true);
							flowy[flcount].sprite = images[(int)Pp[j][i].getID()-1];
						}
						else if(j == 1) {
							ft2[flcount].SetActive(true);
							flowy2[flcount].sprite = images[(int)Pp[j][i].getID()-1];
						}
						else if(j == 2) {
							ft3[flcount].SetActive(true);
							flowy3[flcount].sprite = images[(int)Pp[j][i].getID()-1];
						}
						else if(j == 3) {
							ft4[flcount].SetActive(true);
							flowy4[flcount].sprite = images[(int)Pp[j][i].getID()-1];
						}
					    fl[flcount++] = Pp[j][i];
					    Pp[j][i] = Mahjong_Set[master++];
					    while(Pp[j][i].getFlower()) {
							flower_count++;
						    if(j == 0)
							    ft[flcount].SetActive(true);
							else if(j == 1)
								ft2[flcount].SetActive(true);
							else if(j == 2)
								ft3[flcount].SetActive(true);
							else if(j == 3)
								ft4[flcount].SetActive(true);
						    fl[flcount++] = Pp[j][i];
						    Pp[j][i] = Mahjong_Set[master++];
                        }						
				    	counter--;
			    	}
			    }
		    }

			flstore[j] = flcount;
		    System.Array.Clear(fl,0,flcount);
			flcount = 0;
        }		
		
		Tile temp;
		
		for(int k=0; k<4; k++) {
			current = 0;
		    for(int i=1; i<=42; i++) {
			    for(int j=0; j<16; j++) {
				    if(Pp[k][j].getID() == i){
					    temp = Pp[k][current];
					    Pp[k][current++] = Pp[k][j];
					    Pp[k][j] = temp;
                    }					
			    }
		    }
		}
		
		
	//	Pp[3][0] = new Tile(suit3[0], 1, false, false, 1);
	//	Pp[3][1] = new Tile(suit3[0], 1, false, false, 1);
	 //   Pp[3][2] = new Tile(suit3[0], 1, false, false, 1);
	//	Pp[3][3] = new Tile(suit3[0], 1, false, false, 1);
	//	Pp[0][4] = new Tile(suit3[0], 3, false, false, 3);
	//	Pp[0][5] = new Tile(suit3[0], 3, false, false, 3);
		/*
		Pp[0][6] = new Tile(suit3[2], 2, false, false, 20);
		Pp[0][7] = new Tile(suit3[2], 3, false, false, 21);
		
		Pp[0][8] = new Tile(suit3[2], 3, false, false, 21);
		Pp[0][9] = new Tile(suit3[2], 4, false, false, 22);
		Pp[0][10] = new Tile(suit3[2], 4, false, false, 22);
		
		Pp[0][11] = new Tile(suit3[1], 5, false, false, 14);
        Pp[0][12] = new Tile(suit3[1], 6, false, false, 15);
		Pp[0][13] = new Tile(suit3[1], 7, false, false, 16);
		
		Pp[0][14] = new Tile(suit3[3], 0, true, false, 28);
		Pp[0][15] = new Tile(suit3[3], 0, true, false, 28);
		*/
		
		Mahjong_Set[master+1] = new Tile(suit3[0], 1, false, false, 1);
		//Mahjong_Set[master+6] = new Tile(suit3[0], 3, false, false, 21);
    //	Mahjong_SetReverse[0] = new Tile(suit3[0], 1, false, true, 41);
	   // Mahjong_SetReverse[1] = new Tile(suit3[0], 1, false, true, 1);
		//Mahjong_Set[78] = new Tile(suit3[0], 1, false, false, 1);
		/*
		for(int j=0; j<4; j++) {
		    for(int i=0; i<16; i++) {
		  	    hand[j].text = hand[j].text + Pp[j][i].playNum();
			    if(j == 0) {
					if(Pp[j][i].getSuit() == "萬")
						xt[i].color = Color.red;
					else if (Pp[j][i].getSuit() == "條")
						xt[i].color = Color.green;
					else if (Pp[j][i].getSuit() == "飾")
						xt[i].color = Color.blue;
					else
						xt[i].color = Color.black;
			        xt[i].text = Pp[j][i].playNum2();
				    	
				}
			}
        }
		*/
	}
	
	public static bool f = true;
	public static bool e = false;
	public static Tile htile = new Tile("", 1, false, false, 5);
	public int count = 0;
	public static int reversecounter = 0;
	public static bool lol = false;
	public static bool sup = false;
	public static bool xda = true;
	
    // Update is called once per frame
    void Update()
    {
		
		for(int j=0; j<4; j++){
			for(int i=0; i<16; i++) {
				if(Pp[j][i].getID() != 45)
					xd[j,i].image.sprite = images[(int)Pp[j][i].getID()-1]; 
				else
					xd[j,i].gameObject.SetActive(false);	
			}			
		}
		
		for(int j=0; j<4; j++) {
			for(int i=0; i<16; i++) 
				Ppd[j,i] = Pp[j][i];	
		}
		
		if(pong || kong) 
			FuncPongKong();
		
		//Hidden Kong For the Tile Drawn (Precdence over FuncAnKongInHand if both are available)
		if(hKong) 
			FuncAnKongDraw();
		
		//Hidden Kong for Kong in hand (Precedence for Rightmost Kong in hand)
		if(hKong2) 
			FuncAnKongInHand();
		
		//Chi Function
		if(chi || pongFlag > 0) 
            FuncChi();
		
		if(redraw) 
           FuncRedraw();
		
		for(int j=0; j<4; j++) {
				for(int i=0; i<16; i++) {
					xd[j,i].interactable = false;
				}
		}
		
		TurnTracker.turn = TurnTracker.turn%4;
	//	Debug.Log("Swap: " + swapping + " Toss: " + tossing + " Not Flower: " +  (!LatestDiscards.stream[LatestDiscards.counter].getFlower()) + " CE: " + (!TurnTracker.ce) + " Grace: " + (!TurnTracker.grace));
	//	Debug.Log("Active Button: " + (!TurnTracker.sets[TurnTracker.turn,4].gameObject.activeSelf) + " PONG: " + pong2 + " KONG: " + kong2 + " CHI: " + chi2 + " Pongflag=1?: " + pongFlag);
		//Hand Interactablility
		if((swapping || tossing) && (!LatestDiscards.stream[LatestDiscards.counter].getFlower() || pong2 || kong2 || chi2 || pongFlag == -1)
			&& (!TurnTracker.sets[TurnTracker.turn,4].gameObject.activeSelf || pong2 || kong2 || chi2 || pongFlag == -1) && !TurnTracker.ce && !TurnTracker.grace) {
			for(int j=0; j<4; j++) {
				for(int i=0; i<16; i++) {
					if(TurnTracker.turn == j)
						xd[j,i].interactable = true;
					else
						xd[j,i].interactable = false;
				}
			}
		} 
		
		//Toss Interactability
	//	if(!TurnTracker.sets[TurnTracker.turn, 4].gameObject.activeSelf && !LatestDiscards.stream[LatestDiscards.counter].getFlower() && !TurnTracker.onIt) 
		//	diu.interactable = true;
	  //  else 
		//	diu.interactable = false;

		
		
		 if (Input.GetKeyDown("space") && master < 143)
            master++;
	
		if(f) {
		   count = 143 - TileGenerator.master;
		   f = false;
		}
		
		e = false;
		if(count != (143 - TileGenerator.master)) 
		   e = true;	

		if(e) {
	    	Text[] hand = {helloworld, P2, P3, P4};
			clearHands(hand);
		    for(int j=0; j<4; j++) {
		        for(int i=0; i<16; i++) {
			        if(Pp[j][i].getID() != 45)
		  	            hand[j].text = hand[j].text + Pp[j][i].playNum();
		    	}
			}
			count--;
        }		
    }
	
	void FuncPongKong() {
		if(pong) {
			pong = false;
			pong2 = true;
			LatestDiscards.recordTilePong(2);
		} else {
			kong = false;
			kong2 = true;
			LatestDiscards.recordTilePong(3);
		}
	    float x = LatestDiscards.stream[LatestDiscards.counter-1].getID();
		float y = -1;
		for(int i=15; i>=0; i--) {
			if(x == Pp[TurnTracker.turn][i].getID()) {
				y = i;
			}
		}
		Pp[TurnTracker.turn][(int)y]   = new Tile("", 1, false, false, 45);
		Pp[TurnTracker.turn][(int)y+1] = new Tile("", 1, false, false, 45);

		LatestDiscards.pongCounter[TurnTracker.turn, (int) LatestDiscards.stream[LatestDiscards.counter - 1].getID()] -= 2;
		if(kong2) {
			Pp[TurnTracker.turn][(int)y+2] = new Tile("", 1, false, false, 45);
			LatestDiscards.pongCounter[TurnTracker.turn, (int) LatestDiscards.stream[LatestDiscards.counter - 1].getID()] -= 1;
			TileCenter.swift = true;
			sup = true;
		}
		Printy();
	}
	
	void FuncAnKongInHand(){
		float x = LatestDiscards.stream[LatestDiscards.counter].getID();
		float y = -1;
		for(int i=0; i<16; i++) {
			if(LatestDiscards.pongCounter[TurnTracker.turn,(int)Pp[0][i].getID()] == 4) {
				y = i;
			}
		}
		/*
		htile = new Tile(Pp[TurnTracker.turn][(int)y].getSuit(), Pp[TurnTracker.turn][(int)y].getNumber(), Pp[TurnTracker.turn][(int)y].getHonor(),Pp[TurnTracker.turn][(int)y].getFlower(),Pp[TurnTracker.turn][(int)y].getID());
		for(int i=0; i<3; i++) 
			Pp[TurnTracker.turn][(int)y-i]   = new Tile("", 1, false, false, 45);
		Pp[TurnTracker.turn][(int)y-3] = new Tile(LatestDiscards.stream[LatestDiscards.counter].getSuit(), LatestDiscards.stream[LatestDiscards.counter].getNumber(), LatestDiscards.stream[LatestDiscards.counter].getHonor(), 
								   LatestDiscards.stream[LatestDiscards.counter].getFlower(), LatestDiscards.stream[LatestDiscards.counter].getID());
		Printy();
		*/
		hKong2 = false;
		RiverTiles.isActivate = true;
		TileCenter.swift = true;
		TileCenter.swift = true;
		swapping = false;
	}
	
	void FuncAnKongDraw(){
		float x = LatestDiscards.stream[LatestDiscards.counter].getID();
		float y = -1;
		for(int i=15; i>=0; i--) {
			if(x == Pp[TurnTracker.turn][i].getID()) 
				y = i;
		}
		for(int i=0; i<3; i++) 
			Pp[TurnTracker.turn][(int)y+i]   = new Tile("", 1, false, false, 45);
		Printy();
		hKong = false;
		TileCenter.swift = true;
	}
	
	void FuncRedraw() {
		 if(gotFlower) {
				flower_count++;
                gotFlower = false;
				if(TurnTracker.turn == 0) {
					ft[flstore[TurnTracker.turn]].SetActive(true);
					flowy[flstore[TurnTracker.turn]++].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter].getID()-1];
				}					
            	else if(TurnTracker.turn == 1) {
					ft2[flstore[TurnTracker.turn]].SetActive(true);
					flowy2[flstore[TurnTracker.turn]++].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter].getID()-1];
				}		
				else if(TurnTracker.turn == 2) {
					ft3[flstore[TurnTracker.turn]].SetActive(true);
					flowy3[flstore[TurnTracker.turn]++].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter].getID()-1];
				}		
				else if(TurnTracker.turn == 3) {
					ft4[flstore[TurnTracker.turn]].SetActive(true);
					flowy4[flstore[TurnTracker.turn]++].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter].getID()-1];
				}				
			}
			//TurnTracker.sets[TurnTracker.turn,4].gameObject.SetActive(true);						
			redraw = false;
			redraw2 = true;
			
			LatestDiscards.s = true;
			LatestDiscards.update = true;
			
			multi++;
			if(multi > 1)
				reversecounter++;
			
		    //LatestDiscards.counter++;
			if(lol && xda) {
				master++;
				xda = false;
			}
			lol = false;
			kong3 = false;
	}
	
	void FuncChi() {
		int i = pongFlag;
		LatestDiscards.recordTileChi(i);
		float x = LatestDiscards.stream[LatestDiscards.counter-1].getID();
		float y = -1;
		float z = -1;
		float a = 0;
		float b = 0;
		if(i == 1){
			a = 1;
			b = 2;
		}
		else if(i==2){
			a = 1;
			b = -1;
		}
		else if (i==3){
			a = -1;
			b = -2;
		}
		for(int j=0; j<16; j++) {
			if(x+a == Pp[TurnTracker.turn][j].getID()) 
				y = j;
			if(x+b == Pp[TurnTracker.turn][j].getID())	   
				z = j;	
		}
		LatestDiscards.pongCounter[TurnTracker.turn, (int) LatestDiscards.stream[LatestDiscards.counter - 1].getID() + (int)a] -= 1;
		LatestDiscards.pongCounter[TurnTracker.turn, (int) LatestDiscards.stream[LatestDiscards.counter - 1].getID() + (int)b] -= 1;
		LatestDiscards.pongCounter[TurnTracker.turn, (int) LatestDiscards.stream[LatestDiscards.counter - 1].getID() + (int)b] -= 1;
		Pp[TurnTracker.turn][(int)y] = new Tile("", 1, false, false, 45);
		Pp[TurnTracker.turn][(int)z] = new Tile("", 1, false, false, 45);
		Printy();
		chi2 = true;
		tossing = true;
	    chi = false;	
	    pongFlag = -1;
	}
	
	void clearHands(Text[] hand) {
		for(int j=0; j<4; j++) {
		  	hand[j].text = "";	
		}
	}
	
	void TaskDiu() {
	}
	
	void TaskLiu() {
		swapping = true;
	}
	
	public static void TaskSwap(int x){
		Tile temp;

		TurnTracker.isDrawn = false;
		TurnTracker.sets[TurnTracker.turn,0].gameObject.SetActive(false);
		if(pong2 || chi2) { //Throw Out after a Pong or Chi
			LatestDiscards.stream[LatestDiscards.counter-1] = Pp[TurnTracker.turn][x];
		    Pp[TurnTracker.turn][x] = new Tile("", 1, false, false, 45);
		    InsertionSort(Pp[TurnTracker.turn]);
			tossing = false;
			LatestDiscards.counter--;
			TileCenter.swift = true;
			Action.toss = true;
			Action.swap = false;
		}
		else if(kong2) { //
			temp = Mahjong_Set[master];
			LatestDiscards.stream[LatestDiscards.counter-1] = Pp[TurnTracker.turn][x];
		    Pp[TurnTracker.turn][x] = temp;
		    InsertionSort(Pp[TurnTracker.turn]);
			LatestDiscards.counter--;
			swapping = false;
			TileCenter.swift = true;
			Action.toss = false;
			Action.swap = true;
			
		}
		else {
			temp = LatestDiscards.stream[LatestDiscards.counter];
			//Debug.Log(x);
		    LatestDiscards.stream[LatestDiscards.counter] = Pp[TurnTracker.turn][x];
		    Pp[TurnTracker.turn][x] = temp;
		    InsertionSort(Pp[TurnTracker.turn]);
		    swapping = false;
			Action.toss = false;
			Action.swap = true;
		}
		
		for(int i=0; i<16; i++) {
			/*
			if(Pp[0][i].getSuit() == "萬")
				xt[i].color = Color.red;
			else if (Pp[0][i].getSuit() == "條")
				xt[i].color = Color.green;
			else if (Pp[0][i].getSuit() == "飾")
				xt[i].color = Color.blue;
			else
				xt[i].color = Color.black;
			xt[i].text = Pp[0][i].playNum2();
			*/
			if(Pp[TurnTracker.turn][i].getID() == 45) {
			   xd[TurnTracker.turn,i].gameObject.SetActive(false);	
			}
		}
		if(!pong2 && !chi2 && !sup) {
		   if(master < 143)
			   master++;
		}
		
		pong2 = false;
		kong2 = false;
		chi2 = false;
		if(sup)
			lol = true;
		sup = false;
		xda = true;
	    if(LatestDiscards.update) {
			reversecounter++;
			LatestDiscards.update = false;
		}
		multi = 0;
		LatestDiscards.e = true;
		TurnTracker.prev_turn = TurnTracker.turn;
		TurnTracker.turn++;
		TurnTracker.turn = TurnTracker.turn%4;
		TurnTracker.newTurn = true;
		TurnTracker.newTurn2 = true;
		TurnTracker.newTurn3 = true;
	}
	
	public static void InsertionSort(Tile[] x) {
		current = 0;
		Tile temp;
		for(int i=1; i<=42; i++) {
			for(int j=0; j<16; j++) {
				if(x[j].getID() == i){
				    temp = x[current];
					x[current++] = x[j];
					x[j] = temp;
                }					
			}
		}
	}
	
	public void Shuffle() {
		for (int i = 0; i < Mahjong_Set.Length; i++) {
             int rnd = Random.Range(0, Mahjong_Set.Length);
             tempGO = Mahjong_Set[rnd];
             Mahjong_Set[rnd] = Mahjong_Set[i];
             Mahjong_Set[i] = tempGO;
         }
	}
	
	void Printy() {
		InsertionSort(Pp[TurnTracker.turn]);
		/*
		for(int i=0; i<16; i++) {
		    if(Pp[0][i].getSuit() == "萬")
			   	xt[i].color = Color.red;
		    else if (Pp[0][i].getSuit() == "條")
			    xt[i].color = Color.green;
			else if (Pp[0][i].getSuit() == "飾")
				xt[i].color = Color.blue;
			else
				xt[i].color = Color.black;
			    xt[i].text = Pp[0][i].playNum2();
			if(Pp[0][i].getID() == 45) {
			    xd[i].gameObject.SetActive(false);	
			}
			if(Pp[0][i].getID() == 1)
				xd[i].image.sprite = images[(int)Pp[0][i].getID()-1]; 
			else
				xd[i].image.sprite = images[1]; 
			
		}
		*/
	}
}
