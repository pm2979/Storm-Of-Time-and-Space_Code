using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Upgrade Table", fileName = "NewUpgradeTable")]
public class UpgradeTableSO : ScriptableObject
{
    [field: SerializeField] public StatType StatType { get; private set; }
    [field: SerializeField] public UpgradeLevelData[] Levels { get; private set; }

    public UpgradeLevelData GetDataForLevel(int level)
    {
        if (level < 0 || level >= Levels.Length)
            return null;

        return Levels[level];
    }
}

[System.Serializable]
public class UpgradeLevelData
{
    public int level;
    public int cost;
    public float value;
}
