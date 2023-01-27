using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCenter : MonoBehaviour
{
	public GameObject hand;
	public Transform t;
	public static bool swift = false;
	public static int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        t = hand.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
		if (swift) {
			swift = false;
			counter++;
			if(counter == 1)
				t.Translate(100f,0,0);
			else if(counter == 2)
				t.Translate(125f,0,0);
			else if(counter == 3)
				t.Translate(130f,0,0);
			else if(counter == 4)
				t.Translate(135f,0,0);
			else if(counter == 5)
				t.Translate(155f,0,0);
		}
    }
}
