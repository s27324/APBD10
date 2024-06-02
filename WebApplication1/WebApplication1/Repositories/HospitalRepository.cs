using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public class HospitalRepository : IHospitalRepository
{
    private HospitalDbContext _hospitalDbContext;

    public HospitalRepository(HospitalDbContext hospitalDbContext)
    {
        _hospitalDbContext = hospitalDbContext;
    }

    public async Task<int> CheckForPatient(CancellationToken cancellationToken, PrescriptionDTO prescriptionDto)
    {
        if (!await _hospitalDbContext.Patients.AnyAsync(e => e.IdPatient == prescriptionDto.patient.IdPatient, cancellationToken))
        {
            await _hospitalDbContext.Patients.AddAsync(new Patient
            {
                IdPatient = prescriptionDto.patient.IdPatient, FirstName = prescriptionDto.patient.FirstName,
                LastName = prescriptionDto.patient.LastName, Birthdate = DateOnly.FromDateTime(prescriptionDto.patient.Birthdate)
            }, cancellationToken);
            await _hospitalDbContext.SaveChangesAsync(cancellationToken);
        }

        return 0;
    }
    
    public List<int> ListOfMedicamentIdsInPrescription(PrescriptionDTO prescriptionDto)
    {
        return prescriptionDto.medicaments.Select(e => e.IdMedicament).ToList();
    }

    public List<Medicament> PrescribedMedicamentInDb(PrescriptionDTO prescriptionDto, List<int> medicamentsPrescribed)
    {
        return _hospitalDbContext.Medicaments.Where(m => medicamentsPrescribed.Contains(m.IdMedicament)).ToList();
    }

    public bool DueDateHigherThanDate(PrescriptionDTO prescriptionDto)
    {
        if (prescriptionDto.DueDate >= prescriptionDto.Date)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> CheckForDoctor(PrescriptionDTO prescriptionDto, CancellationToken cancellationToken)
    {
        return await _hospitalDbContext.Doctors.AnyAsync(e => e.IdDoctor == prescriptionDto.IdDoctor,
            cancellationToken);
    }


    public async Task<int> AddNewPrescriptionAsync(CancellationToken cancellationToken, PrescriptionDTO prescriptionDto)
    {
        await CheckForPatient(cancellationToken, prescriptionDto);

        var medicamentsPrescribed = ListOfMedicamentIdsInPrescription(prescriptionDto);
        var prescMedsInDb = PrescribedMedicamentInDb(prescriptionDto, medicamentsPrescribed);
        
        if (medicamentsPrescribed.Count != prescMedsInDb.Count)
        {
            return -1;
        }

        if (medicamentsPrescribed.Count > 10)
        {
            return -2;
        }

        if (DueDateHigherThanDate(prescriptionDto) == false)
        {
            return -3;
        }
        
        if (await CheckForDoctor(prescriptionDto, cancellationToken) == false)
        {
            return -4;
        }

        await _hospitalDbContext.Prescriptions.AddAsync(new Prescription
        {
            Date = DateOnly.FromDateTime(prescriptionDto.Date), DueDate = DateOnly.FromDateTime(prescriptionDto.DueDate),
            IdPatient = prescriptionDto.patient.IdPatient, IdDoctor = prescriptionDto.IdDoctor
        });
        await _hospitalDbContext.SaveChangesAsync(cancellationToken);

        int newPrescriptiondId = await _hospitalDbContext.Prescriptions.Where(e =>
                e.IdDoctor == prescriptionDto.IdDoctor && e.Date == DateOnly.FromDateTime(prescriptionDto.Date) &&
                e.DueDate == DateOnly.FromDateTime(prescriptionDto.DueDate) && e.IdPatient == prescriptionDto.patient.IdPatient)
            .Select(e => e.IdPrescription).FirstOrDefaultAsync(cancellationToken);


        foreach (var med in prescriptionDto.medicaments)
        {
            await _hospitalDbContext.PrescriptionMedicaments.AddAsync(new PrescriptionMedicament
            {
                IdMedicament = med.IdMedicament, IdPrescription = newPrescriptiondId, Dose = med.Dose,
                Details = med.Description
            });
        }
        await _hospitalDbContext.SaveChangesAsync(cancellationToken);

        return 0;
    }

    public async Task<GetClientDTO> GetClient(int id)
    {
        if (!await _hospitalDbContext.Patients.AnyAsync(e => e.IdPatient == id))
        {
            return new GetClientDTO();
        }
        var tmp = await _hospitalDbContext.Patients
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.PrescriptionMedicaments)
                .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPatient == id);
        return new GetClientDTO
        {
            IdPatient = id,
            FirstName = tmp.FirstName,
            LastName = tmp.LastName,
            Birthdate = tmp.Birthdate,
            Prescriptions = tmp.Prescriptions
                .OrderBy(e => e.DueDate)
                .Select(e => new GetClientPrescriptionsDTO
                {
                    IdPrescription = e.IdPrescription,
                    Date = e.Date,
                    DueDate = e.DueDate,
                    Medicaments = e.PrescriptionMedicaments.Select(ee => new GetClientMedicaments
                    {
                        IdMedicament = ee.Medicament.IdMedicament,
                        Name = ee.Medicament.Name,
                        Description = ee.Medicament.Description,
                        Dose = ee.Dose
                    }).ToList(),
                    Doctor = new GetClientDoctor
                    {
                        IdDoctor = e.Doctor.IdDoctor,
                        FirstName = e.Doctor.FirstName
                    }
                }).ToList()
        };

    }
}