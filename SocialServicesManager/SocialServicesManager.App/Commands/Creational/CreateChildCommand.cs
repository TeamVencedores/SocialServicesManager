﻿using System;
using System.Collections.Generic;
using SocialServicesManager.App.Commands.Abstarcts;
using SocialServicesManager.Data.Factories.Contracts;
using SocialServicesManager.Interfaces;
using SocialServicesManager.App.Exceptions;
using System.Globalization;
using SocialServicesManager.Data.DataValidation;

namespace SocialServicesManager.App.Commands.Creational
{
    public class CreateChildCommand : CreationalCommand, ICommand
    {
        private const int ParameterCount = 5;
        public CreateChildCommand(IModelsFactory modelFactory, IDataFactory dataFactory) : base(modelFactory, dataFactory)
        {
        }

        public override string Execute(IList<string> parameters)
        {
            this.ValidateParameters(parameters, ParameterCount);

            var firstName = parameters[0];
            var lastName = parameters[1];
            var gender = parameters[2];
            var birthDate = parameters[3];
            int familyId;

            var parsedGender = this.dataFactory.GetGender(gender);

            if (parsedGender == null)
            {
                throw new EntryNotFoundException($"Gender '{gender}' not found. Use 'undefined' if gender is unknown.");
            }

            DateTime? parsedBirthday;

            if (birthDate == "null")
            {
                parsedBirthday = null;
            }
            else
            {
                parsedBirthday = DateTime.ParseExact(birthDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            
            if (!int.TryParse(parameters[4], out familyId))
            {
                throw new ParameterValidationException("Family id should be a number.");
            }

            var familyFound = this.dataFactory.FindFamily(familyId);

            if (familyFound == null)
            {
                throw new EntryNotFoundException($"Family id {familyId} not found.");
            }

            var child = this.ModelFactory.CreateChild(firstName, lastName, parsedGender, parsedBirthday, familyFound);

            this.dataFactory.AddChild(child);
            this.dataFactory.SaveAllChanges();

            return $"Child {child.FirstName} with id {child.Id} created in family {child.Family.Name}.";
        }

        protected override void ValidateParameters(IList<string> parameters, int paramterCount)
        {
            base.ValidateParameters(parameters, paramterCount);

            var firstName = parameters[0];
            var lastName = parameters[1];

            if (firstName.Length < ModelsConstraints.NameMinLenght || firstName.Length > ModelsConstraints.NameMaxLenght)
            {
                throw new ParameterValidationException(string.Format(ValidationText, "First name", ModelsConstraints.NameMinLenght, ModelsConstraints.NameMaxLenght));
            }

            if (lastName.Length < ModelsConstraints.NameMinLenght || lastName.Length > ModelsConstraints.NameMaxLenght)
            {
                throw new ParameterValidationException(string.Format(ValidationText, "Last name", ModelsConstraints.NameMinLenght, ModelsConstraints.NameMaxLenght));
            }
        }
    }
}
