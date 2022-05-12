using Microsoft.VisualStudio.TestTools.UnitTesting;

using SocialUserLib;

namespace SocialUserTests;

[TestClass]
public class SocialUserTests
{
    [TestMethod]
    [DataRow("")]
    [DataRow("\t")]
    [DataRow(" ")]
    [ExpectedException(typeof(ArgumentException))]
    public void Ctor_InvalidName_ThrowsArgumentException(string name)
    {
        SocialUser _ = new(name);
    }

    [TestMethod]
    public void Ctor_ValidName_StoresNameAndHasNoFriends()
    {
        const string name = "Barry";

        SocialUser actual = new(name);

        Assert.AreEqual(name, actual.Name);
        Assert.AreEqual(0, actual.NumberOfFriends);
    }

    [TestMethod]
    public void Befriend_AddAFriend_TheyAreMutualFriends()
    {
        const string name = "Barry";
        const string otherName = "Karen";
        SocialUser socialUser = new(name);
        SocialUser otherSocialUser = new(otherName);

        socialUser.Befriend(otherSocialUser);

        Assert.AreEqual(1, socialUser.NumberOfFriends);
        Assert.AreEqual(1, otherSocialUser.NumberOfFriends);
        Assert.IsTrue(socialUser.IsFriend(otherSocialUser));
        Assert.IsTrue(otherSocialUser.IsFriend(socialUser));
    }

    [TestMethod]
    public void IsFriend_AddAFriend_TheyAreMutualFriends()
    {
        const string name = "Barry";
        const string otherName = "Karen";
        SocialUser socialUser = new(name);
        SocialUser otherSocialUser = new(otherName);
        socialUser.Befriend(otherSocialUser);

        Assert.IsTrue(socialUser.IsFriend(otherSocialUser));
        Assert.IsTrue(otherSocialUser.IsFriend(socialUser));
    }
}
