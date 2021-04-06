using Microsoft.AspNetCore.Mvc;
using MiniBook.Strings;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MiniBook
{
    //This class is to create api
    public static class ApiResultExtensions
    {
        #region Result Error 
        public static IActionResult ErrorResult(this ControllerBase controller, int errorCode, string errorMessage)
        {
            return JsonResult(new ApiResponse<object>(errorCode, errorMessage));
        }
        //Able to config status code manually
        public static IActionResult ErrorResult(this ControllerBase controller, int errorCode, string errorMessage, HttpStatusCode httpStatusCode)
        {
            return JsonResult(new ApiResponse<object>(errorCode, errorMessage), httpStatusCode);
        }
        //Merge errorCode and errorMessage in a type ErrorCode, always return Badrequest
        public static IActionResult ErrorResult(this ControllerBase controller, ErrorCode errorCode)
        {
            return JsonResult(new ApiResponse<object>((int)errorCode, 
                ErrorResource.ResourceManager.GetString(errorCode.ToString())), HttpStatusCode.BadRequest);
        }
        #endregion

        #region Result Ok
        public static IActionResult OkResult(this ControllerBase controller, object result)
        {
            return JsonResult(new ApiResponse<object>(result));
        }

        public static IActionResult OkResult(this ControllerBase controller)
        {
            return JsonResult(new ApiResponse<object>(true));
        }
        #endregion

        //Response Result 
        private static IActionResult JsonResult(object result, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new ApiJsonResult(result, httpStatusCode);
        }
    }
}
