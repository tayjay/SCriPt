using System;
using System.Collections.Generic;
using System.Linq;
using LabApi.Features.Wrappers;

namespace SCriPt.LabAPI.Utils;

public static class CollectionExtensions
{
    public static Player Random(this IReadOnlyCollection<Player> players)
    {
        if (players.Count == 0)
            return null;

        var random = new Random();
        var index = random.Next(players.Count);
        return players.ElementAt(index);
        
    }
} 