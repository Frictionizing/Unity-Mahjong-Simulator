using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurnPileCount : MonoBehaviour
{
	public Text[] pile;
    // Start is called before the first frame update
    void Start()
    {
	}

    // Update is called once per frame
    void Update()
    {
		for(int i=0; i<4; i++)
			pile[i].text = "[ 花 ]: " +  TileGenerator.flower_count  +"/8";
    }
}
