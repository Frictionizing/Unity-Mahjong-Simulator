using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class English : MonoBehaviour
{
	public GameObject Eng_text; 
	public bool toggle = false;
    // Start is called before the first frame update
    void Start()
    {
        Button e = GetComponent<Button>();
		e.onClick.AddListener(TaskToggleEnglish);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void TaskToggleEnglish(){
		    toggle = !toggle;
			Eng_text.SetActive(toggle);
	}
}
