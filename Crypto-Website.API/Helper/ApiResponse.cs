namespace Crypto_Website.API.Helper
{
    public class ApiError
    {
        public string Code { get; set; }
        public string Details { get; set; }
    }
    public class ApiResponse<t>
    {
        

        public bool success { get; set; }
        public string message { get; set; }
        public t data { get; set; }
        public ApiError error { get; set; } 
        

        public static ApiResponse<t> SuccessResponse(t data)
        {
            return new ApiResponse<t>
            {
                success = true,
                message = "Api Executed Successfully",
                data = data,
                error = null,
            };
        }
        public static ApiResponse<t> FailureResponse(string code,string Details)
        {
            return new ApiResponse<t>
            {
                success = false,
                message="Internal server error",
                data=default,
                error=new ApiError
                {
                    Code=code,
                    Details=Details
                }
            };


                
        }
            

        
    }
}
