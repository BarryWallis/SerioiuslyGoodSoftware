namespace ReferenceLib;
/// <summary>
/// Create a new <see cref="User"/> with the given <paramref name="Name"/>.
/// </summary>
/// <param name="Name">The name of the <see cref="User"/>.</param>
public record User(string Name)
{
    private readonly ISet<User> _friends = new HashSet<User>();

    /// <summary>
    /// The name of the <see cref="User"/>. 
    /// </summary>
    public string Name { get; init; } = string.IsNullOrWhiteSpace(Name) ? throw new ArgumentNullException(nameof(Name)) : Name;

    /// <summary>
    /// Make <paramref name="other"/> a direct friend.
    /// </summary>
    /// <param name="other">The new friend.</param>
    public void Befriend(User other)
    {
        if (this != other && _friends.Add(other))
        {
            other.Befriend(this);
        }
    }

    /// <summary>
    /// Check if the <see cref="User"/> has any friends.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="User"/> has any friends; otherwise <see langword="false"/>.</returns>
    public bool HasFriends() => _friends.Count > 0;

    /// <summary>
    /// Check if the <see cref="User"/> has no friends.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="User"/> has no friends; otherwise <see langword="false"/>.</returns>
    public bool HasNoFriends() => !HasFriends();

    /// <summary>
    /// Is the <see cref="User"/> a direct friend of <paramref name="other"/>.
    /// </summary>
    /// <param name="other">The <see cref="User"/> to check for direct friendship.</param>
    /// <returns><see langword="true"/> if the <see cref="User"/> is a direct friend; otherwise <see langword="false"/>.</></returns>
    public bool IsDirectFriendOf(User other) => _friends.Contains(other);

    /// <summary>
    /// Is the <see cref="User"/> an indirect friend of <paramref name="other"/>.
    /// </summary>
    /// <remarks>An indeirect friend is one that is a friend of a friend (possibly many times removed).</remarks>
    /// <param name="other">The <see cref="User"/> to check for indirect friendship.</param>
    /// <returns><see langword="true"/> if the <see cref="User"/> is an indirect friend; otherwise <see langword="false"/>.</></returns>
    public bool IsIndirectFriendOf(User other)
    {
        Stack<User> stack = new();
        stack.Push(this);
        while (stack.Count > 0)
        {
            User user = stack.Pop();
            foreach (User friend in user._friends)
            {
                if (friend == other)
                {
                    return true;
                }

                if (friend != this)
                {
                    stack.Push(friend);
                }
            }
        }

        return false;
    }
}
