﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DreamsRentBack.Entities
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
