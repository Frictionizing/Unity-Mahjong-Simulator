using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robot2 : MonoBehaviour
{
	private IEnumerator coroutine;
	private int swap = -1;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }
	
	private IEnumerator AutoPlay(float time) {
		yield return new WaitForSeconds(Timer.seconds + 1);
		TurnTracker.TaskTile();
		yield return new WaitForSeconds(1);
		//printit();
		swap = discard();
		//Debug.Log(swap);
		if(swap == 16)
			TurnTracker.TaskDiu();
		else
		    TileGenerator.TaskSwap(swap);
		//LatestDiscards.reset();
		//printit();
		//TurnTracker.TaskDiu();
		yield break;
    }
	
	
    // Update is called once per frame
    void Update()
    {
        if( !TurnTracker.grace && TurnTracker.newTurn3) {
			TurnTracker.newTurn3 = false;
			coroutine = AutoPlay(1);
			StartCoroutine(coroutine);
		}
	}
	
	
	void printit(int[] a){
        for(int i=0; i<17; i++) {
			Debug.Log(i + ": " + a[i]);
		}				
	}
	
	int[] collectID(int flag) {
        int j = 0;
		int[] a = new int[17];
        for(int i=1; i<46; i++){
            if(LatestDiscards.pongCounter[TurnTracker.turn, i] >= 1){
			    for(int k=0; k<	LatestDiscards.pongCounter[TurnTracker.turn, i]; k++) {
				    a[j] = i;
                    j++;					
				}
			}	
        }
        a[16] = (int)LatestDiscards.stream[LatestDiscards.counter].getID();
        return a;		
 	}
	
	int neighborCheck(int[] a, int b) {
		if(b == 1 || b == 10 || b == 19) {
		    if (a[b+1] >= 1 && a[b+2] >= 1)
                return 5;				
			else if (a[b+1] >= 1)
			    return 3;
			else if (a[b+2] >= 1)
			    return 2;
			else
				return 1;
		}
		
		else if(b == 9 || b == 18 || b == 27) {
		    if (a[b-1] >= 1 && a[b-2] >= 1)
                return 5;				
			else if (a[b-1] >= 1)
			    return 3;
			else if (a[b-2] >= 1)
			    return 2;
			else
				return 1;
		}
		
		else if(b == 2 || b == 11 || b == 20) {
		    if ((a[b-1] >= 1 && a[b+1] >= 1) || (a[b+1] >= 1 && a[b+2] >= 1))
                return 5;				
			else if (a[b-1] >= 1 || a[b+1] >= 1)
			    return 3;
			else if (a[b+2] >= 1)
			    return 2;
			else
				return 1;
		}
		
		else if(b == 8 || b == 17 || b == 26) {
			if ((a[b-1] >= 1 && a[b+1] >= 1) || (a[b-1] >= 1 && a[b-2] >= 1))
                return 5;				
			else if (a[b-1] >= 1 || a[b+1] >= 1)
			    return 3;
			else if (a[b-2] >= 1)
			    return 2;
			else
				return 1;
		}
		
		else {
			if ((a[b+1] >= 1 && a[b+2] >= 1) || (a[b-1] >= 1 && a[b-2] >= 1) || (a[b-1] >= 1 && a[b+1] >= 1))
				return 5;
			else if (a[b+1] >= 1 || a[b-1] >= 1)
				return 3;
			else if (a[b-2] >= 1 || a[b+2] >= 1)
			    return 2;
			else
				return 1;
		
		}
	}
	
	int[] pointPrecedence(int[] a) {
		//int j = 0;
		int[] s = new int[17];
		int[] pcount = new int[46];
		
		for(int i=0; i<46; i++)
		    pcount[i] = (int)LatestDiscards.pongCounter[TurnTracker.turn, i];
		
		pcount[a[16]]++;
		
		for(int i=0; i<17; i++){
			if(a[i] >= 28 && pcount[a[i]] == 1)
				s[i] = 0;
			else if(a[i] >= 28 && pcount[a[i]] == 2)
				s[i] = 4;
			else if(a[i] >= 28 && pcount[a[i]] == 3)
				s[i] = 5;
			else
				s[i] = neighborCheck(pcount, a[i]);
			
		    if(pcount[a[i]] >= 2)
                s[i]++;				
		}
		return s;
	}
	
	int randLowestScore(int[] a){
		int b = a[0];
		List<int> allot = new List<int>();
		System.Random ran = new System.Random();
		
		for(int i=1; i<17; i++){
		    if(a[i] < b)
                b = a[i];				
		}
		
		for(int i=0; i<17; i++){
		    if(a[i] == b)
                allot.Add(i);				
		}
		
		int index = ran.Next(allot.Count);
		return allot[index];
	}

	int discard(){
		int[] score = pointPrecedence(collectID(1));
		return randLowestScore(score);
	}
}













