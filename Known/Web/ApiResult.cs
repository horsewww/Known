﻿namespace Known.Web
{
    public class ApiResult
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public static ApiResult ToData<T>(T data)
        {
            return new ApiResult { Status = 0, Data = data };
        }

        public static ApiResult ToPageData<T>(PagingResult<T> pr)
        {
            return ToData(new { total = pr.TotalCount, data = pr.PageData });
        }

        public static ApiResult Success(string message, object data = null)
        {
            return new ApiResult { Status = 0, Message = message, Data = data };
        }

        public static ApiResult Error(string message, object data = null)
        {
            return new ApiResult { Status = 1, Message = message, Data = data };
        }
    }
}
