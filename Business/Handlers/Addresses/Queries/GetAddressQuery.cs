﻿
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Addresses.Queries
{
    public class GetAddressQuery : IRequest<IDataResult<Address>>
    {
        public string Street { get; set; } // Cadde adı
        public int StreetNumber { get; set; } // Sokak numarası

        public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, IDataResult<Address>>
        {
            private readonly IAddressRepository _addressRepository;
            private readonly IMediator _mediator;

            public GetAddressQueryHandler(IAddressRepository addressRepository, IMediator mediator)
            {
                _addressRepository = addressRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Address>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
            {
                var address = await _addressRepository.GetAsync(p => p.Street == request.Street && p.StreetNumber == request.StreetNumber);
                return new SuccessDataResult<Address>(address);
            }
        }
    }
}
