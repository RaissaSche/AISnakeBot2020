using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/Playerbot")]
public class Playerbot : AIBehaviour
{

    public static int state = 0;
    //public static List<GameObject> list = new List<GameObject>();
    public static Vector2 pos;

    public List<GameObject> orbs = new List<GameObject>();
    public List<GameObject> bots = new List<GameObject>();
    public List<GameObject> bodys = new List<GameObject>();

    MonoBehaviour mb;

    public int radius = 5;
    public GameObject player;
    public float delayFuga = 5;

    public override void Init(GameObject own, SnakeMovement ownMove)
    {
        base.Init(own, ownMove);
        ownerMovement.StartCoroutine(UpdateDirEveryXSeconds(timeChangeDir));
        player = GameObject.Find("SnakeBot0");
        //Debug.Log(player.name);

    }

    //seria interessante ter um controlador com o colisor que define o mundo pra poder gerar pontos dentro desse colisor

    public override void Execute()
    {
        //MoveForward();
        //Debug.Log("loop");
        Debug.Log("state " + state);

        switch (state)
        {
            case 0:
                //MoveRandom();
                MoveForward();
                break;
            case 1:
                MoveFood();
                break;
            case 2:
                MoveFlee();
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
        randomPoint = new Vector2(
                Random.Range(
                    Random.Range(owner.transform.position.x - 10, owner.transform.position.x - 5),
                    Random.Range(owner.transform.position.x + 5, owner.transform.position.x + 10)
                ),
                Random.Range(
                    Random.Range(owner.transform.position.y - 10, owner.transform.position.y - 5),
                    Random.Range(owner.transform.position.y + 5, owner.transform.position.y + 10)
                )
            );
        direction = randomPoint - owner.transform.position;

        overlap();

        ownerMovement.StartCoroutine(UpdateDirEveryXSeconds(x));
    }

    void MoveFood()
    {
        float currentDist = 5000;
        int currentIndex = 0;

        overlap();

        if (orbs.Count > 0)
        {
            for (int i = 0; i < orbs.Count; i++)
            {
                if (orbs[i] == null)
                {
                    orbs.RemoveAt(i);
                }
                else
                {
                    float d = Vector2.Distance(orbs[i].transform.position, owner.transform.position);
                    if (d < currentDist)
                    {
                        currentDist = d;
                        currentIndex = i;
                    }
                }
            }
            owner.transform.position = Vector2.MoveTowards(owner.transform.position,
                orbs[currentIndex].transform.position, ownerMovement.speed * Time.deltaTime);
        }
    }

    void MoveFlee()
    {
        //Debug.Log("fugindo");
        remove(bots);
        remove(bodys);
        if (pos.x < owner.transform.position.x)
            owner.transform.position = owner.transform.position + (owner.transform.right * -1 * ownerMovement.speed * Time.deltaTime);
        if (pos.x > owner.transform.position.x)
            owner.transform.position = owner.transform.position + (owner.transform.right * ownerMovement.speed * Time.deltaTime);
        if (pos.y < owner.transform.position.y)
            owner.transform.position = owner.transform.position + (owner.transform.up * ownerMovement.speed * Time.deltaTime);
        if (pos.y > owner.transform.position.y)
            owner.transform.position = owner.transform.position + (owner.transform.up * -1 * ownerMovement.speed * Time.deltaTime);
    }

    public void overlap()
    {
        Collider2D[] hitC = Physics2D.OverlapCircleAll(owner.transform.position, radius);
        //OnDrawGizmosSelected();

        remove(orbs);
        remove(bots);
        remove(bodys);

        foreach (var hit in hitC)
        {
            if (hit.CompareTag("Orb"))
            {
                if (orbs.Contains(hit.gameObject) == false)
                {
                    orbs.Add(hit.gameObject);
                    //Debug.Log("entrou orb, " + orbs.Count);
                }
            }
            if (hit.CompareTag("Bot") && hit.gameObject != owner.gameObject)
            {
                bots.Add(hit.gameObject);
                //Debug.Log("entrou bot, " + hit.gameObject.name
                //    + ", " + hit.gameObject.transform.position + ", " + hit.transform.parent.name);
            }
            if (hit.CompareTag("Body"))
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

                if (hit.transform.parent.name != player.name)
                {
                    bodys.Add(hit.gameObject);
                    //Debug.Log("entrou body, " + hit.gameObject.name
                    //    + ", " + hit.gameObject.transform.position + ", " + hit.transform.parent.name);
                }
            }
        }

        if (orbs.Count > 0)
        {
            state = 1;
        }
        if (bodys.Count > 0 || bots.Count > 0)
        {
            state = 2;
            pos = new Vector2(bots[0].transform.position.x, bots[0].transform.position.y);
            orbs.Clear();
            ownerMovement.StartCoroutine(RunningAway(delayFuga));
        }
        if (orbs.Count <= 0 && (bodys.Count <= 0 || bots.Count <= 0))
        {
            state = 0;
        }
    }

    public void remove(List<GameObject> l)
    {
        if (l.Count > 0)
        {
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i] == null)
                {
                    l.RemoveAt(i);
                }
            }
        }
    }

    IEnumerator RunningAway(float x)
    {
        yield return new WaitForSeconds(x);
        Debug.Log("terminou");
        bots.Clear();
        bodys.Clear();
        state = 0;
    }

}
