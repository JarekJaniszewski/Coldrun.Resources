using MediatR;

namespace Resources.Contracts
{
    public interface IValidatableRequest<out TResponse> : IRequest<TResponse>
    {
    }
}
