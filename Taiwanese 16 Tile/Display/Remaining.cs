using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Remaining : MonoBehaviour
{
    public Text[] remaining;
	public static Text[] remaining2;
	public static int fcount = 0;
	public static bool e = true;
	public static int back = 16;
	public static int taiwan = 64;
	public static int counter = 0;

	public bool start;
    // Start is called before the first frame update
    void Start()
    {
		start = true;
		counter = 0;
	}

    // Update is called once per frame
    void Update()
    {
		if (start){
			fcount = add_flowers(TileGenerator.flstore);
			start = false;
		}

		for(int j=0; j<4; j++){
			remaining[j].text = "x" + (144 - taiwan - fcount - counter - 16);
		//	Debug.Log(taiwan + " " + fcount + " " + counter);
		}
		
		if((144 - taiwan - fcount - counter) == 16 && Timer.e)
			WinCondition.endoftiles = true;
    }
	
	int add_flowers(int[] x) {
		int i = 0;
		for(int j=0; j<4; j++) 
			i+= x[j];
		return i;
	}
}
