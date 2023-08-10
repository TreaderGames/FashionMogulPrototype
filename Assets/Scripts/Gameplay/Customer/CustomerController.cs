using UnityEngine;

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
    #endregion

    #region Private

    private void SpawnCustomers()
    {
        for (int i = 0; i < customerDatas.Length; i++)
        {
            Customer customer = Instantiate<Customer>(customerTemplate, transform);
            customer.InitCustomer(customerDatas[i]);
            activeCustomers[i] = customer;
        }
    }
    #endregion
}
