using BusinessCardInformation.Core.Common.Enums;
using BusinessCardInformation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessCardInformation.Core.Common
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            Errors = new List<Error>();
        }

        public ServiceResult(ResultCode status) : this() // Calls the parameterless constructor to initialize Errors
        {
            Status = status;
        }

        public ResultCode Status { get; set; }

        public List<Error> Errors { get; set; } = new List<Error>();

        public bool IsSucceed
        {
            get
            {
                return Status == ResultCode.Ok || Status == ResultCode.NoContent || Status == ResultCode.Created;
            }
        }

        public bool HasErrors
        {
            get
            {
                return Errors != null && Errors.Count > 0;
            }
        }

        public void AddErrors(params string[] codes)
        {
            List<Error> errors = codes.Select(c => new Error(c)).ToList();
            Errors.AddRange(errors);
        }

        public void AddErrorWithExtraMessage(string extraMessage, params string[] codes)
        {
            List<Error> errors = codes.Select(c => new Error(c, extraMessage)).ToList();
            Errors.AddRange(errors);
        }

        public void AddErrorWithParams(string code, params object[] parameters)
        {
            Error error = new Error(code, string.Empty, parameters);
            Errors.Add(error);
        }

        public void AddErrors(List<Error> errors)
        {
            Errors.AddRange(errors ?? new List<Error>());
        }

        public void AddError(Error error)
        {
            if (error != null)
            {
                Errors.Add(error);
            }
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult() : base()
        {
        }

        public ServiceResult(ResultCode status) : base(status)
        {
        }

        public T Data { get; set; }
    }

}
