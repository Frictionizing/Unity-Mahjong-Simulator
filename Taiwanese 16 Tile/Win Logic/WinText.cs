using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinText : MonoBehaviour
{
    // Start is called before the first frame update
	public Text t;
	public Button b;
    Button k;
	public static bool end = false;
    void Start()
    {
        t = GetComponent<Text>();
		k = b.GetComponent<Button>();
		k.onClick.AddListener(TaskEndGame);       
    }

    // Update is called once per frame
    void Update()
    {
        if(end) {
			Debug.Log(WinCondition.point);
			if(WinCondition.point >= 5.5f)
			   t.text = "勝";
		    else
			    t.text = "輸?";
		}
		end = false;
    }
	
	void TaskEndGame() {
		end = true;
	}
	
}
