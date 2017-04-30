using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public class Data
    {
        public int id;
        public Vector3 position;

        public int health;
    };

    public int health;

    public static int nextId = 0;

    public Data data;

    public float moveTime = 0.1f;
    public bool isMoving = false;

    new private BoxCollider2D collider;
    new private Rigidbody2D rigidbody;
    private float inverseMoveTime;

    protected virtual void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();

        data = new Data();
        data.id = nextId++;
        data.position = transform.position;

        inverseMoveTime = 1f / moveTime;
    }

    public abstract void DoTurn();

    protected bool Move(Vector2 dir, out RaycastHit2D rayHit)
    {
        Vector2 start = transform.position;
        Vector2 dest = start + dir;

        collider.enabled = false;
        rayHit = Physics2D.Linecast(start, dest);
        collider.enabled = true;

        // Nothing in the way, move
        if(rayHit.transform == null)
        {
            data.position = dest;
            StartCoroutine(SmoothMoveTo(dest));
            return true;
        }

        return false;
    }

    protected void Attack(int damage, Vector2 dir, Entity entity)
    {
        entity.TakeDamage(damage);
        StartCoroutine(AttackAnimation(dir));
    }

    protected abstract void TakeDamage(int amount);

    protected IEnumerator SmoothMoveTo(Vector3 dest)
    {
        float sqrDistance = (transform.position - dest).sqrMagnitude;

        isMoving = true;
        GameManager.singleton.somethingMoving = true;
        while(sqrDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigidbody.position, dest, inverseMoveTime * Time.deltaTime);
            rigidbody.MovePosition(newPosition);

            sqrDistance = (transform.position - dest).sqrMagnitude;

            yield return null;
        }

        isMoving = false;
        GameManager.singleton.somethingMoving = false;
    }

    protected IEnumerator AttackAnimation(Vector3 dir)
    {
        Vector3 start = transform.position;
        Vector3 dest = start + dir * 0.5f;
        float sqrDistance = (transform.position - dest).sqrMagnitude;

        isMoving = true;
        GameManager.singleton.somethingMoving = true;
        while(sqrDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigidbody.position, dest, inverseMoveTime * Time.deltaTime);
            rigidbody.MovePosition(newPosition);

            sqrDistance = (transform.position - dest).sqrMagnitude;

            yield return null;
        }

        sqrDistance = (transform.position - start).sqrMagnitude;
        while(sqrDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigidbody.position, start, inverseMoveTime * Time.deltaTime);
            rigidbody.MovePosition(newPosition);

            sqrDistance = (transform.position - start).sqrMagnitude;

            yield return null;
        }

        isMoving = false;
        GameManager.singleton.somethingMoving = false;
    }
}
