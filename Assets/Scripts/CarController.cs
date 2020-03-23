using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] private GameObject m_carModel;
    [SerializeField] private Rigidbody m_carMovementSphere;
    [SerializeField] private Vector3 m_carModelOffset;
    [Header("Speed")]
    [SerializeField] private float m_speed;
    [Header("Steer")]
    [SerializeField] private float m_steer;
    [Header("Forces")]
    [SerializeField] private float m_gravity;
    [SerializeField] private float m_groundLength = 1;
    [SerializeField] private LayerMask m_carMask;

    private Vector3 m_inputDir;
    private float m_targetRot;
    private float m_currentNormalisedSpeed;
    RaycastHit m_hit;
    bool m_grounded;

    private void FixedUpdate()
    {
        m_carMovementSphere.AddForce(Vector3.down * m_gravity, ForceMode.Acceleration);
        Move();

        m_grounded = Physics.Raycast(m_carMovementSphere.transform.position, Vector3.down, out m_hit, m_groundLength, m_carMask);
        m_carModel.transform.up = m_hit.normal;
    }

    public void HorizontalMovement(float amount)
    {
        m_inputDir.x = amount;
    }

    public void VerticalMovement(float amount)
    {
        m_inputDir.z = amount;
    }

    private void Move()
    {
        m_currentNormalisedSpeed = Mathf.SmoothStep(m_currentNormalisedSpeed, m_inputDir.z, Time.fixedDeltaTime * m_speed);
        if (m_grounded)
        {
            m_carMovementSphere.velocity = m_carMovementSphere.transform.forward * (m_currentNormalisedSpeed * m_speed);
        }
    }

    private void Update()
    {
        SetModelPosition();
        Steer(m_inputDir.x > 0 ? 1 : -1 , Mathf.Abs(m_inputDir.x));
    }

    private void Steer(float dir, float amount)
    {
        m_targetRot = (m_steer * dir) * amount;
        m_carMovementSphere.transform.eulerAngles = new Vector3(0.0f, m_carMovementSphere.transform.eulerAngles.y+ m_targetRot, 0.0f);
        m_carModel.transform.rotation = m_carMovementSphere.transform.rotation;
        m_carModel.transform.rotation = Quaternion.FromToRotation(m_carModel.transform.up, m_hit.normal) * m_carModel.transform.rotation;
    }

    private void SetModelPosition()
    {
        m_carModel.transform.position = m_carMovementSphere.transform.position + m_carModelOffset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(m_carMovementSphere.transform.position, m_carMovementSphere.transform.position + (Vector3.down * m_groundLength));
    }
}