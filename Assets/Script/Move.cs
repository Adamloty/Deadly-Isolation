using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using UnityEngine.SceneManagement;
//using System.Security.Cryptography;
public class move : MonoBehaviourPunCallbacks
{
    private float Gravity = 20f;
    [SerializeField] private float jump = 20f;
    public float speed = 1f;
    Vector3 moveDirection = Vector3.zero;
    private CharacterController ch;
    private PhotonView pv;
    private Animator anim;
    [SerializeField] private TextMeshPro Username;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ch = GetComponent<CharacterController>();
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
           
      
            Destroy(transform.GetChild(20).gameObject);
        }
        if(!pv.IsMine)
        {
            Destroy(transform.GetChild(21).gameObject);
           
        }
   
       
    }

    // Update is called once per frame
    void Update()
    {
    
        if (pv.IsMine)
        {

            PhotonNetwork.NickName = PlayerPrefs.GetString("ur");
            pv.RPC("TakeName", RpcTarget.All, PlayerPrefs.GetString("ur"));
            Username.gameObject.SetActive(false);
            if (ch.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection *= speed;
                moveDirection = transform.TransformDirection(moveDirection);
                if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
                {
                   // anim.SetBool("", false);
                    if (speed == 1 && !Input.GetKey(KeyCode.LeftShift))
                    {
                        anim.SetBool("Walk", true);
                        anim.SetBool("Run", false);
                    }
                    if (speed == 4)
                    {
                        anim.SetBool("Walk", false);
                        anim.SetBool("Run", true);
                    }


                }
                else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);
                }
                if (Input.GetButtonDown("Jump"))
                {
                    anim.SetTrigger("Jump");
                    moveDirection.y = jump;
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    speed = 4f;
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    speed = 1f;
                }
            }
            moveDirection.y -= Gravity * Time.deltaTime;
            ch.Move(moveDirection * Time.deltaTime);
        }
    }

    [PunRPC]
    public void TakeName(string name)
    {
        // Destroy(this.gameObject, 0.1f);
        //  PhotonNetwork.NickName= PlayerPrefs.GetString("ur");
        this.Username.text = name;

    }
    private void OnTriggerEnter(Collider other)
    {
      //  if(pv.IsMine)
       // {
           // if(other.CompareTag("Player"))
           // {
                //if (other.CompareTag("bulite"))
                //{
                //    PhotonNetwork.Destroy(this.gameObject);
                //    try
                //    {
                //        PhotonNetwork.LoadLevel(1);
                //    }
                //    catch
                //    {
                //        SceneManager.LoadScene(1);
                //    }

                //}
            //}
     //   }
   
       // if(!pv.IsMine)
      //  {
            //if(other.CompareTag("Player"))
            //{
            //    if(other.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(2).gameObject.activeSelf)
            //    {
            //        this.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(2).gameObject.SetActive(true);
            //        other.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(2).gameObject.SetActive(false);
            //    }
            //    else
            //    {
            //        this.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(2).gameObject.SetActive(false);
            //        other.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(2).gameObject.SetActive(true);
            //    }
            //}
      //  }
    }
    // [PunRPC]
    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LoadLevel(1);
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    //private void Animation()
    //{
    //    if(Input.GetAxisRaw("Vertical")!=0||Input.GetAxisRaw("Horizontal")!=0)
    //    {
    //        if(speed==6&&!Input.GetKey(KeyCode.LeftShift))
    //        {
    //            anim.SetBool("Walk", true);
    //            anim.SetBool("Run", false);
    //        }
    //        if (speed == 12)
    //        {
    //            anim.SetBool("Walk", false);
    //            anim.SetBool("Run", true);
    //        }


    //    }
    //    else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
    //    {
    //        anim.SetBool("Walk", false);
    //        anim.SetBool("Run", false);
    //    }
    //    if(transform.position.y<-10)
    //    {
    //        anim.SetBool("Walk", false);
    //        anim.SetBool("Run", false);
    //    }

    //}
    //private void Awake()
    //{
    //   // if (pv.IsMine)
    //  //  {
    //    try
    //    {
    //        if (PlayerLeftanim == null)
    //        {
    //            PlayerLeftanim = GameObject.FindWithTag("PlayerLeft").GetComponent<Animator>();
    //        }
    //        if (PlayerLeftanim != null && PlayerLeftanim == null)
    //        {
    //            playerleftname = PlayerLeftanim.gameObject.GetComponentInChildren<Text>();
    //        }
    //    }
    //    catch
    //    {
    //        print("Nothing");
    //    }

    //   // }

    //}
}
