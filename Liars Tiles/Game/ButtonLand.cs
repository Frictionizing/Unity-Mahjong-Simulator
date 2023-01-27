using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLand : Photon.MonoBehaviour
{
	/*
	  Buttons[]
	  Element 0 - Confirm
	  1-9         Numbers
	  10-18       Circles
	  19-27       Bamboo
	  28-31       Honor
	  32-34       Dragon
	  35          Joker

	  Hands[]
	  0-5         High - Flush
	  6-11        Full House - Six+ of a Kind
	*/

	//Which hand button is pressed
	public static int hand_tier = -1;
	//which tile button is pressed
	public static int tile_tier = -1;
	//How many tile buttons have been pressed before confirm is active(depending on hand_tier requirements)
	public static int hand_counter = 0;
	//Index for hands that are no longer avaiable
	public static int hand_out = -1;

	public static int interact = -1;

	public Button[] buttons;
	public Button[] hands;
	public Image[] results;
	public Image[] highest_hand;

	public static Button[] hand = new Button[13];

	public Button back;
	public Button liar;
	public GameObject flush;
	public GameObject meld;
	public GameObject six_plus;
	public GameObject PlusMinus;
	public GameObject HandScreen;
	public GameObject HandTiles;
	public GameObject ResultsScreen;
	private static Text multiplier;

	public Text bount;

	public Button three_in_row;
	public Button three_of_kind;

	public Button minus;
	public Button plus;

	public int meld_flag = 1;
	public static int counter;

	//Serialize and holds claim ([0] is length)
	public static int[] holder        = new int[10];
	//Holds current claim
	public static int[] holder_temp   = new int[10];
	public static int  hand_tier_temp; 
	public static int  counter_temp;


	//2 Pair and Full House require 2 button presses
	public static int[] tally       = {1,1,2,1,1,1, 2,1,2,1,3,1,1};

	//Display how many tiles for hand
	public static int[] display_num = {1,2,4,3,5,1, 5,4,6,5,9,8,1};

    // Start is called before the first frame update
    void Start()
    {
    	buttons[0].interactable = false;
  
    	buttons[0].onClick.AddListener( () => TaskConfirm(tile_tier, hand_tier) );

        for(int i=1; i<buttons.Length; i++) {
        	int temp = i;
        	buttons[temp].onClick.AddListener( () => TaskReturnTileInc(temp) );
        }

        for(int i=0; i<hands.Length; i++) {
        	int temp = i;
        	hand[i] = hands[i];
        	hands[temp].onClick.AddListener( () => TaskReturnHandTier(temp) );
        }


        back.onClick.AddListener(TaskBack);
        liar.onClick.AddListener(TaskLiar);

        three_of_kind.onClick.AddListener(TaskFlagFlip);
        three_in_row.onClick.AddListener(TaskFlagFlip);

        minus.onClick.AddListener(TaskSubtract);
        plus.onClick.AddListener(TaskAdd);

        multiplier = six_plus.GetComponent<Text>();
        multiplier.text = "";

        meld.SetActive(false);
        PlusMinus.SetActive(false);
        three_in_row.interactable = false;

        minus.interactable = false;
        bount.text = counter.ToString();

        for(int j=0; j<DisplayedTiles.c.Length; j++)
    		DisplayedTiles.c[j].enabled = false;

        TaskFlagFlip();
        TaskFlagFlip();

        holder[0] = 0;
        holder_temp[0] = 0;

        six_plus.SetActive(true);
        counter = 6;
        bount.text = "6";

        counter_temp = 6;
        hand_tier_temp = -1;

        TaskBack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DisableTier(int tier)
    {
    	for(int i=0; i<tier; i++)
    	{
    		hand[i].interactable = false;
    	}
    }

    public static void textActive(int i, bool j)
    {
    	if(i == 1)
			multiplier.text = "FLUSH";
		else if(i == 2)
			if(!j)
				multiplier.text = "X" + counter_temp.ToString();
			else
				multiplier.text = "X" + counter.ToString();
		else
			multiplier.text = "";
    }


    void TaskSubtract()
    {
    	counter--;
    	if(counter <= 6)
    		minus.interactable = false;
    	else
    		minus.interactable = true;

    	plus.interactable = true;
    	bount.text = counter.ToString();
    	hand_counter = 0;
    	ActivateConfirm(hand_tier, 0);
    	TaskReturnHandTier(hand_tier);
    }


    void TaskAdd()
    {
    	counter++;
    	if(counter >= 12)
    		plus.interactable = false;
    	else
    		plus.interactable = true;

    	minus.interactable = true;
    	bount.text = counter.ToString();
    	hand_counter = 0;
    	ActivateConfirm(hand_tier, 0);
    	TaskReturnHandTier(hand_tier);
    }

    //MeldFlag 1 = Three of Kind; 2 = Three in Row
    void TaskFlagFlip()
    {
    	if(three_of_kind.interactable)
    	{
    		three_of_kind.interactable = false;
    		three_in_row.interactable = true;
    		meld_flag = 1;
    	}
    	else
    	{
			three_of_kind.interactable = true;
    		three_in_row.interactable = false;
    		meld_flag = 2;
    	}
    	MeldSeperate();
    }

    //Back button resets hand choice vars
    void TaskBack()
    {
    	hand_counter = 0;
    	hand_tier = hand_out;
    	buttons[0].interactable = false;
    	ResetInteract();

    	for(int j=0; j<DisplayedTiles.c.Length; j++)
    		DisplayedTiles.c[j].enabled = false;

    	meld.SetActive(false);
    	PlusMinus.SetActive(false);

    	for(int j=0; j<holder_temp[0]; j++) {
    		DisplayedTiles.c[j].enabled = true;
    		DisplayedTiles.c[j].sprite  = TileSet.tile[holder_temp[j+1]];
    	}

    	if(hand_tier_temp == 5) 
    		ButtonLand.textActive(1,false);
    	else if(hand_tier_temp == 12 && counter_temp > 9)
    		ButtonLand.textActive(2,false);
    	else
    		ButtonLand.textActive(3,false);
    		
    }

    //Serialize Lies
    void TaskLiar()
    {
    	photonView.RPC("CalledLiar", PhotonTargets.AllBuffered, WinCheck.WinOrLose(holder_temp, DisplayedTiles.agg, hand_tier_temp));
    }

    /*
    I -> Tile chosen 
    Counter -> What num meld
    Flag -> Three of a kind or Three in a row
    */
    void MeldCreator(int i, int counter, int flag)
    {
    	int k;
    	if(counter == 1) {
    		k = 0;
    		for(int j=0; j<9; j++)
    			DisplayedTiles.c[j+k].enabled = false;
    	}
    	else
    		k = (counter-1)*3;

    	for(int j=0; j<3; j++)
	    {
	    	DisplayedTiles.c[j+k].enabled = true;
	    	if(flag == 1) {
	    		DisplayedTiles.c[j+k].sprite  = TileSet.tile[i];
	    		holder[j+k+1] = i;
	    	}
	    	else {
	    		DisplayedTiles.c[j+k].sprite  = TileSet.tile[i+j];
	    		holder[j+k+1] = i+j;
	    	}
	    }
    }


    //High through 6 of a kind buttons
    void TaskReturnTileInc(int i)
    {
    	//Multiple click hands check
    	tile_tier = i;
    	if(tally[hand_tier] == 2 && (hand_counter+1)%3 != 0 && hand_tier != 10)
    		hand_counter = (hand_counter+1)%3;
    	else if(hand_tier == 10 && (hand_counter+1)%4 != 0)
    		hand_counter = (hand_counter+1)%4;
    	else
    		hand_counter = 1;

    	//holder[0] = display_num[hand_tier];

    	//Nth of a Kind
    	if(hand_tier != 2 && hand_tier != 3 && hand_tier != 4 && hand_tier != 5 && hand_tier != 6 && hand_tier != 8 && hand_tier != 10 && hand_tier != 11) 
    	{
    		for(int j=0; j<display_num[hand_tier]; j++)
    		{
    			DisplayedTiles.c[j].enabled = true;
    			DisplayedTiles.c[j].sprite  = TileSet.tile[i];
    			holder[j+1] = i;
    		}
    	}

    	//2 Pair
    	else if (hand_tier == 2)
    	{
    		if(hand_counter == 1){
    			buttons[i].interactable = false;
    			interact = i;
    			for(int j=0; j<4; j++){
    				if(j < 2) {
    					DisplayedTiles.c[j].enabled = true;
    					DisplayedTiles.c[j].sprite  = TileSet.tile[i];
    					holder[j+1] = i;
    				}
    				else 
    					DisplayedTiles.c[j].enabled = false;
    		    }
    		}
    		else {
    			buttons[interact].interactable = true;
    			for(int j=2; j<4; j++){
    				DisplayedTiles.c[j].enabled = true;
    				DisplayedTiles.c[j].sprite  = TileSet.tile[i];
    				holder[j+1] = i;
    			}
    		}
    	}

    	//Meld, 2 Meld, 3 Meld
    	else if (hand_tier == 3 || hand_tier == 8 || hand_tier == 10)
    		MeldCreator(i, hand_counter, meld_flag);

    	//Full House
    	else if (hand_tier == 6)
    	{
    		if(hand_counter == 1)
    		{
    			buttons[i].interactable = false;
    			interact = i;
    			for(int j=0; j<5; j++)
    			{
    				if(j < 3) 
    				{
    					DisplayedTiles.c[j].enabled = true;
    					DisplayedTiles.c[j].sprite  = TileSet.tile[i];
    					holder[j+1] = i;
    				}
    				else
    					DisplayedTiles.c[j].enabled = false;
    		    }
    		}
    		else
    		{
    			buttons[interact].interactable = true;
    			for(int j=3; j<5; j++){
    				DisplayedTiles.c[j].enabled = true;
    				DisplayedTiles.c[j].sprite  = TileSet.tile[i];
    				holder[j+1] = i;
    			}
    		}
    	}

    	//Straight/Whole Set
    	else if (hand_tier == 4)
    	{
    		if(i < 5 || (i >= 10 && i<14) || (i >= 19 && i<23))
    		{
    			for(int j=0; j<5; j++)
    			{
    				DisplayedTiles.c[j].enabled = true;
    				DisplayedTiles.c[j].sprite  = TileSet.tile[i+j];
    				holder[j+1] = i+j;
    			}	
    		}
    		else if (i>=28 && i<=31)
    		{
    			for(int j=0; j<4; j++)
    			{
    				DisplayedTiles.c[j].enabled = true;
    				DisplayedTiles.c[j].sprite  = TileSet.tile[28+j];
    				holder[j+1] = 28+j;
    			}	
    			DisplayedTiles.c[4].enabled = false;
    		}
    		else if (i>=32 && i<=34)
    		{
    			for(int j=0; j<3; j++)
    			{
    				DisplayedTiles.c[j].enabled = true;
    				DisplayedTiles.c[j].sprite  = TileSet.tile[32+j];
    				holder[j+1] = 32+j;
    			}	
    			DisplayedTiles.c[4].enabled = false;
    			DisplayedTiles.c[3].enabled = false;
    		}
    		else if(i<10)
    		{
    			for(int j=0; j<5; j++)
    			{
    				DisplayedTiles.c[j].enabled = true;
    				DisplayedTiles.c[j].sprite  = TileSet.tile[5+j];
    				holder[j+1] = 5+j;
    			}	
    		}
    		else if(i<19)
    		{
    			for(int j=0; j<5; j++)
    			{
    				DisplayedTiles.c[j].enabled = true;
    				DisplayedTiles.c[j].sprite  = TileSet.tile[14+j];
    				holder[j+1] = 14+j;
    			}	
    		}
    		else
    		{
    			for(int j=0; j<5; j++)
    			{
    				DisplayedTiles.c[j].enabled = true;
    				DisplayedTiles.c[j].sprite  = TileSet.tile[23+j];
    				holder[j+1] = 23+j;
    			}
    		}
    	}

    	//Flush
    	else if (hand_tier == 5)
    	{
    		DisplayedTiles.c[0].enabled = true;
    		holder[1] = i;
    		DisplayedTiles.c[0].sprite  = TileSet.tile[i];
    		textActive(1,true);
    	}

    	//Eight Orphans
    	if(hand_tier == 11)
    	{
    		for(int j=0; j<8; j++)
    		{
    			DisplayedTiles.c[j].enabled = true;
    			DisplayedTiles.c[j].sprite  = TileSet.tile[28+j];
    			holder[j+1] = 28+j;
    		}
    	}

    	//6+ of a Kind
    	else if (hand_tier == 12)
    	{
    		if(counter > 9) {
    			for(int j=1; j<9; j++)
    				DisplayedTiles.c[j].enabled = false;
    			DisplayedTiles.c[0].enabled = true;
    			DisplayedTiles.c[0].sprite  = TileSet.tile[i];
    			holder[1] = i;
    			display_num[12] = 1;
    		}
    		
    		else
    		{
    			for(int j=0; j<counter; j++)
    			{
    				display_num[12] = counter;
    				DisplayedTiles.c[j].enabled = true;
    				DisplayedTiles.c[j].sprite  = TileSet.tile[i];
    				holder[j+1] = i;
    			}
    		}
    	}


    	ActivateConfirm(hand_tier ,hand_counter);
    }

    void MeldSeperate()
    {
    	meld.SetActive(true);
    	if(!three_in_row.interactable)
    	{
    		for(int j=0; j<2; j++)
    		{
    			buttons[8+j].interactable = false;
    			buttons[17+j].interactable = false;
    			buttons[26+j].interactable = false;
    		}
		    for(int j=0; j<8; j++)
    			buttons[28+j].interactable = false;
    	}
    	else
    	{
    		for(int j=1; j<36; j++)
    			buttons[j].interactable = true;
    	}
    }

    //Disables interacables
    void TaskReturnHandTier(int i)
    {
    	textActive(3,false);
  
    	//Disable claim river
    	for(int j=0; j<9; j++) {
    		DisplayedTiles.c[j].enabled = false;
    	}

    	hand_tier = i;
    	if(i == 4 || i == 5 || i == 8 || i == 10)
    	    buttons[35].interactable = false;

    	//Straight
    	if(i == 4)
    	{
    		for(int j=0; j<4; j++)
    		{
    			buttons[6+j].interactable = false;
    			buttons[15+j].interactable = false;
    			buttons[24+j].interactable = false;
    		}

    		for(int j=28; j<36; j++)
    			buttons[j].interactable = false;
    	}

    	//Meld Logic
    	if (i == 3 || i == 8 || i == 10)
    	{
	 		MeldSeperate();
    	}

    	//Flush
    	else if(i == 5)
    	{
    		for(int j=0; j<8; j++)
    		{
    			buttons[2+j].interactable = false;
    			buttons[11+j].interactable = false;
    			buttons[20+j].interactable = false;
    		}

    		for(int j=0; j<7; j++)
    			buttons[28+j].interactable = false;
    	}

    	//Eight Orphans
    	else if(i == 11)
    	{
  			for(int j=0; j<28; j++)
    			buttons[j].interactable = false;
    	}

    	//6+ Of a Kind
    	else if(i == 12)
    	{
    		meld.SetActive(false);
    		PlusMinus.SetActive(true);

    		if(counter > 8)
    			buttons[35].interactable = false;
    		else
    			buttons[35].interactable = true;
    	}

    }

    void DisableWeakHands(int j)
    {
    	for(int i=0; i<j; i++)
    		hands[i].interactable = false;
    }

    void TaskConfirm(int i, int j) {
    	TaskBack();
    }

    void ResetInteract()
    {
    	for(int i=1; i<buttons.Length; i++)
    		buttons[i].interactable = true;
    }

    void DisableInteract()
    {
    	for(int i=1; i<buttons.Length; i++)
    		buttons[i].interactable = false;
    }

    void ActivateConfirm(int i, int j)
    {
    	if(tally[i] == j)
    		buttons[0].interactable = true;
    	else 
    		buttons[0].interactable = false;
    }

    [PunRPC]

    private void CalledLiar(int[] i){
    	HandScreen.SetActive(false);
    	HandTiles.SetActive(false);

    	for(int j=i.Length; j<9; j++)
    	{
    		results[j].enabled = false;
    	}
 
    	for(int j=0; j<i.Length; j++)
    		results[j].sprite = TileSet.tile[i[j]];

    	ResultsScreen.SetActive(true);
    }
}
