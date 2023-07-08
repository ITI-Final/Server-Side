using APIApp.DTOs;

namespace APIApp.AppContsants
{
    public static class AppConstants
    {
        public static object GetBadRequest() => new
        {
            StatusCode = 400,
            StatusMessage = "The request was invalid or malformed.",
        };
        public static object GetEmptyList() => new
        {
            StatusCode = 204,
            StatusMessage = "No Content.",
        };
        public static object GetNotFound() => new
        {
            StatusCode = 404,
            StatusMessage = "Not Found.",
        };
        public static object GetEmailFound() => new
        {
            StatusCode = 400,
            StatusMessage = "This Email Is Already Taken.",
        };
        public static object UpdatedSuccessfully() => new
        {
            StatusCode = 200,
            StatusMessage = "Updated Successfully",
        };
        public static object DeleteSuccessfully() => new
        {
            StatusCode = 200,
            StatusMessage = "Delete Successfully",
        };
        public static object AdminLoginSuccessfully(AdminLoginDTO adminLoginDTO, string _token)
        {
            return new
            {
                StatusCode = 200,
                StatusMessage = "Login Successfully",
                response = new
                {
                    token = _token,
                    role = "Admin",
                    admin = new
                    {
                        id = adminLoginDTO.Id,
                        name = adminLoginDTO.Name,
                        email = adminLoginDTO.Email,
                        permissions = adminLoginDTO.Permissions
                    }
                }
            };
        }
    }

}
