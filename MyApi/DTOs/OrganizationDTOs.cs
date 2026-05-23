using System;
using System.Collections.Generic;

namespace MyApi.DTOs;

public class OrganizationResponse
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public Guid? ParentId { get; set; }
}

public class CreateOrganizationRequest
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public Guid? ParentId { get; set; }
}

public class DepartmentResponse
{
    public Guid Id { get; set; }
    public Guid OrganizationId { get; set; }
    public string? Name { get; set; }
}

public class CreateDepartmentRequest
{
    public Guid OrganizationId { get; set; }
    public string? Name { get; set; }
}

public class PositionResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CreatePositionRequest
{
    public string? Name { get; set; }
}

public class EmployeeResponse
{
    public Guid Id { get; set; }
    public string? EmployeeCode { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public Guid? OrganizationId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? PositionId { get; set; }
    public string? EmployeeType { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateEmployeeRequest
{
    public string? EmployeeCode { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public Guid? OrganizationId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? PositionId { get; set; }
    public string? EmployeeType { get; set; }
}
