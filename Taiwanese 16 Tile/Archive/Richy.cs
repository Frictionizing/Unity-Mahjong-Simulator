using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Richy : MonoBehaviour
{
	public static TMP_Text rich;
    // Start is called before the first frame update
    void Start()
    {
        rich = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        rich.text = "<color=red>pony";
    }
}
