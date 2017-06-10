﻿using SocialServicesManager.Data.Models.Constants;
using System.ComponentModel.DataAnnotations;

namespace SocialServicesManager.Data.Models
{
    public class VisitType
    {
        public int Id { get; set; }

        [MaxLength(ModelsConstraints.NameMaxLenght), MinLength(ModelsConstraints.NameMinLenght)]
        public string Name { get; set; }
    }
}
