using System.Diagnostics;

namespace PopularityContestLib;
/// <summary>
/// A popularity contest among a set of objects of the given type.
/// </summary>
/// <typeparam name="TContestant">The type of objects in the popularity contest.</typeparam>
public class PopularityContest<TContestant> where TContestant : notnull, IEquatable<TContestant>
{
    private readonly IDictionary<TContestant, int> _contestants = new Dictionary<TContestant, int>();

    /// <value>
    /// Return the contestant with the most votes, so far.
    /// </value>
    public TContestant MostVoted
        => _contestants.Aggregate((l, r)
            => l.Value > r.Value ? l : r).Key;

    /// <value>
    /// Return a collection of all the contestants.
    /// </value>
    public ICollection<TContestant> Contestants => _contestants.Keys;

    /// <summary>
    /// Add a new <paramref name="contestant"/>.
    /// <para>Adding a duplicate contestant is ignored.</para>
    /// </summary>
    /// <param name="contestant"></param>
    /// <returns>
    /// <see langword="true"/> if the contestant was added; otherwise <see langword="false"/>.
    /// </returns>
    public bool AddContestant(TContestant contestant)
    {
        bool result = false;
        if (!_contestants.ContainsKey(contestant))
        {
            _contestants[contestant] = 0;
            result = true;
        }
        Debug.Assert(PostconditionAddContestant(contestant));
        Debug.Assert(InvariantsPopularityContest());
        return result;
    }

    /// <summary>
    /// Verify invariants for <see cref="PopularityContest{TContestant}"/>:
    /// 1) No <see cref="_contestants"/> have a negative value.
    /// </summary>
    /// <returns></returns>
    private bool InvariantsPopularityContest()
        => _contestants.All(kvp => kvp.Value >= 0);

    /// <summary>
    /// Verify postconditions for AddContestant
    /// 1) The contestant was added to the <see cref="_contestants"/> dictionary.
    /// 2) The added contestant has a value of zero.
    /// </summary>
    /// <param name="contestant">The contestant that was added.</param>
    /// <returns><see langword="true"/> if postcnditions were met; otherwise <see langword="false"/>.</returns>
    private bool PostconditionAddContestant(TContestant contestant)
        => _contestants.Any(kvp => kvp.Key.Equals(contestant) && kvp.Value == 0);

    /// <summary>
    /// Record a vote for the given <paramref name="contestant"/>. 
    /// </summary>
    /// <param name="contestant"></param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="contestant"/> is not part of the contest.
    /// </exception>
    /// <returns>The number of votes the <paramref name="contestant"/> has.</returns>
    public int VoteFor(TContestant contestant)
    {
        PreconditionContestantInContest(contestant);
#if DEBUG
        int oldValue = _contestants[contestant];
#endif
        _contestants[contestant] += 1;

        Debug.Assert(PostConditionsVoteFor(contestant, oldValue));
        Debug.Assert(InvariantsPopularityContest());
        return _contestants[contestant];
    }

    /// <summary>
    /// Verify post conditions for <see cref="VoteFor(TContestant)"/>:
    /// 1) New value must be one more than old value.
    /// </summary>
    /// <param name="contestant">The contestant to add a vote for.</param>
    /// <param name="oldValue">The original value for the <paramref name="contestant"/>.</param>
    /// <returns></returns>
    private bool PostConditionsVoteFor(TContestant contestant, int oldValue)
        => oldValue + 1 == _contestants[contestant];

    /// <summary>
    /// Return the number of votes for the given <paramref name="contestant"/>.
    /// </summary>
    /// <param name="contestant">The contestand to return the votes for.</param>
    /// <returns>The number of votes for the <paramref name="contestant"/>.</returns>
    /// <exception cref="ArgumentException">
    /// The <paramref name="contestant"/> is not part of the contest.
    /// </exception>
    public int VotesFor(TContestant contestant)
    {
        PreconditionContestantInContest(contestant);

        return _contestants[contestant];
    }

    private void PreconditionContestantInContest(TContestant contestant)
    {
        if (!_contestants.ContainsKey(contestant))
        {
            throw new ArgumentException($"{contestant} is not part of the contest", nameof(contestant));
        }
    }
}
