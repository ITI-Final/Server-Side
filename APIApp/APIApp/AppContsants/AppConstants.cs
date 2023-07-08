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
        public static object GetNotFount() => new
        {
            StatusCode = 404,
            StatusMessage = "Not Found.",
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
    }

}
