using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ReferenceLib;

namespace ReferenceTests;

[TestClass]
public class UserTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ctor_EmptyString_ThrowsArgumentNullException() => _ = new User(" ");

    [TestMethod]
    public void ctor_CopyWithEmptyString_ThrowsArgumentNullException()
    {
        User user = new("Barry");
        _ = user with { Name = " " };
    }

    [TestMethod]
    public void ctor_Name_ContainsName()
    {
        const string name = "Barry";

        User actual = new(name);

        Assert.AreEqual(name, actual.Name);
    }

    [TestMethod]
    public void IsDirectFriendOf_NotFound_ReturnsFalse()
    {
        const string userName = "Barry";
        const string lookForName = "Karen";
        User user = new(userName);
        User lookForUser = new(lookForName);

        bool actual = user.IsDirectFriendOf(lookForUser);

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void IsDirectFriendOf_Found_ReturnsTrue()
    {
        const string userName = "Barry";
        const string otherName = "Karen";
        User user = new(userName);
        User otherUser = new(otherName);
        user.Befriend(otherUser);

        bool actual = user.IsDirectFriendOf(otherUser);

        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void HasFriends_NoFriendAdded_ReturnsFalse()
    {
        User user = new("Barry");

        bool actual = user.HasFriends();

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void HasFriends_FriendAdded_ReturnsTrue()
    {
        User user = new("Barry");
        user.Befriend(new User("Karen"));

        bool actual = user.HasFriends();

        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void HasNoFriends_NoFriendAdded_ReturnsTrue()
    {
        User user = new("Barry");

        bool actual = user.HasNoFriends();

        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void HasNoFriends_FriendAdded_ReturnsFalse()
    {
        User user = new("Barry");
        user.Befriend(new User("Karen"));

        bool actual = user.HasNoFriends();

        Assert.IsFalse(actual);
    }

    [TestMethod]
    public void Befriend_AddFriend_IsDirectFriendOf()
    {
        const string userName = "Barry";
        const string otherName = "Karen";
        User user = new(userName);
        User otherUser = new(otherName);

        user.Befriend(otherUser);

        Assert.IsTrue(user.IsDirectFriendOf(otherUser));
        Assert.IsTrue(otherUser.IsDirectFriendOf(user));
    }

    [TestMethod]
    public void Befriend_This_HasNoFriends()
    {
        User user = new("Barry");

        user.Befriend(user);

        Assert.IsTrue(user.HasNoFriends());
    }

    [TestMethod]
    public void IsIndirectFriendOf_HasIndirectFriend_ReturnsTrue()
    {
        const string userName = "Barry";
        const string friendName = "Karen";
        const string indirectFriendName = "Emma";
        User user = new(userName);
        User friend = new(friendName);
        User indirectFriend = new(indirectFriendName);
        user.Befriend(friend);
        friend.Befriend(indirectFriend);

        bool actual = user.IsIndirectFriendOf(indirectFriend);

        Assert.IsTrue(actual);
    }

    [TestMethod]
    public void IsIndirectFriendOf_HasNoIndirectFriend_ReturnsFalse()
    {
        const string userName = "Barry";
        const string friendName = "Karen";
        const string indirectFriendName = "Emma";
        User user = new(userName);
        User friend = new(friendName);
        User indirectFriend = new(indirectFriendName);
        user.Befriend(friend);

        bool actual = user.IsIndirectFriendOf(indirectFriend);

        Assert.IsFalse(actual);
    }
}
