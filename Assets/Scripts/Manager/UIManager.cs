using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject turretShopPanel;
    [SerializeField] private GameObject nodeUIPanel;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI sellText;



    private Node _currentNodeSelected;

    public void CloseTurretShopPanel()
    {
        turretShopPanel.SetActive(false);
    }
    private void NodeSelected(Node nodeSeleted)
    {
        _currentNodeSelected = nodeSeleted;
        if(_currentNodeSelected.IsEmpty())
        {
            turretShopPanel.SetActive(true);
        }
        else
        {
            showNodeUI();
        }
    }

    public void CloseSellPanel()
    {
        nodeUIPanel.SetActive(false);
    }

    private void showNodeUI()
    {
        nodeUIPanel.SetActive(true);
        UpdateSellValue();
        
    }

    public void SellTurret()
    {
        _currentNodeSelected.SellTurret();
        _currentNodeSelected = null;
        nodeUIPanel.SetActive(false);
    }

    public void UpdateSellValue()
    {
        int sellAmount = _currentNodeSelected.Turret.SellValue;
        sellText.text = sellAmount.ToString();
    }


    private void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;   
    }

    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
    }
}
