using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testepass : MonoBehaviour
{
    // Start is called before the first frame update
    float comprimento = 50f;
    void Start()
    {
        Debug.Log("entrou no "+ this.gameObject.name);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("está no" + this.gameObject.name);

        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.right, comprimento);

        if (hit.collider != null)
        {
            //Hit something, print the tag of the object
            Debug.Log("Hitting: " + hit.collider.gameObject.tag);
        }
        if (hit.collider.gameObject.tag == "Orb")
        {
            //Hit something, print the tag of the object
            Debug.Log("Hitting: Orb");
        }
        if (hit.collider.gameObject.tag == "Bot")
        {
            //Hit something, print the tag of the object
            Debug.Log("Hitting: Bot");
        }

        Debug.DrawRay(this.gameObject.transform.position, Vector2.right * comprimento, Color.red);
    }
}


//Vector3 fwd = transform.TransformDirection(Vector3.forward);

//if (Physics.Raycast(transform.position, fwd, 10))
//    print("There is something in front of the object!");
////pseudo-código
//if (collider da comida){
//    //MétodoComida()
//}
//if (collider de inimigo){
//    //MétodoInimigo()
//}