using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childcollider : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject find;
    void Start()
    {
        find = GameObject.Find("GameLogic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Orb")
        {
            //this.collider2D.attachedRigidbody.SendMessage("OnCollisionEnter2D", collision);
            //this.transform.parent.GetComponent<Playerbot>().GetRoute(collision.gameObject);
            //find.GetComponent<Playerbot>().GetRoute(collision.gameObject);
            Debug.Log("entrou");
        }
        //else if (collision.gameObject.tag == "Bot")
        //{
        //    //this.collider2D.attachedRigidbody.SendMessage("OnCollisionEnter2D", collision);
        //    if(this.transform.parent.transform.position != collision.gameObject.transform.position)
        //    {
        //        this.transform.parent.GetComponent<move>().Flee(collision.gameObject);
        //        Debug.Log(collision.gameObject.name);
        //    }
        //}
    }
}
