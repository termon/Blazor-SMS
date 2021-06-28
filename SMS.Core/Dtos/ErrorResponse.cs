using System.Collections.Generic;

// https://www.devtrends.co.uk/blog/handling-errors-in-asp.net-core-web-api
namespace SMS.Core.Dtos
{
    public class ErrorResponse
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public string Message { get; set; }

    }
}
