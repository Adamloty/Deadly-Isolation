using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cc : MonoBehaviour
{

    public float spead = 100f;
    [SerializeField]private Transform player;
    float rotationf=0f;
    public float max;
    public float min;
    //private Move move;
    //private LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        //layerMask = ~((1 << LayerMask.NameToLayer(transform.name)));
   
       // else
        //{
            //move=this.transform.parent.GetComponent<Move>();
            Cursorvisible();
            spead = PlayerPrefs.GetFloat("sense");
        //}
    }

    // Update is called once per frame
    void Update()
    {
       // if(!move.foc)
       // {
            camermouse();
        //}
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

    }
    private void camermouse()
    {
        float mousex = Input.GetAxis("Mouse X") * spead * Time.deltaTime;
        float mousey = Input.GetAxis("Mouse Y") * spead * Time.deltaTime;
        rotationf -= mousey;
        //rotationf = Mathf.Clamp(rotationf, min, max);
        rotationf = Mathf.Clamp(rotationf, min, max);
        transform.localRotation = Quaternion.Euler(rotationf, 0, 0);
        //player.Rotate(0,mousex,0);
        // player.Rotate(new Vector3(0, 1, 0) * mousex);
        player.Rotate(Vector3.up * mousex);

    }
    private void Cursorvisible()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
