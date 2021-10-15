using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testepass : MonoBehaviour
{
    public float comprimento = 20f;
    void Start()
    {
        //Debug.Log("entrou no "+ this.gameObject.name);
    }

    void FixedUpdate()
    {
        //Debug.Log("está no" + this.gameObject.name);

        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, this.transform.TransformDirection(Vector2.up), comprimento);
        Debug.DrawRay(this.gameObject.transform.position, this.transform.TransformDirection(Vector2.up) * comprimento, Color.red);

        if (hit.collider != null && hit.collider.gameObject.name != "GameLogic")//hit.collider.gameObject != this.gameObject || hit.collider.gameObject != this.gameObject.GetComponentInChildren<GameObject>()
        {
            //Hit something, print the tag of the object
            //Debug.DrawRay(this.gameObject.transform.position, this.transform.TransformDirection(Vector2.up) * hit.distance, Color.white);
            //Debug.Log("Hitting: " + hit.collider.gameObject.name);

            if (hit.collider.gameObject.tag == "Orb")
            {
                //Playerbot.GetRoute(hit.collider.gameObject);
                Debug.Log("Hitting: " + hit.collider.gameObject.name);
            }
            else if (hit.collider.gameObject.tag == "Bot" && hit.collider.gameObject != this.gameObject)
            {
                //Playerbot.PrepareToRun(hit.collider.gameObject.transform.position);
                Debug.Log("Hitting: " + hit.collider.gameObject.name);
            }
            //else if (hit.collider.gameObject.tag == "Body")
            //{
            //    //Playerbot.PrepareToRun(hit.collider.gameObject.transform.position);
            //    Debug.Log("Hitting: " + hit.collider.gameObject.name);
            //}

        }
    }
}