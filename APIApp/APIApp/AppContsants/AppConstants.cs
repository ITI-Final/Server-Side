using System.Dynamic;
using System.Reflection;

namespace APIApp.AppContsants
{

    public class Response<T>
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public T Data { get; set; }
    }


    public static class AppConstants
    {
        #region Status Code
        public const int successCode = 200;
        public const int noContentCode = 204;
        public const int badRequestCode = 400;
        public const int notFoundCode = 404;
        public const int errorCode = 500;
        #endregion

        #region Status Message
        public const string loginSuccessMessage = "Login Successfully.";
        public const string getSuccessMessage = "Get Successfully.";
        public const string addSuccessMessage = "Added Successfully.";
        public const string deleteSuccessMessage = "Deleted Successfully.";
        public const string updateSuccessMessage = "Updated Successfully.";
        public const string emailIsAlreadyMessage = "This Email Is Already Taken.";
        public const string notFoundMessage = "Not Found.";
        public const string notContentMessage = "No Content.";
        public const string invalidMessage = "The request was invalid or malformed.";
        public const string errorMessage = "Error 500.";
        #endregion

        #region Methods

        #region Response
        public static Response<object> Response<T>(int statusCode, string statusMessage, T data = default)
        {
            return new Response<object>
            {
                StatusCode = statusCode,
                StatusMessage = statusMessage,
                Data = data
            };
        }
        #endregion

        #region Login
        public static object LoginSuccessfully<T>(T loginDTO, string _token)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            dynamic response = new ExpandoObject();
            response.id = null;
            response.email = null;
            response.permissions = null;
            response.token = _token;

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Id")
                    response.id = property.GetValue(loginDTO);
                else if (property.Name == "Name")
                    response.name = property.GetValue(loginDTO);
                else if (property.Name == "Email")
                    response.email = property.GetValue(loginDTO);
                else if (property.Name == "Permissions")
                    response.permissions = property.GetValue(loginDTO);
            }

            return new
            {
                StatusCode = 200,
                StatusMessage = "Login Successfully",
                response
            };
        }

        #endregion

        #endregion

        #region V1 Comment
        //public static object GetBadRequest() => new
        //{
        //    StatusCode = 400,
        //    StatusMessage = "The request was invalid or malformed.",
        //};
        //public static object GetEmptyList() => new
        //{
        //    StatusCode = 204,
        //    StatusMessage = "No Content.",
        //};
        //public static object GetNotFound() => new
        //{
        //    StatusCode = 404,
        //    StatusMessage = "Not Found.",
        //};
        //public static object GetEmailFound() => new
        //{
        //    StatusCode = 400,
        //    StatusMessage = "This Email Is Already Taken.",
        //};
        //public static object UpdatedSuccessfully() => new
        //{
        //    StatusCode = 200,
        //    StatusMessage = "Updated Successfully",
        //};
        //public static object DeleteSuccessfully() => new
        //{
        //    StatusCode = 200,
        //    StatusMessage = "Delete Successfully",
        //};

        //public static object AdminLoginSuccessfully(AdminLoginDTO adminLoginDTO, string _token)
        //{
        //    return new
        //    {
        //        StatusCode = 200,
        //        StatusMessage = "Login Successfully",
        //        response = new
        //        {
        //            id = adminLoginDTO.Id,
        //            name = adminLoginDTO.Name,
        //            email = adminLoginDTO.Email,
        //            permissions = adminLoginDTO.Permissions,
        //            token = _token,
        //        }

        //    };
        //}

        //public static object UserLoginSuccessfully(UserLoginDTO userLoginDTO, string _token)
        //{
        //    return new
        //    {
        //        StatusCode = 200,
        //        StatusMessage = "Login Successfully",
        //        Name = userLoginDTO.Email,
        //        Token = _token,
        //    };
        //}
        #endregion
    }

}

