using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Voice.PUN;
using UnityEngine.SceneManagement;
using Photon.Voice.Unity;
//using System.Security.Cryptography;
public class move : MonoBehaviourPunCallbacks
{
    //private float Gravity = 20f;
    [SerializeField] private float jump = 10000f;
    public float speed = 1f;
    Vector3 moveDirection = Vector3.zero;
   // private CharacterController ch;
    private PhotonView pv;
    private Animator anim;
    [SerializeField] private TextMeshPro Username;
    private bool shiftdown;
    private bool reconnecting = false;
    private VoiceConnection voiceConnection;
    private Rigidbody rb;
    private bool isGrounded;
    public float gravityMultiplier = 0.5f;
    public float minFallSpeed = -5f; // الحد الأدنى للسرعة أثناء النزول
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //ch = GetComponent<CharacterController>();
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
           
      
            Destroy(transform.GetChild(20).gameObject);
        }
        if(!pv.IsMine)
        {
            Destroy(transform.GetChild(21).gameObject);
           
        }
        voiceConnection = FindObjectOfType<VoiceConnection>();
        if (voiceConnection == null)
        {
            Debug.LogError("VoiceConnection component not found. Please add it to your scene.");
        }

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            rb.AddForce(Physics.gravity * (gravityMultiplier - 1) * rb.mass);
        }

        // التحقق من إذا كان الكائن على الأرض باستخدام Raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 230f); // 

    }
    void Update()
    {
    
        if (pv.IsMine)
        {

            PhotonNetwork.NickName = PlayerPrefs.GetString("ur");
            pv.RPC("TakeName", RpcTarget.All, PlayerPrefs.GetString("ur"));
            Username.gameObject.SetActive(false);
            //if (ch.isGrounded)
         //   {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                if(Input.GetKeyDown(KeyCode.LeftControl)||Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    shiftdown = !shiftdown;
                    if(!shiftdown)
                    {
                    //rb.velocity = new Vector3(0, 1, 0);
                    //transform.Translate(0, jump-50, 0);
                    rb.AddForce(Vector3.up * (jump-50), ForceMode.Impulse);
                }
                }
                if(shiftdown)
                {
                    anim.SetBool("crouch", true);
                    moveDirection *= speed-50f;
                }
                else
                {
                    anim.SetBool("crouch", false);
                    moveDirection *= speed;
                }
                if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
                {
                   // anim.SetBool("", false);
                    if (speed == 200 && !Input.GetKey(KeyCode.LeftShift))
                    {
                        anim.SetBool("Walk", true);
                        anim.SetBool("Run", false);
                    }
                    if (speed == 300)
                    {
                        anim.SetBool("Walk", false);
                        anim.SetBool("Run", true);
                        shiftdown = false;
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
                    rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
                     // transform.Translate(0, jump, 0);
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    speed = 300f;
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    speed = 100f;
                }
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);

            if (rb.velocity.y > minFallSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, minFallSpeed, rb.velocity.z);
                print("yes");
            }
        }
       
        // transform.Translate(moveDirection);
        //  moveDirection.y -= Gravity * Time.deltaTime;
        //    ch.Move(moveDirection * Time.deltaTime);
        // }
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

 
  

    // سيتم استدعاء هذا الحدث عند انقطاع الاتصال
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);

        if (!reconnecting)
        {
            reconnecting = true;
            Invoke("Reconnect", 5); // الانتظار لمدة 5 ثوانٍ قبل إعادة المحاولة
        }
    }

    // محاولة إعادة الاتصال
    private void Reconnect()
    {
        if (PhotonNetwork.ReconnectAndRejoin())
        {
            Debug.Log("Reconnecting...");
        }
        else
        {
            Debug.LogError("Reconnect and rejoin failed. Retrying...");
            Invoke("Reconnect", 5); // إعادة المحاولة بعد 5 ثوانٍ أخرى
        }
    }

    // سيتم استدعاء هذا الحدث عند النجاح في إعادة الانضمام للغرفة
    public override void OnJoinedRoom()
    {
        reconnecting = false;
        Debug.Log("Reconnected and rejoined the room successfully.");

        // إعادة الاتصال بـ Photon Voice
        ReconnectVoice();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("OnJoinRoomFailed() was called by PUN with error code {0} and message {1}", returnCode, message);
        Invoke("Reconnect", 5); // إعادة المحاولة بعد 5 ثوانٍ أخرى
    }

    // إعادة الاتصال بـ Photon Voice
    private void ReconnectVoice()
    {
        if (voiceConnection != null)
        {
            if (voiceConnection.ClientState == Photon.Realtime.ClientState.Joined)
            {
                Debug.Log("Photon Voice is already connected.");
            }
            else
            {
                Debug.Log("Reconnecting Photon Voice...");
                voiceConnection.ConnectUsingSettings();
            }
        }
    }

    // سيتم استدعاء هذا الحدث عند النجاح في الاتصال بـ Photon Voice
    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon Voice reconnected successfully.");
    }

    // التعامل مع أخطاء الاتصال بـ Photon Voice
    private void OnVoiceDisconnected(DisconnectCause cause)
    {
        Debug.LogErrorFormat("Failed to reconnect Photon Voice with reason {0}. Retrying...", cause);
        Invoke("ReconnectVoice", 5); // إعادة المحاولة بعد 5 ثوانٍ أخرى
    }
}
