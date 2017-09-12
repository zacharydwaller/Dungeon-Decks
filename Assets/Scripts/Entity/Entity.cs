using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public enum Type
    {
        Player, Enemy, Card
    };

    public class Data
    {
        public int id;
        public Vector3 position;
        public Type type;

        // For enemies
        public int health;

        public DBItem info;
    };

    public static int nextId = 0;

    public Data data;

    public int health;

    private List<Aura> auras;
    public int auraCount { get { return auras.Count; } }
    public int maxAuras { get { return 16; } }

    public float moveTime = 0.1f;
    public bool isMoving = false;

    new private BoxCollider2D collider;
    new private Rigidbody2D rigidbody;
    private float inverseMoveTime;

    protected virtual void Awake()
    {
        data = new Data();
        data.id = nextId++;
        data.position = base.transform.position;
    }

    protected virtual void Start()
    {
        auras = new List<Aura>();

        collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();

        inverseMoveTime = 1f / moveTime;
    }


    public abstract void DoTurn();
    public abstract void TakeDamage(int amount);

    public void ApplyAura(Aura newAura)
    {
        // If too many auras, remove the oldest aura
        if(auras.Count == maxAuras)
        {
            Aura oldAura = GetAura(0);
            oldAura.OnRemove();
            auras.RemoveAt(0);
        }

        auras.Add(newAura);
        newAura.OnAdd();
    }

    public Aura GetAura(int index)
    {
        if(index >= maxAuras || index >= auras.Count) return null;

        return auras[index];
    }

    public void TickAuras()
    {
        foreach(Aura aura in auras.ToArray())
        {
            // Aura.Tick() returns true when finished
            if(aura != null && aura.Tick())
            {
                aura.OnRemove();
                auras.Remove(aura);
            }

            if(health <= 0) break;
        }
    }

    public virtual void SetData(Data newData)
    {
        data = newData;
        transform.position = data.position;
    }

    /*
     * Checks if the entity will collide with something when moving to dir
     * out Rayhit - hit from raycast
     * Returns - true if nothing in the way, false if will collide with something
     */
    protected bool CheckMove(Vector2 dir, out RaycastHit2D rayHit)
    {
        Vector2 start = base.transform.position;
        Vector2 dest = start + dir;

        collider.enabled = false;
        rayHit = Physics2D.Linecast(start, dest);
        collider.enabled = true;

        // Nothing in the way
        if(rayHit.transform == null || rayHit.transform.tag == "Card")
        {
            return true;
        }

        return false;
    }

    protected void Move(Vector3 dir)
    {
        data.position = base.transform.position + dir;
        StartCoroutine(SmoothMoveTo(base.transform.position + dir));
    }

    public void DoAttackAnimation(Vector2 dir)
    {
        StartCoroutine(AttackAnimation(dir));
    }

    protected virtual void Die()
    {
        GameManager.singleton.UnregisterEntity(this);
    }

    protected IEnumerator SmoothMoveTo(Vector3 dest)
    {
        float sqrDistance = (base.transform.position - dest).sqrMagnitude;

        isMoving = true;
        GameManager.singleton.somethingMoving = true;
        while(sqrDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigidbody.position, dest, inverseMoveTime * Time.deltaTime);
            rigidbody.MovePosition(newPosition);
            sqrDistance = (base.transform.position - dest).sqrMagnitude;

            yield return null;
        }

        isMoving = false;
        GameManager.singleton.somethingMoving = false;
    }

    protected IEnumerator AttackAnimation(Vector3 dir)
    {
        Vector3 start = transform.position;
        Vector3 dest = start + dir * 0.5f;
        float sqrDistance = (start - dest).sqrMagnitude;

        isMoving = true;
        GameManager.singleton.somethingMoving = true;
        while(sqrDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, dest, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;

            sqrDistance = (transform.position - dest).sqrMagnitude;

            yield return null;
        }

        sqrDistance = (transform.position - start).sqrMagnitude;
        while(sqrDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, start, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;

            sqrDistance = (transform.position - start).sqrMagnitude;

            yield return null;
        }

        isMoving = false;
        GameManager.singleton.somethingMoving = false;
    }
}
