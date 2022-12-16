namespace Impostor.Http;

using Impostor.Api.Games;
using Impostor.Api.Games.Managers;
using Impostor.Api.Innersloth;

/**
 * <summary>
 * Perform game listing filtering.
 * </summary>
 */
public class ListingManager
{
    private readonly IGameManager gameManager;

    private readonly IServiceProvider serviceProvider;

    /**
     * <summary>
     * Initializes a new instance of the <see cref="ListingManager"/> class.
     * </summary>
     * <param name="gameManager">GameManager containing a list of games.</param>
     * <param name="serviceProvider">Used to retrieve a list of IListingFilter's.</param>
     */
    public ListingManager(IGameManager gameManager, IServiceProvider serviceProvider)
    {
        this.gameManager = gameManager;
        this.serviceProvider = serviceProvider;
    }

    /**
     * <summary>
     * Find listings that match the requested settings.
     * </summary>
     * <param name="ctx">The context of this http request.</param>
     * <param name="map">The selected maps.</param>
     * <param name="impostorCount">The amount of impostors. 0 is any.</param>
     * <param name="language">Chat language of the game.</param>
     * <param name="maxListings">Maximum amount of games to return.</param>
     * <returns>Listings that match the required criteria.</returns>
     */
    public IEnumerable<IGame> FindListings(HttpContext ctx, int map, int impostorCount, GameKeywords language, int maxListings = 10)
    {
        int resultCount = 0;

        Func<IGame, bool>[] filters = this.serviceProvider.GetServices<IListingFilter>().Select(f => f.GetFilter(ctx)).ToArray();

        // Find games that have not started yet.
        foreach (var game in this.gameManager.Games.Where(x =>
            x.IsPublic &&
            x.GameState == GameStates.NotStarted &&
            x.PlayerCount < x.Options.CurrentGameOptions.MaxPlayers))
        {
            var options = game.Options.CurrentGameOptions;

            // Check for options.
            if ((map & (1 << (int)options.Map)) == 0)
            {
                continue;
            }

            if (language != options.Keywords)
            {
                continue;
            }

            if (impostorCount != 0 && options.NumImpostors != impostorCount)
            {
                continue;
            }

            bool addGame = true;
            foreach (var filter in filters)
            {
                if (!filter(game))
                {
                    addGame = false;
                    break;
                }
            }

            if (addGame)
            {
                // Add to result.
                yield return game;

                // Break out if we have enough.
                if (++resultCount == maxListings)
                {
                    yield break;
                }
            }
        }
    }
}
