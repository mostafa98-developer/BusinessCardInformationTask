using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.Entities
{
    public class ServiceResult<T>
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);

        public ServiceResult(T data)
        {
            Data = data;
        }

        public ServiceResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }

}
