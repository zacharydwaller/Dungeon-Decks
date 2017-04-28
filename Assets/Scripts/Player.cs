using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public int armor;

    Vector2 prevInput;

    protected override void Start()
    {
        base.Start();

        prevInput = Vector2.zero;
    }

    private void Update()
    {
        if(!GameManager.singleton.isPlayerTurn) return;

        DoTurn();
    }

    public override void DoTurn()
    {
        Vector2 input = new Vector2();
        input.x = Mathf.Round(Input.GetAxisRaw("Horizontal"));
        input.y = Mathf.Round(Input.GetAxisRaw("Vertical"));

        if(Input.GetKey(KeyCode.Space))
        {
            GameManager.singleton.EndPlayerTurn();
            return;
        }

        if(!isMoving && (input.x != 0 || input.y != 0))
        {
            RaycastHit2D rayHit;
            if(!Move(input, out rayHit))
            {
                // Hit something
                if(rayHit.transform.tag == "Enemy")
                {
                    Attack(input, rayHit.transform.GetComponent<Enemy>());
                }
            }

            GameManager.singleton.EndPlayerTurn();
        }
    }
}
