using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants.Messages
{
    public static class BusinessMessages
    {
        public static readonly string CarAdded = "Car was added.";

        public static readonly string CarDeleted = "Car was deleted.";

        public static readonly string CarsListed = "All cars were listed.";

        public static readonly string CarUpdated = "Car was updated.";

        public static readonly string CarAlreadyRented = "Car was already rented.";

        public static readonly string RentalDeleted = "Rental was deleted.";

        public static readonly string AllRentalsListed = "All rentals were listed.";

        public static readonly string CarCountOfBrandError =
            "Car count limit exceeded for this brand.";

        public static readonly string CarDescriptionAlreadyExists = "Car name already exists.";

        public static readonly string BrandLimitExceeded =
            "Brand count limit is exceeded. Thus, car cannot be added.";

        public static readonly string UserRegistered = "User has been registered.";

        public static readonly string UserAlreadyExists = "User already exists.";

        public static readonly string CarWasNotRented = "Car has not been rented yet.";

        public static readonly string CarAlreadyDelivered = "car has already been delivered.";

        public static readonly string CarNotDelivered = "Car has not been delivered yet.";

        public static readonly string CarDeliveredSuccessfully =
            "Car has been delivered successfully.";

        public static readonly string PasswordUpdated = "Password was updated.";

        public static readonly string UserNotExist = "Any user with such email could not be found.";

        public static readonly string PasswordError = "Password is not correct.";
    }
}
