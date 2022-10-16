using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tooltip Link")]
public class TooltipLinkData : ScriptableObject
{
    public string ID = "";
    public string Header = "";
    [Multiline(10)]
    public string Description = "";
}
