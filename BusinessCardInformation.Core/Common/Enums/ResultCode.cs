using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.Common.Enums
{
    public enum ResultCode
    {
        Ok = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        NotAllowed = 405,
        Conflict = 409,
        InternalError = 500,
        NotImplemented = 501,
        ServiceUnavailable = 503
    }
}
