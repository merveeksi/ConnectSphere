using FluentValidation;
using MediatR;

namespace ConnectSphere.Application.Common.PiplineBehaviors
{
    /// <summary>
    /// MediatR pipeline'ında çalışan, request'lerin validasyonunu gerçekleştiren davranış sınıfı
    /// </summary>
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Request için tanımlanmış tüm validatorları constructor injection ile alır
        /// </summary>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Pipeline'da request işlenmeden önce validasyon kurallarını çalıştırır
        /// </summary>
        /// <exception cref="ValidationException">Validasyon kuralları geçilmezse fırlatılır</exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Eğer request için herhangi bir validator tanımlanmamışsa gereksiz işlemleri atla
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            // Tüm validatorları paralel olarak çalıştır
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            // Eğer herhangi bir validasyon hatası varsa exception fırlat
            if (failures.Any())
                throw new ValidationException(failures);

            return await next();
        }
    }
}