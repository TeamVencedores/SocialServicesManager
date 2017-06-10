﻿using SocialServicesManager.Models;
using System.Collections.Generic;

namespace SocialServicesManager.Interfaces
{
    public interface IDataFactory
    {
        void AddFamily(Family family);

        void AddUser(User user);

        void AddVisit(Visit visit);

        void AddMedicalDoctor(MedicalDoctor doctor);

        void AddMedicalRecord(MedicalRecord record);

        void SaveAllChanges();

        MedicalDoctor GetMedicalDoctor(int id);

        VisitType GetVisitType(string type);

        Family GetFamily(int id);

        IEnumerable<Family> GetAllFamilies();

        void UpdateFamily(Family family, IList<string> parameters);
    }
}
