using Microsoft.AspNetCore.Http;

namespace webapi.Database.Models
{
    public class ErrorModel
    {
        public required string ErrorMessage { get; set; }

        public ErrorModel(string errorMessage) 
        {
            ErrorMessage = errorMessage;
        }

    }
}
