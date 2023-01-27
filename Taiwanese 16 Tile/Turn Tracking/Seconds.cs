using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seconds : MonoBehaviour
{
	public Text t;
	public static float i;
    public static bool e;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
		e = true;
    }


    // Update is called once per frame
    void Update()
    {
		if(e) {
			e = false;
			i = (Timer.seconds*240);
		}
		if(TurnTracker.grace || TurnTracker.newGrace) {
		//	Debug.Log(i);
			i--;
			if(i<0)
				t.text = "0";
			else
				t.text = (int)i/60 + "";
		} else {
			t.text = "0";
			i = (Timer.seconds*240);
		}
    }
}
