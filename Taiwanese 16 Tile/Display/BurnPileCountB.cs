﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurnPileCountB : MonoBehaviour
{
    public Text[] pile;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
	    for(int j=0; j<4; j++) {
			pile[j].text = "";
			for(int i=1; i<10; i++)
				pile[j].text += "[" +i+ "餅]: " +  LatestDiscards.discardcount[1,i-1]  + " ";
		}
	}
}
