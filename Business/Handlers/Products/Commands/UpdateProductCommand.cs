﻿
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Products.ValidationRules;


namespace Business.Handlers.Products.Commands
{


    public class UpdateProductCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string ColorName { get; set; }
        public Core.Enums.SizeEnum Size { get; set; }
        public int Quantity { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, IResult>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;

            public UpdateProductCommandHandler(IProductRepository productRepository, IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateProductValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var isThereProductRecord = await _productRepository.GetAsync(u => u.Category == request.Category
                                                                && u.ProductName == request.ProductName
                                                                && u.ColorName == request.ColorName);


                isThereProductRecord.CreatedDate = request.CreatedDate;
                isThereProductRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereProductRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereProductRecord.Status = request.Status;
                isThereProductRecord.IsDeleted = request.IsDeleted;
                isThereProductRecord.Id = request.Id;
                isThereProductRecord.Category = request.Category;
                isThereProductRecord.ProductName = request.ProductName;
                isThereProductRecord.ColorName = request.ColorName;
                isThereProductRecord.Size = request.Size;
                isThereProductRecord.Quantity = request.Quantity;


                _productRepository.Update(isThereProductRecord);
                await _productRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

