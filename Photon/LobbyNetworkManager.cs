using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class LobbyNetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonChatManager chatManager;
    [SerializeField] private ButtonManager agoraController;

    [SerializeField] private RoomItemUI roomUIPrefab;
    [SerializeField] private Transform roomListParent;

    [SerializeField] private RoomItemUI playerItemUIPrefab;
    [SerializeField] private Transform playerListParent;

    [SerializeField] private Text statusField;
    [SerializeField] private Button leaveRoomButton;


    private List<RoomItemUI> roomList = new List<RoomItemUI>();
    public List<RoomItemUI> playerList = new List<RoomItemUI>(); //player list ordered

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);        
    }


#region PhotonCallbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        JoinOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("UserPrefab", new Vector3(Random.Range(90, 100), Random.Range(90, 100), 0), Quaternion.identity);

        statusField.text = ("Joined " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);

        leaveRoomButton.interactable = true;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    public override void OnLeftRoom()
    {
        statusField.text = "LOBBY";
        Debug.Log("Left Room: " );

        leaveRoomButton.interactable = false;

        UpdatePlayerList();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    #endregion

    public void Initialize()
    {
        leaveRoomButton.interactable = false;
        Connect();
    }

    public void Connect()
    {
        PhotonNetwork.NickName = Data.currentName; //"Player" + Random.Range(0, 5000);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    public void UpdateRoomList(List<RoomInfo> list)
    {
        //Clear the current list of rooms
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i].gameObject);
        }

        roomList.Clear();


        //Generate a new list with the updated info
        for (int i = 0; i < list.Count; i++)
        {
            //Skip empty rooms
            if (list[i].PlayerCount == 0)
                continue;

            RoomItemUI newRoomItem = Instantiate(roomUIPrefab);
            newRoomItem.LobbyNetworkParent = this;
            newRoomItem.SetName(list[i].Name);
            newRoomItem.transform.SetParent(roomListParent);
            newRoomItem.transform.localScale = new Vector3(1f, 1f, 1f);

            

            roomList.Add(newRoomItem); 
        }


    }

    private void JoinOrCreateRoom() 
    {
        PhotonNetwork.JoinOrCreateRoom("Lobby", new RoomOptions() { MaxPlayers = 10 }, null);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void UpdatePlayerList()
    {
        //Clear the current player list
        for (int i = 0; i < playerList.Count; i++)
        {
            Destroy(playerList[i].gameObject);
        }

        playerList.Clear();

        if (PhotonNetwork.CurrentRoom == null) { return; }

        //Generate a new list of players
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {

            RoomItemUI newPlayerItem = Instantiate(playerItemUIPrefab);
            

            newPlayerItem.SetName(player.Value.NickName);
            newPlayerItem.SetPosition(player.Value.ActorNumber);

            playerList.Add(newPlayerItem);
            playerList = playerList.OrderBy(e => e.GetPosition()).ToList();
        }


        foreach (RoomItemUI playerObject in playerList)
        {
            playerObject.transform.SetParent(playerListParent);
            playerObject.transform.localScale = new Vector3(1f, 1f, 1f);
            Debug.Log("user name: " + playerObject.GetUserName() + " position: " + playerObject.GetPosition());
        }

        
    }

    public void JoinRoom(string name)
    {
        PhotonNetwork.JoinRoom(name);
    }

    public void LoadScene(string name)
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.LoadLevel(name);
    }

    public void ExitLobby()
    {
        LeaveRoom();

        chatManager.ConnectUser();

        agoraController.OnJoinARClicked();

        Destroy(this.gameObject);
    }

            
}
