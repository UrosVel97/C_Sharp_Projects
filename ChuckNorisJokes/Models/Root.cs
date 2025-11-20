using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorisJokes.Models
{
    public record Root(
                     IReadOnlyList<string> categories,
                     string created_at,
                     string icon_url,
                     string id,
                     string updated_at,
                     string url,
                     string value
                     );
}
