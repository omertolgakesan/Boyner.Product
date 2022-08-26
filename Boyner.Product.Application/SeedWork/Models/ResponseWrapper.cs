using System;

namespace Boyner.Product.Application.SeedWork.Models
{
    public class ResponseWrapper<TResponse> : IResponseWrapper<TResponse>
    {
        public TResponse Response { get; }

        public string RequestId { get; }

        public bool IsSuccess { get; } //=> Error != null ? false : true;

        public Exception Error { get; }

        public ResponseWrapper(TResponse response, bool isSuccess = true)
        {
            this.Response = response;
            this.IsSuccess = isSuccess;
        }
        public ResponseWrapper(TResponse response, string requestId, bool isSuccess = true)
        {
            this.Response = response;
            this.RequestId = requestId;
            this.IsSuccess = isSuccess;
        }
        public ResponseWrapper(Exception exception)
        {
            this.Error = exception;
            this.IsSuccess = false;
        }
        public ResponseWrapper(TResponse response, Exception exception, bool isSuccess = true)
        {
            this.Response = response;
            this.Error = exception;
            this.IsSuccess = isSuccess;
        }
    }
}
