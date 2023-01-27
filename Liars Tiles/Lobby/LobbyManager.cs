using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Photon.MonoBehaviour
{
	public Text t;
	Text y;
	Button x;
	public Button b;

    private bool lobby = true;

    //Text lists of players
	public Text[] queue = new Text[6];
    //temp holds the raw string of players from the server
    private string temp = "";
    //temp2 temporaily holds the string of just the name
    private string temp_name = "";
    //gettng the char of '
    private static string s = "'";
    private char c = s[0];

	public  GameObject start;
	public  GameObject Lobby;
	public  Text PlayerNameText;
	public  new PhotonView photonView;

    // Start is called before the first frame update
    private void Awake()
    { 
        PlayerNameText.text = "You are player: " + PhotonNetwork.playerName;
    }


    void Start()
    {
        x = start.GetComponent<Button>();
        y = t.GetComponent<Text>();
        x.onClick.AddListener(TaskDel);

        for(int i=0; i<queue.Length; i++)
        {
        	queue[i].text = "";
        }

        if(PhotonNetwork.player.ID != 1)
        {
        	start.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(lobby) {
    	    //Debug.Lo(PhotonNetwork.playerList[0]);
    	    for(int i=0; i<PhotonNetwork.playerList.Length; i++)
            {
    		    for(int j=0; j<PhotonNetwork.playerList.Length; j++)
                {
    			    if(PhotonNetwork.playerList[j].ID == i+1) 
                    {
    				    temp = PhotonNetwork.playerList[j].ToString();
                        for(int k=1; k<temp.Length; k++)
                        {
                            if(temp[k].Equals(c) || queue[i].text.Length > 0) 
                            {
                                if(queue[i].text.Length == 0) 
                                {
                                    if(PhotonNetwork.playerList[j].ID == 1)
                                        queue[i].text = temp_name + " (HOST)";
                                    else
                                        queue[i].text = temp_name;
                                }
                                temp_name = "";
                                temp      = "";
                                break;
                            }
                            temp_name += temp[k];
                        }
    			    }
    		    }
    	    }

    	    for(int i=queue.Length-1; i>PhotonNetwork.playerList.Length-1; i--)
            {
    	        queue[i].text = "";
    	    }

    	    if(photonView.isMine) 
            {
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
            }
        }
    }

    void TaskDel(){
    	photonView.RPC("DeleteIt", PhotonTargets.AllBuffered, Random.Range(0,PhotonNetwork.playerList.Length), PhotonNetwork.playerList.Length, Random.Range(1,2147483647));
    }

    [PunRPC]

    private void DeleteIt(int u, int v, int w){
        lobby = false;
    	Lobby.SetActive(false);
        BSTurnTracker.ID = PhotonNetwork.player.ID - 1;
        BSTurnTracker.turn     = u;
        BSTurnTracker.players  = v;
        BSTurnTracker.names = queue;
        BSTurnTracker.start = true;
        BSTurnTracker.seed = w;
    }
}
