using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Flowers.Model
{
    public class FlowersService : IFlowersService
    {
        private const string RequestUrl =
            "http://www.galasoft.ch/labs/Flowers/FlowersService.ashx?{0}={1}&{2}={3}&ticks={4}";

        public async Task<IList<Flower>> Refresh()
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(
                    RequestUrl,
                    WebConstants.AuthenticationKey,
                    WebConstants.AuthenticationId,
                    WebConstants.ActionKey,
                    WebConstants.ActionGet,
                    DateTime.Now.Ticks);

                var json = await client.GetStringAsync(url);

                var result = JsonConvert.DeserializeObject<FlowersResult>(json);

                foreach (var model in result.Data)
                {
                    model.HasChanges = false;
                }

                return result.Data;
            }
        }

        public async Task<bool> Save(Flower flower)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format(
                    RequestUrl,
                    WebConstants.AuthenticationKey,
                    WebConstants.AuthenticationId,
                    WebConstants.ActionKey,
                    WebConstants.ActionSave,
                    DateTime.Now.Ticks);

                var json = JsonConvert.SerializeObject(flower);

                var content = new FormUrlEncodedContent(
                    new[]
                    {
                        new KeyValuePair<string, string>("flower", json)
                    });

                try
                {
                    var response = await client.PostAsync(url, content);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        flower.HasChanges = false;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}