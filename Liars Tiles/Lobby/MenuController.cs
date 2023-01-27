using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] public string  ServerName = "";
    [SerializeField] private GameObject UsernameMenu = null;
    [SerializeField] private GameObject ConnectPanel;

    [SerializeField] private InputField UsernameInput = null;
    [SerializeField] private InputField CreateGameInput = null;
    [SerializeField] private InputField JoinGameInput;

    [SerializeField] private GameObject StartButton = null;

    private void Awake(){
    	PhotonNetwork.ConnectUsingSettings(VersionName);

	}

	private void Start(){
		UsernameMenu.SetActive(true);


	}

	private void OnConnectedToMaster(){
		PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
	}

	public void ChangeUserNameInput(){
		if(UsernameInput.text.Length == 2)
			StartButton.SetActive(true);
		else
			StartButton.SetActive(false);
 
	}

	public void SetUsername(){
		UsernameMenu.SetActive(false);
		PhotonNetwork.playerName = UsernameInput.text;
	}


	public void CreateGame(){
		PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { MaxPlayers = 6}, null);

	}

	public void CreateGame2(){
		PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() { MaxPlayers = 4}, null);

	}

	public void ServerGame(){
		ServerName = "Wyvernus_01";
	}

	public void ServerGame2(){
		ServerName = "Wyvernus_02";
	}

	public void JoinGame(){
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 6;
		PhotonNetwork.JoinOrCreateRoom(ServerName, roomOptions, TypedLobby.Default);
	}

	private void OnJoinedRoom(){
		if(ServerName == "Wyvernus_01")
			PhotonNetwork.LoadLevel("BS_Tiles");
		else if(ServerName == "Wyvernus_02")
			PhotonNetwork.LoadLevel("Taiwan_Classic");
	}

}
