using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiverTiles : MonoBehaviour
{
	public GameObject[] river;
	public Text[] xtex;
	public GameObject[] OpRiver1;
	public GameObject[] OpRiver2;
	public GameObject[] OpRiver3;
	public GameObject[] OpRiver4;
	//public static int fl

	
	public static GameObject[,] list2  = new GameObject[3,5];
	public static GameObject[,,] mat2  = new GameObject[3, 5, 4];
	public static       Text[,,] x2    = new Text[3, 5, 4];
	public static     Image[,,] st2   = new Image[3, 5, 4];
	
	public static GameObject[,] list3  = new GameObject[3,5];
	public static GameObject[,,] mat3  = new GameObject[3, 5, 4];
	public static       Text[,,] x3    = new Text[3, 5, 4];
	public static     Image[,,] st3   = new Image[3, 5, 4];
	
	public static GameObject[,] list4  = new GameObject[3,5];
	public static GameObject[,,] mat4  = new GameObject[3, 5, 4];
	public static       Text[,,] x4   = new Text[3, 5, 4];
	public static     Image[,,] st4   = new Image[3, 5, 4];
	
	public static GameObject[,] list5  = new GameObject[3,5];
	public static GameObject[,,] mat5  = new GameObject[3, 5, 4];
	public static       Text[,,] x5   = new Text[3, 5, 4];
	public static     Image[,,] st5   = new Image[3, 5, 4];
	
	
	public static GameObject[,] list  = new GameObject[4,5];
	public static GameObject[,,] mat  = new GameObject[4, 5, 4];
	//public static       Text[,,] x    = new Text[4, 5, 4];
	public static     Image[,,] st   = new Image[4, 5, 4];
	public static int[] rivercount     = {0, 0, 0, 0};
	public static bool isActivate    = false;
	public static bool isKong        = false;
	public static bool Chi1          = false;
	public static bool Chi2          = false;
	public static bool Chi3          = false;
	public static bool Hidden        = false;
	public static bool Hidden2       = false;
	public Sprite[] images = new Sprite[43];
	public static Sprite[] images2 = new Sprite[43];
    // Start is called before the first frame update
    void Start()
    {

		for(int i=0; i<43; i++){
			images2[i] = images[i];
		}

		isActivate = false;
		isKong = false;
		Chi1 = false;
		Chi2 = false;
		Chi3 = false;
		Hidden =  false;
		Hidden2 = false;
		for(int i=0; i<4; i++)
			rivercount[i] = 0;
		
		
		for(int k=0; k<4; k++) {
		for(int i=0; i<5; i++) {			
			list[k,i] = river[k].transform.GetChild(i).gameObject;
			list[k,i].gameObject.SetActive(false);
		    for(int j=0; j<4; j++) {
			    mat[k,i,j] = list[k,i].transform.GetChild(j).gameObject;
				st[k,i,j]  = mat[k,i,j].GetComponent<Image>();
			}
			mat[k,i,3].gameObject.SetActive(false);
	    }
		}
		
		for(int k=0; k<3; k++){
			for(int i=0; i<5; i++){
				list2[k,i] = OpRiver1[k].transform.GetChild(i).gameObject;
				list2[k,i].gameObject.SetActive(false);
				for(int j=0; j<4; j++) {
					mat2[k,i,j] = list2[k,i].transform.GetChild(j).gameObject;
		//			x2[k,i,j]   = mat2[k,i,j].GetComponentInChildren<Text>();
					st2[k,i,j]  = mat2[k,i,j].GetComponent<Image>();
					if(j==3){
						st2[k,i,j].gameObject.SetActive(false);
					}
		//			x2[k,i,j].text = "";
		//			x2[k,i,j].color = Color.black;
				}
			}
		}
		
		for(int k=0; k<3; k++){
			for(int i=0; i<5; i++){
				list3[k,i] = OpRiver2[k].transform.GetChild(i).gameObject;
				list3[k,i].gameObject.SetActive(false);
				for(int j=0; j<4; j++) {
					mat3[k,i,j] = list3[k,i].transform.GetChild(j).gameObject;
					x3[k,i,j]   = mat3[k,i,j].GetComponentInChildren<Text>();
					st3[k,i,j]  = mat3[k,i,j].GetComponent<Image>();
					if(j==3){
						st3[k,i,j].gameObject.SetActive(false);
					}
	//				x3[k,i,j].text = "";
	//				x3[k,i,j].color = Color.black;
				}
			}
		}
		
		for(int k=0; k<3; k++){
			for(int i=0; i<5; i++){
				list4[k,i] = OpRiver3[k].transform.GetChild(i).gameObject;
				list4[k,i].gameObject.SetActive(false);
				for(int j=0; j<4; j++) {
					mat4[k,i,j] = list4[k,i].transform.GetChild(j).gameObject;
					x4[k,i,j]   = mat4[k,i,j].GetComponentInChildren<Text>();
					st4[k,i,j]  = mat4[k,i,j].GetComponent<Image>();
					if(j==3){
						st4[k,i,j].gameObject.SetActive(false);
					}
				}
			}
		}
		
		for(int k=0; k<3; k++){
			for(int i=0; i<5; i++){
				list5[k,i] = OpRiver4[k].transform.GetChild(i).gameObject;
				list5[k,i].gameObject.SetActive(false);
				for(int j=0; j<4; j++) {
					mat5[k,i,j] = list5[k,i].transform.GetChild(j).gameObject;
					x5[k,i,j]   = mat5[k,i,j].GetComponentInChildren<Text>();
					st5[k,i,j]  = mat5[k,i,j].GetComponent<Image>();
					if(j==3){
						st5[k,i,j].gameObject.SetActive(false);
					}
				//	x5[k,i,j].text = "";
				//	x5[k,i,j].color = Color.black;
				}
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
		for(int i=0; i<3; i++) {
			xtex[i].text = "花:" + TileGenerator.flstore[0];
			xtex[3+i].text = "花:" + TileGenerator.flstore[1];
			xtex[6+i].text = "花:" + TileGenerator.flstore[2];
			xtex[9+i].text = "花:" + TileGenerator.flstore[3];
		}
	
	    if(isActivate){
			list[TurnTracker.turn, rivercount[TurnTracker.turn]].gameObject.SetActive(true);
			if(TurnTracker.turn == 3) {
				for(int i=0; i<3; i++) 
					list2[i,rivercount[TurnTracker.turn]].gameObject.SetActive(true);
			}
			
			if(TurnTracker.turn == 2) {
				for(int i=0; i<3; i++) 
					list3[i,rivercount[TurnTracker.turn]].gameObject.SetActive(true);
			}
			
			if(TurnTracker.turn == 1) {
				for(int i=0; i<3; i++) 
					list4[i,rivercount[TurnTracker.turn]].gameObject.SetActive(true);
			}
			
			if(TurnTracker.turn == 0) {
				for(int i=0; i<3; i++) 
					list5[i,rivercount[TurnTracker.turn]].gameObject.SetActive(true);
			}
			
			isActivate = false;
			float IDchange = -1;
			if(LatestDiscards.counter > 0) {
				IDchange = (LatestDiscards.stream[LatestDiscards.counter - 1].getID() % 10) + (int)((LatestDiscards.stream[LatestDiscards.counter - 1].getID() / 10));
			    if(LatestDiscards.stream[LatestDiscards.counter - 1].getID() == 19)
				    IDchange = 1;
			}
			
			if(Chi1){
				/*
				x[rivercount, 0].text = (IDchange - 2) + "" + LatestDiscards.stream[LatestDiscards.counter - 1].getSuit();
				x[rivercount, 1].text = (IDchange - 1) + "" + LatestDiscards.stream[LatestDiscards.counter - 1].getSuit();
				x[rivercount, 2].text =  IDchange      + "" + LatestDiscards.stream[LatestDiscards.counter - 1].getSuit();
				*/

				//st2[0,rivercount[TurnTracker.turn],3].sprite = images[4];

				st[TurnTracker.turn, rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 3]; 
				st[TurnTracker.turn, rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 2]; 
				st[TurnTracker.turn, rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1]; 
				
				if(TurnTracker.turn == 3) {
					for(int i=0; i<3; i++) {
						st2[i,rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 3]; 
						st2[i,rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 2]; 
						st2[i,rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1];
					}
				}
				
				if(TurnTracker.turn == 2) {
					for(int i=0; i<3; i++) {
						st3[i,rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 3]; 
						st3[i,rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 2]; 
						st3[i,rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1];
					}
				}
				
				if(TurnTracker.turn == 1) {
					for(int i=0; i<3; i++) {
						st4[i,rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 3]; 
						st4[i,rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 2]; 
						st4[i,rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1];
					}
				}
				
				if(TurnTracker.turn == 0) {
					for(int i=0; i<3; i++) {
						st5[i,rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 3]; 
						st5[i,rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 2]; 
						st5[i,rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1];
					}
				}
				
				Chi1 = false;
			}
			else if(Chi2) {
				/*
				x[rivercount, 0].text = (IDchange - 1) + "" + LatestDiscards.stream[LatestDiscards.counter - 1].getSuit();
				x[rivercount, 1].text = (IDchange + 1) + "" + LatestDiscards.stream[LatestDiscards.counter - 1].getSuit();
				x[rivercount, 2].text =  IDchange      + "" + LatestDiscards.stream[LatestDiscards.counter - 1].getSuit();
				*/
				st[TurnTracker.turn, rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 2]; 
				st[TurnTracker.turn, rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID()]; 
				st[TurnTracker.turn, rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1]; 
				
				if(TurnTracker.turn == 3) {
					for(int i=0; i<3; i++) {
						st2[i,rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 2]; 
						st2[i,rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID()]; 
						st2[i,rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1];
					}
				}
				
				if(TurnTracker.turn == 2) {
					for(int i=0; i<3; i++) {
						st3[i,rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 2]; 
						st3[i,rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID()]; 
						st3[i,rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1];
					}
				}
				
				if(TurnTracker.turn == 1) {
					for(int i=0; i<3; i++) {
						st4[i,rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 2]; 
						st4[i,rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID()]; 
						st4[i,rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1];
					}
				}
				
				if(TurnTracker.turn == 0) {
					for(int i=0; i<3; i++) {
						st5[i,rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 2]; 
						st5[i,rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID()]; 
						st5[i,rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1];
					}
				}
				
				Chi2 = false;
			}
			else if(Chi3) {
				/*
				x[rivercount, 0].text = (IDchange + 1) + "" + LatestDiscards.stream[LatestDiscards.counter - 1].getSuit();
				x[rivercount, 1].text = (IDchange + 2) + "" + LatestDiscards.stream[LatestDiscards.counter - 1].getSuit();
				x[rivercount, 2].text =  IDchange      + "" + LatestDiscards.stream[LatestDiscards.counter - 1].getSuit();
				*/
				st[TurnTracker.turn, rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID()]; 
				st[TurnTracker.turn, rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() + 1]; 
				st[TurnTracker.turn, rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1]; 
				
				if(TurnTracker.turn == 3) {
					for(int i=0; i<3; i++) {
						st2[i,rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID()]; 
						st2[i,rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() + 1]; 
						st2[i,rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1];
					}
				}
				
				if(TurnTracker.turn == 2) {
					for(int i=0; i<3; i++) {
						st3[i,rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID()]; 
						st3[i,rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() + 1]; 
						st3[i,rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1];
					}
				}
				
				if(TurnTracker.turn == 1) {
					for(int i=0; i<3; i++) {
						st4[i,rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID()]; 
						st4[i,rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() + 1]; 
						st4[i,rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1];
					}
				}
				
				if(TurnTracker.turn == 0) {
					for(int i=0; i<3; i++) {
						st5[i,rivercount[TurnTracker.turn],0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID()]; 
						st5[i,rivercount[TurnTracker.turn],1].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() + 1]; 
						st5[i,rivercount[TurnTracker.turn],2].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1];
					}
				}
				
				Chi3 = false;
			}
			else if(Hidden) {
				//color2();
				IDchange = (LatestDiscards.stream[LatestDiscards.counter].getID() % 10) + (int)((LatestDiscards.stream[LatestDiscards.counter].getID() / 10));
				if(LatestDiscards.stream[LatestDiscards.counter].getID() == 19)
					IDchange = 1;
				//if(!LatestDiscards.stream[LatestDiscards.counter].getHonor())
				//	x[rivercount, 0].text = IDchange + "" + LatestDiscards.stream[LatestDiscards.counter].getSuit();
				//else
					//x[rivercount, 0].text = " " + LatestDiscards.stream[LatestDiscards.counter].getSuit();
				
				for(int i=0; i<4; i++) {
					st[TurnTracker.turn, rivercount[TurnTracker.turn],i].sprite = images[42]; 
				//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color  = Color.black;
				//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].text = " 暗";
					
					if(TurnTracker.turn ==3){
						for(int j=0; j<3; j++) {
							st2[j,rivercount[TurnTracker.turn],i].sprite = images[42]; 
					//		x2[j,rivercount[TurnTracker.turn], i].color  = Color.black;
					//		x2[j,rivercount[TurnTracker.turn], i].text = " 暗";	
						}
					}
					
					if(TurnTracker.turn == 2) {
						for(int j=0; j<3; j++) {
							st3[j,rivercount[TurnTracker.turn],i].sprite = images[42]; 
					//		x3[j,rivercount[TurnTracker.turn], i].color  = Color.black;
					//		x3[j,rivercount[TurnTracker.turn], i].text = " 暗";	
						}
					}
					
					if(TurnTracker.turn == 1) {
						for(int j=0; j<3; j++) {
							st4[j,rivercount[TurnTracker.turn],i].sprite = images[42]; 
						//	x4[j,rivercount[TurnTracker.turn], i].color  = Color.black;
						//	x4[j,rivercount[TurnTracker.turn], i].text = " 暗";	
						}
					}
					
					if(TurnTracker.turn == 0) {
						for(int j=0; j<3; j++) {
							st5[j,rivercount[TurnTracker.turn],i].sprite = images[42]; 
						//	x5[j,rivercount[TurnTracker.turn], i].color  = Color.black;
						//	x5[j,rivercount[TurnTracker.turn], i].text = " 暗";	
						}
					}
					
				}
				st[TurnTracker.turn, rivercount[TurnTracker.turn], 0].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter].getID() - 1];
				//x[TurnTracker.turn, rivercount[TurnTracker.turn], 0].text = "";
				mat[TurnTracker.turn, rivercount[TurnTracker.turn] ,3].gameObject.SetActive(true);
				Hidden = false;
				for(int j=0; j<3; j++) {
						st2[j,rivercount[TurnTracker.turn],3].gameObject.SetActive(true);
						st3[j,rivercount[TurnTracker.turn],3].gameObject.SetActive(true); 
						st4[j,rivercount[TurnTracker.turn],3].gameObject.SetActive(true); 
						st5[j,rivercount[TurnTracker.turn],3].gameObject.SetActive(true);  
					}
			}
			else if(Hidden2) {
				//color3();
				for(int i=0; i<4; i++) {
					st[TurnTracker.turn, rivercount[TurnTracker.turn], i].sprite = images[42]; 
				//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color  = Color.black;
				//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].text = " 暗";
					
					if(TurnTracker.turn == 3){
						for(int j=0; j<3; j++) {
							st2[j,rivercount[TurnTracker.turn],i].sprite = images[42]; 
					//		x2[j,rivercount[TurnTracker.turn], i].color  = Color.black;
					//		x2[j,rivercount[TurnTracker.turn], i].text = " 暗";	
						}
					}
					if(TurnTracker.turn == 2){
						for(int j=0; j<3; j++) {
							st3[j,rivercount[TurnTracker.turn],i].sprite = images[42]; 
						//	x3[j,rivercount[TurnTracker.turn], i].color  = Color.black;
						//	x3[j,rivercount[TurnTracker.turn], i].text = " 暗";	
						}
					}
					if(TurnTracker.turn == 1){
						for(int j=0; j<3; j++) {
							st4[j,rivercount[TurnTracker.turn],i].sprite = images[42]; 
						//	x4[j,rivercount[TurnTracker.turn], i].color  = Color.black;
						//	x4[j,rivercount[TurnTracker.turn], i].text = " 暗";	
						}
					}
					if(TurnTracker.turn == 0){
						for(int j=0; j<3; j++) {
							st5[j,rivercount[TurnTracker.turn],i].sprite = images[42]; 
						//	x5[j,rivercount[TurnTracker.turn], i].color  = Color.black;
						//	x5[j,rivercount[TurnTracker.turn], i].text = " 暗";	
						}
					}
					for(int j=0; j<3; j++) {
						st2[j,rivercount[TurnTracker.turn],3].gameObject.SetActive(true);
						st3[j,rivercount[TurnTracker.turn],3].gameObject.SetActive(true); 
						st4[j,rivercount[TurnTracker.turn],3].gameObject.SetActive(true); 
						st5[j,rivercount[TurnTracker.turn],3].gameObject.SetActive(true);  
					}
					
				}
				st[TurnTracker.turn, rivercount[TurnTracker.turn],0].sprite = images[(int)TileGenerator.htile.getID()-1];
				//x[TurnTracker.turn, rivercount[TurnTracker.turn], 0].text = "";
				mat[TurnTracker.turn, rivercount[TurnTracker.turn],3].gameObject.SetActive(true);
				Hidden2 = false;

			}
			else {
			    for(int i=0; i<4; i++) {
					st[TurnTracker.turn, rivercount[TurnTracker.turn],i].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1]; 
				//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].text = "";
					
					if(TurnTracker.turn == 3) {
						for(int j=0; j<3; j++) 
							st2[j,rivercount[TurnTracker.turn],i].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1]; 
					}
					if(TurnTracker.turn == 2) {
						for(int j=0; j<3; j++) 
							st3[j,rivercount[TurnTracker.turn],i].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1]; 
					}
					if(TurnTracker.turn == 1) {
						for(int j=0; j<3; j++) 
							st4[j,rivercount[TurnTracker.turn],i].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1]; 
					}
					if(TurnTracker.turn == 0) {
						for(int j=0; j<3; j++) 
							st5[j,rivercount[TurnTracker.turn],i].sprite = images[(int)LatestDiscards.stream[LatestDiscards.counter - 1].getID() - 1]; 
					}
				    //x[rivercount, i].text = LatestDiscards.stream[LatestDiscards.counter - 1].playNum2();
	            }
				if(TileGenerator.kong2){
					isKong = false;
					for(int j=0; j<3; j++) {
						st2[j,rivercount[TurnTracker.turn],3].gameObject.SetActive(true);
						st3[j,rivercount[TurnTracker.turn],3].gameObject.SetActive(true); 
						st4[j,rivercount[TurnTracker.turn],3].gameObject.SetActive(true); 
						st5[j,rivercount[TurnTracker.turn],3].gameObject.SetActive(true);  
					}
				}
				/*
			    if(TileGenerator.kong2){
				    isKong = false;
				    mat[TurnTracker.turn, rivercount[TurnTracker.turn],3].gameObject.SetActive(true);
					if(TurnTracker.turn == 3) {
						for(int j=0; j<3; j++) {
							st2[j,rivercount[TurnTracker.turn], 0].sprite = images[42]; 
							st2[j,rivercount[TurnTracker.turn], 2].sprite = images[42];
						//	x2[j,rivercount[TurnTracker.turn], 0].text = " 槓";
						//	x2[j,rivercount[TurnTracker.turn], 2].text = " 槓";
						}
					}
					if(TurnTracker.turn == 2) {
						for(int j=0; j<3; j++) {
							st3[j,rivercount[TurnTracker.turn], 0].sprite = images[42]; 
							st3[j,rivercount[TurnTracker.turn], 2].sprite = images[42];
						//	x3[j,rivercount[TurnTracker.turn], 0].text = " 槓";
						//	x3[j,rivercount[TurnTracker.turn], 2].text = " 槓";
						}
					}
					if(TurnTracker.turn == 1) {
						for(int j=0; j<3; j++) {
							st4[j,rivercount[TurnTracker.turn], 0].sprite = images[42]; 
							st4[j,rivercount[TurnTracker.turn], 2].sprite = images[42];
						//	x4[j,rivercount[TurnTracker.turn], 0].text = " 槓";
						//	x4[j,rivercount[TurnTracker.turn], 2].text = " 槓";
						}
					}
					if(TurnTracker.turn == 0) {
						for(int j=0; j<3; j++) {
							st5[j,rivercount[TurnTracker.turn], 0].sprite = images[42]; 
							st5[j,rivercount[TurnTracker.turn], 2].sprite = images[42];
						//	x5[j,rivercount[TurnTracker.turn], 0].text = " 槓";
						//	x5[j,rivercount[TurnTracker.turn], 2].text = " 槓";
						}
					}
					*/
			    //}
			}
            rivercount[TurnTracker.turn]++;			
		}
		
		//st[0,0] = images[0]; 
    }
	/*
	void color(){
		for(int i=0; i<4; i++)
			if(LatestDiscards.stream[LatestDiscards.counter - 1].getSuit() == "萬")
			//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color = Color.red;
			else if (LatestDiscards.stream[LatestDiscards.counter - 1].getSuit() == "條")
			//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color = Color.green;
			else if (LatestDiscards.stream[LatestDiscards.counter - 1].getSuit() == "飾")
			//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color = Color.blue;
			else
			//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color = Color.black;	
	}
	void color2(){
		for(int i=0; i<4; i++)
			if(LatestDiscards.stream[LatestDiscards.counter].getSuit() == "萬")
			//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color = Color.red;
			else if (LatestDiscards.stream[LatestDiscards.counter].getSuit() == "條")
			//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color = Color.green;
			else if (LatestDiscards.stream[LatestDiscards.counter].getSuit() == "飾")
			//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color = Color.blue;
			else
			//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color = Color.black;	
	}
	void color3(){
		for(int i=0; i<4; i++)
			if(TileGenerator.htile.getSuit() == "萬")
			//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color = Color.red;
			else if (TileGenerator.htile.getSuit() == "條")
			//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color = Color.green;
			else if (TileGenerator.htile.getSuit() == "飾")
			//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color = Color.blue;
			else
			//	x[TurnTracker.turn, rivercount[TurnTracker.turn], i].color = Color.black;	
	}
	*/
}
