﻿namespace _0_Framework.Application
{
    public class OperationResult
    {
        public bool IsSucceeded { get; set; }
        //public bool IsFailed { get; set; }
        public string Message { get; set; }

        public OperationResult()
        {
            IsSucceeded=false;
        }
        public OperationResult Succeeded(string message = "عملیات با موفقیت انجام شد")
        {
            IsSucceeded = true;
            Message = message;
            return this;
        }

        public OperationResult Failed(string message = "خطا در انجام عملیات")
        {
            IsSucceeded = false;
            Message = message;
            return this;
        }

    }
}
