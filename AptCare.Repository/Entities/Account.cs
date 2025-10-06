﻿using AptCare.Repository.Enum.AccountUserEnum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AptCare.Repository.Entities
{
    public class Account
    {
        [Key]
        [ForeignKey("User")]
        public int AccountId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool LockoutEnabled { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime? LockoutEnd { get; set; }

        public AccountRole Role { get; set; }

        public User User { get; set; }

        // Navigation properties
        public ICollection<AccountToken>? AccountTokens { get; set; }
        public ICollection<AccountOTPHistory>? AccountOTPHistories { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
    }
}
