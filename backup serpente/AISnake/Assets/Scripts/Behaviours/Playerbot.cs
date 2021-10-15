using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/Playerbot")]
public class Playerbot : AIBehaviour
{

    public static int state = 0;
    public bool ativo = false;
    public GameObject obj;
    public static List<GameObject> list = new List<GameObject>();
    MonoBehaviour mb;
    public static Vector2 pos;

    public List<GameObject> orbs = new List<GameObject>();
    public List<GameObject> bots = new List<GameObject>();
    public List<GameObject> bodys = new List<GameObject>();

    public int radius = 5;
    public GameObject player;

    public override void Init(GameObject own, SnakeMovement ownMove)
    {
        base.Init(own, ownMove);
        ownerMovement.StartCoroutine(UpdateDirEveryXSeconds(timeChangeDir));
        player = GameObject.Find("SnakeBot0");
        Debug.Log(player.name);
        
    }

    //seria interessante ter um controlador com o colisor que define o mundo pra poder gerar pontos dentro desse colisor

    public override void Execute()
    {
        //MoveForward();
        //Debug.Log("loop");

        switch (state)
        {
            case 0:
                //MoveRandom();
                MoveForward();
                break;
            case 1:
                //MoveFood();
                break;
            case 2:
                //MoveFlee();
                break;
        }
    }

    //ia basica, move, muda de direcao e move
    void MoveForward()
    {
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, rotation, ownerMovement.speed * Time.deltaTime);

        owner.transform.position = Vector2.MoveTowards(owner.transform.position, randomPoint, ownerMovement.speed * Time.deltaTime);
    }

    void MouseRotationSnake()
    {

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - owner.transform.position;
        direction.z = 0.0f;

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, rotation, ownerMovement.speed * Time.deltaTime);
    }

    IEnumerator UpdateDirEveryXSeconds(float x)
    {
        yield return new WaitForSeconds(x);
        ownerMovement.StopCoroutine(UpdateDirEveryXSeconds(x));
        randomPoint = new Vector3(
                Random.Range(
                    Random.Range(owner.transform.position.x - 10, owner.transform.position.x - 5),
                    Random.Range(owner.transform.position.x + 5, owner.transform.position.x + 10)
                ),
                Random.Range(
                    Random.Range(owner.transform.position.y - 10, owner.transform.position.y - 5),
                    Random.Range(owner.transform.position.y + 5, owner.transform.position.y + 10)
                ),
                0
            );
        direction = randomPoint - owner.transform.position;
        direction.z = 0.0f;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(owner.transform.position, radius);
        //OnDrawGizmosSelected();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Orb"))
            {
                orbs.Add(hitCollider.gameObject);
                //Debug.Log("entrou orb, " + orbs.Count);
            }
            if (hitCollider.CompareTag("Bot") && hitCollider.gameObject != owner.gameObject)
            {
                bots.Add(hitCollider.gameObject);
                Debug.Log("entrou bot, " + hitCollider.gameObject.name 
                    + ", " + hitCollider.gameObject.transform.position + ", " + hitCollider.transform.parent.name);
            }
            if (hitCollider.CompareTag("Body"))
            {
                {
                    //bool notchild = false;
                    //foreach (Transform child in player.transform)
                    //{
                    //    if (child != hitCollider.gameObject.transform && hitCollider.gameObject != owner.gameObject)
                    //    {
                    //        notchild = true;
                    //        break;
                    //    }
                    //    //Debug.Log("Foreach loop: " + child);
                    //}

                    //if (notchild == true)
                    //{
                    //    bodys.Add(hitCollider.gameObject);
                    //    Debug.Log("entrou body, " + hitCollider.gameObject.name + ", " + hitCollider.gameObject.transform.position);
                    //}
                }

                if(hitCollider.transform.parent.name != player.name)
                {
                    bodys.Add(hitCollider.gameObject);
                    Debug.Log("entrou body, " + hitCollider.gameObject.name 
                        + ", " + hitCollider.gameObject.transform.position + ", " + hitCollider.transform.parent.name);
                }
            }
        }


        ownerMovement.StartCoroutine(UpdateDirEveryXSeconds(x));
    }

    public static void GetRoute(GameObject obj)
    {
        //alvo = obj;
        if (FindInList(obj) == false)
        {
            list.Add(obj);
            state = 1;
            //Debug.Log("colidiu");
        }

    }

    public static bool FindInList(GameObject obj)
    {
        bool r = false;
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == obj)
                {
                    r = true;
                    break;
                }

            }
        }
        return r;
    }

    void MoveFood()
    {
        float currentDist = 5000;
        int currentIndex = 0;
        for (int i = 0; i < list.Count; i++)
        {
            float d = Vector2.Distance(list[i].transform.position, owner.transform.position);
            if (d < currentDist)
            {
                currentDist = d;
                currentIndex = i;
            }
        }
        owner.transform.position = Vector3.MoveTowards(owner.transform.position, list[currentIndex].transform.position, ownerMovement.speed * Time.deltaTime);
    }

    void MoveFlee()
    {
        if (pos.x < owner.transform.position.x)
            owner.transform.position = owner.transform.position + (owner.transform.right * -1 * ownerMovement.speed * Time.deltaTime);
        if (pos.x > owner.transform.position.x)
            owner.transform.position = owner.transform.position + (owner.transform.right * ownerMovement.speed * Time.deltaTime);
        if (pos.y < owner.transform.position.y)
            owner.transform.position = owner.transform.position + (owner.transform.up * ownerMovement.speed * Time.deltaTime);
        if (pos.y > owner.transform.position.y)
            owner.transform.position = owner.transform.position + (owner.transform.up * -1 * ownerMovement.speed * Time.deltaTime);
    }

    public static void PrepareToRun(Vector2 n)
    {
        list.Clear();
        pos = new Vector2(n.x, n.y);
        state = 2;
    }
}
