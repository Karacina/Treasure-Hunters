using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotDraw : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _label;
    [SerializeField] private Text _stackLabel;
    public void Set(InventorySlot item)
    {
        _icon.sprite = item.item.icon;
        _label.text = item.item.displayName;
        _stackLabel.text = item.sizeSlot.ToString();
    }
}
