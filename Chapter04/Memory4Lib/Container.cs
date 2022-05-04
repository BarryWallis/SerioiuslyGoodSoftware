namespace Memory4Lib;

public static class Container
{
    // If this is positive, it is an index + 1 to the next item; if negative it is the negative of the amount.
    // Zero is always an amount and never an index.
    private static float[] _nextOrAmount = Array.Empty<float>();

    /// <summary>
    /// Create a new empty <see cref="Container"/> and return its id.
    /// </summary>
    /// <returns>The id of the new <see cref="Container"/>.</returns>
    public static int NewContainer()
    {
        Array.Resize(ref _nextOrAmount, _nextOrAmount.Length + 1);
        _nextOrAmount[^1] = 0;
        return _nextOrAmount.Length - 1;
    }

    /// <summary>
    /// Return the amount of water in the given <see cref="Container"/>.
    /// </summary>
    /// <param name="containerId">The id of the <see cref="Container"/>.</param>
    /// <returns>The amount of water in the <see cref="Container"/>.</returns>
    public static float GetAmount(int containerId)
    {
        while (_nextOrAmount[containerId] > 0)
        {
            containerId = (int)_nextOrAmount[containerId] - 1;
        }

        return -_nextOrAmount[containerId];
    }

    /// <summary>
    /// Add the amount of water to the given <see cref="Container"/>.
    /// </summary>
    /// <param name="containerId">The id of the <see cref="Container"/>.</param>
    /// <param name="amountToAdd">The amount of water to add.</param>
    public static void AddWater(int containerId, float amountToAdd)
    {
        int firstContainerOfGroup = FindirstContainerOfGroup(containerId);
        (int numberOfContainersInGroup, int lastContainerInGroup)
            = FindNumberOfContainersAndLastContainerInGroup(firstContainerOfGroup);
        _nextOrAmount[lastContainerInGroup] -= amountToAdd / numberOfContainersInGroup;
    }

    /// <summary>
    /// Connects <paramref name="containerId1"/> to <paramref name="containerId2"/>.
    /// </summary>
    /// <param name="containerId1">The first container to connect.</param>
    /// <param name="containerId2">the container to connect <paramref name="containerId1"/> to.</param>
    public static void Connect(int containerId1, int containerId2)
    {
        int firstContainerOfGroup1 = FindirstContainerOfGroup(containerId1);
        int firstContainerOfGroup2 = FindirstContainerOfGroup(containerId2);
        if (firstContainerOfGroup1 == firstContainerOfGroup2)
        {
            return;
        }

        (int numberOfContainersInGroup1, int lastContainerInGroup1)
            = FindNumberOfContainersAndLastContainerInGroup(firstContainerOfGroup1);
        (int numberOfContainersInGroup2, int lastContainerInGroup2)
            = FindNumberOfContainersAndLastContainerInGroup(firstContainerOfGroup2);
        float amount1 = -_nextOrAmount[lastContainerInGroup1];
        float amount2 = -_nextOrAmount[lastContainerInGroup2];
        float newAmount = ((amount1 * numberOfContainersInGroup1) + (amount2 * numberOfContainersInGroup2))
                          / (numberOfContainersInGroup1 + numberOfContainersInGroup2);

        _nextOrAmount[lastContainerInGroup1] = firstContainerOfGroup2 + 1;
        _nextOrAmount[lastContainerInGroup2] = -newAmount;
    }

    /** Connects the specified containers, so that water can flow from one to the other.
    */
    //public static void connect(int containerID1, int containerID2)
    //{
    //    int first1 = findFirstOfGroup(containerID1),
    //        first2 = findFirstOfGroup(containerID2);
    //    if (first1 == first2) return;

    //    int[] lastAndSize1 = findLastOfGroupAndCount(first1),
    //          lastAndSize2 = findLastOfGroupAndCount(first2);
    //    int last1 = lastAndSize1[0],
    //        last2 = lastAndSize2[0];
    //    float amount1 = -nextOrAmount[last1],
    //          amount2 = -nextOrAmount[last2],
    //        newAmount = ((amount1 * lastAndSize1[1]) + (amount2 * lastAndSize2[1]))
    //        / (lastAndSize1[1] + lastAndSize2[1]);

    //    nextOrAmount[last1] = first2 + 1; // concatenation
    //    nextOrAmount[last2] = -newAmount;
    //}

    /// <summary>
    /// Returns the id of the first <see cref="Container"/> in the group with the given <see cref="Container"/>.
    /// </summary>
    /// <param name="containerId">The <see cref="Container"/> to get the first <see cref="Container"/> in the group.</param>
    /// <returns>The id of the first <see cref="Container"/> in the group.</returns>
    private static int FindirstContainerOfGroup(int containerId)
    {
        int currentContainerId = containerId;
        int count;
        do
        {
            for (count = 0; count < _nextOrAmount.Length; count++)
            {
                if (_nextOrAmount[count] == currentContainerId + 1)
                {
                    currentContainerId = count;
                    break;
                }
            }
        } while (count < _nextOrAmount.Length);

        return currentContainerId;
    }

    /// <summary>
    /// Find the number of <see cref="Container"/>s in the group and the id of the last <see cref="Container"/> in the group.
    /// </summary>
    /// <param name="firstContainerOfGroup">The id of the first <see cref="Container"/> in the group.</param>
    /// <returns>
    /// The number of <see cref="Container"/>s in the group and the id of the last <see cref="Container"/> in the group.
    /// </returns>
    private static (int numberOfContainersInGroup, int lastContainerInGroup) FindNumberOfContainersAndLastContainerInGroup(int firstContainerOfGroup)
    {
        int numberOfContainersInGroup = 1;
        int lastContainerInGroup = firstContainerOfGroup;
        while (_nextOrAmount[lastContainerInGroup] > 0)
        {
            numberOfContainersInGroup += 1;
            lastContainerInGroup = (int)_nextOrAmount[lastContainerInGroup] - 1;
        }

        return (numberOfContainersInGroup, lastContainerInGroup);
    }
}
