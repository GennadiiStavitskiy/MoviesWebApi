using System;
using System.Collections.Generic;
using System.Text;

namespace Smile.Movies.Shared
{
    public class PropertyMatcherSettings
    {
        public Dictionary<string, PropertyMatchingRules[]> AgencyCodeToMatchingRulesDic { get; set; }
    }
}
