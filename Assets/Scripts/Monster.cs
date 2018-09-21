using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit 
{
    protected virtual void Awake()
    {

    }


    // Use this for initialization
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    // Когда коллайдер мостра соприкасаеться с монстром, то он умирает (удаляеться со сцены)
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();

        if (character)
        {
            // Если мы пругнули на монстра то его убить
            if (Mathf.Abs(character.transform.position.x - transform.position.x) < 0.5f)
            {
                ReceiveDamage();
            }
            // иначе получаем урон
            else
            {
                character.ReceiveDamage();
            }
        }
    }

}
