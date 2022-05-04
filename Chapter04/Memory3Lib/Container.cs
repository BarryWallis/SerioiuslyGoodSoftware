namespace Memory3Lib;

public static class Container
{
    private static int[] _group = Array.Empty<int>();
    private static float[] _amount = Array.Empty<float>();

    public static int NewContainer()
    {
        int numberOfContainers = _group.Length;
        int numberOfGroups = _amount.Length;
        Array.Resize(ref _amount, numberOfGroups + 1);
        Array.Resize(ref _group, numberOfContainers + 1);
        _group[numberOfContainers] = numberOfGroups;
        return numberOfContainers;
    }

    public static float GetAmount(int containerId)
    {
        int groupId = _group[containerId];
        return _amount[groupId];
    }

    public static void Connect(int containerId1, int containerId2)
    {
        int groupId1 = _group[containerId1];
        int groupId2 = _group[containerId2];
        int size1 = GroupSize(groupId1);
        int size2 = GroupSize(groupId2);

        if (groupId1 == groupId2)
        {
            return;
        }

        float amount1 = _amount[groupId1] * size1;
        float amount2 = _amount[groupId2] * size2;
        _amount[groupId1] = (amount1 + amount2) / (size1 + size2);

        for (int i = 0; i < _group.Length; i++)
        {
            if (_group[i] == groupId2)
            {
                _group[i] = groupId1;
            }
        }

        RemoveGroupAndDefrag(groupId2);
    }

    public static void AddWater(int containerId, float amount)
    {
        int groupId = _group[containerId];
        int groupSize = GroupSize(groupId);
        _amount[groupId] += amount / groupSize;
    }

    private static void RemoveGroupAndDefrag(int groupId)
    {
        for (int containerId = 0; containerId < _group.Length; containerId++)
        {
            if (_group[containerId] == _amount.Length - 1)
            {
                _group[containerId] = groupId;
            }
        }

        _amount[groupId] = _amount[^1];
        Array.Resize(ref _amount, _amount.Length - 1);
    }

    private static int GroupSize(int groupId) => _group.Where(g => g == groupId).Count();
}
