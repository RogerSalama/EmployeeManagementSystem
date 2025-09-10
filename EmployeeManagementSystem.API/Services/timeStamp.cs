using System.Net.Http;
using HtmlAgilityPack;

namespace EmployeeManagementSystem.API.Services
{
    //this service simply fetches the current time but from an actual website by scraping it instead of
    // relying on the device timestamp as it won't be global nor accurate
    public class timeStamp
    {
        private readonly HttpClient _httpClient;

        public timeStamp(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetCurrentTimeAsync()
        {

            var html = await _httpClient.GetStringAsync("https://www.timeanddate.com/worldclock/egypt/cairo");

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            // Select the span with id="ct"
            var timeNode = htmlDoc.DocumentNode.SelectSingleNode("//span[@id='ct']");
            return timeNode?.InnerText ?? "Not found";
        }
    }
}
