using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
	Button r;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Button>();
		r.onClick.AddListener(TaskReset);
    }
     
	void TaskReset() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}	
}
