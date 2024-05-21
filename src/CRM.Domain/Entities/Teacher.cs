﻿using CRM.Domain.Commons;

namespace CRM.Domain.Entities;

public class Teacher : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public IEnumerable<Group> Groups { get; set; }
}
