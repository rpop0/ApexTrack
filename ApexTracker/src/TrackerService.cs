using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using SCSSdkClient;
using SCSSdkClient.Object;

namespace ApexTracker;

public sealed class TrackerService: INotifyPropertyChanged
{
    
    private Truck _currentTruck;

    // ReSharper disable once MemberCanBePrivate.Global, needs to be public because it's accessed by data binding.
    public Truck CurrentTruck
    {
        get => _currentTruck;
        // ReSharper disable once UnusedMember.Local, used with INotifyPropertyChanged to update the binding.
        private set
        {
            _currentTruck = value;
            OnPropertyChanged();
        }
    }

    private Delivery? _currentDelivery;

    public Delivery? CurrentDelivery
    {
        get => _currentDelivery;
        private set
        {
            _currentDelivery = value;
            OnPropertyChanged();
        }
    }
    
    private bool _simulatorRunning;

    // ReSharper disable once MemberCanBePrivate.Global, even though it's not used directly by code, it is used in data binding.
    public bool SimulatorRunning
    {
        // ReSharper disable once UnusedMember.Global, Used in data binding and not directly by code.
        get => _simulatorRunning;
        set
        {
            _simulatorRunning = value;
            OnPropertyChanged();
        }
    }

    private Process? _gameProcess;

    public TrackerService()
    {
        _currentTruck = new Truck();
        _currentDelivery = null;
        var telemetry = new SCSSdkTelemetry();
        telemetry.Data += Telemetry_Data;
        telemetry.JobDelivered += TelemetryJobDelivered;
    }

    private void gameProcess_Exited(object? sender, EventArgs e)
    {
        SimulatorRunning = false;
        CurrentTruck.ResetAlLValues();
        CurrentDelivery = null;
    }


    private void TelemetryJobDelivered(object? sender, EventArgs e)
    {
        Trace.WriteLine("Making post request here.");
        if (CurrentDelivery != null)
        {
            Trace.WriteLine(CurrentDelivery.Income);
        }
        // Getting the mileage at the end from CurrentTruck.Odometer.
    }
    
    private void Telemetry_Data(SCSTelemetry data, bool updated)
    {

        if (!SimulatorRunning)
        {
            // Tries to get the ets2 process, if it's not found, try the ats one.
            var process = Process.GetProcessesByName("eurotrucks2");
            
            if (process.Length == 0)
            {
                process = Process.GetProcessesByName("amtrucks");
            }
            
            // If neither are found, just return with no process.
            if (process.Length == 0)
            {
                return;
            }

            // GetProcessesByName returns a list, so grab the first from the list. EnableRaisingEvents is required
            // for the Exited event to be fired.
            SimulatorRunning = true;
            _gameProcess = process.First();
            _gameProcess.EnableRaisingEvents = true;
            _gameProcess.Exited += gameProcess_Exited;
        }

        if (!updated) {
            return;
        }
        
        try {
            if (data.TruckValues.ConstantsValues.Id != CurrentTruck.Id)
            {
                CurrentTruck.Brand = data.TruckValues.ConstantsValues.Brand;
                CurrentTruck.BrandId = data.TruckValues.ConstantsValues.BrandId;
                CurrentTruck.Id = data.TruckValues.ConstantsValues.Id;
                CurrentTruck.Name = data.TruckValues.ConstantsValues.Name;
                CurrentTruck.FuelCapacity = data.TruckValues.ConstantsValues.CapacityValues.Fuel;
                CurrentTruck.Game = data.Game;
            }
            CurrentTruck.Odometer = data.TruckValues.CurrentValues.DashboardValues.Odometer;
            CurrentTruck.Fuel = data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount;
            Trace.WriteLine(CurrentTruck.Id);


            switch (data.SpecialEventsValues.OnJob)
            {
                case true:
                    CurrentDelivery = new Delivery();
                    break;
                // CurrentDelivery is only reset when not on a job.
                case false:
                    CurrentDelivery = null;
                    return;
            }

            // Everything below only gets executed when on an active delivery.
            

            if (!CurrentDelivery.Started)
            {
                // Only gets executed at the beginning of a delivery.
                CurrentDelivery.CargoMass = data.JobValues.CargoValues.Mass;
                CurrentDelivery.CargoName = data.JobValues.CargoValues.Name;
                CurrentDelivery.CityDestination = data.JobValues.CityDestination;
                CurrentDelivery.CitySource = data.JobValues.CitySource;
                CurrentDelivery.CompanyDestination = data.JobValues.CompanyDestination;
                CurrentDelivery.CompanySource = data.JobValues.CompanySource;
                CurrentDelivery.OdometerStart = data.TruckValues.CurrentValues.DashboardValues.Odometer;
                CurrentDelivery.Started = true;
            }
            
            // Gets executed during the delivery.
            CurrentDelivery.Income = data.JobValues.Income;
            CurrentDelivery.CargoDamage = data.JobValues.CargoValues.CargoDamage;


        }
        catch (Exception ex)
        {
            // ignored
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}