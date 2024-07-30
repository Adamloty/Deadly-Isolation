using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class SaveSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_InputField username_text;
    private Transform usernameparent;
    void Start()
    {
        if(username_text != null)
        {
            usernameparent = username_text.transform.parent.parent;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(username_text!=null)
        {
           // if(PlayerPrefs.HasKey("ur")||PlayerPrefs.GetString("ur")!="")
          //  {
           //     usernameparent.gameObject.SetActive(false);
           // }
        }
     //   print(PlayerPrefs.GetString("ur")+"  "+username_text.text);
    }
    public void saveUsername()
    {
       if(username_text!= null)
        {
          
            PlayerPrefs.SetString("ur",username_text.text);
            usernameparent.gameObject.SetActive(false);
        }
    }
    public void ReturnToLobby()
    {
        SceneManager.LoadScene("Loading");
    }
}
