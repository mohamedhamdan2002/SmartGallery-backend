﻿namespace Application.Dtos.ReservationDtos;

public record ContactDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PhoneNumber { get; init; }
}
