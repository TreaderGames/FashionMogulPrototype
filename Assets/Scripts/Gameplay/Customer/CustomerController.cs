using UnityEngine;
using System;

public class CustomerController : MonoBehaviour
{
    [SerializeField] CustomerData[] customerDatas;
    [SerializeField] Customer customerTemplate;

    Customer[] activeCustomers;

    #region Unity
    // Start is called before the first frame update
    void Start()
    {
        activeCustomers = new Customer[customerDatas.Length];
        SpawnCustomers();
    }

    private void OnDisable()
    {
        for (int i = 0; i < activeCustomers.Length; i++)
        {
            activeCustomers[i]?.RemoveListener(HandleCustomerStateChanged);
        }
    }
    #endregion

    #region Private

    private void SpawnCustomers()
    {
        WardrobeData.WardrobeType wardrobeType = GetRandomWardrobeType();
        for (int i = 0; i < customerDatas.Length; i++)
        {
            Customer customer = Instantiate<Customer>(customerTemplate, transform);
            customerDatas[i].wardrobeRequest = wardrobeType;
            customer.InitCustomer(customerDatas[i]);
            activeCustomers[i] = customer;

            customer.AddListener(HandleCustomerStateChanged);
        }
    }

    private WardrobeData.WardrobeType GetRandomWardrobeType()
    {
        int randomNum;
        int enumCount = Enum.GetNames(typeof(WardrobeData.WardrobeType)).Length;

        randomNum = UnityEngine.Random.Range(1, enumCount);

        return (WardrobeData.WardrobeType)randomNum;
    }

    private void SendCustomerBack(Customer customer)
    {
        customer.pCustomerData.wardrobeRequest = GetRandomWardrobeType();
        customer.ChangeState(CustomerState.MovingToMirror);
    }
    #endregion

    #region Callback
    private void HandleCustomerStateChanged(CustomerState customerState, Customer customer)
    {
        switch (customerState)
        {
            case CustomerState.None:
                SendCustomerBack(customer);
                break;
        }
    }
    #endregion
}
