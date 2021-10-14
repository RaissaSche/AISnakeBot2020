using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AIBehaviours/Playerbot")]
public class Playerbot : AIBehaviour
{
    //enum states { ANDAR=0, COMER=1, FUGIR=2};

    public int state = 0, choose = 0;
    public bool ativo = false;
    public GameObject obj;
    public List<GameObject> list = new List<GameObject>();
    MonoBehaviour mb;

    public override void Init(GameObject own, SnakeMovement ownMove)
    {
        base.Init(own, ownMove);
        ownerMovement.StartCoroutine(UpdateDirEveryXSeconds(timeChangeDir));
        //Debug.Log("começo");
        //CreateChild();
        //Debug.Log(own.name);

        //GameObject loadedObject = Resources.Load<GameObject>("Circle");
        //GameObject myNewSmoke = Instantiate(loadedObject, own.transform.position,
        //    Quaternion.identity, own.transform);
        //myNewSmoke.name = "area-1";
        //myNewSmoke.transform.localScale = new Vector3(8.0f, 8.0f, 8.0f);
        //obj = own;
        own.AddComponent<testepass>();
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

        ownerMovement.StartCoroutine(UpdateDirEveryXSeconds(x));
    }

    public void GetRoute(GameObject obj)
    {
        //alvo = obj;
        if (FindInList(obj) == false)
        {
            list.Add(obj);
            //state = 1;
            Debug.Log("colidiu");
        }

    }

    bool FindInList(GameObject obj)
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

    public void MoveRandom()
    {
        if (ativo == true)
        {
            if (choose == 0)
            {
                owner.transform.position = owner.transform.position + (owner.transform.right * -1 * 5 * Time.deltaTime);
            }
            else if (choose == 1)
            {
                owner.transform.position = owner.transform.position + (owner.transform.right * 5 * Time.deltaTime);
            }
            else if (choose == 2)
            {
                owner.transform.position = owner.transform.position + (owner.transform.up * 5 * Time.deltaTime);
            }
            else if (choose == 3)
            {
                owner.transform.position = owner.transform.position + (owner.transform.up * -1 * 5 * Time.deltaTime);
            }

        }
        else if (ativo == false)
        {
            choose = Random.Range(0, 4);
            ativo = true;
            new WaitForSeconds(2);
            ativo = false;
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2);
        ativo = false;
    }

    void MoveFood()
    {

    }

    void MoveFlee()
    {

    }
}
