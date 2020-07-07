using System.Collections.Generic;

namespace SMS.Core.Dtos
{
    public class ApiResponse<T>
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public T Result { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        // TODO - add other success codes
        public bool IsSuccess =>  StatusCode == 200 || StatusCode == 201 || StatusCode == 204;
    }
    public class ApiResponse : ApiResponse<object> {}

}
