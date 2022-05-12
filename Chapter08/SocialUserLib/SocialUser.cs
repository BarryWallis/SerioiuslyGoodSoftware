namespace SocialUserLib;

public class SocialUser
{
    private readonly ISet<SocialUser> _friends = new HashSet<SocialUser>();

    /// <value>The name of this user.</value>
    public string Name { get; }

    /// <value>The number of friends this user has.</value>
    public int NumberOfFriends => _friends.Count;

    /// <summary>
    /// Create a new <see cref="SocialUser"/> with the given name.
    /// </summary>
    /// <param name="name">The name of the social user.</param>
    /// <exception cref="ArgumentException"><paramref name="name"/> must not be empty.</exception>
    public SocialUser(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Must not be empty", nameof(name));
        }

        Name = name;
    }
    /// <summary>
    /// Make <paramref name="otherSocialUser"/> a friend.
    /// </summary>
    /// <param name="otherSocialUser">The <see cref="SocialUser"/> to befriend.</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Befriend(SocialUser otherSocialUser)
    {
        lock (this)
        {
            _ = _friends.Add(otherSocialUser);
            lock (otherSocialUser)
            {
                _ = otherSocialUser._friends.Add(this);
            }
        }
    }

    /// <summary>
    /// Return true if the user is friends with <paramref name="otherSocialUser"/>.
    /// </summary>
    /// <param name="otherSocialUser">The <see cref="SocialUser"/> to check for frienship.</param>
    /// <returns><see langword="true"/> if the two social users are friends; 
    /// otherwise <see langword="false"/>.</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public bool IsFriend(SocialUser otherSocialUser) => _friends.Contains(otherSocialUser);
}
