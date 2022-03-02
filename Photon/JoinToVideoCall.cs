using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class JoinToVideoCall : MonoBehaviourPunCallbacks
{
    private JoinToVideoCall target;

    public RoomItemUI myRoomItemUI;
    public LobbyNetworkManager lobbyNetworkManager;

    public PhotonView pv;


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        lobbyNetworkManager = FindObjectOfType<LobbyNetworkManager>();
    }

    public void GetRoomItemUI()
    {
        RoomItemUI[] players = FindObjectsOfType<RoomItemUI>();


        foreach (RoomItemUI item in players)
        {
            if (item.GetUserName() == pv.Owner.NickName)
            {
                myRoomItemUI = item;

                break;
            }
            else
            {
                continue;
            }
        }
    }

    public void SearchForClient(/*bool rearCamera,*/ string hostName, string photonChannel, string agoraChannel, string hostEmail, string hostRealName)
    {
        GetRoomItemUI();


        JoinToVideoCall[] clientList = FindObjectsOfType<JoinToVideoCall>();

        string nameUserToJoin = "";
               

        if (lobbyNetworkManager.playerList.Count > 1) //I'm not the only one
        {
            foreach (RoomItemUI user in lobbyNetworkManager.playerList)
            {
                if (user.GetUserName().Contains("host_"))
                    continue; //This user is a host
                else
                { 
                    nameUserToJoin = user.GetUserName();
                    break; //Found first client in the queue            
                }
            }
        }

        Data.clientName = nameUserToJoin;


        foreach (JoinToVideoCall client in clientList)
        {
            if (client.pv.Owner.NickName.Equals(hostName)) //If it's the host assigned to the call
            {
                if (client.pv.IsMine && SceneManager.GetActiveScene().name == "SceneHost") //If is the owner and the scene is SceneHost
                {
                    TMP_Text uiUserName = GameObject.FindGameObjectWithTag("ClientText").GetComponent<TMP_Text>();

                    if (uiUserName != null)
                    {
                        uiUserName.text = nameUserToJoin; //Set name in the UI
                        break; //Found            
                    }
                }

                continue; 
            }                


            if (client.pv.Owner.NickName.Equals(nameUserToJoin) && client.pv.IsMine)
            {
                Data.hostName = hostName;
                Data.PhotonChannel = photonChannel;
                Data.AgoraChannel = agoraChannel;
                Data.hostNameFirebase = hostRealName;
                Data.hostEmailFirebase = hostEmail;                

                lobbyNetworkManager.ExitLobby();
            }                
        }        
    }

    public void NextClient()
    {
        pv.RPC("NextClientRPC", RpcTarget.All, Data.currentName, Data.PhotonChannel, Data.AgoraChannel, Data.hostEmailFirebase, Data.hostNameFirebase);
    }



    [PunRPC]
    public void NextClientRPC(string hostName, string photonChannel, string agoraChannel, string hostEmail, string hostRealName)
    {     
        SearchForClient(hostName, photonChannel, agoraChannel, hostEmail, hostRealName);
    }

    

}
