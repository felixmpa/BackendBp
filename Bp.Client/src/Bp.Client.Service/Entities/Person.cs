using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Bp.Common;

namespace Bp.Client.Service.Entities
{
    public class Person : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Identification { get; set; }

        private string name;

        [Required]
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or whitespace.", nameof(value));
                name = value;
            }
        }

        // Convert the int to the enum for use in your application
        public Sex Sex { get; set; }

        public int? Age { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
    }

    public enum Sex : byte
    {
        Male,
        Female,
        Other
    }

}