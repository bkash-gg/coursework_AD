﻿namespace AD_Coursework.DTOs.User
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }

        public static ServiceResponse<T> Success(T data, string? message = null)
        {
            return new ServiceResponse<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };
        }

        public static ServiceResponse<T> Failure(string message)
        {
            return new ServiceResponse<T>
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
