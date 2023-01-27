using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	private IEnumerator coroutine;
	public static float seconds = 1;
	public static bool e = false;

    // Start is called before the first frame update
    void Start()
    {
		e =false;
		seconds = 1;
    }
	
    public static IEnumerator TurnGracePeriod2(float time) {
		TurnTracker.newGrace = true;
		yield return new WaitForSeconds(seconds);
		TurnTracker.newGrace = false;
		yield break;
	}

	 private IEnumerator TurnGracePeriod(float time) {
		TurnTracker.grace = true;
		yield return new WaitForSeconds(time);
		TurnTracker.grace = false;
		Seconds.i = (seconds*60);	
		if((144 - Remaining.taiwan - Remaining.fcount - Remaining.counter) == 16)
			e = true;
		if(TurnTracker.turn != 0 && !WinCondition.endGame) {
			TurnTracker.turnone = false;
		    if(LatestDiscards.counter > 0) {
			    int j = 0;
			    if(LatestDiscards.counter < LatestDiscards.turntable)
			    	j = LatestDiscards.counter;
		    	else
				    j = LatestDiscards.turntable;
			}	
		}
		yield break;
	}

    // Update is called once per frame
    void Update()
    {
        if(TurnTracker.newTurn2) {
			seconds = 1;
			Seconds.e = true;
			coroutine = TurnGracePeriod(seconds);
			TurnTracker.newTurn2 = false;
			TileGenerator.redraw2 = false;
			StartCoroutine(coroutine);
		}

		if(TurnTracker.showcaseTile) {
			seconds = 2;
			Seconds.e = true;
			coroutine = TurnGracePeriod2(seconds);
			TurnTracker.showcaseTile = false;
			StartCoroutine(coroutine);
		}
    }
}
