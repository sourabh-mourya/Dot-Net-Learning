
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediSetu.Api.Data;
using MediSetu.Api.DTOs;
using MediSetu.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Register your existing AppDbContext configuration here.
// Keep your current database provider and connection string.

// 2. Keep your existing JWT authentication configuration here.
// Example: builder.Services.AddAuthentication(...).AddJwtBearer(...);

builder.Services.AddAuthorization();

var app = builder.Build();

// 3. Middleware
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Helper functions: read current user's claims from JWT
int GetCurrentUserId(HttpContext context)
{
    var userId = context.User.FindFirstValue("sub");

    if (!int.TryParse(userId, out var id))
        throw new UnauthorizedAccessException("Invalid user ID claim.");

    return id;
}

string? GetCurrentRole(HttpContext context)
{
    return context.User.FindFirstValue("role");
}

// 4. Common route group
// All endpoints in this group require authentication.
var appointments = app.MapGroup("/api/appointments")
    .RequireAuthorization();


// =======================================================
// GET /api/appointments?page=1&limit=10
// Get appointments for the logged-in Doctor or Patient
// =======================================================
appointments.MapGet("/", async (
    HttpContext context,
    AppDbContext db,
    int page = 1,
    int limit = 10) =>
{
    if (page < 1 || limit < 1 || limit > 100)
    {
        return Results.BadRequest(new
        {
            message = "Page must be >= 1 and limit must be between 1 and 100."
        });
    }

    var userId = GetCurrentUserId(context);
    var role = GetCurrentRole(context);

    var query = db.Appointments
        .AsNoTracking()
        .AsQueryable();

    if (role == Roles.Doctor)
    {
        query = query.Where(a => a.DoctorId == userId);
    }
    else if (role == Roles.Patient)
    {
        query = query.Where(a => a.PatientId == userId);
    }
    else
    {
        return Results.Forbid();
    }

    var total = await query.CountAsync();

    var items = await query
        .OrderByDescending(a => a.ScheduledAt)
        .Skip((page - 1) * limit)
        .Take(limit)
        .Select(a => new AppointmentResponseDto(
            a.Id,
            a.Doctor.Name,
            a.Patient.Name,
            a.ScheduledAt,
            a.Status))
        .ToListAsync();

    return Results.Ok(new { total, page, limit, items });
});


// =======================================================
// GET /api/appointments/5
// Get one appointment with ownership verification
// =======================================================
appointments.MapGet("/{id:int}", async (
    int id,
    HttpContext context,
    AppDbContext db) =>
{
    var userId = GetCurrentUserId(context);

    var appt = await db.Appointments
        .AsNoTracking()
        .Where(a => a.Id == id)
        .Select(a => new
        {
            Appointment = a,
            DoctorName = a.Doctor.Name,
            PatientName = a.Patient.Name
        })
        .FirstOrDefaultAsync();

    if (appt is null)
    {
        return Results.NotFound(new
        {
            message = "Appointment not found"
        });
    }

    if (appt.Appointment.PatientId != userId &&
        appt.Appointment.DoctorId != userId)
    {
        return Results.Forbid();
    }

    return Results.Ok(new AppointmentResponseDto(
        appt.Appointment.Id,
        appt.DoctorName,
        appt.PatientName,
        appt.Appointment.ScheduledAt,
        appt.Appointment.Status));
});


// =======================================================
// POST /api/appointments
// Only Patient can book an appointment
// =======================================================
appointments.MapPost("/", async (
    CreateAppointmentDto dto,
    HttpContext context,
    AppDbContext db) =>
{
    var userId = GetCurrentUserId(context);

    var doctorExists = await db.Users.AnyAsync(u =>
        u.Id == dto.DoctorId &&
        u.Role == Roles.Doctor);

    if (!doctorExists)
    {
        return Results.BadRequest(new
        {
            message = "Doctor not found"
        });
    }

    var slotTaken = await db.Appointments.AnyAsync(a =>
        a.DoctorId == dto.DoctorId &&
        a.ScheduledAt == dto.ScheduledAt &&
        a.Status != "Cancelled");

    if (slotTaken)
    {
        return Results.Conflict(new
        {
            message = "Slot already booked"
        });
    }

    var appt = new Appointment
    {
        PatientId = userId,
        DoctorId = dto.DoctorId,
        ScheduledAt = dto.ScheduledAt,
        Notes = dto.Notes
    };

    db.Appointments.Add(appt);
    await db.SaveChangesAsync();

    return Results.Created(
        $"/api/appointments/{appt.Id}",
        new { appt.Id });
})
.RequireAuthorization(policy =>
    policy.RequireRole(Roles.Patient));


// =======================================================
// PATCH /api/appointments/5/status
// Only the assigned Doctor can update status
// =======================================================
appointments.MapPatch("/{id:int}/status", async (
    int id,
    UpdateStatusDto dto,
    HttpContext context,
    AppDbContext db) =>
{
    var userId = GetCurrentUserId(context);

    var appt = await db.Appointments.FirstOrDefaultAsync(a =>
        a.Id == id &&
        a.DoctorId == userId);

    if (appt is null)
    {
        return Results.NotFound();
    }

    appt.Status = dto.Status;

    await db.SaveChangesAsync();

    return Results.NoContent();
})
.RequireAuthorization(policy =>
    policy.RequireRole(Roles.Doctor));


// =======================================================
// DELETE /api/appointments/5
// Patient can delete their own appointment
// =======================================================
appointments.MapDelete("/{id:int}", async (
    int id,
    HttpContext context,
    AppDbContext db) =>
{
    var userId = GetCurrentUserId(context);

    var appt = await db.Appointments.FindAsync(id);

    if (appt is null)
    {
        return Results.NotFound();
    }

    if (appt.PatientId != userId)
    {
        return Results.Forbid();
    }

    db.Appointments.Remove(appt);

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
