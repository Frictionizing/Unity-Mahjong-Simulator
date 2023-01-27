using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Handles whose turn it is.

public class TurnTracker : MonoBehaviour
{
	
	public class Player {
		string compass;
		public Player(string compass) {
			this.compass  = compass;
		}
		public string getCompass(){
			return compass;
		}	
		public int getPlayer(){
				if(compass == "南")
					return 0;
				else if(compass == "東")
					return 1;
				else if(compass == "北")
					return 2;
				else
					return 3;
		}
	}
	public Button[] set1;
	public Button[] set2;
	public Button[] set3;
	public Button[] set4;
	public Text[] zt;
	public static Button[,] sets = new Button[4,6];
    public Button chi;
	Button c;
	public Button pong;
	Button p;
	public Button kong;
	Button k;
	public Button diu;
	Button d;
	public Button liu;
	Button l;
	public Button tile;
	public Button hidden;
	Button h;
	public static Button t;
	Text[] x = new Text[4];
	public static int turn;
	public int turnID;
	public static int ID = -1;
	public static Text current;
	public string[] honors = {"南", "東", "北", "西"};
	public static Player nan = new Player("南");
	public static bool onIt = false;
	public static bool grace = false;
	public static bool newGrace = false;
	public static bool hidden3 = true;
	public static bool ce = false;
	public static bool turnone = true;
	public static bool newTurn = false;
	public static bool newTurn2 = false;
	public static bool newTurn3 = false;
	public static bool showcaseTile = false;
	public int tp = 0;
	public int wrap = 0;
	public static int prev_turn = 0;

	public static bool backDraw;
	public static bool isDrawn;

	private static IEnumerator coroutine;
	
    // Start is called before the first frame update
    void Start()
    {
		isDrawn = false;
		backDraw = false;
		turnone = true;
		newTurn = false;
		newTurn2 = false;
		newTurn3 = false;
		showcaseTile = false;
		tp = 0;
		wrap = 0;
		
		turn = Random.Range(0,0);
        current = GetComponent<Text>();
		ID = -1;
		

		for(int i=0; i<6; i++) {
			sets[0,i] = set1[i].GetComponent<Button>();
			sets[1,i] = set2[i].GetComponent<Button>();
			sets[2,i] = set3[i].GetComponent<Button>();
			sets[3,i] = set4[i].GetComponent<Button>();
		}
		
		for(int i=0; i<4; i++) {
			sets[i,0].onClick.AddListener(TaskRedraw);
			sets[i,3].onClick.AddListener(TaskDiu);
			sets[i,4].onClick.AddListener(TaskTile);
			sets[i,5].onClick.AddListener(TaskHidden);
			x[i] = sets[i,4].GetComponentInChildren<Text>();
		}
			sets[0,1].onClick.AddListener(() => TaskPong(0));
			sets[0,2].onClick.AddListener(() => TaskKong(0));
			sets[1,1].onClick.AddListener(() => TaskPong(1));
			sets[1,2].onClick.AddListener(() => TaskKong(1));
			sets[2,1].onClick.AddListener(() => TaskPong(2));
			sets[2,2].onClick.AddListener(() => TaskKong(2));
			sets[3,1].onClick.AddListener(() => TaskPong(3));
			sets[3,2].onClick.AddListener(() => TaskKong(3));
		
		for(int i=0; i<4; i++) 
			sets[i,0].gameObject.SetActive(false);
		//x = tile.GetComponentInChildren<Text>();
		/*
		c = chi.GetComponent<Button>();
		c.onClick.AddListener(TaskRedraw);
		p = pong.GetComponent<Button>();
		p.onClick.AddListener(TaskPong);
		k = kong.GetComponent<Button>();
		k.onClick.AddListener(TaskKong);
		d = diu.GetComponent<Button>();
		d.onClick.AddListener(TaskDiu);
	    t = tile.GetComponent<Button>();
		t.onClick.AddListener(TaskTile);
		h = hidden.GetComponent<Button>();
		h.onClick.AddListener(TaskHidden);
		//x = tile.GetComponentInChildren<Text>();
		l = liu.GetComponent<Button>();
		l.onClick.AddListener(TaskLiu);
		*/
		ID = turnID;
    }

    // Update is called once per frame
    void Update()
    {   
	/*
	0 - Redraw
	1 - Pong
	2 - Kong
	3 - Discard
	4 - Draw Tile
	5 - Hidden Kong
	*/
     //   current.text = honors[turn];
		turn = turn%4;
		if (Input.GetKeyDown("space"))
        {
            turn++;
			turn = turn%4;
			newTurn = true;
			turnone = false;
			newTurn2 = true;
			newTurn3 = true;
        }
		
		for(int i=0; i<4; i++)
			zt[i].text = TurnTracker.turn+1 +"";
		
		if(turnone) {
			for(int i=0; i<4; i++) {
				sets[i,1].interactable = false;
				sets[i,2].interactable = false;
			}
		}
		
		for(int i=0; i<4; i++) {
			if((!sets[i,4].gameObject.activeSelf && !(LatestDiscards.stream[LatestDiscards.counter].getFlower())) && i == turn && !WinCondition.endGame)  
				sets[i,3].interactable = true;			
			else 
				sets[i,3].interactable = false;
		}
		
		if(!turnone) {
			int modul = TurnTracker.turn-1;
			if(TurnTracker.turn == 0)
				modul = 3;
			for(int i=0; i<4; i++) {
				if(LatestDiscards.interact[i] && sets[turn,4].gameObject.activeSelf && !WinCondition.endGame && i != modul) 
					sets[i,1].interactable = true;		
				else
					sets[i,1].interactable = false;	
			}				
			for(int i=0; i<4; i++) {
				if(LatestDiscards.interact2[i] && sets[turn,4].gameObject.activeSelf && !WinCondition.endGame  && i != modul)
					sets[i,2].interactable = true;		
				else
					sets[i,2].interactable = false;		
			}
		}
		
		for(int i=0; i<4; i++) {
			if((!WinCondition.endGame && ((LatestDiscards.stream[LatestDiscards.counter].getFlower() && !sets[i,4].gameObject.activeSelf && !newGrace) || onIt) || (TileGenerator.kong2 && i == turn) )) {
				sets[i,0].interactable = true;
			}
			else
				sets[i,0].interactable = false;	
		}
			
		for(int i=0; i<4; i++) {
			if(!WinCondition.endGame && !sets[i,4].gameObject.activeSelf && (LatestDiscards.pongCounter[TurnTracker.turn, (int) LatestDiscards.stream[LatestDiscards.counter].getID()] + 1 >= 4 || LatestDiscards.kongHand) && !onIt && !LatestDiscards.stream[LatestDiscards.counter].getFlower()) {
				sets[i,5].interactable = true;	
			} else
				sets[i,5].interactable = false;	
		}
		
		for(int i=0; i<4; i++) {
			if((!TileGenerator.tossing  && !TurnTracker.grace && i == turn &&!TileGenerator.kong2))  {
				sets[i,4].interactable = true;	
				x[i].color = Color.white;
			} else {
				sets[i,4].gameObject.SetActive(true);
				sets[i,4].interactable = false;
				x[i].color = Color.red;
			}
		}
		
		for(int i=0; i<4; i++) {
			if(sets[i, 0].interactable)
				ce = true;
			else
				ce = false;
		}
    }
    
	static void TaskRedraw() {
		
		sets[turn,0].gameObject.SetActive(false);
		TileGenerator.redraw = true;
		if(TileGenerator.kong2) {
			TileGenerator.kong2 = false;
			TileGenerator.kong3 = true;
		}
		if(LatestDiscards.stream[LatestDiscards.counter].getFlower())
			TileGenerator.gotFlower = true;
		TileGenerator.lol = true;
		TileGenerator.sup = true;
		onIt = false;
		backDraw = true;
		TaskTile();
		
	}
	
	public static void TaskDiu() {
		sets[turn,0].gameObject.SetActive(false);
		TurnTracker.prev_turn = TurnTracker.turn;
		turn++;
		turn = turn%4;
		TurnTracker.newTurn2 = true;
		TurnTracker.newTurn3 = true;
		turnone = false;
		TileGenerator.swapping = false;
		TileGenerator.tossing = false;
		TileGenerator.sup = false;
		if(LatestDiscards.update) {
			TileGenerator.reversecounter++;
			LatestDiscards.update = false;
		} else
			TileGenerator.master++;
		TurnTracker.newTurn = true;
		TileGenerator.xda = true;
		TileGenerator.multi = 0;
		Action.toss = true;
		Action.swap = false;

		isDrawn = false;
	}
	
	void TaskLiu() {
	}
	
	public static void TaskHidden() {
		if(LatestDiscards.pongCounter[TurnTracker.turn, (int) LatestDiscards.stream[LatestDiscards.counter].getID()] + 1 >= 4) {
			TileGenerator.hKong = true;
		     RiverTiles.Hidden     = true;
		}
		else {
			TileGenerator.hKong2 = true;
			 RiverTiles.Hidden2     = true;
		}
		RiverTiles.isActivate = true;
		onIt = true;
		hidden3 = true;
	}
		
	public static void TaskPong(int i) {
		turn = i;
		TileGenerator.pong = true;
		TileGenerator.tossing = true;
		RiverTiles.isActivate = true;
	}
	
	public static void TaskKong(int i) {
		turn = i;
		TileGenerator.kong = true;
		TileGenerator.redraw = true;
		RiverTiles.isActivate = true;
	}
	
	public static void TaskTile() {
		isDrawn = true;
		if(LatestDiscards.stream[LatestDiscards.counter].getFlower()) {
			Debug.Log(LatestDiscards.counter);
			showcaseTile = true;
			sets[turn,0].gameObject.SetActive(true);
		}

		if(backDraw)
			backDraw = false;
		else
			LatestDiscards.recordPile();

		Remaining.counter++;
		for(int i=0; i< 4; i++){
			sets[i,4].gameObject.SetActive(false);
			TileGenerator.swapping = true;
		}
	}
}
