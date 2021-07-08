using System;
using System.Collections.Generic;

namespace Cohesion.Domain.ServiceRequests
{
    public sealed class ServiceRequest
    {
        public ServiceRequest(Guid id,
            string buildingCode,
            string description,
            CurrentStatus currentStatusCode,
            string createdBy,
            DateTime createdDate,
            string lastModifiedBy,
            DateTime? lastModifiedDate)
        {
            Id = id;
            BuildingCode = buildingCode;
            Description = description;
            CurrentStatusCode = currentStatusCode;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            LastModifiedBy = lastModifiedBy;
            LastModifiedDate = lastModifiedDate;
        }

        public Guid Id { get; set; }
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public CurrentStatus CurrentStatusCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public static ServiceRequestResult Create(Guid id,
            string buildingCode,
            string description,
            CurrentStatus currentStatusCode,
            string createdBy,
            DateTime createdDate,
            string lastModifiedBy,
            DateTime? lastModifiedDate)
        {
            var result = new ServiceRequestResult
            {
                ErrorMessages = new List<string>()
            };

            ValidateServiceRequest(buildingCode, currentStatusCode, createdBy, createdDate, result.ErrorMessages);

            if (result.ErrorMessages.Count == 0)
                result.Instance = new ServiceRequest(id,
                    buildingCode,
                    description,
                    currentStatusCode,
                    createdBy,
                    createdDate,
                    lastModifiedBy,
                    lastModifiedDate);

            return result;
        }


        public static ServiceRequestResult Edit(Guid originalId,
            string buildingCode,
            string description,
            CurrentStatus currentStatusCode,
            string createdBy,
            DateTime createdDate,
            string lastModifiedBy,
            DateTime? lastModifiedDate)
        {
            var result = new ServiceRequestResult
            {
                ErrorMessages = new List<string>()
            };

            ValidateServiceRequest(buildingCode, currentStatusCode, createdBy, createdDate, result.ErrorMessages);

            if (result.ErrorMessages.Count == 0)
                result.Instance = new ServiceRequest(originalId,
                    buildingCode,
                    description,
                    currentStatusCode,
                    createdBy,
                    createdDate,
                    lastModifiedBy,
                    lastModifiedDate);

            return result;
        }

        private static void ValidateServiceRequest(string buildingCode,
            CurrentStatus currentStatusCode,
            string createdBy,
            DateTime createdDate,
            List<string> errorMessages)
        {
            if (string.IsNullOrEmpty(buildingCode) || buildingCode.Trim().Length != 3)
                errorMessages.Add("Building code must not be empty and length must be 3 characters");

            if (!Enum.IsDefined(typeof(CurrentStatus), currentStatusCode))
                errorMessages.Add("Invalid status");

            if (string.IsNullOrEmpty(createdBy))
                errorMessages.Add("Valid creator name must be entered");

            if (createdDate == DateTime.MinValue)
                errorMessages.Add("Valid creator date must be entered");
        }
    }
}