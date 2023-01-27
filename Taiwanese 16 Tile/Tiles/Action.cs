using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Action : MonoBehaviour
{
	public Text x;
	public static bool toss = false;
	public static bool swap = false;
	public int i = 0;
	private string[] names = {"P1", "P2", "P3" , "P4"};
    // Start is called before the first frame update
    void Start()
    {
        x = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
		//Debug.Log(toss + " " + swap);
		i = TurnTracker.prev_turn;
		if(i == -1)
			i = 3;
		
        if(swap)
			x.text = names[i] + " Swap:";
	 	else if(toss)
			x.text = names[i] + " Toss:";
		
	
		
		if(TurnTracker.turnone)
			x.text = "";

    }
}
