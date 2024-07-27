using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class createorjoinroom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createinput;
    [SerializeField] private TMP_InputField joininput;
    private bool Updatelist;
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.InLobby&&!PhotonNetwork.ConnectUsingSettings())
        {
            PhotonNetwork.ConnectUsingSettings();
            print("Connecting...");
        }
        //if(PhotonNetwork.InLobby)
        //{
        //    PhotonNetwork.CreateRoom(Random.Range(0,100000000000000000).ToString());
        //    PhotonNetwork.LeaveRoom(this);
            
        //    Updatelist = true;
        //}
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel(1);
    }
    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        if(!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
            print("Connected...Done");
        }
       
    }
    public override void OnJoinedRoom()
    {
       // if(Updatelist)
        //{
            PhotonNetwork.LoadLevel("Game");
      //  }
        
    }
    public void JoinRoomList(string RoomName)
    {
        PhotonNetwork.JoinRoom(RoomName);
    }
    void Update()
    {
        
    }
    public void Createroom()
    {
        if(PhotonNetwork.InLobby)
        {
            PhotonNetwork.CreateRoom(createinput.text);
        }
       
    }
    public void JoinRoom()
    {
        if(PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinRoom(joininput.text);
        }
    }
}
