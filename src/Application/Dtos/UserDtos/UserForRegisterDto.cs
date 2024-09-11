﻿namespace Application.Dtos.UserDtos;

public record UserForRegisterDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string Address { get; init; }
    public string PhoneNumber { get; init; }

}
