using MediatR;

namespace Questao5.Application.Abstractions.Command
{
    public interface ICommand : IRequestHandler<IRequest, Unit>
    {
        Task Handle(IRequest request, CancellationToken cancellationToken);
    }
    public interface ICommand<TResponse> : IRequestHandler<IRequest<TResponse>, TResponse>
    {
        Task<TResponse> Handle(IRequest<TResponse> request, CancellationToken cancellationToken);
    }
    public interface ICommand<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}