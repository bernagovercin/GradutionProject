﻿
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
using Business.Handlers.Colors.ValidationRules;

namespace Business.Handlers.Colors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateColorCommand : IRequest<IResult>
    {

        public string ColorName { get; set; }
        public string HexCode { get; set; }


        public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, IResult>
        {
            private readonly IColorRepository _colorRepository;
            private readonly IMediator _mediator;
            public CreateColorCommandHandler(IColorRepository colorRepository, IMediator mediator)
            {
                _colorRepository = colorRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateColorValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(PostgreSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateColorCommand request, CancellationToken cancellationToken)
            {
                var isThereColorRecord = _colorRepository.Query().Any(u => u.ColorName == request.ColorName);

                if (isThereColorRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedColor = new Color
                {
                    ColorName = request.ColorName,
                    HexCode = request.HexCode,

                };

                _colorRepository.Add(addedColor);
                await _colorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}