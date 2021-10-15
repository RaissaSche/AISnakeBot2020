using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteCreate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GameObject square = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //square.transform.position = this.transform.position;

        GameObject loadedObject = Resources.Load<GameObject>("Circle");
        GameObject myNewSmoke = Instantiate(loadedObject, this.transform.position, 
            Quaternion.identity, this.gameObject.transform);
        myNewSmoke.name = "area";
        myNewSmoke.transform.localScale = new Vector3(8.094784f, 8.094784f, 8.094784f);
        //myNewSmoke.transform.parent = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
