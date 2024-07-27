using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject Roomprefap;
    public GameObject[] AllRooms;
    //private RoomList Roomslist;
    private void Start()
    {
        //if(PhotonNetwork.InLobby)
        //{
           // PhotonNetwork.GetCustomRoomList(TypedLobby.Default, "");
       // }
       // else
       // {
           // print("errrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrror");
        //}
      
    }
    private void Update()
    {
       
    }
   
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
       UpdateRoomList(roomList);
        //roomlist2=roomList;
    }
    void UpdateRoomList(List<RoomInfo> roomList)
    {
        print("yeeeeeeeeeeeeeeees");
        // AllRooms=new GameObject[roomList.Count];
        for (int i = 0; i < AllRooms.Length; i++)
        {

            if (AllRooms[i] != null)
            {
                Destroy(AllRooms[i]);
            }
        }
        AllRooms = new GameObject[roomList.Count];
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1)
            {
                print(roomList[i]);
                GameObject Room = Instantiate(Roomprefap, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
                Room.GetComponent<Room>().Name.text = roomList[i].Name;
                AllRooms[i] = Room;
            }

        }
    }
}
