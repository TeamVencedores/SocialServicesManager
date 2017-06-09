﻿using SocialServicesManager.Models;
using System;
using System.Linq;
using System.Data.Entity;
using System.Globalization;

namespace SocialServicesManager.Data.Factories
{
    public class ModelsFactory : IModelsFactory
    {
        public ModelsFactory(SQLServerDbContext sqlDbContext, PostgreDbContext postgreDbContext)
        {
            this.SqlDbContext = sqlDbContext;
            this.PostgreDbContext = postgreDbContext;
        }

        public SQLServerDbContext SqlDbContext { get; private set; }

        public PostgreDbContext PostgreDbContext { get; private set; }

        public string CreateFamily(string name)
        {
            var family = new Family
            {
                Name = name
            };

            this.SqlDbContext.Families.Add(family);
            this.SqlDbContext.SaveChanges();

            return $"Family {family.Name} with id {family.Id} created.";
        }

        public string CreateUser(string name)
        {
            var user = new User
            {
                Name = name
            };

            this.SqlDbContext.Users.Add(user);
            this.SqlDbContext.SaveChanges();

            return $"User {user.Name} with {user.Id} created";
        }

        public string CreateVisit(string date, string descirption, int userId, int familyId, string type)
        {
            var visit = new Visit
            {
                Date = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Description = descirption,
                UserId = userId,
                FamilyId = familyId,
                VisitType = this.PostgreDbContext.Visittypes.Where(x => x.Name == type).Count() > 0 ? 
                            this.PostgreDbContext.Visittypes.Where(x => x.Name == type).First() : 
                            this.CreateVisitType(type),
            };

            this.PostgreDbContext.Visits.Add(visit);
            this.PostgreDbContext.SaveChanges();

            return $"Visit on {visit.Date} with id: {visit.Id} was created";
        }

        public VisitType CreateVisitType(string name)
        {
            var visitType = new VisitType
            {
                Name = name
            };

            this.PostgreDbContext.Visittypes.Add(visitType);
            this.PostgreDbContext.SaveChanges();

            return visitType;
        }

    }
}