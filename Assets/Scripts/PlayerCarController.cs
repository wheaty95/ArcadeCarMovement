using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private CarController m_car;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            m_car.HorizontalMovement(-1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_car.HorizontalMovement(1);
        }

        if (Input.GetKey(KeyCode.W))
        {
            m_car.VerticalMovement(1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_car.VerticalMovement(-1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            m_car.HorizontalMovement(0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            m_car.HorizontalMovement(0);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            m_car.VerticalMovement(0);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            m_car.VerticalMovement(0);
        }
    }
}