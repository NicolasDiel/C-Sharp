using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumbersUsers : MonoBehaviour
{
    private LobbyNetworkManager scriptLobbyNetworkManager;

    [SerializeField] private TMP_Text txtCount;

    private bool running = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject inst = GameObject.FindGameObjectWithTag("Lobby");

        scriptLobbyNetworkManager = inst.GetComponent<LobbyNetworkManager>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (running == false)
            StartCoroutine(CheckNumberUsersInTheLobby());
    }

    IEnumerator CheckNumberUsersInTheLobby()
    {
        running = true;

        int number = (scriptLobbyNetworkManager.playerList.Count - 1); //Numbers of clients in the lobby (one is the host)

        if (number < 1) //Avoid values minors than 0
            number = 0;


        txtCount.text = number.ToString();

        yield return new WaitForSeconds(2f);

        running = false;

    }


}
