using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour 
{
    // Получение урона смерть
    public virtual void ReceiveDamage()
    {
        Die();
    }

    // Удаление объекта со сцены
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
