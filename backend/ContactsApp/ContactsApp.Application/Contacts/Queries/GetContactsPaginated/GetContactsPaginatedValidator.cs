using FluentValidation;

namespace ContactsApp.Application.Contacts.Queries.GetContactsPaginated
{
    public class GetContactsPaginatedValidator : AbstractValidator<GetContactsPaginatedQuery>
    {
        public GetContactsPaginatedValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Page size must not exceed 100.");
        }
    }
}
