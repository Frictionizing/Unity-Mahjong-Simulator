using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManagerTaiwan : Photon.MonoBehaviour
{
   
    Button x;
    public Button b;

    private bool lobby = true;

    //Text lists of players
    public Text[] queue = new Text[4];
    //temp holds the raw string of players from the server
    private string temp = "";
    //temp2 temporaily holds the string of just the name
    private string temp_name = "";
    //gettng the char of '
    private static string s = "'";
    private char c = s[0];

    public  GameObject starty;
    public  GameObject Lobby;
    public  Text PlayerNameText;
    public  new PhotonView photonView;

    void Start()
    {
        x = starty.GetComponent<Button>();
        x.onClick.AddListener(TaskDel);

        for(int i=0; i<queue.Length; i++)
        {
            queue[i].text = "";
        }

        if(PhotonNetwork.player.ID != 1)
        {
            starty.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(lobby){
           Debug.Log(PhotonNetwork.playerName);
        }
    }


    void TaskDel(){
        photonView.RPC("DeleteIt", PhotonTargets.AllBuffered);
    }

    [PunRPC]

    private void DeleteIt(){

    }
}
