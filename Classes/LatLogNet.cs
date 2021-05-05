using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLN.Classes
{
    using LatLongNet;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The lat long net client class.
    /// </summary>
    public static class LatLongNetClient
    {
        /// <summary>
        /// Searches the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public static LatLongNetResult Search(String address)
        {
            return SearchAsync(address, CancellationToken.None).Result;
        }

        /// <summary>
        /// Searches the asynchronous.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public static async Task<LatLongNetResult> SearchAsync(String address, CancellationToken token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.latlong.net");
                client.DefaultRequestHeaders.ExpectContinue = false;
                client.DefaultRequestHeaders.TryAddWithoutValidation("x-requested-with", "XMLHttpRequest");
                var values = new Dictionary<String, String>()
                {
                    {"c1", address},
                    {"action", "gpcm"},
                    {"cp", String.Empty}
                };
                var response = await client.PostAsync("_spm4.php", new FormUrlEncodedContent(values), token).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var data = content.Split(',');
                if (data.Length != 2)
                    return null;
                return new LatLongNetResult { Latitude = data[0], Longitude = data[1] };
            }
        }
    }
}
