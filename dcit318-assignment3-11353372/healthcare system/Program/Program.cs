using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Healthcare System Initializing")
        Console.WriteLine("*** HealthCare System ***");

        HealthSystemApp app = new HealthSystemApp();

        app.SeedData();
        app.BuildPrescriptionMap();

        Console.WriteLine("\n-- All Patients --");
        app.PrintAllPatients();

        Console.WriteLine("\n-- Prescriptions for Patient with ID 1 --");
        app.PrintPrescriptionsForPatient(1);

        Console.WriteLine("\n-- Prescriptions for Patient with ID 2 --");
        app.PrintPrescriptionsForPatient(2);
    }
}


public class Repository<T>
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public List<T> GetAll()
    {
        return items;
    }

    public T? GetById(Func<T, bool> predicate)
    {
        foreach (var item in items)
        {
            if (predicate(item))
                return item;
        }
        return default;
    }

    public bool Remove(Func<T, bool> predicate)
    {
        var item = GetById(predicate);
        if (item != null)
        {
            return items.Remove(item);
        }
        return false;
    }
}

// Patient Class
public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }

    public Patient(int id, string name, int age, string gender)
    {
        Id = id;
        Name = name;
        Age = age;
        Gender = gender;
    }
}


// Prescription Class
public class Prescription
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string MedicationName { get; set; }
    public DateTime DateIssued { get; set; }

    public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
    {
        Id = id;
        PatientId = patientId;
        MedicationName = medicationName;
        DateIssued = dateIssued;
    }
}

// HealthSystemApp Class
public class HealthSystemApp
{
    private Repository<Patient> _patientRepo;
    private Repository<Prescription> _prescriptionRepo;
    private Dictionary<int, List<Prescription>> _prescriptionMap;

    public HealthSystemApp()
    {
        _patientRepo = new Repository<Patient>();
        _prescriptionRepo = new Repository<Prescription>();
        _prescriptionMap = new Dictionary<int, List<Prescription>>();
    }

    public void SeedData()
    {
        // Patients
        _patientRepo.Add(new Patient(1, "Effah Aboagye", 21, "Male"));
        _patientRepo.Add(new Patient(2, "Micheal Johnson", 25, "Male"));
        _patientRepo.Add(new Patient(3, "Mark Atta", 45, "Male"));

        // Prescriptions
        _prescriptionRepo.Add(new Prescription(101, 1, "Gebedol Extra", DateTime.Now.AddDays(-14)));
        _prescriptionRepo.Add(new Prescription(102, 1, " Dewormer", DateTime.Now.AddDays(-4)));
        _prescriptionRepo.Add(new Prescription(103, 2, "Paracetamol", DateTime.Now.AddDays(-1)));
        _prescriptionRepo.Add(new Prescription(101, 2, "Panadol", DateTime.Now.AddDays(-20)));
        _prescriptionRepo.Add(new Prescription(104, 3, "Aboniki Balm", DateTime.Now.AddDays(-2)));
        _prescriptionRepo.Add(new Prescription(105, 1, "Vitamin C", DateTime.Now));
    }

    public void BuildPrescriptionMap()
    {
        _prescriptionMap.Clear();
        foreach (var prescription in _prescriptionRepo.GetAll())
        {
            if (!_prescriptionMap.ContainsKey(prescription.PatientId))
            {
                _prescriptionMap[prescription.PatientId] = new List<Prescription>();
            }
            _prescriptionMap[prescription.PatientId].Add(prescription);
        }
    }

    public void PrintAllPatients()
    {
        foreach (var patient in _patientRepo.GetAll())
        {
            Console.WriteLine($"ID: {patient.Id}, Name: {patient.Name}, Age: {patient.Age}, Gender: {patient.Gender}");
        }
    }

    public List<Prescription> GetPrescriptionsByPatientId(int id)
    {
        if (_prescriptionMap.ContainsKey(id))
        {
            return _prescriptionMap[id];
        }
        return new List<Prescription>();
    }

    public void PrintPrescriptionsForPatient(int id)
    {
        var prescriptions = GetPrescriptionsByPatientId(id);
        if (prescriptions.Count == 0)
        {
            Console.WriteLine($"No prescriptions found for patient ID {id}.");
        }
        else
        {
            foreach (var p in prescriptions)
            {
                Console.WriteLine($"Prescription ID: {p.Id}, Medication: {p.MedicationName}, Date Issued: {p.DateIssued.ToShortDateString()}");
            }
        }
    }
}


