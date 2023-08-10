using UnityEngine;

public class Customer : MonoBehaviour
{
    CustomerData customerData;

    #region Public
    public void InitCustomer(CustomerData inCustomerData)
    {
        customerData = inCustomerData;
        transform.position = customerData.startPos.position;
    }

    public void ChangeState(CustomerState customerState)
    {
        customerData.customerState = customerState;

        switch(customerState)
        {
            case CustomerState.MovingToMirror:
                break;
        }
    }
    #endregion
}
