using System.Collections.Generic;

namespace SMS.Core.Dtos
{
    public class ErrorResponse
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public string Message { get; set; }

    }
}
