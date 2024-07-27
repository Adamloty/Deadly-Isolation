using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;
    [SerializeField]private float minX;
    [SerializeField]private float maxX;
    [SerializeField]private float minZ;
    [SerializeField]private float maxZ;
    [SerializeField] private GameObject bulites;
    //[SerializeField] public GameObject[] Players;
    private Color takecolor;
   
   // RoomInfo roomInfo;
  //  RoomOptions roomOptions;
    // Start is called before the first frame update
    void Start()
    {
        //if(GameObject.FindWithTag("bulite"))
        //{
        //    Destroy(GameObject.FindWithTag("bulite"));
        //}
        
        takecolor = Random.ColorHSV();
        Vector3 spawnpos = new Vector3(Random.Range(this.transform.position.x+minX,this.transform.position.x+maxX),20, Random.Range(this.transform.position.z+minZ,this.transform.position.z+maxZ));
        GameObject Player= PhotonNetwork.Instantiate(player.name, spawnpos, Quaternion.identity);
        Player.GetComponent<MeshRenderer>().material.color = takecolor;
        //Players=new GameObject[Players.Length];
        //Players[Players.Length - 1] = Player;

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
