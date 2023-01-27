using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BSTurnTracker : Photon.MonoBehaviour
{
	public static int max = 6;
	private int counter;
	public new PhotonView photonView;
	public static Text[] names = new Text[6];
	public Text[] list;

	public Text test;
	Text t;

	public Button confirm;
	Button b;

	public GameObject predict;

	public Image[] markers;
	public static Queue<BSTiles.Tile> Set;
	public static int ID;
	public static int turn;
	public static int players;
	public static int seed;
	public static bool start = false;

    // Start is called before the first frame update
    void Start()
    {
		for(int i=0; i<max; i++)
		{
			list[i].text = "";
			markers[i].enabled = false;
		}

		t = test.GetComponent<Text>();
		b = confirm.GetComponent<Button>();
		b.onClick.AddListener(TaskTurn);
    }

    // Update is called once per frame
    void Update()
    {
    	if(start)
    	{
    		start = !start;
    		Initialize();
    		DisplayedTiles.start = true;
    	}

    	if(ID != turn){
    		predict.SetActive(false);
    	}
    	else
    	{
    		predict.SetActive(true);
    	}

    }

    public void Initialize()
    {
    	//Debug.Log(turn);
    	for(int i=0; i<max; i++)
    	{
    		list[i].text = names[(ID+i)%max].text.ToString();
    		for(int j=0; j<5; j++)
    		{
    		//	DisplayedTiles.h[(ID+i)%max,j].sprite = TileSet.tile[BSTurnTracker.Set.Dequeue().getID()];
    		}
    		if((ID+i)%max == turn)
    		{
    			markers[i].enabled = true;
    			counter = i;
    		}
    	}
    	Set = BSTiles.Shuffle(seed);
    }

    public void Skip()
    {
    	int i = counter;
    	int j = 0;
    	counter = (counter+1) % max;
    	while(j<max && list[i%max].text.Equals("")){
    		j++;
    		counter = (counter+1) % max;
    	}

    }

    void TaskTurn()
    {
    	ButtonLand.hand_counter = 1;

    	ButtonLand.holder[0] = ButtonLand.display_num[ButtonLand.hand_tier];


    	for(int i=0; i<ButtonLand.holder[0]; i++){
    		DisplayedTiles.c[i].enabled = true;
    	}



    	
    	photonView.RPC("TurnChange", PhotonTargets.AllBuffered, counter,
    															ButtonLand.holder[0],
    															ButtonLand.holder[1],
    															ButtonLand.holder[2],
    															ButtonLand.holder[3],
    															ButtonLand.holder[4],
    															ButtonLand.holder[5],
    															ButtonLand.holder[6],
    															ButtonLand.holder[7],
    															ButtonLand.holder[8],
    															ButtonLand.holder[9], ButtonLand.hand_tier, ButtonLand.counter);														
    }

    [PunRPC]

    private void TurnChange(int counter, int b1, int b2, int b3, int b4, int b5, int b6, int b7, int b8, int b9, int b10, int tier, int cou){

    	markers[counter].enabled = false;

    	if(turn+1 == players)
    	{
    		for(int i=0; i<max-players+1; i++) {
    			counter = (counter+1) % max;
    		}
    	} 
    	else 
    	{
    		counter = (counter+1) % max;
    	}

    	turn = (turn+1) % players;
    	markers[counter].enabled = true;

		//Manual Button Labor
    	ButtonLand.holder[0]  = b1;
    	ButtonLand.holder[1]  = b2;
    	ButtonLand.holder[2]  = b3;
    	ButtonLand.holder[3]  = b4;
    	ButtonLand.holder[4]  = b5;
    	ButtonLand.holder[5]  = b6;
    	ButtonLand.holder[6]  = b7;
    	ButtonLand.holder[7]  = b8;
    	ButtonLand.holder[8]  = b9;
    	ButtonLand.holder[9]  = b10;

    	ButtonLand.holder_temp[0]  = b1;
    	ButtonLand.holder_temp[1]  = b2;
    	ButtonLand.holder_temp[2]  = b3;
    	ButtonLand.holder_temp[3]  = b4;
    	ButtonLand.holder_temp[4]  = b5;
    	ButtonLand.holder_temp[5]  = b6;
    	ButtonLand.holder_temp[6]  = b7;
    	ButtonLand.holder_temp[7]  = b8;
    	ButtonLand.holder_temp[8]  = b9;
    	ButtonLand.holder_temp[9]  = b10;
   
    	DisplayedTiles.c[0].sprite  = TileSet.tile[ButtonLand.holder[1]];
    	DisplayedTiles.c[1].sprite  = TileSet.tile[ButtonLand.holder[2]];
        DisplayedTiles.c[2].sprite  = TileSet.tile[ButtonLand.holder[3]];
    	DisplayedTiles.c[3].sprite  = TileSet.tile[ButtonLand.holder[4]];
        DisplayedTiles.c[4].sprite  = TileSet.tile[ButtonLand.holder[5]];
    	DisplayedTiles.c[5].sprite  = TileSet.tile[ButtonLand.holder[6]];
        DisplayedTiles.c[6].sprite  = TileSet.tile[ButtonLand.holder[7]];
        DisplayedTiles.c[7].sprite  = TileSet.tile[ButtonLand.holder[8]];
    	DisplayedTiles.c[8].sprite  = TileSet.tile[ButtonLand.holder[9]];

    	ButtonLand.hand_tier_temp = tier;
    	ButtonLand.counter_temp   = cou;

    	ButtonLand.DisableTier(tier);

    	//Debug.Log(b1);

    	if(tier == 5) 
    		ButtonLand.textActive(1,false);

    	else if(tier == 12) 
    	{
    		if(cou > 9) {
    			ButtonLand.textActive(2,false);
    		}
    		else
    		{
    			ButtonLand.textActive(3,false);
			}
		}

    	else
    		ButtonLand.textActive(3,false);


    	for(int i=0; i<DisplayedTiles.c.Length; i++){
    		DisplayedTiles.c[i].enabled = false;
    	}

    	for(int i=0; i<b1; i++){
    		DisplayedTiles.c[i].enabled = true;
    	}
    }
}
