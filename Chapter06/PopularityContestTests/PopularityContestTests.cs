using Microsoft.VisualStudio.TestTools.UnitTesting;

using PopularityContestLib;

namespace PopularityContestTests;

[TestClass]
public class PopularityContestTests
{
    private readonly PopularityContest<string> _popularityContest = new();

    [TestMethod]
    public void AddContestant_EmptyContest_ContestantAdded()
    {
        const string contestant1 = "Barry";
        Collection<string> expected = new() { contestant1 };

        _ = _popularityContest.AddContestant(contestant1);
        ICollection<string> actual = _popularityContest.Contestants;

        Assert.AreEqual(0, _popularityContest.VotesFor(contestant1));
        Assert.IsTrue(actual.SequenceEqual(expected));
    }

    [TestMethod]
    public void AddContestant_NotEmptyContest_ContestantAdded()
    {
        const string contestant1 = "Barry";
        const string contestant2 = "Karen";
        Collection<string> expected = new() { contestant1, contestant2 };
        _ = _popularityContest.AddContestant(contestant1);

        bool wasAdded = _popularityContest.AddContestant(contestant2);
        ICollection<string> actual = _popularityContest.Contestants;

        Assert.AreEqual(0, _popularityContest.VotesFor(contestant1));
        Assert.AreEqual(0, _popularityContest.VotesFor(contestant2));
        Assert.IsTrue(actual.SequenceEqual(expected));
        Assert.IsTrue(wasAdded);
    }

    [TestMethod]
    public void AddContestant_DuplicateContestant_ContestantNotAdded()
    {
        const string contestant1 = "Barry";
        Collection<string> expected = new() { contestant1 };
        _ = _popularityContest.AddContestant(contestant1);

        bool wasAdded = _popularityContest.AddContestant(contestant1);
        ICollection<string> actual = _popularityContest.Contestants;

        Assert.AreEqual(0, _popularityContest.VotesFor(contestant1));
        Assert.IsTrue(actual.SequenceEqual(expected));
        Assert.IsFalse(wasAdded);
    }

    [TestMethod]
    public void VoteFor_AddAVoteContestantExists_TalliesVoteAndReturnsVoteTotal()
    {
        const string contestant1 = "Barry";
        _ = _popularityContest.AddContestant(contestant1);

        int totalVotes = _popularityContest.VoteFor(contestant1);

        Assert.AreEqual(1, totalVotes);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void VoteFor_AddAVoteContestantDoesNotExist_ThrowsArgumentException()
        => _ = _popularityContest.VoteFor("Barry");

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void VotesFor_ContestantDoesNotExist_ThrowsArgumentException()
        => _ = _popularityContest.VotesFor("Barry");

    [TestMethod]
    public void VotesFor_ContestantExists_ReturnsNumberOfVotesForContestant()
    {
        const string contestant1 = "Barry";
        _ = _popularityContest.AddContestant(contestant1);
        _ = _popularityContest.VoteFor(contestant1);
        _ = _popularityContest.VoteFor(contestant1);
        _ = _popularityContest.VoteFor(contestant1);

        int actual = _popularityContest.VotesFor(contestant1);

        Assert.AreEqual(3, actual);
    }
}
