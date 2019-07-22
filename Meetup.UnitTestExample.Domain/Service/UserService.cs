using FluentValidation.Results;
using Meetup.UnitTestExample.Domain.Model;
using Meetup.UnitTestExample.Domain.Repository;
using Meetup.UnitTestExample.Domain.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Meetup.UnitTestExample.Domain.Service
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;
        public List<DomainNotification> Notifications { get; set; }

        public UserService(IUnitOfWork uow,
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _uow = uow;
            Notifications = new List<DomainNotification>();
        }

        public Task<bool> RegisterNew(User user,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            _uow.OpenConnection();
            ValidationResult validationResult = new UserValidation().Validate(user);

            if (!validationResult.IsValid)
            {
                NotifyValidationErrors(validationResult);
                return Task.FromResult(false);
            }

            _userRepository.Insert(user).Wait();

            return Task.FromResult(Commit());
        }

        protected void NotifyValidationErrors(ValidationResult validationResult)
        {
            foreach (ValidationFailure error in validationResult.Errors)
            {
                Notifications.Add(new DomainNotification(error.ErrorCode, error.ErrorMessage));
            }
        }

        public bool Commit()
        {
            if (Notifications.Any()) return false;
            if (_uow.Commit()) return true;

            Notifications.Add(new DomainNotification
                ("Commit", "We had a problem during saving your data."));

            return false;
        }
    }
}
