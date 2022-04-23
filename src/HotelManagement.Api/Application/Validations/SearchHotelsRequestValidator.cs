using HotelManagement.Api.Requests;
using FluentValidation;

namespace HotelManagement.Api.Application.Validations
{
    public class SearchHotelsRequestValidator : AbstractValidator<SearchPageRequest>
    {
        public SearchHotelsRequestValidator()
        {
            RuleFor(request => request.Args).NotNull();
            RuleFor(request => request.Args.PageIndex).GreaterThan(0);
            RuleFor(request => request.Args.PageSize).InclusiveBetween(10, 100);
        }
    }
}