using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    	string str = "applesauce";
    	string con = "";

        for (int i=0; i<str.Length; i++)
        {
        	for(int j=0; j<str.Length; j++)
        	{
        	    con = con + str[(i+j)%str.Length];
            }
            Debug.Log(con);
            con = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
