using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "RuntimeSets/TooltipLinkData")]
public class RuntimeSetTooltipLinkData : RuntimeSet<TooltipLinkData> 
{
    public TooltipLinkData GetDataByID(string id)
    {
        TooltipLinkData[] data = Items.Where(data => data.ID == id).ToArray();
        if (data.Length == 0) return null;

        return data[0];
    }
}
