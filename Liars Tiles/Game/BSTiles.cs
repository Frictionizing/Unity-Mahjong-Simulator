using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Generates and shuffles tiles

public class BSTiles : MonoBehaviour
{
//	private string[] suit3 = {"萬", "飾", "條", "東", "南", "西", "北", "發", "中", "白", "花"};
	private string[] suit3 = {"Number", "Circle", "Bamboo", "East", "South", "West", "North", "Fa", "Zhong", "Bai", "Flower"};
	public  int startNum ;

	public class Tile {
		string suit;
		int number;
		bool isHonor;
		bool isFlower;
		int id;
		float TileNum;
		Sprite spr;
		
		public Tile(string suit, int number, bool isHonor, bool isFlower, int id, float TileNum, Sprite spr) {
			this.suit     = suit;
			this.number   = number;
			this.isHonor  = isHonor;
			this.isFlower = isFlower;
			this.id       = id;
			this.TileNum  = TileNum;
			this.spr      = spr;
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
		public int getID(){
			return id;
		}
		public float getTileNum(){
			return TileNum;
		}
		public Sprite getSprite(){
			return spr;
		}
		
		public string toString(){
			if(!getHonor())
				return "[" + this.getSuit() + ": " + this.getNumber() + ", TileNum: " + this.getTileNum() + "]";
			else
				return "[" + this.getSuit() +", TileNum: " + this.getTileNum() + "]";
		}
		
		public string playNum(){
			if (!this.getHonor())
				return " [" + this.getNumber() + this.getSuit() + "] ";
			else {
				return " [ " + this.getSuit() + " ] ";
			}	
		}
		
		public string playNum2(){
			if (!this.getHonor())
				return this.getNumber() + this.getSuit();
			else {
				return " " + this.getSuit() + " ";
			}	
		}
	}

	public static Tile[] Mahjong_Set = new Tile[144];
	public static Queue<Tile> Q_Mahjong;
	public static Stack<Tile> S_Mahjong;
	
    // Start is called before the first frame update
    void Start()
    {
  		Tile[,] Players = new Tile[PhotonNetwork.playerList.Length , startNum];

    	int current     = 1; //Resets to 1 after 9 for every suit
    	int counter     = 1; //Tile # in master mahjong set
		int suit_count  = 0; //Which suit being made

		//CreateSuits(1, 28, false, false);
		//CreateSuits(28, 35, true, false);
		//CreateSuits(35, 43, false, true);

		for(int i=1; i<28; i++)
		{
			for(int j=0; j<4; j++)
				Mahjong_Set[counter-1] = new Tile(suit3[suit_count], current%10, false, false, i, counter++, TileSet.tile[i]);

			current++;
			if(current%10 == 0){
				suit_count++;
				current = 1;
			}
		}

		for(int i=28; i<35; i++)
		{
			for(int j=0; j<4; j++)
				Mahjong_Set[counter-1] = new Tile(suit3[suit_count], 0, true, false, i, counter++, TileSet.tile[i]);
			suit_count++;
		}

		for(int i=35; i<43; i++) {
			Mahjong_Set[counter-1] = new Tile(suit3[suit_count], i-34, false, true, i, counter++, TileSet.tile[i]);
		}

	
	//	for(int i=0; i<Mahjong_Set.Length; i++)
	//		Debug.Log(Mahjong_Set[i].playNum());
	}

	public static Queue<Tile> Shuffle(int l) {
		Tile tempGO;
		Random.InitState(l);
		for (int i = 0; i < Mahjong_Set.Length; i++) {
             int rnd = Random.Range(0, Mahjong_Set.Length);
             tempGO = Mahjong_Set[rnd];
             Mahjong_Set[rnd] = Mahjong_Set[i];
             Mahjong_Set[i] = tempGO;
         }
         Q_Mahjong = new Queue<Tile>(Mahjong_Set);
         return Q_Mahjong;
	}
/*
	private Tile CreateTile(int i, bool x, bool y)
	{
		if(x)
			return new Tile(suit3[suit_count], 0, x, y, i, counter++, TileSet.tile[i]);
		else if(y)
			return new Tile(suit3[suit_count], i-34, x, y, i, counter++, TileSet.tile[i]);
		else
			return new Tile(suit3[suit_count], current%10, x, y, i, counter++, TileSet.tile[i]);
	}

	//A min range, B max range, x isHonor, y isFlower
	private void CreateSuits(int a, int b, bool x, bool y)
	{
		if(!y){
			for(int i=a; i<b; i++)
			{
				for(int j=0; j<4; j++)
					Mahjong_Set[counter-1] = CreateTile(i, x, y);

				current++;
				if(current%10 == 0 || i>27){
					suit_count++;
					current = 1;
				}
			}
		}
		else
		{
			for(int j=0; j<8; j++)
				Mahjong_Set[counter-1] = CreateTile(i, x, y);
		}
	}
	*/
}
