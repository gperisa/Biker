using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeGround.API.Common
{
    public sealed class TokenCollection
    {
        public Dictionary<string, long> activeTokens = new Dictionary<string, long>();

        private static readonly Lazy<TokenCollection> lazy =
            new Lazy<TokenCollection>(() => new TokenCollection());

        public static TokenCollection Instance { get { return lazy.Value; } }

        private TokenCollection()
        {
        }
    }
}