using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour 
{
    // Скорость
    [SerializeField]
    private float speed = 2.0f;

    // Цель за кем камера следует
    [SerializeField]
    private Transform target;

    private void Awake()
    {
        if (!target)
        {
            target = FindObjectOfType<Character>().transform;
        }
    }

    private void Start()
    {
        //
    }

    // По кадровое изменение положения камеры в зависимости от положения нашего героя
    private void Update()
    {
        if (Input.GetKey("escape"))
            SceneManager.LoadScene(0);

        Vector3 position = target.position;
        position.z = -10.0f;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
