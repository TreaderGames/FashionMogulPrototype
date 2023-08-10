using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    [SerializeField] Customer customer;
    [SerializeField] float speed = 4;
    [SerializeField] float distThreshold = 0.1f;

    Vector3 target;
    Vector3 moveDirection;
    bool canMove = false;

    #region Unity

    private void OnEnable()
    {
        customer.AddListener(HandleCustomerStateChanged);
    }

    private void OnDisable()
    {
        customer?.RemoveListener(HandleCustomerStateChanged);
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            Move();
        }
    }
    #endregion

    #region Private
    private void StartMovingTowardMirrior()
    {
        target = customer.pCustomerData.endPos.position;
        moveDirection = Vector3.Normalize(target - transform.position);
        transform.LookAt(moveDirection);
        canMove = true;
    }

    private void Move()
    {
        transform.position += (speed * Time.deltaTime) * moveDirection;

        if(Vector3.SqrMagnitude(target - transform.position) < distThreshold)
        {
            if (customer.pCustomerData.customerState == CustomerState.MovingToMirror)
            {
                customer.ChangeState(CustomerState.Waiting);
            }
        }
    }
    #endregion

    #region Callback
    private void HandleCustomerStateChanged(CustomerState customerState)
    {
        switch(customerState)
        {
            case CustomerState.MovingToMirror:
                StartMovingTowardMirrior();
                break;
            case CustomerState.None:
            case CustomerState.Waiting:
                canMove = false;
                break;
        }
    }
    #endregion
}
