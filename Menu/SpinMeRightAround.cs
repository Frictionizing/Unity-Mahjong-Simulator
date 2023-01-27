using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinMeRightAround : MonoBehaviour
{
	public Transform i;
	public GameObject emblem;
    // Start is called before the first frame update
    void Start()
    {
        i = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        i.Rotate(0f,0f,-1f);
       // Debug.Log(i.Rotate.position.z);
    }
}
