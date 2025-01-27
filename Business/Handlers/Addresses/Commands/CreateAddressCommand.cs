
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Addresses.ValidationRules;

namespace Business.Handlers.Addresses.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateAddressCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Header { get; set; }
        public string Neighborhood { get; set; }
        public string Avenue { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }


        public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, IResult>
        {
            private readonly IAddressRepository _addressRepository;
            private readonly IMediator _mediator;
            public CreateAddressCommandHandler(IAddressRepository addressRepository, IMediator mediator)
            {
                _addressRepository = addressRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateAddressValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
            {
                var isThereAddressRecord = _addressRepository.Query().Any(u => u.CreatedDate == request.CreatedDate);

                if (isThereAddressRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedAddress = new Address
                {
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    IsDeleted = request.IsDeleted,
                    Id = request.Id,
                    CustomerId = request.CustomerId,
                    Header = request.Header,
                    Neighborhood = request.Neighborhood,
                    Avenue = request.Avenue,
                    Street = request.Street,
                    StreetNumber = request.StreetNumber,
                    ApartmentNumber = request.ApartmentNumber,
                    District = request.District,
                    City = request.City,
                    Country = request.Country,
                    FullAddress = request.FullAddress,

                };

                _addressRepository.Add(addedAddress);
                await _addressRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}