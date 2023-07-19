using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static Action<Node> OnNodeSelected;
    public static Action OnTurretSold;

    [SerializeField] private GameObject attackRangeSprite;

    public Turret Turret { get; set; }

    private float _rangeSize;

    private Vector3 _rangeOriginalSize;

    public GameObject AttackRangeSprite => attackRangeSprite;


    private void Start()
    {
        _rangeSize = attackRangeSprite.GetComponent<SpriteRenderer>().bounds.size.y;
        _rangeOriginalSize =attackRangeSprite.transform.localScale;
    }

    public void SetTurret(Turret turret)
    {
        Turret = turret; 
    }

    public bool IsEmpty()
    {
        return Turret == null;
    }

    public void SelectTurret()
    {
        OnNodeSelected?.Invoke(this);
        if(!IsEmpty())
        {
            ShowTurretInfo();
        }
    }

    public void SellTurret()
    {
        if (!IsEmpty())
        {
            CurrencySystem.Instance.AddCoins(Turret.SellValue);
            Destroy(Turret.gameObject);
            Turret = null;
            attackRangeSprite.SetActive(false);
            OnTurretSold?.Invoke();
        }
    }

    private void ShowTurretInfo()
    {
        attackRangeSprite.SetActive(true);
        attackRangeSprite.transform.localScale = _rangeOriginalSize * Turret.AttackRange / (_rangeSize / 2);
    }
}
