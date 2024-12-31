using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectSphere.Application.Features.Pages.Commands
{
    public class CreatePageCommandHandler : IRequestHandler<CreatePageCommand, long>
    {
        private readonly IApplicationDbContext _context;

        private readonly ICacheInvalidator _cacheInvalidator;

        public CreatePageCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
        {
            _context = context;
            _cacheInvalidator = cacheInvalidator;
        }

        public async Task<long> Handle(CreatePageCommand request, CancellationToken cancellationToken)
        {
            var page = Page.Create(request.Title, request.Description, request.Url);

            _context.Pages.Add(page);
            await _context.SaveChangesAsync(cancellationToken);

            await _cacheInvalidator.InvalidateGroupAsync("Pages", cancellationToken);

            return page.Id; 
        }
    }
} 