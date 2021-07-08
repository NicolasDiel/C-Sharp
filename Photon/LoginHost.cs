using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginHost : MonoBehaviour
{
    [SerializeField] private Text txt;
    [SerializeField] private GameObject panelButton;
    [SerializeField] private Toggle useShopLinkUI;

    [SerializeField] private PhotonChatManager chatManager;
    [SerializeField] private ButtonManager agora;
    [SerializeField] private LobbyNetworkManager lobbyNetworkManager;

    public string password = "setpassword";


    private void Update()
    {
        if (CheckPassword())
            panelButton.SetActive(true);
        else
            panelButton.SetActive(false);
    }


    public void GoToNextScene()
    {
        if (CheckPassword())
        {
            Data.currentName = "host"; //Set user name
            lobbyNetworkManager.Initialize(); //Connect to masterServer Photon
            chatManager.ConnectUser(); //Connect to PhotonChat
            agora.OnJoinVideoClicked(); //Connect to Agora Video Call            
        }            


    }

    private bool CheckPassword()
    {
        if (txt.text.Equals(password))
        {
            return true;
        }            
        else
        {
            return false;
        }
    }

    public void UseShopLink(bool value)
    {
        Data.shopLink = useShopLinkUI.isOn;

        Debug.Log(Data.shopLink);
    }

    public void UseRearCamera(bool use)
    {
        Data.useRearCamera = use;
    }

}
