using System;
using UnityEngine;

public class CustomerOrderIndicator : MonoBehaviour
{
    [Serializable]
    public struct IndicatorColorData
    {
        public WardrobeData.WardrobeType wardrobeType;
        public Color color;
    }

    [SerializeField] Customer customer;
    [SerializeField] IndicatorColorData[] indicatorColorDatas;
    [SerializeField] MeshRenderer indicator;

    #region Unity

    private void OnEnable()
    {
        customer.AddListener(HandleCustomerStateChanged);
    }

    private void OnDisable()
    {
        customer?.RemoveListener(HandleCustomerStateChanged);
    }
    #endregion

    #region Private
    private void SetIndicator(bool active)
    {
        indicator.gameObject.SetActive(active);

        if(active)
        {
            Color color = Color.white;
            for (int i = 0; i < indicatorColorDatas.Length; i++)
            {
                if(indicatorColorDatas[i].wardrobeType.Equals(customer.pCustomerData.wardrobeRequest))
                {
                    color = indicatorColorDatas[i].color;
                }
            }

            indicator.material.color = color;
        }
    }
    #endregion

    #region Callback
    private void HandleCustomerStateChanged(CustomerState customerState)
    {
        switch (customerState)
        {
            case CustomerState.Waiting:
                SetIndicator(true);
                break;
            default:
                SetIndicator(false);
                break;
        }
    }
    #endregion
}
