using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnIndicator : MonoBehaviour
{
	
	public GameObject[] ind;
	public static GameObject[,] list  = new GameObject[4,4];
	public static int flick = 0;
    // Start is called before the first frame update
    void Start()
    {
		flick = 0;
        for(int k=0; k<4; k++) {
			for(int i=0; i<4; i++) {
				list[k,i] = ind[k].transform.GetChild(i).gameObject;
				list[k,i].SetActive(false);
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
		flick++;
		flick = flick%60;
		for(int k=0; k<4; k++) {
			for(int i=0; i<4; i++) {
				if(i == TurnTracker.turn) {
					if(flick < 30)
						list[k,i].SetActive(true);
					else
						list[k,i].SetActive(false);
					
				}
				else
					list[k,i].SetActive(false);
			}
		}
        
    }
}
