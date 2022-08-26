using System;

namespace Boyner.Product.Application.SeedWork.Models
{
    public interface IResponseWrapper<TResponse>
    {
        TResponse Response { get; }
        string RequestId { get; }
        bool IsSuccess { get; }
        Exception Error { get; }
    }
}
