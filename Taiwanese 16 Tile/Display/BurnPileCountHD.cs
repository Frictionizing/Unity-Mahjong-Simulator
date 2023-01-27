using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BurnPileCountHD : MonoBehaviour
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
			for(int i=0; i<7; i++)
				pile[j].text += "[ " + LatestDiscards.honor2[i] + " ]: " +  LatestDiscards.honorcount[i]  + " ";
		}
	}
}
