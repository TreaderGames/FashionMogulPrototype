using UnityEngine;
using System;

public class Customer : MonoBehaviour
{
    [SerializeField] Animator animator;

    CustomerData customerData;
    Action<CustomerState> customerStateChange;

    public CustomerData pCustomerData { get => customerData; }

    #region Public
    public void InitCustomer(CustomerData inCustomerData)
    {
        customerData = inCustomerData;
        transform.position = customerData.startPos.position;
        ChangeState(CustomerState.MovingToMirror);
    }

    public void ChangeState(CustomerState customerState)
    {
        customerData.customerState = customerState;

        switch(customerState)
        {
            case CustomerState.MovingToMirror:
            case CustomerState.MovingOut:
                animator.SetBool(AnimationKeys.WALK_ANIMATION_KEY, true);
                break;
            case CustomerState.None:
            case CustomerState.Waiting:
                animator.SetBool(AnimationKeys.WALK_ANIMATION_KEY, false);
                break;
        }

        customerStateChange?.Invoke(customerState);
    }

    public void AddListener(Action<CustomerState> action)
    {
        customerStateChange += action;
    }
    public void RemoveListener(Action<CustomerState> action)
    {
        customerStateChange -= action;
    }
    #endregion
}
