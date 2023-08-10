using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    [SerializeField] Customer customer;
    [SerializeField] CustomerCollider customerCollider;
    [SerializeField] float speed = 4;
    [SerializeField] float distThreshold = 0.1f;

    Vector3 target;
    Vector3 moveDirection;
    bool canMove = false;

    #region Unity

    private void OnEnable()
    {
        customer.AddListener(HandleCustomerStateChanged);
        customerCollider.AddListener(HandleCustomerCollisionWithPlayer);
    }

    private void OnDisable()
    {
        customer?.RemoveListener(HandleCustomerStateChanged);
        customerCollider?.RemoveListener(HandleCustomerCollisionWithPlayer);
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
        StartMoving();
    }
    private void StartMovingTowardStart()
    {
        target = customer.pCustomerData.startPos.position;
        StartMoving();
    }

    private void StartMoving()
    {
        moveDirection = Vector3.Normalize(target - transform.position);
        transform.LookAt(target);
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
            else if (customer.pCustomerData.customerState == CustomerState.MovingOut)
            {
                customer.ChangeState(CustomerState.None);
            }
        }
    }
    #endregion

    #region Callback
    private void HandleCustomerStateChanged(CustomerState customerState, Customer customer)
    {
        switch(customerState)
        {
            case CustomerState.MovingToMirror:
                StartMovingTowardMirrior();
                break;
            case CustomerState.MovingOut:
                StartMovingTowardStart();
                break;
            case CustomerState.None:
            case CustomerState.Waiting:
                canMove = false;
                break;
        }
    }

    private void HandleCustomerCollisionWithPlayer()
    {
        bool removed = PlayerDataController.Instance.RemoveFromWardrobe(customer.pCustomerData.wardrobeRequest);

        if(removed)
        {
            customer.ChangeState(CustomerState.MovingOut);
        }
    }
    #endregion
}
