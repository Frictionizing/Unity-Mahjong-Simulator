using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentRiver : MonoBehaviour
{
	public GameObject[] river;
	//public static GameObject[,] list  = new GameObject[5];
	public static GameObject[] mat  = new GameObject[4];
	public static       Text[] x    = new Text[4];
	public static     Image[] st   = new Image[4];
	
    // Start is called before the first frame update
    void Start()
    {
		/*
        for(int i=0; i<5; i++) {			
			list[i] = river[i].transform.GetChild(i).gameObject;
			list[k,i].gameObject.SetActive(false);
		    for(int j=0; j<4; j++) {
			    mat[k,i,j] = list[k,i].transform.GetChild(j).gameObject;
				x[k,i,j]   = mat[k,i,j].GetComponentInChildren<Text>();
				st[k,i,j]  = mat[k,i,j].GetComponent<Image>();
				x[k,i,j].text = "";
			}
			mat[k,i,3].gameObject.SetActive(false);
	    }
		*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
