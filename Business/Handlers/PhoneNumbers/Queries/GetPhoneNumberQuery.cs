
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.PhoneNumbers.Queries
{
    public class GetPhoneNumberQuery : IRequest<IDataResult<PhoneNumber>>
    {
        public int CustomerId { get; set; }

        public class GetPhoneNumberQueryHandler : IRequestHandler<GetPhoneNumberQuery, IDataResult<PhoneNumber>>
        {
            private readonly IPhoneNumberRepository _phoneNumberRepository;
            private readonly IMediator _mediator;

            public GetPhoneNumberQueryHandler(IPhoneNumberRepository phoneNumberRepository, IMediator mediator)
            {
                _phoneNumberRepository = phoneNumberRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<PhoneNumber>> Handle(GetPhoneNumberQuery request, CancellationToken cancellationToken)
            {
                var phoneNumber = await _phoneNumberRepository.GetAsync(p => p.CustomerId == request.CustomerId);
                return new SuccessDataResult<PhoneNumber>(phoneNumber);
            }
        }
    }
}
