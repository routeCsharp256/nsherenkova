using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions
{
    public class IncorrectRequestStatus: Exception
    {
        public IncorrectRequestStatus(string message) : base(message)
        {
            
        }
        
        public IncorrectRequestStatus(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}