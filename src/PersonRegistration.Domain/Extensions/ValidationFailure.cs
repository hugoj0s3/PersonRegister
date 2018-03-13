using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using PersonRegistration.Domain.Core;

namespace PersonRegistration.Domain.Extensions
{
    public static class ValidationFailureExtension
    {
        public static DomainNotification ToDomainNotification(this ValidationFailure objThis)
        {
            return new DomainNotification(objThis.PropertyName, objThis.ErrorMessage);
        }

        public static IList<DomainNotification> ToDomainNotification(this IEnumerable<ValidationFailure> objThis)
        {
            return objThis.Select(ToDomainNotification).ToList();
        }

        public static IList<DomainNotification> ToDomainNotification(this ValidationResult objThis)
        {
            return objThis.Errors.ToDomainNotification();
        }
    }
}
