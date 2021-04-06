using System;

namespace MiniBook
{
    //This class is to format response (De tuong thich voi Mobile va Web)
    public class ApiResponse<T>
    {
        public ApiResponse(T result)
        {
            Successful = true;
            Result = result;
        }

        public ApiResponse(bool successful)
        {
            Successful = successful;
        }

        public ApiResponse(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        //Response thanh cong hay khong
        public bool Successful { get; set; }

        //Ket qua tra ve (Generic)
        //Duoc config  ben class ApiJsonResult
        public T Result { get; set; }

        //Ma so tra ve: 404, 400, 500...
        public int? ErrorCode { get; set; }

        //Tin nhan bao loi
        public string ErrorMessage { get; set; }
    }
}
