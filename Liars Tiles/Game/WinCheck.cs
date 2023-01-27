using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WinCheck : MonoBehaviour
{
	static int jok = 35;
	static int[] Claim = new int[9];
	static int[] Result;
	static int[] Result_List = new int[jok+1];
	static int len;
	static bool[] Confirmed = new bool[10]; 


    public static int[] WinOrLose(int[] a, int[] b, int c)
    {
    	for(int i=0; i<a[0]; i++){
    		Claim[i] = a[i+1];
    	}	
    	len = a[0];
    	Result = b;
    	Order(); 
    	Print(Result);
    	FormResultList();
    	int[] s;
    	if(c == 5)
    		s = CheckWinFlush(Claim[0], Result_List);
    	else if(c == 12)
    		s = CheckWinSixPlus(Claim[0], Result_List);
    	else
    		s = CheckWin(Claim, Result_List);
    	return s;
    }

    //Order the array
    static void Order()
    {
    	//Array.Sort(Claim);
    	Array.Sort(Result);
    }

    //Return amount of Jokers in the alloted pool
    static int JokerCount(int[] a)
    {
        int t = 0;
    	foreach(int i in a)
    	{
    		if(i >= jok)
    			t++;
    	}
    	return t;
    }

    //Form array with available resources from aggregate
    static void FormResultList()
    {
    	foreach(int i in Result)
    	{
    		if(i < jok)
    			Result_List[i]++;
    	}

    	Result_List[jok] = JokerCount(Result);
    }

    static int[] CheckWin(int[] a, int[] b)
    {
    	int counter = 0;
    	int[] answer = new int[9]; 
    	for(int i=0; i<len; i++)
    	{
    	//	Debug.Log(i + ": " + b[i]);
    		if(a[i] == 0)
    			break;

    		if(b[a[i]] > 0)
    		{
    			answer[i] = a[i];
    			b[a[i]]--;
    		}
    		else if(b[jok] > 0)
    		{
    			b[jok]--;
    			answer[i] = jok;
    		}
    		else
    			answer[i] = 43;

    		counter++;
    	}

    	int[] answer2 = new int[counter];
    	for(int i=0; i<counter; i++)
    		answer2[i] = answer[i];

    	return answer2;
    }

    static int[] CheckWinFlush(int a, int[] b)
    {
    	int counter = 0;
    	int[] answer = new int[5]; 
    	int x = a+9;

    	
    	for(int i=a; i<x; i++)
    	{
    		while(b[i] > 0 && counter < 5)
    		{
    			answer[counter] = i;
    			b[i]--;
    			counter++;
    		}
    	}
    	
    	while(b[jok] > 0 && counter < 5)
    	{
    		answer[counter] = jok;
    		b[jok]--;
    		counter++;
    	}

    	while(counter < 5)
    	{
    		answer[counter] = 43;
    		counter++;
    	}

    	return answer;
    }

    static int[] CheckWinSixPlus(int a, int[] b)
    {
    	return null;
    }

    //Print int Array
    static void Print(int[] i)
    {
    	string s = "";
    	foreach (int j in i)
    		s += j + " ";

    	Debug.Log(s);
    }
}
