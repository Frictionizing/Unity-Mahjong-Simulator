using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerWeeee :  Photon.MonoBehaviour
{
	public new PhotonView photonView;
	public float MoveSpeed;
	public GameObject PlayerCamera;
	public Text PlayerNameText;
	public SpriteRenderer sr;
    // Start is called before the first frame update
    private void Awake()
    {
        if(photonView.isMine) {
       		 PlayerNameText.text = PhotonNetwork.playerName;
        }
    //    else {
      //  	PlayerNameText.text = photonView.owner.name;
       // 	PlayerNameText.color = Color.cyan; 
      //  }
    }

    // Update is called once per frame
    private void Update()
    {
        if(photonView.isMine) {
        	CheckInput();
        }
    }


    private void CheckInput(){
    	var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
    	transform.position += move * MoveSpeed * Time.deltaTime;

    if(Input.GetKeyDown(KeyCode.A))
     	photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);

    if(Input.GetKeyDown(KeyCode.D))
     	photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);

    }

    [PunRPC]

    private void FlipTrue(){
    	//sr.flipX = true;
    }
    private void FlipFlase(){
    	//sr.flipX = false;
    }

}
