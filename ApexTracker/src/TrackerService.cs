using System;
using System.ComponentModel;
using System.Diagnostics;
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
    

    public TrackerService()
    {
        _currentTruck = new Truck();
        _currentDelivery = null;
        var telemetry = new SCSSdkTelemetry();
        telemetry.Data += Telemetry_Data;
        telemetry.JobDelivered += TelemetryJobDelivered;
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
        if (Process.GetProcessesByName("eurotrucks2").Length != 0 || Process.GetProcessesByName("amtrucks").Length != 0)
        {
            SimulatorRunning = true;
        }
        else
        {
            SimulatorRunning = false;
            CurrentTruck.ResetAlLValues();
            CurrentDelivery = null;
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